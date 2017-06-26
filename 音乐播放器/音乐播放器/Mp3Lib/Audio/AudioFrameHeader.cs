using System;
using System.IO;
using System.Diagnostics;
using Id3Lib;

namespace Mp3Lib
{
	/// <summary>
	/// Read mp3 frame header
	/// </summary>
    /// <remarks>
    /// additional info: http://www.codeproject.com/KB/audio-video/mpegaudioinfo.aspx
    /// </remarks>

	public class AudioFrameHeader
    {
        #region Enums

        /// <summary>
        /// stereo mode options
        /// </summary>
        public enum ChannelModeCode
        {
            /// <summary>
            /// 00 - full stereo (2 indepentent channels)
            /// </summary>
            Stereo = 0,
            /// <summary>
            /// 01 - joint stereo (stereo encoded as sum + difference)
            /// </summary>
            JointStereo = 1,
            /// <summary>
            /// 10 - two independent soundtracks (e.g. 2 languages)
            /// </summary>
            DualMono = 2,
            /// <summary>
            /// 11 - just one channel
            /// </summary>
            Mono = 3
        }

        /// <summary>
        /// emphasis options
        /// </summary>
        public enum EmphasisCode
        {
            /// <summary>
            /// 00 - none
            /// </summary>
            None = 0,
            /// <summary>
            /// 01 - 50/15 ms
            /// </summary>
            E5015 = 1,
            /// <summary>
            /// 10 - reserved
            /// </summary>
            Reserved = 2,
            /// <summary>
            /// 11 - CCIT J.17
            /// </summary>
            CCIT = 3
        }

        #endregion
        
        #region Static Fields
        // bit rate, indexed by RawBitRate
		static uint[] V1L1   = new uint[] { 0, 32, 64, 96, 128, 160, 192, 224, 256, 288, 320, 352, 384, 416, 448, uint.MaxValue };
        static uint[] V1L2   = new uint[] { 0, 32, 48, 56,  64,  80,  96, 112, 128, 160, 192, 224, 256, 320, 384, uint.MaxValue };
        static uint[] V1L3   = new uint[] { 0, 32, 40, 48,  56,  64,  80,  96, 112, 128, 160, 192, 224, 256, 320, uint.MaxValue };
        static uint[] V2L1   = new uint[] { 0, 32, 48, 56,  64,  80,  96, 112, 128, 144, 160, 176, 192, 224, 256, uint.MaxValue };
        static uint[] V2L2L3 = new uint[] { 0,  8, 16, 24,  32,  40,  48,  56,  64,  80,  96, 112, 128, 144, 160, uint.MaxValue };
        // Frequency sampling speed, indexed by RawSampleFreq
        static uint[] MPEG1  = new uint[] { 44100, 48000, 32000, uint.MaxValue };
        static uint[] MPEG2  = new uint[] { 22050, 24000, 16000, uint.MaxValue };
        static uint[] MPEG25 = new uint[] { 11025, 12000,  8000, uint.MaxValue };
        // AudioFrame size in samples, indexed by RawLayer
        //                                  0 (illegal)    1=L3  2=L2  3=L1
        static uint[] V1Size = new uint[] { uint.MaxValue, 1152, 1152, 384 };
        static uint[] V2Size = new uint[] { uint.MaxValue,  576, 1152, 384 };
        // Channel Mode names, indexed by ChannelMode
        static string[] MPEGChannelMode = new string[] { "Stereo", "Joint Stereo", "Dual channel (2 mono channels)", "Single channel (Mono)" };
        // Layer names, indexed by RawLayer
        static string[] MPEGLayer = new string[] { "Reserved", "Layer III", "Layer II", "Layer I" };
        // Emphasis names, indexed by Emphasis
        static string[] MPEGEmphasis = new string[] { "None", "50/15 ms", "Reserved", "CCIT J.17" };

		#endregion

        #region Fields

