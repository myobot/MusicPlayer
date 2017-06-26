using System;
using Id3Lib;
using Id3Lib.Exceptions;

namespace Mp3Lib
{

	/// <summary>
	/// The exception is thrown when an audio frame is corrupt.
	/// </summary>
    [Serializable]
    public class InvalidAudioFrameException : InvalidStructureException
	{
        /// <summary>
        /// 
        /// </summary>
		public InvalidAudioFrameException()
		{
		}
	
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
		public InvalidAudioFrameException(string message): base(message)
		{
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public InvalidAudioFrameException(string message, Exception inner)
            : base(message, inner)
		{
		}
	}
}