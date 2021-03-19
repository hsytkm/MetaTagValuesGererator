using System;
using System.Runtime.Serialization;

namespace CsharpSourceGeneratorSamples.Metas
{
    public enum ExposureProgram     // 0x8822
    {
        [EnumMember(Value = "未定義")] NotDefined,
        [EnumMember(Value = "マニュアル")] Manual,
        [EnumMember(Value = "プログラム")] Program,
        [EnumMember(Value = "絞り優先")] AperturePriority,
        [EnumMember(Value = "シャッタ優先")] ShutterPriority
    }

    public enum MeteringMode    // 0x9207
    {
        [EnumMember(Value = "不明")] Unknown,
        [EnumMember(Value = "平均測光")] Average,
        [EnumMember(Value = "中央重点測光")] CenterWeightedAverage,
        [EnumMember(Value = "スポット測光")] Spot,
        [EnumMember(Value = "マルチスポット測光")] MultiSpot,
        [EnumMember(Value = "評価測光")] Pattern,
        [EnumMember(Value = "分割測光")] Partial,
        [EnumMember(Value = "その他の測光")] Other = 255,
    }

}
