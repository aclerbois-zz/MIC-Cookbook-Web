using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Mic.CookBook.Web.Helpers
{
    public class FileSaver
    {
        private IHostingEnvironment hostingEnvironment;
        private string appRootFolder;

        public FileSaver(IHostingEnvironment env)
        {
            hostingEnvironment = env;
            appRootFolder = env.ContentRootPath;
        }

        public async Task<string> SaveFileToWwwFolder(string fileName, IFormFile file)
        {
            var filePath = "/images/" + fileName;
            var pathToFile = hostingEnvironment.WebRootPath + filePath;

            using (var stream = new FileStream(pathToFile, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filePath;
        }
    }
}
