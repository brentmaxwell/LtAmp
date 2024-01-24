// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: CurrentPresetStatus.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from CurrentPresetStatus.proto</summary>
public static partial class CurrentPresetStatusReflection {

  #region Descriptor
  /// <summary>File descriptor for CurrentPresetStatus.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static CurrentPresetStatusReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "ChlDdXJyZW50UHJlc2V0U3RhdHVzLnByb3RvImwKE0N1cnJlbnRQcmVzZXRT",
          "dGF0dXMSGQoRY3VycmVudFByZXNldERhdGEYASACKAkSGAoQY3VycmVudFNs",
          "b3RJbmRleBgCIAIoBRIgChhjdXJyZW50UHJlc2V0RGlydHlTdGF0dXMYAyAC",
          "KAg="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::CurrentPresetStatus), global::CurrentPresetStatus.Parser, new[]{ "CurrentPresetData", "CurrentSlotIndex", "CurrentPresetDirtyStatus" }, null, null, null, null)
        }));
  }
  #endregion

}
#region Messages
/// <summary>
///
/// Returns the state of the current preset
/// </summary>
[global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
public sealed partial class CurrentPresetStatus : pb::IMessage<CurrentPresetStatus>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<CurrentPresetStatus> _parser = new pb::MessageParser<CurrentPresetStatus>(() => new CurrentPresetStatus());
  private pb::UnknownFieldSet _unknownFields;
  private int _hasBits0;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pb::MessageParser<CurrentPresetStatus> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::CurrentPresetStatusReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public CurrentPresetStatus() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public CurrentPresetStatus(CurrentPresetStatus other) : this() {
    _hasBits0 = other._hasBits0;
    currentPresetData_ = other.currentPresetData_;
    currentSlotIndex_ = other.currentSlotIndex_;
    currentPresetDirtyStatus_ = other.currentPresetDirtyStatus_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public CurrentPresetStatus Clone() {
    return new CurrentPresetStatus(this);
  }

  /// <summary>Field number for the "currentPresetData" field.</summary>
  public const int CurrentPresetDataFieldNumber = 1;
  private readonly static string CurrentPresetDataDefaultValue = "";

  private string currentPresetData_;
  /// <summary>
  /// JSON data conatining current preset data
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public string CurrentPresetData {
    get { return currentPresetData_ ?? CurrentPresetDataDefaultValue; }
    set {
      currentPresetData_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }
  /// <summary>Gets whether the "currentPresetData" field is set</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool HasCurrentPresetData {
    get { return currentPresetData_ != null; }
  }
  /// <summary>Clears the value of the "currentPresetData" field</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearCurrentPresetData() {
    currentPresetData_ = null;
  }

  /// <summary>Field number for the "currentSlotIndex" field.</summary>
  public const int CurrentSlotIndexFieldNumber = 2;
  private readonly static int CurrentSlotIndexDefaultValue = 0;

  private int currentSlotIndex_;
  /// <summary>
  /// Current preset bank number
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int CurrentSlotIndex {
    get { if ((_hasBits0 & 1) != 0) { return currentSlotIndex_; } else { return CurrentSlotIndexDefaultValue; } }
    set {
      _hasBits0 |= 1;
      currentSlotIndex_ = value;
    }
  }
  /// <summary>Gets whether the "currentSlotIndex" field is set</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool HasCurrentSlotIndex {
    get { return (_hasBits0 & 1) != 0; }
  }
  /// <summary>Clears the value of the "currentSlotIndex" field</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearCurrentSlotIndex() {
    _hasBits0 &= ~1;
  }

  /// <summary>Field number for the "currentPresetDirtyStatus" field.</summary>
  public const int CurrentPresetDirtyStatusFieldNumber = 3;
  private readonly static bool CurrentPresetDirtyStatusDefaultValue = false;

  private bool currentPresetDirtyStatus_;
  /// <summary>
  /// True if current preset has been edited and not saved
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool CurrentPresetDirtyStatus {
    get { if ((_hasBits0 & 2) != 0) { return currentPresetDirtyStatus_; } else { return CurrentPresetDirtyStatusDefaultValue; } }
    set {
      _hasBits0 |= 2;
      currentPresetDirtyStatus_ = value;
    }
  }
  /// <summary>Gets whether the "currentPresetDirtyStatus" field is set</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool HasCurrentPresetDirtyStatus {
    get { return (_hasBits0 & 2) != 0; }
  }
  /// <summary>Clears the value of the "currentPresetDirtyStatus" field</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearCurrentPresetDirtyStatus() {
    _hasBits0 &= ~2;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override bool Equals(object other) {
    return Equals(other as CurrentPresetStatus);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Equals(CurrentPresetStatus other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (CurrentPresetData != other.CurrentPresetData) return false;
    if (CurrentSlotIndex != other.CurrentSlotIndex) return false;
    if (CurrentPresetDirtyStatus != other.CurrentPresetDirtyStatus) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override int GetHashCode() {
    int hash = 1;
    if (HasCurrentPresetData) hash ^= CurrentPresetData.GetHashCode();
    if (HasCurrentSlotIndex) hash ^= CurrentSlotIndex.GetHashCode();
    if (HasCurrentPresetDirtyStatus) hash ^= CurrentPresetDirtyStatus.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void WriteTo(pb::CodedOutputStream output) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    output.WriteRawMessage(this);
  #else
    if (HasCurrentPresetData) {
      output.WriteRawTag(10);
      output.WriteString(CurrentPresetData);
    }
    if (HasCurrentSlotIndex) {
      output.WriteRawTag(16);
      output.WriteInt32(CurrentSlotIndex);
    }
    if (HasCurrentPresetDirtyStatus) {
      output.WriteRawTag(24);
      output.WriteBool(CurrentPresetDirtyStatus);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
    if (HasCurrentPresetData) {
      output.WriteRawTag(10);
      output.WriteString(CurrentPresetData);
    }
    if (HasCurrentSlotIndex) {
      output.WriteRawTag(16);
      output.WriteInt32(CurrentSlotIndex);
    }
    if (HasCurrentPresetDirtyStatus) {
      output.WriteRawTag(24);
      output.WriteBool(CurrentPresetDirtyStatus);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(ref output);
    }
  }
  #endif

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int CalculateSize() {
    int size = 0;
    if (HasCurrentPresetData) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(CurrentPresetData);
    }
    if (HasCurrentSlotIndex) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(CurrentSlotIndex);
    }
    if (HasCurrentPresetDirtyStatus) {
      size += 1 + 1;
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(CurrentPresetStatus other) {
    if (other == null) {
      return;
    }
    if (other.HasCurrentPresetData) {
      CurrentPresetData = other.CurrentPresetData;
    }
    if (other.HasCurrentSlotIndex) {
      CurrentSlotIndex = other.CurrentSlotIndex;
    }
    if (other.HasCurrentPresetDirtyStatus) {
      CurrentPresetDirtyStatus = other.CurrentPresetDirtyStatus;
    }
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(pb::CodedInputStream input) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    input.ReadRawMessage(this);
  #else
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 10: {
          CurrentPresetData = input.ReadString();
          break;
        }
        case 16: {
          CurrentSlotIndex = input.ReadInt32();
          break;
        }
        case 24: {
          CurrentPresetDirtyStatus = input.ReadBool();
          break;
        }
      }
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
          break;
        case 10: {
          CurrentPresetData = input.ReadString();
          break;
        }
        case 16: {
          CurrentSlotIndex = input.ReadInt32();
          break;
        }
        case 24: {
          CurrentPresetDirtyStatus = input.ReadBool();
          break;
        }
      }
    }
  }
  #endif

}

#endregion


#endregion Designer generated code
