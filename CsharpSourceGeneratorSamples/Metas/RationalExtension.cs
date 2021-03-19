using System;
using System.Linq;

namespace CsharpSourceGeneratorSamples.Metas
{
    static class RationalExtension
    {
        public static int ToInt(in this MetadataExtractor.Rational rational)
        {
            var bs = rational.Numerator;
            var bb = rational.Denominator;

            while ((bs % 10) == 0 && (bb % 10) == 0)
            {
                bs /= 10;
                bb /= 10;
            }
            if (bb != 1) throw new NotSupportedException();

            return bs <= int.MaxValue ? (int)bs : throw new InvalidCastException();
        }

        public static string ToRationalString(in this MetadataExtractor.Rational rational)
        {
            ReadOnlySpan<char> bs = rational.Numerator.ToString();
            ReadOnlySpan<char> bb = rational.Denominator.ToString();

            while (bs[^1] == '0' && bb[^1] == '0')
            {
                bs = bs[0..^1];
                bb = bb[0..^1];
            }
            if (bb.Length == 1 && bb[0] == '1') return bs.ToString();

            return bs.ToString() + "/" + bb.ToString();
        }

        public static string ToRationalsString(this MetadataExtractor.Rational[] rationals, string separator = ", ")
            => string.Join(separator, rationals.Select(r => r.ToRationalString()));

    }
}
