using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using Id3Lib;

namespace Mp3Lib
{
    internal static class AudioFrameFactory
    {
        #region Frames

        /// <summary>
        /// seek and create derived type of AudioFrame from stream
        /// </summary>
        /// <param name="stream">source stream, advanced by length of the frame on read</param>
        /// <param name="remainingBytes">number of bytes in audio block, as reported by the caller</param>
        /// <returns>wrapper for derived type of AudioFrame</returns>
        static public AudioFrame CreateFrame( Stream stream, uint remainingBytes )
        {
            // find and parse frame header, then rewind stream back to start.
            // if reach the end of the file, return null
            // if any other error, throw
            long firstFrameStart = stream.Position;

            AudioFrameHeader header = CreateHeader( stream, remainingBytes );
            if( header == null )
                return null;

            uint frameFullSize;
            // if free rate file, find the start of the next frame and use the difference as the frame size.
            // NB. This won't be very efficient!
            if( header.IsFreeBitRate )
            {
                uint firstFrameHdrSize = (uint)(stream.Position - firstFrameStart);
                frameFullSize = firstFrameHdrSize + GetNextFrameOffset(stream, remainingBytes - (uint)(stream.Position - firstFrameStart));
                
                //GetNextFrameOffset(stream, remainingBytes - (uint)(stream.Position - firstFrameStart));
                //GetNextFrameOffset(stream, remainingBytes - (uint)(stream.Position - firstFrameStart));
                //GetNextFrameOffset(stream, remainingBytes - (uint)(stream.Position - firstFrameStart));
                //GetNextFrameOffset(stream, remainingBytes - (uint)(stream.Position - firstFrameStart));
                //GetNextFrameOffset(stream, remainingBytes - (uint)(stream.Position - firstFrameStart));
                //GetNextFrameOffset(stream, remainingBytes - (uint)(stream.Position - firstFrameStart));
                //GetNextFrameOffset(stream, remainingBytes - (uint)(stream.Position - firstFrameStart));
                //GetNextFrameOffset(stream, remainingBytes - (uint)(stream.Position - firstFrameStart));
                //GetNextFrameOffset(stream, remainingBytes - (uint)(stream.Position - firstFrameStart));
                //GetNextFrameOffset(stream, remainingBytes - (uint)(stream.Position - firstFrameStart));
            }
            else
            {
                uint? frameLengthInBytes = header.FrameLengthInBytes;
                Trace.Assert(frameLengthInBytes != null);
                frameFullSize = (uint)header.FrameLengthInBytes;
            }

            // rewind the stream to the start of the frame, so we can read it all in one chunk
            stream.Position = firstFrameStart;

            AudioFrame firstTry = new AudioFrame(stream, header, frameFullSize, remainingBytes);

            return CreateSpecialisedHeaderFrame( firstTry );
        }

        /// <summary>
        /// create derived type of AudioFrame from buffer, or throw
        /// </summary>
        /// <param name="sourceBuffer"></param>
        /// <returns>wrapper for derived type of AudioFrame</returns>
        static public AudioFrame CreateFrame( byte[] sourceBuffer )
        {
            AudioFrame firstTry = new AudioFrame( sourceBuffer );

            return CreateSpecialisedHeaderFrame( firstTry );
        }

        /// <summary>
        /// skip to start of next frame
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="remainingBytes"></param>
        /// <returns>number of bytes skipped, not length of frame found!</returns>
        static private uint GetNextFrameOffset( Stream stream, uint remainingBytes )
        {
            long prevHeaderEnd = stream.Position;

            AudioFrameHeader header = CreateHeader(stream, remainingBytes);
            if( header == null )
                return 0;

            Trace.WriteLine(String.Format("next frame is {0} bytes further", stream.Position - prevHeaderEnd));
            Trace.WriteLine(header.DebugString);

            return (uint)(stream.Position - prevHeaderEnd);
        }

        private static AudioFrame CreateSpecialisedHeaderFrame( AudioFrame firstTry )
        {
            //string wholeHeader = ASCIIEncoding.ASCII.GetString(_frameBuffer, 0, _frameBuffer.Length);

            if( firstTry.IsXingHeader )
            {
                //if (firstTry.IsLameHeader)
                //    return new AudioFrameLameHeader(firstTry);
                //else
                return new AudioFrameXingHeader( firstTry );
            }
            else if( firstTry.IsVbriHeader )
                return new AudioFrameVbriHeader( firstTry );
            else
                return firstTry;
        }