        /// <summary>
        /// byte array containing at least 4 bytes of raw frame data
        /// </summary>
        protected byte[] _headerBuffer = null;

        #endregion

        #region Internal Properties

        //MPEG sync mark                                                                // 76543210 76543210 76543210 76543210
                                                                                        // 11111111 111..... ........ ........
        //MPEG Audio version ID
        byte RawMpegVersion { get { return (byte)((_headerBuffer[1] & 0x18) >> 3); } }  // ........ ...XX... ........ ........      
		//Layer description
		byte RawLayer       { get { return (byte)((_headerBuffer[1] & 0x06)>>1); } }    // ........ .....XX. ........ ........      
        //Protection bit 0 - Protected by CRC (16bit crc follows header)
		bool RawProtection  { get { return (_headerBuffer[1] & 0x01)>0; } }             // ........ .......X ........ ........
		//Bitrate index
		byte RawBitRate     { get { return (byte)((_headerBuffer[2] & 0xf0)>>4); } }    // ........ ........ XXXX.... ........
		//Sampling rate frequency index
		byte RawSampleFreq  { get { return (byte)((_headerBuffer[2] & 0x0C)>>2); } }    // ........ ........ ....XX.. ........
		//Padding bit
		bool RawPadding     { get { return (_headerBuffer[2] & 0x02)>0; } }             // ........ ........ ......X. ........
		//Private bit.
		bool RawPrivate     { get { return (_headerBuffer[2] & 0x01)>0; } }             // ........ ........ .......X ........
        //Chanel Mode 00 - Stereo, 01 - JointStereo, 10 - Dual Mono, 11 - Mono
		byte RawChannelMode { get { return (byte)((_headerBuffer[3] & 0xC0)>>6); } }    // ........ ........ ........ XX......   
        // Mode Extension - only if joint stereo
        byte RawModeExtension{ get { return (byte)((_headerBuffer[3] & 0x30) >> 4); } } // ........ ........ ........ ..XX....   
        // Copyright
		bool RawCopyright   { get { return (_headerBuffer[3] & 0x08)>0; } }             // ........ ........ ........ ....X...   
		// Original
		bool RawOriginal    { get { return (_headerBuffer[3] & 0x04)>0; } }             // ........ ........ ........ .....X..   
		// Emphasis
		byte RawEmphasis    { get { return (byte)((_headerBuffer[3] & 0x03)); } }       // ........ ........ ........ ......XX   

        #endregion

        #region Public Properties

        /// <summary>
        /// Simple validity check to verify all header fields are in legal ranges
        /// </summary>
        public bool Valid
        {
            get
            {
                return RawMpegVersion != 1
                    && RawLayer != 0
                    && RawBitRate != 15
                    && RawSampleFreq != 3
                    && RawBitRate != 15;
            }
        }

        /// <summary>
        /// mpeg version and layer
        /// </summary>
        public string VersionLayer
        {
            get
            {
			    switch(RawMpegVersion)
			    {
                case 0: // MPEG 2.5
                {
                    switch (RawLayer)
                    {
                    case 1: // Layer III
                        return "MPEG-2.5 layer 3";
                    case 2: // Layer II and I
                        return "MPEG-2.5 layer 2";
                    case 3:
                        return "MPEG-2.5 layer 1";
                    default:
                        throw new InvalidAudioFrameException("MPEG Version 2.5 Layer not recognised");
                    }
                }
                case 2: // MPEG 2
			    {
				    switch(RawLayer)
				    {
				    case 1: // Layer III
					    return "MPEG-2 layer 3";
				    case 2: // Layer II and I
					    return "MPEG-2 layer 2";
				    case 3:
					    return "MPEG-2 layer 1";
                    default:
					    throw new InvalidAudioFrameException("MPEG Version 2 (ISO/IEC 13818-3) Layer not recognised");
				    }
			    }
			    case 3: // MPEG 1
			    {
				    switch(RawLayer)
				    {
				    case 1: // Layer III
					    return "MPEG-1 layer 3";
				    case 2: // Layer II and I
					    return "MPEG-1 layer 2";
				    case 3:
					    return "MPEG-1 layer 1";
                    default:
					    throw new InvalidAudioFrameException("MPEG Version 1 (ISO/IEC 11172-3) Layer not recognised");
				    }
			    }
                default:
				    throw new InvalidAudioFrameException("MPEG Version not recognised");
			    }
            }
        }

