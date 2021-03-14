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
        private readonly Dictionary<string, MetaPage?> _namePagePair = new();

        public MetaValues(IReadOnlyCollection<MetaPage> metaPages) => _metaPages = metaPages;

        #region base methods
        private MetaPage? GeMetaPage(string pageName)
        {
            if (_namePagePair.TryGetValue(pageName, out var p)) return p;

            var page = _metaPages.FirstOrDefault(x => x.Name == pageName);
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
            MetadataExtractor.Rational r => r.ToInt(),
            _ => throw new NotImplementedException(),
        };

        private string GetTagValue_string(string pageName, int tagId) => GetMetaTagData(pageName, tagId) switch
        {
            MetadataExtractor.StringValue sv => sv.ToString(),
            byte[] bs => System.Text.Encoding.ASCII.GetString(bs),
            string s => s,
            MetadataExtractor.Rational r => r.ToRationalString(),
            _ => throw new NotImplementedException(),
        };
        #endregion

#pragma warning disable IDE0044
        [MetaTagPropertyGenerator(ExifIFD, 0x010f)]
        private string _maker = default!;

        [MetaTagPropertyGenerator(ExifIFD, 0x0110)]
        private string _model = default!;

        [MetaTagPropertyGenerator(ExifSubIFD, 0x829d)]
        private double _fNumber;

        [MetaTagPropertyGenerator(ExifSubIFD, 0x829a)]
        private string _shutterSpeed = default!;

        [MetaTagPropertyGenerator(ExifSubIFD, 0x8822)]
        private ExposureProgram _exposureProgram;

        [MetaTagPropertyGenerator(ExifSubIFD, 0x8827)]
        private int _isoSpeed;

        [MetaTagPropertyGenerator(ExifSubIFD, 0x920a)]
        private int _focalLength;
#pragma warning restore IDE0044

    }
}
