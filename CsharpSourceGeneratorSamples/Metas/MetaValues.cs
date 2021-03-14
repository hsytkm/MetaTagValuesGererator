using System;
using System.Collections.Generic;
using System.Linq;
using MyGenerator.MetaTagProperty;

namespace CsharpSourceGeneratorSamples.Metas
{
    public partial class MetaValues
    {
        private readonly IReadOnlyCollection<MetaPage> _metaPages;

        private readonly Dictionary<string, MetaPage> _namePagePair = new();

        public MetaValues(IReadOnlyCollection<MetaPage> metaPages)
        {
            _metaPages = metaPages;
        }

        private MetaTag? GetMetaTag(string pageName, int tagId)
        {
            MetaPage? page;
            if (!_namePagePair.ContainsKey(pageName))
            {
                page = _metaPages.FirstOrDefault(x => x.Name == pageName);
                if (page is not null) _namePagePair.Add(pageName, page);
            }
            else
            {
                page = _namePagePair[pageName];
            }
            return page?.Tags.FirstOrDefault(x => x.Id == tagId);
        }

        private object? GetMetaTagData(string pageName, int tagId)
        {
            var tag = GetMetaTag(pageName, tagId);
            return tag?.Data;
        }

        private double GetTagValue_double(string pageName, int tagId) => GetMetaTagData(pageName, tagId) switch
        {
            MetadataExtractor.Rational r => r.ToDouble(),   // (double)r.Numerator / r.Denominator
            _ => 0,
        };

        private int GetTagValue_int(string pageName, int tagId) => GetMetaTagData(pageName, tagId) switch
        {
            short s => s,
            ushort us => us,
            int i => i,
            uint ui => (ui <= int.MaxValue) ? (int)ui : throw new InvalidCastException(),
            _ => 0,
        };

        [MetaTagPropertyGenerator("Exif SubIFD", 0x8827)]
        private int _isoSpeed;

        [MetaTagPropertyGenerator("Exif SubIFD", 0x829d)]
        private double _fnumber;

        [MetaTagPropertyGenerator("Exif SubIFD", 0xa002)]
        private int _imageWidth;

        [MetaTagPropertyGenerator("Exif SubIFD", 0xa003)]
        private int _imageHeight;

    }
}