        /// <summary>
        /// mpeg layer
        /// </summary>
        public uint Layer
        {
            get
            {
                switch( RawLayer )
                {
                case 1: // Layer III
                    return 3;
                case 2: // Layer II
                    return 2;
                case 3: // Layer I
                    return 1;
                default:
                    throw new InvalidAudioFrameException("MPEG Layer not recognised");
                }
            }
        }

        /// <summary>
        /// bitrate for this frame
        /// 0 for "free", i.e. free format. The free bitrate must remain constant, 
        /// and must be lower than the maximum allowed bitrate. 
        /// VBR encoders usually select a different one of the standard bitrates for each frame.
        /// </summary>
        public uint? BitRate
        {
            get
            {
                uint retval;

			    switch(RawMpegVersion)
			    {
                case 0: // MPEG 2.5
                {
                    switch (RawLayer)
                    {
                    case 1: // Layer III
                        retval = V2L2L3[RawBitRate]; break;
                    case 2: // Layer II
                        retval = V2L2L3[RawBitRate]; break;
                    case 3: // Layer I
                        retval = V2L1[RawBitRate]; break;
                    default:
                        throw new InvalidAudioFrameException("MPEG Version 2.5 BitRate not recognised");
                    } 
                    break;
                }
                case 2: // MPEG 2
			    {
				    switch(RawLayer)
				    {
                    case 1: // Layer III
                        retval = V2L2L3[RawBitRate]; break;
                    case 2: // Layer II
                        retval = V2L2L3[RawBitRate]; break;
                    case 3: // Layer I
                        retval = V2L1[RawBitRate]; break;
                    default:
					    throw new InvalidAudioFrameException("MPEG Version 2 (ISO/IEC 13818-3) BitRate not recognised");
                    } 
                    break;
			    }
			    case 3: // MPEG 1
			    {
				    switch(RawLayer)
				    {
				    case 1: // Layer III
                        retval = V1L3[RawBitRate]; break;
				    case 2: // Layer II
                        retval = V1L2[RawBitRate]; break;
                    case 3: // Layer I
                        retval = V1L1[RawBitRate]; break;
                    default:
					    throw new InvalidAudioFrameException("MPEG Version 1 (ISO/IEC 11172-3) BitRate not recognised");
                    } 
                    break;
			    }
                default:
				    throw new InvalidAudioFrameException("MPEG Version not recognised");
			    }

                if (retval == uint.MaxValue)
                    throw new InvalidAudioFrameException("MPEG BitRate not recognised");
                else if (retval == 0)
                    return null;
                else
                    return retval * 1000;
            }
        }

        /// <summary>
        /// samples per second; same for every frame
        /// </summary>
        public uint SamplesPerSecond
        {
            get
            {
                uint retval;

			    switch(RawMpegVersion)
			    {
                case 0: // MPEG 2.5
                    retval = MPEG25[RawSampleFreq]; break;
                case 2: // MPEG 2
                    retval = MPEG2[RawSampleFreq]; break;
                case 3: // MPEG 1
                    retval = MPEG1[RawSampleFreq]; break;
                default:
				    throw new InvalidAudioFrameException("MPEG Version not recognised");
			    }

                if (retval == uint.MaxValue)
                    throw new InvalidAudioFrameException("MPEG SampleFreq not recognised");
                else
                    return retval;
            }
        }

