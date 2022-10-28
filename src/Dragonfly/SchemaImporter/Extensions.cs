using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragonfly.SchemaImporter
{
    using Microsoft.Extensions.Hosting;
    using Umbraco.Cms.Core;

    internal static class Extensions
    {
        //Copied from Umbraco 10 Umbraco.Extensions WebHostEnvironmentExtensions

        /// <summary>
        ///     Maps a virtual path to a physical path to the application's web root
        /// </summary>
        /// <remarks>
        ///     Depending on the runtime 'web root', this result can vary. For example in Net Framework the web root and the
        ///     content root are the same, however
        ///     in netcore the web root is /wwwroot therefore this will Map to a physical path within wwwroot.
        /// </remarks>
        public static string MapPathWebRoot(this IWebHostEnvironment webHostEnvironment, string path)
        {
            string path1 = webHostEnvironment.WebRootPath;
            if (string.IsNullOrWhiteSpace(path1))
                path1 = webHostEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string str = path.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);
            if (str.StartsWith(path1))
                throw new ArgumentException(
                    "The path appears to already be fully qualified.  Please remove the call to MapPathWebRoot");
            return Path.Combine(path1, str.TrimStart(Constants.CharArrays.TildeForwardSlashBackSlash));
        }

        //Copied from Umbraco 10 Umbraco.Cms.Core.Extensions HostEnvironmentExtensions
        /// <summary>
        /// Maps a virtual path to a physical path to the application's content root.
        /// </summary>
        /// <remarks>
        /// Generally the content root is the parent directory of the web root directory.
        /// </remarks>
        public static string MapPathContentRoot(this IHostEnvironment hostEnvironment, string path)
        {
            string contentRootPath = hostEnvironment.ContentRootPath;
            string str = path.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);
            if (str.StartsWith(contentRootPath))
                throw new ArgumentException(
                    "The path appears to already be fully qualified.  Please remove the call to MapPathContentRoot");
            return Path.Combine(contentRootPath, str.TrimStart(Constants.CharArrays.TildeForwardSlashBackSlash));
        }

    }
}
