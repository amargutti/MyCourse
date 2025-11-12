using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.Exceptions;
using System;

namespace MyCourse.Models.Services.Application
{
    public class ErrorService
    {
        public string getErrorPage(Exception execption)
        {
            switch (execption)
            {
                case CourseNotFoundException:
                    return "CourseNotFound";
                default:
                    return "Index";
            }
        }
    }
}
