using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Mp3Lib
{
    class AudioFile : Audio
    {
        #region Fields

        /// <summary>
        /// holds audio stream filename; opened afresh when we need the data
        /// </summary>
        private FileInfo _sourceFileInfo;
        /// <summary>
        /// offset from start of stream that the audio starts at
        /// </summary>
        private UInt32 _payloadStart;
        /// <summary>
        /// total length (bytes) of mp3 audio frames in the file,
        /// could be different from what's declared in the header if the file is corrupt.
        /// </summary>
        private UInt32 _payloadNumBytes;

        #endregion

        #region Construction

        /// <summary>
        /// construct audio file
        /// passing in audio size and id3 length tag (if any) to help with bitrate calculations
        /// </summary>
        /// <param name="sourceFileInfo"></param>
        /// <param name="audioStart"></param>
        /// <param name="payloadNumBytes"></param>
        /// <param name="id3DurationTag"></param>
        public AudioFile(FileInfo sourceFileInfo, UInt32 audioStart, UInt32 payloadNumBytes, TimeSpan? id3DurationTag)
        :
            base(ReadFirstFrame(sourceFileInfo, audioStart, payloadNumBytes), id3DurationTag)
        {
            _sourceFileInfo = sourceFileInfo;
            _payloadStart = audioStart;
            _payloadNumBytes = payloadNumBytes;

            CheckConsistency();
        }

        private static AudioFrame ReadFirstFrame(FileInfo sourceFileInfo, UInt32 audioStart, UInt32 audioNumBytes)
        {
            using (FileStream stream = sourceFileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                stream.Seek(audioStart, SeekOrigin.Begin);

                // read a base level mp3 frame header
                // if it can't even do that right, just fail the call.
                return AudioFrameFactory.CreateFrame(stream, audioNumBytes);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// text info, e.g. the encoding standard of audio data in AudioStream
        /// /// </summary>
        public override string DebugString
        {
            get
            {
                //----AudioFile----
                //  Header starts: 12288 bytes
                //  FileSize: 4750766 bytes
                string retval = string.Format("{0}\n----AudioFile----\n  Header starts: {1} bytes\n  Payload: {2} bytes",
                                              base.DebugString,
                                              _payloadStart,
                                              _payloadNumBytes);
                return retval;
            }
        }

        /// <summary>
        /// the number of bytes of data in AudioStream, always the real size of the file
        /// </summary>
        public override uint NumPayloadBytes
        {
            get
            {
                return _payloadNumBytes;
            }
        }

        #endregion

        #region IAudio Functions

        /// <summary>
        /// the stream containing the audio data, wound to the start
        /// </summary>
        /// <remarks>
        /// it is the caller's responsibility to dispose of the returned stream
        /// and to call NumPayloadBytes to know how many bytes to read.
        /// </remarks>
        public override Stream OpenAudioStream()
        {
            FileStream stream = _sourceFileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
            stream.Seek(_payloadStart, SeekOrigin.Begin);
            return stream;
        }

        /// <summary>
        /// calculate sha-1 of the audio data
        /// </summary>
        public override byte[] CalculateAudioSHA1()
        {
            using (Stream stream = OpenAudioStream())
            {
                // This is one implementation of the abstract class SHA1.
                System.Security.Cryptography.SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();

                uint numLeft = _payloadNumBytes;

                const int size = 4096;
                byte[] bytes = new byte[4096];
                int numBytes;

                while (numLeft > 0)
                {
                    // read a whole block, or to the end of the file
                    numBytes = stream.Read(bytes, 0, size);

                    // audio ends on or before end of this read; exit loop and checksum what we have.
                    if (numLeft <= numBytes)
                        break;

                    sha.TransformBlock(bytes, 0, size, bytes, 0);
                    numLeft -= (uint)numBytes;
                }

                sha.TransformFinalBlock(bytes, 0, (int)numLeft);

                byte[] result = sha.Hash;
                return result;
            }
        }

        #endregion
    }
}
