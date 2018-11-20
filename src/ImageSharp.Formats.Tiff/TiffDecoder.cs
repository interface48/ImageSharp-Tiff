using System.IO;
using SixLabors.ImageSharp.PixelFormats;

namespace SixLabors.ImageSharp.Formats.Tiff
{
    public class TiffDecoder : IImageDecoder
    {
        public Image<TPixel> Decode<TPixel>(Configuration configuration, Stream stream) where TPixel : struct, IPixel<TPixel>
        {
            var decoder = new TiffDecoderCore();
            return decoder.Decode<TPixel>(stream);
        }
    }
}
