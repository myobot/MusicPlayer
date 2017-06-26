using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Mp3Lib
{
    class AudioBuffer : Audio
    {
        #region Fields

        /// <summary>
        /// holds audio stream locked so we can rely on it when saving
        /// </summary>
        private byte[] _sourceBuffer;

        #endregion

        #region Construction

        /// <summary>
        /// construct audio file
        /// passing in audio size and id3 length tag (if any) to help with bitrate calculations
        /// </summary>
        /// <param name="sourceBuffer"></param>
        /// <param name="id3DurationTag"></param>
        public AudioBuffer(byte[] sourceBuffer, TimeSpan? id3DurationTag)
        :
            base(AudioFrameFactory.CreateFrame(sourceBuffer), id3DurationTag)
        {
            _sourceBuffer = sourceBuffer;

            CheckConsistency();
        }

        #endregion

        #region Properties

        public byte[] SourceBuffer
        {
            get
            {
                return _sourceBuffer; 
            }
            set
            {
                _sourceBuffer = value;
            }
        }

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
                string retval = string.Format("{0}\n----AudioBuffer----",
                                              base.DebugString);
                return retval;
            }
        }

        #endregion

        #region IAudio Functions

        /// <summary>
        /// the number of bytes of data in AudioStream, always the real size of the file
        /// </summary>
        public override uint NumPayloadBytes
        {
            get
            {
                return (uint)_sourceBuffer.Length;
            }
        }

        /// <summary>
        /// the stream containing the audio data, wound to the start
        /// </summary>
        public override Stream OpenAudioStream()
        {
            MemoryStream str = new MemoryStream(_sourceBuffer);
            return str;
        }

        /// <summary>
        /// calculate sha-1 of the audio data
        /// </summary>
        public override byte[] CalculateAudioSHA1()
        {
            byte[] result;

            // This is one implementation of the abstract class SHA1.
            System.Security.Cryptography.SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            result = sha.ComputeHash(_sourceBuffer);
            return result;
        }

        #endregion
    }
}
