using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using Id3Lib;

namespace Mp3Lib
{
    abstract class Audio : IAudio
    {
        #region Fields

        /// <summary>
        /// first audio frame; could be could be xing or vbri header
        /// </summary>
        protected AudioFrame _firstFrame;

        /// <summary>
        /// duration of the audio block, as parsed from the optional ID3v2 "TLEN" tag
        /// the Xing/VBRI header is more authoritative for bitrate calculations, if present.
        /// </summary>
        protected TimeSpan? _id3DurationTag;

        /// <summary>
        /// number of frames and audio bytes - obtained by counting the entire file - slow!
        /// </summary>
        AudioStats _audioStats;

        /// <summary>
        /// flag that is set on parse error.
        /// This might turn into an enum for different errors at some point
        /// </summary>
        private bool _hasInconsistencies;

        #endregion

        #region Construction

        /// <summary>
        /// construct object to wrap the audio
        /// passing in audio size and id3 length tag (if any) to help with bitrate calculations
        /// </summary>
        /// <param name="firstFrame"></param>
        /// <param name="id3DurationTag">length from ID3v2, if any</param>
        public Audio(AudioFrame firstFrame, TimeSpan? id3DurationTag)
        {
            if (firstFrame == null)
            {
                throw new InvalidAudioFrameException("MPEG Audio Frame not found");
            }
            _firstFrame = firstFrame;
            _id3DurationTag = id3DurationTag;
            /*_hasInconsistencies = false;*/
        }

        protected void CheckConsistency()
        {
            Exception ex = ParsingError;
            if(ex != null)
            {
                _hasInconsistencies = true;
                Trace.WriteLine(ex.Message);
            }
        }

        #endregion

        #region IAudio Properties

        /// <summary>
        /// the mp3 frame header number of bytes of audio data in AudioStream
        /// </summary>
        public AudioFrameHeader Header
        {
            get
            {
                return _firstFrame.Header;
            }
        }

        /// <summary>
        /// text info, e.g. the encoding standard of audio data in AudioStream
        /// </summary>
        public virtual string DebugString
        {
            get
            {
                //----AudioFrame----
                //  Payload: 10336 frames in 4750766 bytes
                //  Length: 270 seconds
                //  140 kbit
                //  Counted: 10567 frames in 4750766 bytes
                string retval = string.Format("{0}\n----Audio----\n  Payload: {1} frames in {2} bytes\n  Length: {3:N3} seconds\n  {4:N4} kbit",
                                     _firstFrame.DebugString,
                                     NumPayloadFrames,
                                     NumPayloadBytes,
                                     Duration,
                                     BitRate);

                if (_audioStats._numFrames > 0)
                {
                    retval += string.Format("\n  Counted: {0} frames, {1} bytes",
                                            _audioStats._numFrames,
                                            _audioStats._numBytes);
                }
                return retval;
            }
        }

        /// <summary>
        /// is it a VBR file? i.e. is it better encoding quality than cbr at the same bitrate?
        /// first we make a guess based on the audio header found in the first frame.
        /// </summary>
        /// <remarks>
        /// if the frame didn't have any strong opinions,
        /// we don't check if the mp3 audio header bitrate is the same as the calculated bitrate
        /// because a truncated file shows up as vbr (because the bitrates don't match)
        /// and we just return false.
        /// </remarks>
        public virtual bool IsVbr
        {
            get
            {
                bool? frameVbr = _firstFrame.IsVbr;
                if (frameVbr != null)
                    return frameVbr.Value;

                return false;
            }
        }

        /// <summary>
        /// does it have a VBR (VBRI, XING, INFO, LAME) header?
        /// </summary>
        public bool HasVbrHeader
        {
            get
            {
                // return true if it's no longer the base class 'AudioFrame'
                return _firstFrame.GetType() != typeof(AudioFrame);
            }
        }
        
        /// <summary>
        /// the number of bytes of data in the Audio payload
        /// always the real size of the file
        /// supplied by AudioFile or AudioBuffer
        /// </summary>
        public virtual uint NumPayloadBytes
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Number of bytes playable audio, VBR header priority, best for calculating bitrates
        /// </summary>
        /// <remarks>
        /// if there is no xing/vbri header, it's the same as NumPayloadBytes
        /// if the xing header doesn't have the audio bytes filled in, 
        /// it can still return 'don't know, but you need to take one header off the file length'
        /// </remarks>
        public uint NumAudioBytes
        {
            get
            {
                uint? numAudioBytes = _firstFrame.NumAudioBytes;
                if (numAudioBytes != null)
                    return numAudioBytes.Value;
                else if( HasVbrHeader )
                {
                    // vbr header will never be free bitrate, because they're vbr instead
                    uint? frameLengthInBytes = _firstFrame.Header.FrameLengthInBytes;
                    if( frameLengthInBytes == null )
                        throw new InvalidAudioFrameException("VBR files cannot be 'free' bitrate");

                    return NumPayloadBytes - (uint)_firstFrame.Header.FrameLengthInBytes;
                }
                else
                    return NumPayloadBytes;
            }
        }

