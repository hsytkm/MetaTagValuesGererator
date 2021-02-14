using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace CsharpSourceGeneratorSamples.Metas
{
    public class MetaShelf
    {
        private readonly IDictionary<string, MetaBook> _booksDict = new Dictionary<string, MetaBook>();

        public MetaShelf()
        {
            Debug.WriteLine($"MetadataExtractor-Version: {MetaShelf.GetMetadataExtractorVersion()}");
        }

        public MetaBook GetOrAdd(string filePath)
        {
            if (!_booksDict.TryGetValue(filePath, out var value))
            {
                var sw = new System.Diagnostics.Stopwatch();
                sw.Start();

                value = new MetaBook(filePath);
                _booksDict.Add(filePath, value);

                sw.Stop();
                Debug.WriteLine($"ReadMeta: {sw.ElapsedMilliseconds} msec");
            }
            return value;
        }

        public static string GetMetadataExtractorVersion()
            => Assembly.GetAssembly(typeof(MetadataExtractor.Tag))?.GetName().Version?.ToString() ?? "";
    }
}
