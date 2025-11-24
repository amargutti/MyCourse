
namespace MyCourse.Models.Services.Infrastructure
{
    public class InsecureImagePersister : IImagePersister
    {
        private readonly IWebHostEnvironment env;

        public InsecureImagePersister(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public async Task<string> SaveCourseImage(int courseId, IFormFile formFile)
        {
            string path = $"/Courses/{courseId}.jpg";
            string pyshicalPath = Path.Combine(env.WebRootPath, "Courses", $"{courseId}.jpg");
            using FileStream fileStream = File.OpenWrite(pyshicalPath);
            await formFile.CopyToAsync(fileStream);

            return path;
        }
    }
}
