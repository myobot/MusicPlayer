using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using Id3Lib;

namespace Mp3Lib
{
    /// <summary>
    /// Read mp3 frame
    /// </summary>
    /// <remarks>
    /// additional info: http://www.codeproject.com/KB/audio-video/mpegaudioinfo.aspx
    /// </remarks>

    public class AudioFrame
    {
        #region Fields

        /// <summary>
        /// byte array containing entire raw frame data
        /// correct size, even for free bitrate files
        /// </summary>
        protected byte[] _frameBuffer;

        /// <summary>
        /// MP3FrameHeader to work out how big the frame is
        /// </summary>
        protected AudioFrameHeader _header;

        /// <summary>
        /// size of mp3 standard header; offset to xing or vbri header (if present)
        /// </summary>
        protected uint _headerBytes;

        #endregion

        #region Public Properties

        /// <summary>
        /// MP3FrameHeader has all sorts of info about a frame of raw mp3 audio
        /// </summary>
        public AudioFrameHeader Header
        {
            get
            {
                return _header;
            }
        }

        /// <summary>
        /// get stored frame length
        /// </summary>
        /// <remarks>
        /// obtained from header, or distance between frames, at construction time.
        /// </remarks>
        public uint FrameLengthInBytes
        {
            get
            {
                return (uint)_frameBuffer.Length;
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
                //  Payload: 10336 frames
                //  Payload size: 4750766 bytes
                //  Length: 270 seconds
                //  140 kbit
                return string.Format("{0}\n----AudioFrame----\n  Audio: {1} frames, {2} bytes\n  Length: {3:N3} seconds",
                                     _header.DebugString,
                                     NumAudioFrames,
                                     NumAudioBytes,
                                     Duration);
            }
        }

        /// <summary>
        /// is it a VBR file?
        /// all we can do here is check the bitrate is not 'free' (which would imply CBR)
        /// then FrameVbriHeader/FrameXingHeader can override it.
        /// </summary>
        public virtual bool? IsVbr
        {
            get
            {
                uint? headerBitRate = Header.BitRate;

                // if header bit rate is 'free', it's definitely CBR
                // "This free format means that the file is encoded with a constant bitrate, 
                //  which is not one of the predefined bitrates."
                if (headerBitRate == null)
                    return false;

                return null;
            }
        }

        /// <summary>
        /// Number of Bytes in file (including this header frame)
        /// if VBR header not present, we can't know, so return null.
        /// </summary>
        public virtual uint? NumPayloadBytes
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Number of bytes playable audio
        /// if VBR header not present, we can't know, so return null.
        /// </summary>
        public virtual uint? NumAudioBytes
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Number of Frames in file
        /// if VBR header not present, we can't know, so return null.
        /// </summary>
        public virtual uint? NumPayloadFrames
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Number of Frames of playable audio
        /// if VBR header not present, we can't know, so return null.
        /// </summary>
        public virtual uint? NumAudioFrames
        {
            get
            {
                return null;
            }
        }
        
        /// <summary>
        /// Number of seconds of playable audio
        /// if VBR header not present, we can't know, so return null.
        /// xing/vbri headers would overload this if they have better information
        /// </summary>
        public virtual double? Duration
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// bitrate published in the standard mp3 header
        /// null for 'free' bitrate
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
        /// audio frame without xing or vbri header returns null
        /// </summary>
        public virtual double? BitRateVbr
        {
            get
            {
                return null;
            }
        }

        #endregion

        #region Construction

        /// <summary>
        /// construct AudioFrame from supplied bytes
        /// </summary>
        /// <param name="frameBuffer"></param>
        /// <remarks>buffer is correct size, even for free bitrate files</remarks>
        public AudioFrame(byte[] frameBuffer)
        {
            // find and parse frame header, rewind stream to start, or throw.
            _header = new AudioFrameHeader(frameBuffer);
            _headerBytes = _header.HeaderSize;
            _frameBuffer = frameBuffer;
        }

        /// <summary>
        /// construct AudioFrame from a larger portion of the stream; don't rewind stream when done
        /// </summary>
        /// <param name="stream">source stream</param>
        /// <param name="header">parsed header</param>
        /// <param name="frameSize">size from header, or scanning for second frame of free bitrate file</param>
        /// <param name="remainingBytes">number of bytes in audio block, as reported by the caller</param>
        public AudioFrame(Stream stream, AudioFrameHeader header, uint frameSize, uint remainingBytes)
        {
            Debug.Assert(header != null);
            _header = header;

            _frameBuffer = new byte[frameSize];
            _headerBytes = _header.HeaderSize;

            int numgot = stream.Read(_frameBuffer, 0, (int)frameSize);
            if( numgot < (int)frameSize )
                throw new InvalidAudioFrameException(
                    string.Format("MPEG Audio AudioFrame: only {0} bytes of frame found when {1} bytes declared",
                                  numgot,
                                  frameSize));
        }

        /// <summary>
        /// copy construct AudioFrame for derived classes
        /// </summary>
        /// <param name="other"></param>
        protected AudioFrame(AudioFrame other)
        {
            _frameBuffer = other._frameBuffer;
            _header = other._header;
            _headerBytes = other._headerBytes;
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// does this frame contain the 'Xing' or 'Info' markers that make it a Xing VBR header?
        /// </summary>
        public bool IsXingHeader
        {
            get
            {
                string _tag = ASCIIEncoding.ASCII.GetString(_frameBuffer, (int)_headerBytes, 4);
                return _tag == "Xing" || _tag == "Info";
            }
        }

        /// <summary>
        /// does this frame contain the 'Xing' or 'Info' markers that make it a Xing VBR header,
        /// then the LAME marker a bit further along?
        /// </summary>
        public bool IsLameHeader
        {
            get
            {
                return false;   // not yet impl!
            }
        }

        /// <summary>
        /// does this frame contain the 'VBRI' marker that make it a fraunhofer VBR header?
        /// </summary>
        public bool IsVbriHeader
        {
            get
            {
                string _tag = ASCIIEncoding.ASCII.GetString(_frameBuffer, (int)_headerBytes, 4);
                return _tag == "VBRI";
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// read 2 bytes as big-endian unsigned int from buffer
        /// </summary>
        /// <param name="buffer">input buffer</param>
        /// <param name="index">read location</param>
        /// <returns>parsed big-endian UInt16</returns>
        protected static UInt16 ParseBigEndianWORD(byte[] buffer, uint index)
        {
            UInt16 result = 0;

            for (uint i = index; i < index + 2; ++i)
                result = (UInt16)((result << 8) + buffer[i]);

            return result;
        }

        /// <summary>
        /// read 4 bytes as big-endian unsigned int from buffer
        /// </summary>
        /// <param name="buffer">input buffer</param>
        /// <param name="index">read location</param>
        /// <returns>parsed big-endian UInt32</returns>
        protected static UInt32 ParseBigEndianDWORD(byte[] buffer, uint index)
        {
            UInt32 result = 0;

            for (uint i = index; i < index + 4; ++i)
                result = (UInt32)((result << 8) + buffer[i]);

            return result;
        }

        #endregion
    }
}