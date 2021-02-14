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

        public MetaBook(string filePath)
        {
            FilePath = filePath;
            Pages = ReadMetaPages(filePath);
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

        // [RationalTag("Exif SubIFD", 0x829d)]
        private double _fnumber;

        #region 自動生成したい
        private bool _fnumber_loaded = false;
        public double Fnumber
        {
            get
            {
                if (!_fnumber_loaded)
                {
                    _fnumber = GetRationalTagValue("Exif SubIFD", 0x829d);
                    _fnumber_loaded = true;
                }
                return _fnumber;
            }
        }
        #endregion

        private double GetRationalTagValue(string pageName, int tagId)
        {
            var tag = GetMetaTag(pageName, tagId);

            if (tag?.Data is Rational rat)
                return (double)rat.Numerator / rat.Denominator;

            return 0;
        }

        private MetaTag? GetMetaTag(string pageName, int tagId)
        {
            var page = Pages.FirstOrDefault(x => x.Name == pageName);
            return page?.Tags.FirstOrDefault(x => x.Id == tagId);
        }

    }
}
