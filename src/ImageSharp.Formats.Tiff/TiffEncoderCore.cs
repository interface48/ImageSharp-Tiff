//using BitMiracle.LibTiff.Classic;
//using SixLabors.ImageSharp.PixelFormats;
//using System;
//using System.IO;

//namespace SixLabors.ImageSharp.Formats.Tiff
//{
//    internal sealed class TiffEncoderCore
//    {
//        private int width;
//        private int height;
//        private int bytesPerPixel = 4;

//        public void Encode<TPixel>(Image<TPixel> image, Stream stream) where TPixel : struct, PixelFormats.IPixel<TPixel>
//        {
//            using (var tif = BitMiracle.LibTiff.Classic.Tiff.ClientOpen("Stream", "w", stream, new TiffStream()))
//            {
//                this.width = image.Width;
//                this.height = image.Height;

//                tif.SetField(TiffTag.IMAGEWIDTH, this.width);
//                tif.SetField(TiffTag.IMAGELENGTH, this.height);
//                tif.SetField(TiffTag.COMPRESSION, Compression.LZW);
//                tif.SetField(TiffTag.PHOTOMETRIC, Photometric.RGB);

//                tif.SetField(TiffTag.ROWSPERSTRIP, image.Height);

//                tif.SetField(TiffTag.XRESOLUTION, image.MetaData.HorizontalResolution);
//                tif.SetField(TiffTag.YRESOLUTION, image.MetaData.VerticalResolution);

//                tif.SetField(TiffTag.BITSPERSAMPLE, 8);
//                tif.SetField(TiffTag.SAMPLESPERPIXEL, this.bytesPerPixel);

//                tif.SetField(TiffTag.PLANARCONFIG, PlanarConfig.CONTIG);


//                using (var pixels = image)
//                {
//                    byte[] color_ptr = new byte[pixels.];

//                    for (int y = 0; y < this.height; y++)
//                    {
//                        CollectPixelBytes(pixels, y, color_ptr);
//                        tif.WriteScanline(color_ptr, y, 0);
//                    }

//                    tif.FlushData();
//                }
//            }
//        }

//        /// <summary>
//        /// Collects a row of true color pixel data.
//        /// </summary>
//        /// <typeparam name="TColor">The pixel format.</typeparam>
//        /// <param name="pixels">The image pixel accessor.</param>
//        /// <param name="row">The row index.</param>
//        /// <param name="rawScanline">The raw scanline.</param>
//        private void CollectColorBytes<TColor>(PixelAccessor<TColor> pixels, int row, byte[] rawScanline)
//            where TColor : struct, PixelFormats.IPixel<TColor>
//        {
//            // We can use the optimized PixelAccessor here and copy the bytes in unmanaged memory.
//            using (PixelArea<TColor> pixelRow = new PixelArea<TColor>(this.width, rawScanline, this.bytesPerPixel == 4 ? ComponentOrder.Xyzw : ComponentOrder.Xyz))
//            {
//                pixels.CopyTo(pixelRow, row);
//            }
//        }

//        /// <summary>
//        /// Collects a row of true color pixel data.
//        /// </summary>
//        /// <typeparam name="TPixel">The pixel format.</typeparam>
//        /// <param name="rowSpan">The row span.</param>
//        private void CollectPixelBytes<TPixel>(Span<TPixel> rowSpan)
//            where TPixel : struct, IPixel<TPixel>
//        {
//            if (this.bytesPerPixel == 4)
//            {
//                PixelOperations<TPixel>.Instance.ToRgba32Bytes(rowSpan, this.rawScanline, this.width);
//            }
//            else
//            {
//                PixelOperations<TPixel>.Instance.ToRgb24Bytes(rowSpan, this.rawScanline, this.width);
//            }
//        }
//    }
//}