        /// <summary>
        /// Number of Frames in file (including the header frame)
        /// VBR header priority, best for calculating bitrates
        /// or if not present, calculated from the number of bytes in the audio block, as reported by the caller
        /// This will be correct for CBR files, at least.
        /// </summary>
        public uint NumPayloadFrames
        {
            get
            {
                uint? numPayloadFrames = _firstFrame.NumPayloadFrames;
                if( numPayloadFrames.HasValue )
                    return numPayloadFrames.Value;
                else
                {
                    double? framelength = Header.IdealisedFrameLengthInBytes;
                    if( framelength.HasValue )
                        return (uint)Math.Round(NumPayloadBytes / framelength.Value);
                    else
                        return NumPayloadBytes / _firstFrame.FrameLengthInBytes;
                }
            }
        }

        /// <summary>
        /// Number of Frames of playable audio
        /// </summary>
        /// <remarks>
        /// if there is no xing/vbri header, it's the same as NumPayloadFrames
        /// if the xing header doesn't have the audio frames filled in, 
        /// it can still return 'don't know, but you need to take one header off the file length'
        /// </remarks>
        public uint NumAudioFrames
        {
            get
            {
                if(HasVbrHeader)
                    return NumPayloadFrames - 1;
                else
                    return NumPayloadFrames;
            }
        }

        /// <summary>
        /// Number of seconds for bitrate calculations.
        /// first get it from the xing/vbri headers,
        /// then from the id3 TLEN tag,
        /// then from the file size and initial frame bitrate.
        /// </summary>
        public double Duration
        {
            get
            {
                double? headerDuration = _firstFrame.Duration;
                if (headerDuration != null)
                    return headerDuration.Value;
                else if (_id3DurationTag != null)
                    return _id3DurationTag.Value.TotalSeconds;
                else
                    return NumAudioFrames * Header.SecondsPerFrame;
            }
        }

        /// <summary>
        /// bitrate calculated from the id3 length tag, and the length of the audio
        /// </summary>
        public double? BitRateCalc
        {
            get
            {
                if (_id3DurationTag == null)
                    return null;
                else
                    return NumPayloadBytes * 8 / _id3DurationTag.Value.TotalSeconds;
            }
        }

        /// <summary>
        /// bitrate published in the standard mp3 header
        /// </summary>
        public uint? BitRateMp3
        {
            get
            {
                return Header.BitRate;
            }
        }

        /// <summary>
        /// vbr bitrate from xing or vbri header frame
        /// audio without xing or vbri header returns null
        /// </summary>
        public double? BitRateVbr
        {
            get
            {
                return _firstFrame.BitRateVbr;
            }
        }

        /// <summary>
        /// overall best guess of bitrate; there's always a way of guessing it
        /// </summary>
        public double BitRate
        {
            get
            {
                // get best guess at duration from derived classes, or id3 TLEN tag, or first frame bitrate
                double duration = Duration;

                // get best guess at numbytes from derived classes, or audio length
                uint numBytes = NumAudioBytes;

                // bitrate is size / time
                return numBytes / duration * 8;
            }
        }

        /// <summary>
        /// did it parse without any errors?
        /// </summary>
        public bool HasInconsistencies
        {
            get
            {
                return _hasInconsistencies;
            }
        }

        /// <summary>
        /// get the error from the parse operation, if any
        /// </summary>
        /// <remarks>
        /// should the parse operation save all thrown exceptions here,
        /// and not generate it on demand?
        /// </remarks>
        public Exception ParsingError
        {
            get
            {
                uint? vbrPayloadBytes = _firstFrame.NumPayloadBytes;
                if( vbrPayloadBytes != null && vbrPayloadBytes != NumPayloadBytes )
                {
                    return new InvalidVbrSizeException(NumPayloadBytes, vbrPayloadBytes.Value);
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        #region IAudio Functions

        virtual public Stream OpenAudioStream()
        {
            throw new NotImplementedException();
        }

        virtual public uint CalculateAudioCRC32()
        {
            throw new NotImplementedException();
        }

        virtual public byte[] CalculateAudioSHA1()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Count frames and bytes of file to see who's telling porkies
        /// </summary>
        public void ScanWholeFile()
        {
            _audioStats._numFrames = 0;
            _audioStats._numBytes = 0;

            using (Stream stream = OpenAudioStream())
            {
                uint payloadStart = (uint)stream.Position;
                try
                {
                    while (true)
                    {
                        uint pos = (uint)stream.Position;
                        uint used = pos - payloadStart;
                        uint remainingBytes = NumPayloadBytes - used;

                        AudioFrame frame = AudioFrameFactory.CreateFrame(stream, remainingBytes);
                        if (frame == null)
                            break;

                        ++_audioStats._numFrames;
                        _audioStats._numBytes += frame.FrameLengthInBytes;
                        //Trace.WriteLine(string.Format("frame {0} ({1} bytes) found at {2}",
                        //                              _audioStats._numFrames,
                        //                              frame.Header.FrameLengthInBytes,
                        //                              stream.Position - frame.Header.FrameLengthInBytes));
                    }
                }
                catch (Exception e)
                {
                    _hasInconsistencies = true;
                    Trace.WriteLine(e.Message);
                }
            }
        }

        #endregion

        #region nested types

        struct AudioStats
        {
            public uint _numFrames;
            public uint _numBytes;
        }

        #endregion
    }
}
