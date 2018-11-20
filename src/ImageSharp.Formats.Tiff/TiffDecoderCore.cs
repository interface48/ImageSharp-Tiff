using BitMiracle.LibTiff.Classic;
using System;
using System.IO;

namespace SixLabors.ImageSharp.Formats.Tiff
{
    internal class TiffDecoderCore
    {
        public Image<TPixel> Decode<TPixel>(Stream stream)
            where TPixel : struct, PixelFormats.IPixel<TPixel>
        {
            Image<TPixel> image = null;

            using (var sourceImage = BitMiracle.LibTiff.Classic.Tiff.ClientOpen("Stream", "r", stream, new TiffStream()))
            {
                // Find the width and height of the image
                FieldValue[] value = sourceImage.GetField(TiffTag.IMAGEWIDTH);
                int width = value[0].ToInt();

                value = sourceImage.GetField(TiffTag.IMAGELENGTH);
                int height = value[0].ToInt();

                value = sourceImage.GetField(TiffTag.XRESOLUTION);
                int xRes = value == null ? 0 : value[0].ToInt();

                value = sourceImage.GetField(TiffTag.YRESOLUTION);
                int yRes = value == null ? 0 : value[0].ToInt();

                if (width > int.MaxValue || height > int.MaxValue)
                {
                    throw new ArgumentOutOfRangeException($"The input png '{width}x{height}' is bigger than the max allowed size '{int.MaxValue}x{int.MaxValue}'");
                }

                image = new Image<TPixel>(width, height);

                int imageSize = height * width;
                int[] raster = new int[imageSize];

                // Read the image into the memory buffer
                if (!sourceImage.ReadRGBAImage(width, height, raster))
                {
                    throw new Exception("Cannot read image into raster");
                }

                if (xRes > 0 && yRes > 0)
                {
                    image.MetaData.HorizontalResolution = xRes;
                    image.MetaData.VerticalResolution = yRes;
                }

                TPixel pixel = default(TPixel);
                
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int offset = (height - y - 1) * width + x;
                        int r = BitMiracle.LibTiff.Classic.Tiff.GetR(raster[offset]);
                        int g = BitMiracle.LibTiff.Classic.Tiff.GetG(raster[offset]);
                        int b = BitMiracle.LibTiff.Classic.Tiff.GetB(raster[offset]);
                        int a = BitMiracle.LibTiff.Classic.Tiff.GetA(raster[offset]);

                        pixel.PackFromRgba32(new Rgba32((byte)(r), (byte)(g), (byte)(b), (byte)a));

                        image[x, y] = pixel;
                    }
                }
            }

            return image;
        }
    }
}
