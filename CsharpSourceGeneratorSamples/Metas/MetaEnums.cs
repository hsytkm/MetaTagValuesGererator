using System;
using System.Runtime.Serialization;

namespace CsharpSourceGeneratorSamples.Metas
{
    public enum ExposureProgram
    {
        [EnumMember(Value = "未定義")] NotDefined,
        [EnumMember(Value = "マニュアル")] Manual,
        [EnumMember(Value = "プログラム")] Program,
        [EnumMember(Value = "絞り優先")] AperturePriority,
        [EnumMember(Value = "シャッタ優先")] ShutterPriority
    }

}
