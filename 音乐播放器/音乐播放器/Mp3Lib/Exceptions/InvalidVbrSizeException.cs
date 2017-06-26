using System;
using System.Diagnostics;
using Id3Lib;
using Id3Lib.Exceptions;

namespace Mp3Lib
{

    /// <summary>
    /// The exception is for when the vbr header claims the audio
    /// is a different length to the file size.
    /// This can happen if the file has been truncated at some point in its history,
    /// but could also be if unrecognised tags (non-id3, e.g. monkey audio) are added to the file.
    /// It is not thrown as such, because it's not an error that needs the parse to fail.
    /// </summary>
    [Serializable]
    public class InvalidVbrSizeException : InvalidStructureException
    {
        uint _measured;
        uint _specified;

        /// <summary>
        /// the number of zero bytes found after the last frame in the id3v2 tag
        /// </summary>
        public uint Measured
        {
            get
            {
                return _measured;
            }
        }

        /// <summary>
        /// the amount of space left over after the last frame in the id3v2 tag
        /// </summary>
        public uint Specified
        {
            get
            {
                return _specified;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="measured"></param>
        /// <param name="specified"></param>
        public InvalidVbrSizeException( uint measured, uint specified )
            : base("VBR header claims audio size is not payload size.")
        {
            Debug.Assert(measured != specified);
            _measured = measured;
            _specified = specified;
        }

        /// <summary>
        /// overrides default message with a specific "Padding is corrupt" one
        /// </summary>
        public override string Message
        {
            get
            {
                if( _specified > _measured )
                    // audio has been truncated due to file system error?
                    return string.Format("VBR header states the audio size is {0} bytes, but the payload is only {1} bytes, so {2} bytes have been lost from the end",
                                         _specified, _measured, _specified - _measured);
                else
                    // maybe something's added a tag we don't understand, but could still be file system error
                    return string.Format("VBR header states audio size is only {0} bytes, but the payload is {1} bytes, so {2} bytes have been added to the end",
                                         _specified, _measured, _measured - _specified);
            }
        }
    }
}