        /// <summary>
        /// samples per frame; same for every frame
        /// </summary>
        public uint SamplesPerFrame
        {
            get
            {
                uint retval;

                switch (RawMpegVersion)
                {
                case 0: // MPEG 2.5
                case 2: // MPEG 2
                    retval = V2Size[RawLayer]; break;
                case 3: // MPEG 1
                    retval = V1Size[RawLayer]; break;
                default:
                    throw new InvalidAudioFrameException("MPEG Version not recognised");
                }

                if (retval == uint.MaxValue)
                    throw new InvalidAudioFrameException("MPEG Layer not recognised");
                else
                    return retval;
            }
        }

        /// <summary>
        /// seconds per frame; same for every frame
        /// e.g. 384 Samples/Frame / 44100 Samples/Second = 8.7mS each
        /// </summary>
        public double SecondsPerFrame
        {
            get
            {
                return (double)SamplesPerFrame / SamplesPerSecond;
            }
        }
        
        /// <summary>
        /// is it a "free" bitrate file?
        /// </summary>
        /// <remarks>most frames know how big they are, but free bitrate files can only know their frame length at the file level.</remarks>
        public bool IsFreeBitRate
        {
            get
            {
                return RawBitRate == 0;
            }
        }

        /// <summary>
        /// padding size; different for every frame
        /// </summary>
        /// <remarks>
        /// Padding is used to exactly fit the bitrate.
        /// As an example: 128kbps 44.1kHz layer II uses a lot of 418 bytes
        /// and some of 417 bytes long frames to get the exact 128k bitrate. 
        /// For Layer I slot is 32 bits (4 bytes) long
        /// For Layer II and Layer III slot is 8 bits (1 byte) long.
        /// </remarks>
        public uint PaddingSize
        {
            get
            {
                if (RawPadding)
                {
                    switch (RawMpegVersion)
                    {
                    case 0: // MPEG 2.5
                    case 2: // MPEG 2
                        return 1;
                    case 3: // MPEG 1
                        return 4;
                    default:
                        throw new InvalidAudioFrameException("MPEG Version not recognised");
                    }
                }
                else
                    return 0;
            }
        }

        /// <summary>
        /// length of this frame in bytes; different for every frame
        /// bitrate calculation includes the standard header bytes of normal audio frames already
        /// returns null for 'free' bitrate files
        /// because parsing the audio coefficients to work out how long it should be is too much work.
        /// If you want to know how long the frame is, ask the audio stream, not the header.
        /// </summary>
        public uint? FrameLengthInBytes
        {
            get
            {
                uint? bitRate = BitRate;
                if( bitRate == null )
                {
                    // free bitrate files are (just about) supported, but you can't tell how long a frame is from the header.
                    // Instead, you have to read forwards until you find another header and measure the difference.
                    return null;
                }

                switch (RawMpegVersion)
                {
                case 0: // MPEG 2.5
                case 2: // MPEG 2
                    {
                        switch (RawLayer)
                        {
                        case 1: // Layer III
                            // FrameLengthInBytes = 72 * BitRate / SamplesPerSecond + Padding 
                            return 72 * bitRate.Value / SamplesPerSecond + (uint)(RawPadding ? 1 : 0);
                        case 2: // Layer II
                            // FrameLengthInBytes = 144 * BitRate / SamplesPerSecond + Padding 
                            return 144 * bitRate.Value / SamplesPerSecond + (uint)(RawPadding ? 1 : 0);
                        case 3: // Layer I
                            // FrameLengthInBytes = (12 * BitRate / SamplesPerSecond + Padding) * 4
                            return (12 * bitRate.Value / SamplesPerSecond + (uint)(RawPadding ? 1 : 0)) * 4;
                        default:
                            throw new InvalidAudioFrameException("MPEG 2 Layer not recognised");
                        }
                    }
                case 3: // MPEG 1
                    {
                        switch (RawLayer)
                        {
                        case 1: // Layer III
                            // FrameLengthInBytes = 144 * BitRate / SamplesPerSecond + Padding 
                            return 144 * bitRate.Value / SamplesPerSecond + (uint)(RawPadding ? 1 : 0);
                        case 2: // Layer II
                            // FrameLengthInBytes = 144 * BitRate / SamplesPerSecond + Padding 
                            return 144 * bitRate.Value / SamplesPerSecond + (uint)(RawPadding ? 1 : 0);
                        case 3: // Layer I
                            // FrameLengthInBytes = (12 * BitRate / SamplesPerSecond + Padding) * 4
                            return (12 * bitRate.Value / SamplesPerSecond + (uint)(RawPadding ? 1 : 0)) * 4;
                        default:
                            throw new InvalidAudioFrameException("MPEG 1 Layer not recognised");
                        }
                    }
                default:
                    throw new InvalidAudioFrameException("MPEG Version not recognised");
                }
            }
        }