        #endregion

        #region Headers

        /// <summary>
        /// Creates Header
        /// </summary>
        /// <remarks>
        /// n.b. doesn't rewind the stream to the start of the frame.
        /// If the caller wants to read the entire frame in one block, they'll have to rewind it themselves.
        /// </remarks>
        /// <param name="stream"></param>
        /// <param name="remainingBytes"></param>
        /// <returns>valid audio header, or null</returns>
        static AudioFrameHeader CreateHeader( Stream stream, uint remainingBytes )
        {
            // save the start of the skip operation, for error messages
            long streamStartpos = stream.Position;
            int framesSkipped = 0;

            // apply an upper limit of 64k to the number of bytes it will skip,
            // to prevent it trying to read the whole of a corrupt 5meg file
            // (MPAHeaderInfo skips 3 frames but I think it's worth checking further than that)
            if( remainingBytes > 65536 )
                remainingBytes = 65536;

            long endPos = streamStartpos + (long)remainingBytes;

            do
            {
                long remaining = endPos - (uint)stream.Position;
                int numskipped = Seek( stream, remaining );

                if( numskipped < 0 )
                {
                    //throw new InvalidAudioFrameException(
                    //    string.Format("MPEG Audio Frame: No header found from offset {0} to the end", streamStartpos));
                    return null;
                }
                //else if( numskipped > 0 )
                //    Trace.WriteLine( string.Format( "{0} bytes skipped to start of frame at offset {1}",
                //                                    numskipped, stream.Position - numskipped ) );

                // save the start of the real frame, i.e. after the rubbish is skipped
                long frameStartPos = stream.Position;

                AudioFrameHeader parsedHeader = new AudioFrameHeader( ReadHeader( stream ) );
                if( parsedHeader.Valid )
                {
                    if( frameStartPos > streamStartpos )
                    {
                        if( framesSkipped > 0 )
                            Trace.WriteLine(string.Format("total {0} bytes and {1} invalid frame headers skipped to get to the start of a valid frame at stream offset {2}",
                                                            frameStartPos - streamStartpos, framesSkipped, frameStartPos));
                        else
                            Trace.WriteLine(string.Format("total {0} bytes skipped to get to the start of a valid frame at stream offset {1}",
                                                            frameStartPos - streamStartpos, frameStartPos));
                    }
                    return parsedHeader;
                }

                // header is invalid mp3 frame, so skip a char and look again
                stream.Position = frameStartPos;
                ++framesSkipped;
                /*byte skipchar = (byte)*/stream.ReadByte();
                //Trace.WriteLine(string.Format("  Invalid MP3 frame; skipping 0x{0:X} at {1}", skipchar, frameStartPos + 1));
            }
            while( true );
        }

        /// <summary>
        /// Find the first occurrence of an mp3 header.
        /// </summary>
        /// <param name="stream">The stream to perform the search on.</param>
        /// <param name="remainingBytes"></param>
        /// <returns>number of bytes skipped; stream is at first mp3 header position.</returns>
        private static int Seek( Stream stream, long remainingBytes )
        {
            byte last = 0;
            byte data = 0;
            int index = 0;

            while( remainingBytes > 0 && stream.Position < stream.Length )
            {
                last = data;
                data = (byte)stream.ReadByte();
                ++index;
                --remainingBytes;

                if( ( last == 0xff ) && ( ( data & 0xe0 ) == 0xe0 ) )
                {
                    stream.Seek( -2, SeekOrigin.Current );
                    return index - 2;
                }

                //if (index >= 2)
                //    Trace.WriteLine(string.Format("  Skipping 0x{0:X} at {1}", last, stream.Position - 1));
            }
            return -1;
        }


        /// <summary>
        /// parse frame header, or throw.
        /// </summary>
        /// <param name="stream"></param>
        private static byte[] ReadHeader( Stream stream )
        {
            byte[] headerBuffer = new byte[4];
            int numgot = stream.Read( headerBuffer, 0, 4 );
            if( numgot < 4 )
            {
                throw new InvalidAudioFrameException(
                    string.Format( "MPEG Audio Frame: only {0} bytes of header left at offset {1}",
                                  numgot,
                                  stream.Position - numgot ) );
            }

            return headerBuffer;
        }

        #endregion
    }
}
