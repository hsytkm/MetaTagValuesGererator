using MetadataExtractor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CsharpSourceGeneratorSamples.Metas
{
    /// <summary>Book=File</summary>
    public class MetaBook
    {
        public enum Extensions
        {
            NotSupported, Jpeg, Bmp, Png, Tiff, Gif
        };

        public string FilePath { get; }
        public IReadOnlyCollection<MetaPage> Pages { get; }

        public MetaValues Values { get; }

        public MetaBook(string filePath)
        {
            FilePath = filePath;
            Pages = ReadMetaPages(filePath);
            Values = new MetaValues(Pages);
        }

        private static Extensions GetExtension(string filePath)
            => Path.GetExtension(filePath).ToLower() switch
            {
                ".jpg" or ".jpeg" => Extensions.Jpeg,
                ".bmp" => Extensions.Bmp,
                ".png" => Extensions.Png,
                ".tif" or ".tiff" => Extensions.Tiff,
                ".gif" => Extensions.Gif,
                _ => Extensions.NotSupported,
            };

        private static bool IsSupportedExtension(string filePath) => GetExtension(filePath) is not Extensions.NotSupported;

        private static IReadOnlyCollection<MetaPage> ReadMetaPages(string filePath)
        {
            var directories = IsSupportedExtension(filePath)
                ? ImageMetadataReader.ReadMetadata(filePath)
                : null;

            return directories is not null
                ? directories.Select(d => new MetaPage(d)).ToArray()
                : Array.Empty<MetaPage>();
        }

    }
}
