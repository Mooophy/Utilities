using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Utilities
{
    public static class Files
    {
        public static IEnumerable<string> CopyLatest(string sourceDirectory, string targetDirectory)
        {
            var targets = new DirectoryInfo(targetDirectory).GetFiles();
            var filesToCopy = new DirectoryInfo(sourceDirectory)
                .GetFiles()
                .Where(s => targets.Any(t => t.Name == s.Name) && targets.First(t => t.Name == s.Name).LastWriteTime < s.LastWriteTime)
                .ToList();
            filesToCopy
                .ForEach(s => File.Copy(s.FullName, Path.Combine(targetDirectory, s.Name), true));
            return filesToCopy.Select(f => f.Name);
        }
    }
}
