
using ImageMagick;

namespace MyCourse.Models.Services.Infrastructure
{
    public class MagickNetImagePersister : IImagePersister
    {
        private readonly IWebHostEnvironment env;

        public MagickNetImagePersister (IWebHostEnvironment env)
        {
            ResourceLimits.Width = 4000;
            ResourceLimits.Height = 4000;
            this.env = env;
        }

        public async Task<string> SaveCourseImage(int courseId, IFormFile formFile)
        {
            string path = $"/Courses/{courseId}.jpg";
            string pyshicalPath = Path.Combine(env.WebRootPath, "Courses", $"{courseId}" );

            using Stream inputStream = formFile.OpenReadStream();
            using MagickImage image = new MagickImage(inputStream);

            int width = 300;
            int height = 300;
            MagickGeometry resizeGeometry = new MagickGeometry((uint)width, (uint)height)
            {
                FillArea = true,
            };
            image.Resize(resizeGeometry);
            image.Crop((uint)width, (uint)width, Gravity.Northwest);

            image.Quality = 70;
            image.Write(pyshicalPath, MagickFormat.Jpg);
            return path;
        }
    }
}
