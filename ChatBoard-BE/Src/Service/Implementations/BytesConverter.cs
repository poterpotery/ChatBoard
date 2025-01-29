using System;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Service.Implementations
{
	public class BytesConverter
	{
        public async Task<byte[]> ConvertIFormFileToByteArray(IFormFile file)
        {
            using MemoryStream memoryStream = new MemoryStream();
            using var stream = file.OpenReadStream();
            byte[] buffer = new byte[1024];
            int bytesRead;
            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
            {
                await memoryStream.WriteAsync(buffer, 0, bytesRead);
            }

            return memoryStream.ToArray();
        }
        public byte[] ConvertIFormFileToByteArrayAndResize(IFormFile file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                var imageFactory = new ImageProcessor.ImageFactory(preserveExifData: true);
                imageFactory.Load(memoryStream.GetBuffer())
                            //.Resize(new Size(500, 500))   // resize operation
                            .Quality(70)   // specifying Quality parameter to reduce the size
                            .Save(memoryStream);

                return memoryStream.ToArray();
            }
        }
    }
}

