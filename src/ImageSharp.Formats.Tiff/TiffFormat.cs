using System.Collections.Generic;

namespace SixLabors.ImageSharp.Formats.Tiff
{
    public class TiffFormat : IImageFormat
    {
        /// <inheritdoc/>
        public string Name => "Tagged Image File Format (TIFF)";

        /// <inheritdoc/>
        public string DefaultMimeType => "image/tiff";

        /// <inheritdoc/>
        public IEnumerable<string> MimeTypes => new string[] { "image/tiff" };
        
        /// <inheritdoc/>
        public IEnumerable<string> FileExtensions => new string[] { "tif", "tiff" };

        /// <inheritdoc/>
        public IImageDecoder Decoder => new TiffDecoder();

        /// <inheritdoc/>
        public IImageEncoder Encoder => new TiffEncoder();

        /// <inheritdoc/>
        public int HeaderSize => 4;

        /// <inheritdoc/>
        public bool IsSupportedFileFormat(byte[] header)
        {

            return header.Length >= this.HeaderSize &&
                (
                   (header[0] == 0x49 && //I
                   header[1] == 0x49 &&  //I
                   header[2] == 0x2A &&
                   header[3] == 0x00)
                   ||
                   (header[0] == 0x4D && //M
                   header[1] == 0x4D &&  //M
                   header[2] == 0x00 &&
                   header[3] == 0x2A)
                );
        }
    }
}
