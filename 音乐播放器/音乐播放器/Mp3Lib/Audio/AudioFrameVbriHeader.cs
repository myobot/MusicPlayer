using System;
using System.IO;
using System.Text;
using System.Diagnostics;

// http://groups.google.com/group/alt.music.mp3/msg/a528fc7afdf353f6

namespace Mp3Lib
{
    class AudioFrameVbriHeader : AudioFrame
    {
        #region Fields
        #endregion

        #region Internal Properties

        //                                                                                                    0          1          2          3
        // space[headersize]                                                                                  01234567 89012345 67890123 45678901
        // VBRI header mark
        // 'VBRI'                                                                                          // VBRI.... ........ ........ ........
        // Version ID as Big-Endian WORD
        UInt16 Version            { get { return ParseBigEndianWORD(_frameBuffer, _headerBytes + 4); } }   // ....vv.. ........ ........ ........      
        // Delay as Big-Endian float
        UInt16 Delay              { get { return ParseBigEndianWORD(_frameBuffer, _headerBytes + 6); } }   // ......dd ........ ........ ........      
        // Quality indicator
        UInt16 QualityIndicator   { get { return ParseBigEndianWORD(_frameBuffer, _headerBytes + 8); } }   // ........ qq...... ........ ........
        // Number of Bytes in the audio stream as Big-Endian DWORD (including the VBRI header frame, and an extra one for luck?)
        UInt32 VbriNumStreamBytes { get { return ParseBigEndianDWORD(_frameBuffer, _headerBytes + 10); } } // ........ ..bbbb.. ........ ........
        // Number of Frames in the audio stream  as Big-Endian DWORD (including the VBRI header frame, and an extra one for luck?)
        UInt32 VbriNumStreamFrames{ get { return ParseBigEndianDWORD(_frameBuffer, _headerBytes + 14); } } // ........ ......ff ff...... ........
        // Number of entries within TOC table as Big-Endian WORD
        UInt16 NumTocEntries      { get { return ParseBigEndianWORD(_frameBuffer, _headerBytes + 18); } }  // ........ ........ ..tt.... ........
        // Scale factor of TOC table entries as Big-Endian DWORD
        UInt16 ScaleTocEntries    { get { return ParseBigEndianWORD(_frameBuffer, _headerBytes + 20); } }  // ........ ........ ....ss.. ........
        // Size per table entry in bytes (max 4) as Big-Endian WORD
        UInt16 SizePerTocEntry    { get { return ParseBigEndianWORD(_frameBuffer, _headerBytes + 22); } }  // ........ ........ ......zz ........   
        // Frames per table entry as Big-Endian WORD
        UInt16 FramesPerTocEntry  { get { return ParseBigEndianWORD(_frameBuffer, _headerBytes + 24); } }  // ........ ........ ........ ff......   
        // TOC entries for seeking as Big-Endian integral.
        // From size per table entry and number of entries, you can calculate the length of this field.

        #endregion

        #region Public Properties

        /// <summary>
        /// some text to show we decoded it correctly
        /// </summary>
        public override string DebugString
        {
            get
            {
                //----VBRI Header----
                //  10567 Frames
                //  4750766 Bytes

                int tocsize = NumTocEntries * SizePerTocEntry;
                int tocend = (int)_headerBytes + 26 + tocsize;
                int overhang = tocend - (int)Header.FrameLengthInBytes;
                Debug.Assert(overhang < 0);

                return string.Format("{0}\n----VBRI Header----\n  {1} Frames, {2} Bytes\n  Quality: {3}, TocBytes: {4}",
                                     base.DebugString,
                                     VbriNumStreamFrames,
                                     NumPayloadBytes,
                                     QualityIndicator,
                                     tocsize );
            }
        }

        /// <summary>
        /// is it a VBR file?
        /// I don't think the presence of a VBRI header necessarily implies a VBR file
        /// and the absence of a VBR header doesn't imply it's not VBR either,
        /// but it's a good indicator, so that's the best assumption here
        /// </summary>
        public override bool? IsVbr
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Number of Bytes in file (including this header frame)
        /// (always present in VBRI header)
        /// </summary>
        public override uint? NumPayloadBytes
        {
            get
            {
                return VbriNumStreamBytes;
            }
        }

        /// <summary>
        /// Number of bytes playable audio (i.e. excluding this header frame, 
        /// but including the standard header bytes of normal audio frames))
        /// </summary>
        public override uint? NumAudioBytes
        {
            get
            {
                return VbriNumStreamBytes - Header.FrameLengthInBytes;
            }
        }

        /// <summary>
        /// Number of Frames in file (including this header frame)
        /// or if not present, calculated from the number of bytes in the audio block, as reported by the caller
        /// </summary>
        public override uint? NumPayloadFrames
        {
            get
            {
                return VbriNumStreamFrames;
            }
        }

        /// <summary>
        /// Number of Frames of playable audio (i.e. excluding any header frame)
        /// </summary>
        public override uint? NumAudioFrames
        {
            get
            {
                return NumPayloadFrames - 1;
            }
        }

        /// <summary>
        /// Number of seconds from the id3 TLEN tag
        /// vbri header *always* has better information
        /// </summary>
        public override double? Duration
        {
            get
            {
                return NumAudioFrames * Header.SecondsPerFrame;
            }
        }

        /// <summary>
        /// vbr bitrate from vbri header frame
        /// </summary>
        public override double? BitRateVbr
        {
            get
            {
                double vbrDuration = (VbriNumStreamFrames - 1) * Header.SecondsPerFrame;

                // bitrate is size / time
                return (VbriNumStreamBytes - Header.FrameLengthInBytes) / vbrDuration * 8;
            }
        }

        #endregion

        #region Construction

        /// <summary>
        /// construct XingHeader frame from a pre-existing raw frame; "downcast".
        /// </summary>
        /// <param name="baseclass"></param>
        public AudioFrameVbriHeader(AudioFrame baseclass)
            : base(baseclass)
        {
        }

        #endregion
    }
}