        /// <summary>
        /// 'ideal' length of a frame at this bitrate; returns double, disregards padding.
        /// returns null for 'free' bitrate files
        /// because parsing the audio coefficients to work out how long it should be is too much work.
        /// If you want to know how long the frame should be, ask the audio stream, not the header.
        /// </summary>
        public double? IdealisedFrameLengthInBytes
        {
            get
            {
                uint? bitRate = BitRate;
                if( bitRate == null )
                    // free bitrate files are (just about) supported, but you can't tell how long a frame is from the header.
                    // Instead, you have to read forwards until you find another header and measure the difference.
                    return null;

                switch (RawMpegVersion)
                {
                case 0: // MPEG 2.5
                case 2: // MPEG 2
                    {
                        switch (RawLayer)
                        {
                        case 1: // Layer III
                            // FrameLengthInBytes = 72 * BitRate / SamplesPerSecond + Padding 
                            return 72.0 * bitRate.Value / SamplesPerSecond;
                        case 2: // Layer II
                            // FrameLengthInBytes = 144 * BitRate / SamplesPerSecond + Padding 
                            return 144.0 * bitRate.Value / SamplesPerSecond;
                        case 3: // Layer I
                            // FrameLengthInBytes = (12 * BitRate / SamplesPerSecond + Padding) * 4
                            return 12.0 * bitRate.Value / SamplesPerSecond * 4;
                        default:
                            throw new InvalidAudioFrameException("MPEG 2 Layer not recognised");
                        }
                    }
                case 3: // MPEG 1
                    {
                        switch (RawLayer)
                        {
                        case 1: // Layer III
                            // FrameLengthInBytes = 144 * BitRate / SamplesPerSecond + Padding 
                            return 144.0 * bitRate.Value / SamplesPerSecond;
                        case 2: // Layer II
                            // FrameLengthInBytes = 144 * BitRate / SamplesPerSecond + Padding 
                            return 144.0 * bitRate.Value / SamplesPerSecond;
                        case 3: // Layer I
                            // FrameLengthInBytes = (12 * BitRate / SamplesPerSecond + Padding) * 4
                            return 12.0 * bitRate.Value / SamplesPerSecond * 4;
                        default:
                            throw new InvalidAudioFrameException("MPEG 1 Layer not recognised");
                        }
                    }
                default:
                    throw new InvalidAudioFrameException("MPEG Version not recognised");
                }
            }
        }


        /// <summary>
        /// checksum size
        /// Protection = 0 - Protected by CRC (16bit crc follows header), 1 - Not protected
        /// </summary>
        public uint ChecksumSize
        {
            get
            {
                if (!RawProtection)
                    return 2;
                else
                    return 0;
            }
        }

