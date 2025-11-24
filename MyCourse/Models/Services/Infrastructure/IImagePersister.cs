namespace MyCourse.Models.Services.Infrastructure
{
    public interface IImagePersister
    {
        /// <summary>
        /// Method that saves the url of the image insert by the user in the Edit Action
        /// </summary>
        /// <returns>The image URL e.g. /Course/1.jpg</returns>
        Task<string> SaveCourseImage(int courseId, IFormFile formFile);
    }
}
