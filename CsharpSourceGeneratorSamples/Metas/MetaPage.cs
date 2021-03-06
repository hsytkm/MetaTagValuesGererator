using System;
using System.Collections.Generic;
using System.Linq;

namespace CsharpSourceGeneratorSamples.Metas
{
    public class MetaPage
    {
        public string Name { get; }
        public IReadOnlyCollection<MetaTag> Tags { get; }

        public MetaPage(MetadataExtractor.Directory directory)
        {
            Name = directory.Name;
            Tags = directory.Tags.Select(x => new MetaTag(x, directory.GetObject(x.Type))).ToArray();
        }

    }
}

