using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Mp3Lib
{
    /// <summary>
    /// interface for audio stored in file or buffer
    /// </summary>
    public interface IAudio
    {
        #region Properties

        /// <summary>
        /// the audio header in AudioStream
        /// </summary>
        AudioFrameHeader Header { get; }

        /// <summary>
        /// the encoding standard of audio data in AudioStream
        /// </summary>
        string DebugString { get; }

        /// <summary>
        /// is it a VBR file? i.e. is it better encoding quality than cbr at the same bitrate?
        /// audio frame without xing or vbri header just checks header bitrate is 'free'
        /// </summary>
        bool IsVbr { get; }

        /// <summary>
        /// the number of bytes of data in the Audio payload
        /// always the real size of the file
        /// </summary>
        uint NumPayloadBytes { get; }

        /// <summary>
        /// Number of bytes playable audio
        /// VBR header priority, best for calculating bitrates
        /// </summary>
        uint NumAudioBytes { get; }

        /// <summary>
        /// Number of Frames in file (including the header frame)
        /// VBR header priority, best for calculating bitrates
        /// or if not present, calculated from the number of bytes in the audio block, as reported by the caller
        /// </summary>
        uint NumPayloadFrames { get; }

        /// <summary>
        /// Number of Frames of playable audio
        /// VBR header priority, best for calculating bitrates
        /// </summary>
        uint NumAudioFrames { get; }

        /// <summary>
        /// Number of seconds from the xing/vbri headers,
        /// or from the id3 TLEN tag,
        /// or null.
        /// </summary>
        double Duration { get; }

        /// <summary>
        /// bit-rate calculated from the id3 length tag, and the length of the audio
        /// </summary>
        double? BitRateCalc { get; }

        /// <summary>
        /// bit-rate published in the standard mp3 header
        /// </summary>
        uint? BitRateMp3 { get; }

        /// <summary>
        /// the VBR bit-rate, if any
        /// </summary>
        double? BitRateVbr { get; }

        /// <summary>
        /// overall best guess of bit-rate; there's always a way of guessing it
        /// </summary>
        double BitRate { get; }

        /// <summary>
        /// did it parse without any errors?
        /// </summary>
        bool HasInconsistencies { get; }

        /// <summary>
        /// get the error from the parse operation, if any
        /// </summary>
        Exception ParsingError { get; }

        #endregion

        #region Functions

        /// <summary>
        /// the stream containing the audio data, wound to the start
        /// </summary>
        Stream OpenAudioStream();

        /// <summary>
        /// calculate CRC of the audio data
        /// </summary>
        byte[] CalculateAudioSHA1();

        /// <summary>
        /// Count frames and bytes of file to see who's telling porkies
        /// </summary>
        void ScanWholeFile();

        #endregion
    }
}
