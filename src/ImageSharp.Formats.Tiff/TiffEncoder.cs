using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SixLabors.ImageSharp.PixelFormats;

namespace SixLabors.ImageSharp.Formats.Tiff
{
    public class TiffEncoder : IImageEncoder
    {
        public void Encode<TPixel>(Image<TPixel> image, Stream stream) where TPixel : struct, IPixel<TPixel>
        {
            throw new NotImplementedException();
            //var encoder = new TiffEncoderCore();
            //encoder.Encode(image, stream);
        }
    }
}
