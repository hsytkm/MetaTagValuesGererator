using System;
using System.Collections.Generic;

namespace CsharpSourceGeneratorSamples.Metas
{
#if false
    static class MetaStaticData
    {
        internal record MetaTitle
        {
            public static MetaTitle Unknown { get; } = new(0, "Unknown");
            public int Id { get; }
            public string Title { get; }
            public bool IsVisible { get; }
            public MetaTitle(int id, string title, bool isVisible = true) => (Id, Title, IsVisible) = (id, title, isVisible);
        }

        private static IReadOnlyDictionary<int, MetaTitle> MetaIdTitleMap { get; } = new Dictionary<int, MetaTitle>
        {
            { 0x0103,  new(0x0103, "圧縮種類") },
            { 0x010f,  new(0x010f, "メーカー名") },
            { 0x0110,  new(0x0110, "モデル名") },
            { 0x0112,  new(0x0112, "画像方向") },
            { 0x011a,  new(0x011a, "画像の幅の解像度") },
            { 0x011b,  new(0x011b, "画像の高さの解像度") },
            { 0x0128,  new(0x0128, "画像の幅と高さの解像度の単位") },
            { 0x0131,  new(0x0131, "使用ソフトウェア名") },
            { 0x0132,  new(0x0132, "ファイル変更日時") },
            { 0x0201,  new(0x0201, "サムネイルへのポインタ") },
            { 0x0202,  new(0x0202, "サムネイルのバイト数") },
            { 0x0213,  new(0x0213, "YCCの画素構成(YとCの位置)") },
            { 0x8769,  new(0x8769, "Exif IFDへのポインタ") },
            { 0x829a,  new(0x829a, "露光時間") },
            { 0x829d,  new(0x829d, "絞り値(F)") },
            { 0x8822,  new(0x8822, "露出プログラム") },
            { 0x8827,  new(0x8827, "ISO感度(2Byte)") },
            { 0x8830,  new(0x8830, "感度種別") },
            { 0x8831,  new(0x8831, "ISO感度(SOC/4Byte)") },
            { 0x8832,  new(0x8832, "ISO感度(REI/4Byte)") },
            { 0x8833,  new(0x8833, "ISO感度(SPEED/4Byte)") },
            { 0x9000,  new(0x9000, "Exifバージョン") },
            { 0x9003,  new(0x9003, "現画像データの生成日時", false) },
            { 0x9004,  new(0x9004, "デジタルデータの生成日時", false) },
            { 0x9010,  new(0x9010, "Date Time の時差データ", false) },
            { 0x9011,  new(0x9011, "Date Time Original の時差データ", false) },
            { 0x9012,  new(0x9012, "Date Time Digitized の時差データ", false) },
            { 0x9101,  new(0x9101, "各コンポーネントの意味") },
            { 0x9102,  new(0x9102, "画像圧縮モード") },
            { 0x9204,  new(0x9204, "露出補正値(EV)") },
            { 0x9205,  new(0x9205, "レンズ開放F値(APEX)") },
            { 0x9207,  new(0x9207, "測光方式") },
            { 0x9208,  new(0x9208, "光源") },
            { 0x9209,  new(0x9209, "フラッシュ") },
            { 0x920a,  new(0x920a, "レンズ実焦点距離") },
            { 0x927c,  new(0x927c, "メーカーノート") },
            { 0x9290,  new(0x9290, "ファイル変更日時のSubSec") },
            { 0x9291,  new(0x9291, "現画像データの生成日時のSubSec") },
            { 0x9292,  new(0x9292, "デジタルデータの生成日時のSubSec") },
            { 0xa000,  new(0xa000, "対応FlashPixバージョン") },
            { 0xa001,  new(0xa001, "色空間情報") },
            { 0xa002,  new(0xa002, "実効画像幅") },
            { 0xa003,  new(0xa003, "実効画像高さ") },
            { 0xa005,  new(0xa005, "互換性 IFDへのポインタ") },
            { 0xa217,  new(0xa217, "イメージセンサ方式") },
            { 0xa300,  new(0xa300, "ファイルソース") },
            { 0xa301,  new(0xa301, "シーンタイプ") },
            { 0xa401,  new(0xa401, "カスタムイメージ処理") },
            { 0xa402,  new(0xa402, "露出モード") },
            { 0xa403,  new(0xa403, "ホワイトバランス") },
            { 0xa404,  new(0xa404, "デジタルズーム倍率") },
            { 0xa405,  new(0xa405, "35mmフイルム換算の焦点距離") },
            { 0xa406,  new(0xa406, "シーンキャプチャーモード") },
            { 0xa407,  new(0xa407, "ゲイン制御") },
            { 0xa408,  new(0xa408, "コントラスト") },
            { 0xa409,  new(0xa409, "彩度") },
            { 0xa40a,  new(0xa40a, "シャープネス") },
            { 0xa431,  new(0xa431, "本体シリアル") },
            { 0xa432,  new(0xa432, "レンズ仕様") },
            { 0xa433,  new(0xa433, "レンズ製造") },
            { 0xa434,  new(0xa434, "レンズ機種名") },
            { 0xa435,  new(0xa435, "レンズシリアル") },
            { 0xc4a5,  new(0xc4a5, "PrintIM IFD") },
        };

        public static MetaTitle GetMetaTitle(int id)
        {
            if (MetaIdTitleMap.TryGetValue(id, out var value))
                return value;

            return MetaTitle.Unknown;
        }

    }
#endif
}
