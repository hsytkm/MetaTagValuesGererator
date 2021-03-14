using System;

namespace CsharpSourceGeneratorSamples.Metas
{
    static class RationalExtension
    {
        public static string ToRationalString(in this MetadataExtractor.Rational rational)
        {
            ReadOnlySpan<char> bs = rational.Numerator.ToString();
            ReadOnlySpan<char> bb = rational.Denominator.ToString();

            while (bs[^1] == '0' && bb[^1] == '0')
            {
                bs = bs[0..^1];
                bb = bb[0..^1];
            }
            return bs.ToString() + "/" + bb.ToString();
        }

    }
}
