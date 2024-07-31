using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace Helper
{
    public static class ImageOptimizer
    {
        public static async Task<string> GetOptimizedBase64VersionOf(MemoryStream stream)
        {
            using (var image = await Image.LoadAsync(stream))
            {
                var encoder = new JpegEncoder
                {
                    Quality = 70
                };

                using (var outputStream = new MemoryStream())
                {
                    await image.SaveAsync(outputStream, encoder);

                    return Convert.ToBase64String(outputStream.ToArray());
                }
            }
        }
    }
}