        /// <summary>
        /// channel config block size; different for every frame
        /// </summary>
        public uint SideInfoSize
        {
            get
            {
                switch (RawMpegVersion)
                {
                case 0: // MPEG 2.5
                case 2: // MPEG 2
                    if( IsMono )
                        return 9;
                    else
                        return 17;
                case 3: // MPEG 1
                    if( IsMono )
                        return 17;
                    else
                        return 32;
                default:
                    throw new InvalidAudioFrameException("MPEG Version not recognised");
                }
            }
        }

        /// <summary>
        /// size of standard header
        /// NB not all these bytes will have been read in
        /// </summary>
        public uint HeaderSize
        {
            get
            {
                return 4 + ChecksumSize + SideInfoSize;
            }
        }

        /// <summary>
        /// Checksum
        /// </summary>
        public bool CRCs
        {
            get
            {
                return !RawProtection;
            }
        }

        /// <summary>
        /// Copyright
        /// </summary>
        public bool Copyright
        {
            get
            { 
                return RawCopyright;
            }
        }

        /// <summary>
        /// Original
        /// </summary>
        public bool Original 
        { 
            get 
            { 
                return RawOriginal; 
            }
        }

        /// <summary>
        /// Private
        /// </summary>
        public bool Private
        {
            get
            {
                return RawPrivate;
            }
        }

        /// <summary>
        /// mono; must be same for every frame
        /// </summary>
        public bool IsMono
        {
            get
            {
                return (ChannelModeCode)RawChannelMode == ChannelModeCode.Mono;
            }
        }

        /// <summary>
        /// stereo mode; different for every frame
        /// </summary>
        public ChannelModeCode ChannelMode
        {
            get
            {
                return (ChannelModeCode)RawChannelMode;
            }
        }

        /// <summary>
        /// emphasis; different for every frame
        /// </summary>
        public EmphasisCode Emphasis
        {
            get
            {
                return (EmphasisCode)RawEmphasis;
            }
        }

        /// <summary>
        /// some text to show we decoded it correctly
        /// </summary>
        public virtual string DebugString
        {
            get
            {
                //----MP3 header----
                //  MPEG-1 layer 3
                //  44100Hz Joint Stereo
                //  Frame: 418 bytes
                //  CRCs: No, Copyrighted: No
                //  Original: No, Emphasis: None

                string retval = String.Format("----MP3 header----\n  {0}\n  {1}Hz {2}\n  Frame: {3} bytes\n  CRCs: {4}, Copyrighted: {5}\n  Original: {6}, Emphasis: {7}",
                                               VersionLayer,
                                               SamplesPerSecond,
                                               MPEGChannelMode[(int)ChannelMode],
                                               FrameLengthInBytes,
                                               CRCs ? "No" : "Yes",
                                               Copyright ? "No" : "Yes",
                                               Original ? "No" : "Yes",
                                               MPEGEmphasis[(int)Emphasis]);
                return retval;
            }
        }
        #endregion

        #region Construction

        /// <summary>
        /// construct MP3FrameHeader from 4 supplied bytes
        /// </summary>
        /// <param name="frameHeader"></param>
        public AudioFrameHeader(byte[] frameHeader)
        {
            _headerBuffer = frameHeader;
        }

        #endregion

        /// <summary>
        /// compare headers
        /// </summary>
        // return true if identical or related
        // return false if no similarities
        // (from an idea in CMPAHeader, http://www.codeproject.com/KB/audio-video/mpegaudioinfo.aspx)

        bool IsCompatible( AudioFrameHeader destHeader )
        {
	        // version change never possible
	        if (destHeader.RawMpegVersion != RawMpegVersion)
		        return false;

	        // layer change never possible
	        if (destHeader.RawLayer != RawLayer)
		        return false;

	        // sampling rate change never possible
	        if (destHeader.RawSampleFreq != RawSampleFreq)
		        return false;

	        // from mono to stereo never possible
	        if (destHeader.IsMono != IsMono)
		        return false;

	        if (destHeader.RawEmphasis != RawEmphasis)
		        return false;

	        return true;
        }
    }
}
