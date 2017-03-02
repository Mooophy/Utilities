using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Utilities
{
    public static class Files
    {
        public static IEnumerable<string> CopyLatest(string from, string to)
        {
            var targets = new DirectoryInfo(to).GetFiles();
            var filesToCopy = new DirectoryInfo(from)
                .GetFiles()
                .Where(f => targets.Any(t => t.Name == f.Name) && targets.First(t => t.Name == f.Name).LastWriteTime < f.LastWriteTime)
                .ToList();
            filesToCopy
                .ForEach(f => File.Copy(f.FullName, Path.Combine(to, f.Name), true));
            return filesToCopy.Select(f => f.Name);
        }
    }
}
