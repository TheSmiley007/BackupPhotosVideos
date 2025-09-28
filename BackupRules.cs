using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupPhotosVideos
{
    internal class BackupRules
    {
        public static readonly string[] AllowedPhotosExtensions = new string[] {
            ".jpeg",
            ".jpg",
            ".png",
            ".webp",
            ".heic",
            ".heif",
            ".avif",
            ".tiff",
            ".gif",
            ".bmp"
        };

        public static readonly string[] AllowedVideosExtensions = new string[] {
            ".mp4",
            ".mp3",
            ".avi",
            ".3gp",
            ".mov",
            ".mkv",
            ".wmv",
            ".flv",
            ".webm",
            ".mpg",
            ".mpeg",
            ".ts",
            ".m2ts",
            ".mxf",
            ".ogv",
            ".vob",
            ".rm",
            ".rmvb"
        };

        // Values correspond to FileType.cs
        public static readonly string[] FolderNames = new string[] {"Fotos", "Videos"};
    }
}
