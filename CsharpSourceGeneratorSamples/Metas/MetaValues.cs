using System;
using System.Collections.Generic;
using System.Linq;
using MyGenerator.MetaTagProperty;

namespace CsharpSourceGeneratorSamples.Metas
{
    public partial class MetaValues
    {
        private const string ExifIFD = "Exif IFD0";
        private const string ExifSubIFD = "Exif SubIFD";

        private readonly IReadOnlyCollection<MetaPage> _metaPages;
        private readonly Dictionary<string, MetaPage> _namePagePair = new();

        public MetaValues(IReadOnlyCollection<MetaPage> metaPages)
        {
            _metaPages = metaPages;
        }

        #region BaseMethods
        private MetaPage? GeMetaPage(string pageName)
        {
            if (_namePagePair.TryGetValue(pageName, out var p))
                return p;

            var page = _metaPages.FirstOrDefault(x => x.Name == pageName);
            if (page is null) throw new KeyNotFoundException();

            _namePagePair.Add(pageName, page);
            return page;
        }

        private MetaTag? GetMetaTag(string pageName, int tagId) => GeMetaPage(pageName)?.Tags.FirstOrDefault(x => x.Id == tagId);

        private object? GetMetaTagData(string pageName, int tagId)
        {
            var tag = GetMetaTag(pageName, tagId);
            return tag?.Data;
        }

        private double GetTagValue_double(string pageName, int tagId) => GetMetaTagData(pageName, tagId) switch
        {
            MetadataExtractor.Rational r => r.ToDouble(),   // (double)r.Numerator / r.Denominator
            _ => throw new NotImplementedException(),
        };

        private int GetTagValue_int(string pageName, int tagId) => GetMetaTagData(pageName, tagId) switch
        {
            byte b => b,
            short s => s,
            ushort us => us,
            int i => i,
            uint ui => (ui <= int.MaxValue) ? (int)ui : throw new InvalidCastException(),
            long l => (l <= int.MaxValue) ? (int)l : throw new InvalidCastException(),
            ulong ul => (ul <= int.MaxValue) ? (int)ul : throw new InvalidCastException(),
            _ => throw new NotImplementedException(),
        };

        private string GetTagValue_string(string pageName, int tagId) => GetMetaTagData(pageName, tagId) switch
        {
            MetadataExtractor.StringValue sv => sv.ToString(),
            MetadataExtractor.Rational r => r.ToRationalString(),
            byte[] bs => System.Text.Encoding.ASCII.GetString(bs),
            string s => s,
            _ => throw new NotImplementedException(),
        };
        #endregion

        [MetaTagPropertyGenerator(ExifIFD, 0x0110)]
        private string _modelName = default!;

        [MetaTagPropertyGenerator(ExifSubIFD, 0x829d)]
        private double _fnumber;

        [MetaTagPropertyGenerator(ExifSubIFD, 0x829a)]
        private string _shutterSpeed = default!;

        [MetaTagPropertyGenerator(ExifSubIFD, 0x8827)]
        private int _isoSpeed;

        [MetaTagPropertyGenerator(ExifSubIFD, 0x9000)]
        private string _exifVersion = default!;

        [MetaTagPropertyGenerator(ExifSubIFD, 0xa002)]
        private int _imageWidth;

        [MetaTagPropertyGenerator(ExifSubIFD, 0xa003)]
        private int _imageHeight;

    }
}
