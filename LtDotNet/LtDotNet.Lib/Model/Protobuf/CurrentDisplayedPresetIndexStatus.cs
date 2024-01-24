// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: CurrentDisplayedPresetIndexStatus.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from CurrentDisplayedPresetIndexStatus.proto</summary>
public static partial class CurrentDisplayedPresetIndexStatusReflection {

  #region Descriptor
  /// <summary>File descriptor for CurrentDisplayedPresetIndexStatus.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static CurrentDisplayedPresetIndexStatusReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "CidDdXJyZW50RGlzcGxheWVkUHJlc2V0SW5kZXhTdGF0dXMucHJvdG8iSAoh",
          "Q3VycmVudERpc3BsYXllZFByZXNldEluZGV4U3RhdHVzEiMKG2N1cnJlbnRE",
          "aXNwbGF5ZWRQcmVzZXRJbmRleBgBIAIoBQ=="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::CurrentDisplayedPresetIndexStatus), global::CurrentDisplayedPresetIndexStatus.Parser, new[]{ "CurrentDisplayedPresetIndex" }, null, null, null, null)
        }));
  }
  #endregion

}
#region Messages
/// <summary>
///
/// The current preset displayed on the amp. Sent when the preset is changed at the amp
/// </summary>
[global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
public sealed partial class CurrentDisplayedPresetIndexStatus : pb::IMessage<CurrentDisplayedPresetIndexStatus>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<CurrentDisplayedPresetIndexStatus> _parser = new pb::MessageParser<CurrentDisplayedPresetIndexStatus>(() => new CurrentDisplayedPresetIndexStatus());
  private pb::UnknownFieldSet _unknownFields;
  private int _hasBits0;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pb::MessageParser<CurrentDisplayedPresetIndexStatus> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::CurrentDisplayedPresetIndexStatusReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public CurrentDisplayedPresetIndexStatus() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public CurrentDisplayedPresetIndexStatus(CurrentDisplayedPresetIndexStatus other) : this() {
    _hasBits0 = other._hasBits0;
    currentDisplayedPresetIndex_ = other.currentDisplayedPresetIndex_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public CurrentDisplayedPresetIndexStatus Clone() {
    return new CurrentDisplayedPresetIndexStatus(this);
  }

  /// <summary>Field number for the "currentDisplayedPresetIndex" field.</summary>
  public const int CurrentDisplayedPresetIndexFieldNumber = 1;
  private readonly static int CurrentDisplayedPresetIndexDefaultValue = 0;

  private int currentDisplayedPresetIndex_;
  /// <summary>
  /// The current preset bank number
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int CurrentDisplayedPresetIndex {
    get { if ((_hasBits0 & 1) != 0) { return currentDisplayedPresetIndex_; } else { return CurrentDisplayedPresetIndexDefaultValue; } }
    set {
      _hasBits0 |= 1;
      currentDisplayedPresetIndex_ = value;
    }
  }
  /// <summary>Gets whether the "currentDisplayedPresetIndex" field is set</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool HasCurrentDisplayedPresetIndex {
    get { return (_hasBits0 & 1) != 0; }
  }
  /// <summary>Clears the value of the "currentDisplayedPresetIndex" field</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearCurrentDisplayedPresetIndex() {
    _hasBits0 &= ~1;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override bool Equals(object other) {
    return Equals(other as CurrentDisplayedPresetIndexStatus);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Equals(CurrentDisplayedPresetIndexStatus other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (CurrentDisplayedPresetIndex != other.CurrentDisplayedPresetIndex) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override int GetHashCode() {
    int hash = 1;
    if (HasCurrentDisplayedPresetIndex) hash ^= CurrentDisplayedPresetIndex.GetHashCode();
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
    if (HasCurrentDisplayedPresetIndex) {
      output.WriteRawTag(8);
      output.WriteInt32(CurrentDisplayedPresetIndex);
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
    if (HasCurrentDisplayedPresetIndex) {
      output.WriteRawTag(8);
      output.WriteInt32(CurrentDisplayedPresetIndex);
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
    if (HasCurrentDisplayedPresetIndex) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(CurrentDisplayedPresetIndex);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(CurrentDisplayedPresetIndexStatus other) {
    if (other == null) {
      return;
    }
    if (other.HasCurrentDisplayedPresetIndex) {
      CurrentDisplayedPresetIndex = other.CurrentDisplayedPresetIndex;
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
        case 8: {
          CurrentDisplayedPresetIndex = input.ReadInt32();
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
        case 8: {
          CurrentDisplayedPresetIndex = input.ReadInt32();
          break;
        }
      }
    }
  }
  #endif

}

#endregion


#endregion Designer generated code
