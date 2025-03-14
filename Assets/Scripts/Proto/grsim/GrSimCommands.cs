// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: grSim_Commands.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from grSim_Commands.proto</summary>
public static partial class GrSimCommandsReflection {

  #region Descriptor
  /// <summary>File descriptor for grSim_Commands.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static GrSimCommandsReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "ChRnclNpbV9Db21tYW5kcy5wcm90byLqAQoTZ3JTaW1fUm9ib3RfQ29tbWFu",
          "ZBIKCgJpZBgBIAIoDRISCgpraWNrc3BlZWR4GAIgAigCEhIKCmtpY2tzcGVl",
          "ZHoYAyACKAISEgoKdmVsdGFuZ2VudBgEIAIoAhIRCgl2ZWxub3JtYWwYBSAC",
          "KAISEgoKdmVsYW5ndWxhchgGIAIoAhIPCgdzcGlubmVyGAcgAigIEhMKC3do",
          "ZWVsc3NwZWVkGAggAigIEg4KBndoZWVsMRgJIAEoAhIOCgZ3aGVlbDIYCiAB",
          "KAISDgoGd2hlZWwzGAsgASgCEg4KBndoZWVsNBgMIAEoAiJnCg5nclNpbV9D",
          "b21tYW5kcxIRCgl0aW1lc3RhbXAYASACKAESFAoMaXN0ZWFteWVsbG93GAIg",
          "AigIEiwKDnJvYm90X2NvbW1hbmRzGAMgAygLMhQuZ3JTaW1fUm9ib3RfQ29t",
          "bWFuZA=="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::grSim_Robot_Command), global::grSim_Robot_Command.Parser, new[]{ "Id", "Kickspeedx", "Kickspeedz", "Veltangent", "Velnormal", "Velangular", "Spinner", "Wheelsspeed", "Wheel1", "Wheel2", "Wheel3", "Wheel4" }, null, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::grSim_Commands), global::grSim_Commands.Parser, new[]{ "Timestamp", "Isteamyellow", "RobotCommands" }, null, null, null, null)
        }));
  }
  #endregion

}
#region Messages
[global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
public sealed partial class grSim_Robot_Command : pb::IMessage<grSim_Robot_Command>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<grSim_Robot_Command> _parser = new pb::MessageParser<grSim_Robot_Command>(() => new grSim_Robot_Command());
  private pb::UnknownFieldSet _unknownFields;
  private int _hasBits0;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pb::MessageParser<grSim_Robot_Command> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::GrSimCommandsReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public grSim_Robot_Command() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public grSim_Robot_Command(grSim_Robot_Command other) : this() {
    _hasBits0 = other._hasBits0;
    id_ = other.id_;
    kickspeedx_ = other.kickspeedx_;
    kickspeedz_ = other.kickspeedz_;
    veltangent_ = other.veltangent_;
    velnormal_ = other.velnormal_;
    velangular_ = other.velangular_;
    spinner_ = other.spinner_;
    wheelsspeed_ = other.wheelsspeed_;
    wheel1_ = other.wheel1_;
    wheel2_ = other.wheel2_;
    wheel3_ = other.wheel3_;
    wheel4_ = other.wheel4_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public grSim_Robot_Command Clone() {
    return new grSim_Robot_Command(this);
  }

  /// <summary>Field number for the "id" field.</summary>
  public const int IdFieldNumber = 1;
  private readonly static uint IdDefaultValue = 0;

  private uint id_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public uint Id {
    get { if ((_hasBits0 & 1) != 0) { return id_; } else { return IdDefaultValue; } }
    set {
      _hasBits0 |= 1;
      id_ = value;
    }
  }
  /// <summary>Gets whether the "id" field is set</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool HasId {
    get { return (_hasBits0 & 1) != 0; }
  }
  /// <summary>Clears the value of the "id" field</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearId() {
    _hasBits0 &= ~1;
  }

  /// <summary>Field number for the "kickspeedx" field.</summary>
  public const int KickspeedxFieldNumber = 2;
  private readonly static float KickspeedxDefaultValue = 0F;

  private float kickspeedx_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public float Kickspeedx {
    get { if ((_hasBits0 & 2) != 0) { return kickspeedx_; } else { return KickspeedxDefaultValue; } }
    set {
      _hasBits0 |= 2;
      kickspeedx_ = value;
    }
  }
  /// <summary>Gets whether the "kickspeedx" field is set</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool HasKickspeedx {
    get { return (_hasBits0 & 2) != 0; }
  }
  /// <summary>Clears the value of the "kickspeedx" field</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearKickspeedx() {
    _hasBits0 &= ~2;
  }

  /// <summary>Field number for the "kickspeedz" field.</summary>
  public const int KickspeedzFieldNumber = 3;
  private readonly static float KickspeedzDefaultValue = 0F;

  private float kickspeedz_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public float Kickspeedz {
    get { if ((_hasBits0 & 4) != 0) { return kickspeedz_; } else { return KickspeedzDefaultValue; } }
    set {
      _hasBits0 |= 4;
      kickspeedz_ = value;
    }
  }
  /// <summary>Gets whether the "kickspeedz" field is set</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool HasKickspeedz {
    get { return (_hasBits0 & 4) != 0; }
  }
  /// <summary>Clears the value of the "kickspeedz" field</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearKickspeedz() {
    _hasBits0 &= ~4;
  }

  /// <summary>Field number for the "veltangent" field.</summary>
  public const int VeltangentFieldNumber = 4;
  private readonly static float VeltangentDefaultValue = 0F;

  private float veltangent_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public float Veltangent {
    get { if ((_hasBits0 & 8) != 0) { return veltangent_; } else { return VeltangentDefaultValue; } }
    set {
      _hasBits0 |= 8;
      veltangent_ = value;
    }
  }
  /// <summary>Gets whether the "veltangent" field is set</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool HasVeltangent {
    get { return (_hasBits0 & 8) != 0; }
  }
  /// <summary>Clears the value of the "veltangent" field</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearVeltangent() {
    _hasBits0 &= ~8;
  }

  /// <summary>Field number for the "velnormal" field.</summary>
  public const int VelnormalFieldNumber = 5;
  private readonly static float VelnormalDefaultValue = 0F;

  private float velnormal_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public float Velnormal {
    get { if ((_hasBits0 & 16) != 0) { return velnormal_; } else { return VelnormalDefaultValue; } }
    set {
      _hasBits0 |= 16;
      velnormal_ = value;
    }
  }
  /// <summary>Gets whether the "velnormal" field is set</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool HasVelnormal {
    get { return (_hasBits0 & 16) != 0; }
  }
  /// <summary>Clears the value of the "velnormal" field</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearVelnormal() {
    _hasBits0 &= ~16;
  }

  /// <summary>Field number for the "velangular" field.</summary>
  public const int VelangularFieldNumber = 6;
  private readonly static float VelangularDefaultValue = 0F;

  private float velangular_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public float Velangular {
    get { if ((_hasBits0 & 32) != 0) { return velangular_; } else { return VelangularDefaultValue; } }
    set {
      _hasBits0 |= 32;
      velangular_ = value;
    }
  }
  /// <summary>Gets whether the "velangular" field is set</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool HasVelangular {
    get { return (_hasBits0 & 32) != 0; }
  }
  /// <summary>Clears the value of the "velangular" field</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearVelangular() {
    _hasBits0 &= ~32;
  }

  /// <summary>Field number for the "spinner" field.</summary>
  public const int SpinnerFieldNumber = 7;
  private readonly static bool SpinnerDefaultValue = false;

  private bool spinner_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Spinner {
    get { if ((_hasBits0 & 64) != 0) { return spinner_; } else { return SpinnerDefaultValue; } }
    set {
      _hasBits0 |= 64;
      spinner_ = value;
    }
  }
  /// <summary>Gets whether the "spinner" field is set</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool HasSpinner {
    get { return (_hasBits0 & 64) != 0; }
  }
  /// <summary>Clears the value of the "spinner" field</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearSpinner() {
    _hasBits0 &= ~64;
  }

  /// <summary>Field number for the "wheelsspeed" field.</summary>
  public const int WheelsspeedFieldNumber = 8;
  private readonly static bool WheelsspeedDefaultValue = false;

  private bool wheelsspeed_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Wheelsspeed {
    get { if ((_hasBits0 & 128) != 0) { return wheelsspeed_; } else { return WheelsspeedDefaultValue; } }
    set {
      _hasBits0 |= 128;
      wheelsspeed_ = value;
    }
  }
  /// <summary>Gets whether the "wheelsspeed" field is set</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool HasWheelsspeed {
    get { return (_hasBits0 & 128) != 0; }
  }
  /// <summary>Clears the value of the "wheelsspeed" field</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearWheelsspeed() {
    _hasBits0 &= ~128;
  }

  /// <summary>Field number for the "wheel1" field.</summary>
  public const int Wheel1FieldNumber = 9;
  private readonly static float Wheel1DefaultValue = 0F;

  private float wheel1_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public float Wheel1 {
    get { if ((_hasBits0 & 256) != 0) { return wheel1_; } else { return Wheel1DefaultValue; } }
    set {
      _hasBits0 |= 256;
      wheel1_ = value;
    }
  }
  /// <summary>Gets whether the "wheel1" field is set</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool HasWheel1 {
    get { return (_hasBits0 & 256) != 0; }
  }
  /// <summary>Clears the value of the "wheel1" field</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearWheel1() {
    _hasBits0 &= ~256;
  }

  /// <summary>Field number for the "wheel2" field.</summary>
  public const int Wheel2FieldNumber = 10;
  private readonly static float Wheel2DefaultValue = 0F;

  private float wheel2_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public float Wheel2 {
    get { if ((_hasBits0 & 512) != 0) { return wheel2_; } else { return Wheel2DefaultValue; } }
    set {
      _hasBits0 |= 512;
      wheel2_ = value;
    }
  }
  /// <summary>Gets whether the "wheel2" field is set</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool HasWheel2 {
    get { return (_hasBits0 & 512) != 0; }
  }
  /// <summary>Clears the value of the "wheel2" field</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearWheel2() {
    _hasBits0 &= ~512;
  }

  /// <summary>Field number for the "wheel3" field.</summary>
  public const int Wheel3FieldNumber = 11;
  private readonly static float Wheel3DefaultValue = 0F;

  private float wheel3_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public float Wheel3 {
    get { if ((_hasBits0 & 1024) != 0) { return wheel3_; } else { return Wheel3DefaultValue; } }
    set {
      _hasBits0 |= 1024;
      wheel3_ = value;
    }
  }
  /// <summary>Gets whether the "wheel3" field is set</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool HasWheel3 {
    get { return (_hasBits0 & 1024) != 0; }
  }
  /// <summary>Clears the value of the "wheel3" field</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearWheel3() {
    _hasBits0 &= ~1024;
  }

  /// <summary>Field number for the "wheel4" field.</summary>
  public const int Wheel4FieldNumber = 12;
  private readonly static float Wheel4DefaultValue = 0F;

  private float wheel4_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public float Wheel4 {
    get { if ((_hasBits0 & 2048) != 0) { return wheel4_; } else { return Wheel4DefaultValue; } }
    set {
      _hasBits0 |= 2048;
      wheel4_ = value;
    }
  }
  /// <summary>Gets whether the "wheel4" field is set</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool HasWheel4 {
    get { return (_hasBits0 & 2048) != 0; }
  }
  /// <summary>Clears the value of the "wheel4" field</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearWheel4() {
    _hasBits0 &= ~2048;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override bool Equals(object other) {
    return Equals(other as grSim_Robot_Command);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Equals(grSim_Robot_Command other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Id != other.Id) return false;
    if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Kickspeedx, other.Kickspeedx)) return false;
    if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Kickspeedz, other.Kickspeedz)) return false;
    if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Veltangent, other.Veltangent)) return false;
    if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Velnormal, other.Velnormal)) return false;
    if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Velangular, other.Velangular)) return false;
    if (Spinner != other.Spinner) return false;
    if (Wheelsspeed != other.Wheelsspeed) return false;
    if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Wheel1, other.Wheel1)) return false;
    if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Wheel2, other.Wheel2)) return false;
    if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Wheel3, other.Wheel3)) return false;
    if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Wheel4, other.Wheel4)) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override int GetHashCode() {
    int hash = 1;
    if (HasId) hash ^= Id.GetHashCode();
    if (HasKickspeedx) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Kickspeedx);
    if (HasKickspeedz) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Kickspeedz);
    if (HasVeltangent) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Veltangent);
    if (HasVelnormal) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Velnormal);
    if (HasVelangular) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Velangular);
    if (HasSpinner) hash ^= Spinner.GetHashCode();
    if (HasWheelsspeed) hash ^= Wheelsspeed.GetHashCode();
    if (HasWheel1) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Wheel1);
    if (HasWheel2) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Wheel2);
    if (HasWheel3) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Wheel3);
    if (HasWheel4) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Wheel4);
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
    if (HasId) {
      output.WriteRawTag(8);
      output.WriteUInt32(Id);
    }
    if (HasKickspeedx) {
      output.WriteRawTag(21);
      output.WriteFloat(Kickspeedx);
    }
    if (HasKickspeedz) {
      output.WriteRawTag(29);
      output.WriteFloat(Kickspeedz);
    }
    if (HasVeltangent) {
      output.WriteRawTag(37);
      output.WriteFloat(Veltangent);
    }
    if (HasVelnormal) {
      output.WriteRawTag(45);
      output.WriteFloat(Velnormal);
    }
    if (HasVelangular) {
      output.WriteRawTag(53);
      output.WriteFloat(Velangular);
    }
    if (HasSpinner) {
      output.WriteRawTag(56);
      output.WriteBool(Spinner);
    }
    if (HasWheelsspeed) {
      output.WriteRawTag(64);
      output.WriteBool(Wheelsspeed);
    }
    if (HasWheel1) {
      output.WriteRawTag(77);
      output.WriteFloat(Wheel1);
    }
    if (HasWheel2) {
      output.WriteRawTag(85);
      output.WriteFloat(Wheel2);
    }
    if (HasWheel3) {
      output.WriteRawTag(93);
      output.WriteFloat(Wheel3);
    }
    if (HasWheel4) {
      output.WriteRawTag(101);
      output.WriteFloat(Wheel4);
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
    if (HasId) {
      output.WriteRawTag(8);
      output.WriteUInt32(Id);
    }
    if (HasKickspeedx) {
      output.WriteRawTag(21);
      output.WriteFloat(Kickspeedx);
    }
    if (HasKickspeedz) {
      output.WriteRawTag(29);
      output.WriteFloat(Kickspeedz);
    }
    if (HasVeltangent) {
      output.WriteRawTag(37);
      output.WriteFloat(Veltangent);
    }
    if (HasVelnormal) {
      output.WriteRawTag(45);
      output.WriteFloat(Velnormal);
    }
    if (HasVelangular) {
      output.WriteRawTag(53);
      output.WriteFloat(Velangular);
    }
    if (HasSpinner) {
      output.WriteRawTag(56);
      output.WriteBool(Spinner);
    }
    if (HasWheelsspeed) {
      output.WriteRawTag(64);
      output.WriteBool(Wheelsspeed);
    }
    if (HasWheel1) {
      output.WriteRawTag(77);
      output.WriteFloat(Wheel1);
    }
    if (HasWheel2) {
      output.WriteRawTag(85);
      output.WriteFloat(Wheel2);
    }
    if (HasWheel3) {
      output.WriteRawTag(93);
      output.WriteFloat(Wheel3);
    }
    if (HasWheel4) {
      output.WriteRawTag(101);
      output.WriteFloat(Wheel4);
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
    if (HasId) {
      size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Id);
    }
    if (HasKickspeedx) {
      size += 1 + 4;
    }
    if (HasKickspeedz) {
      size += 1 + 4;
    }
    if (HasVeltangent) {
      size += 1 + 4;
    }
    if (HasVelnormal) {
      size += 1 + 4;
    }
    if (HasVelangular) {
      size += 1 + 4;
    }
    if (HasSpinner) {
      size += 1 + 1;
    }
    if (HasWheelsspeed) {
      size += 1 + 1;
    }
    if (HasWheel1) {
      size += 1 + 4;
    }
    if (HasWheel2) {
      size += 1 + 4;
    }
    if (HasWheel3) {
      size += 1 + 4;
    }
    if (HasWheel4) {
      size += 1 + 4;
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(grSim_Robot_Command other) {
    if (other == null) {
      return;
    }
    if (other.HasId) {
      Id = other.Id;
    }
    if (other.HasKickspeedx) {
      Kickspeedx = other.Kickspeedx;
    }
    if (other.HasKickspeedz) {
      Kickspeedz = other.Kickspeedz;
    }
    if (other.HasVeltangent) {
      Veltangent = other.Veltangent;
    }
    if (other.HasVelnormal) {
      Velnormal = other.Velnormal;
    }
    if (other.HasVelangular) {
      Velangular = other.Velangular;
    }
    if (other.HasSpinner) {
      Spinner = other.Spinner;
    }
    if (other.HasWheelsspeed) {
      Wheelsspeed = other.Wheelsspeed;
    }
    if (other.HasWheel1) {
      Wheel1 = other.Wheel1;
    }
    if (other.HasWheel2) {
      Wheel2 = other.Wheel2;
    }
    if (other.HasWheel3) {
      Wheel3 = other.Wheel3;
    }
    if (other.HasWheel4) {
      Wheel4 = other.Wheel4;
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
    if ((tag & 7) == 4) {
      // Abort on any end group tag.
      return;
    }
    switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 8: {
          Id = input.ReadUInt32();
          break;
        }
        case 21: {
          Kickspeedx = input.ReadFloat();
          break;
        }
        case 29: {
          Kickspeedz = input.ReadFloat();
          break;
        }
        case 37: {
          Veltangent = input.ReadFloat();
          break;
        }
        case 45: {
          Velnormal = input.ReadFloat();
          break;
        }
        case 53: {
          Velangular = input.ReadFloat();
          break;
        }
        case 56: {
          Spinner = input.ReadBool();
          break;
        }
        case 64: {
          Wheelsspeed = input.ReadBool();
          break;
        }
        case 77: {
          Wheel1 = input.ReadFloat();
          break;
        }
        case 85: {
          Wheel2 = input.ReadFloat();
          break;
        }
        case 93: {
          Wheel3 = input.ReadFloat();
          break;
        }
        case 101: {
          Wheel4 = input.ReadFloat();
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
    if ((tag & 7) == 4) {
      // Abort on any end group tag.
      return;
    }
    switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
          break;
        case 8: {
          Id = input.ReadUInt32();
          break;
        }
        case 21: {
          Kickspeedx = input.ReadFloat();
          break;
        }
        case 29: {
          Kickspeedz = input.ReadFloat();
          break;
        }
        case 37: {
          Veltangent = input.ReadFloat();
          break;
        }
        case 45: {
          Velnormal = input.ReadFloat();
          break;
        }
        case 53: {
          Velangular = input.ReadFloat();
          break;
        }
        case 56: {
          Spinner = input.ReadBool();
          break;
        }
        case 64: {
          Wheelsspeed = input.ReadBool();
          break;
        }
        case 77: {
          Wheel1 = input.ReadFloat();
          break;
        }
        case 85: {
          Wheel2 = input.ReadFloat();
          break;
        }
        case 93: {
          Wheel3 = input.ReadFloat();
          break;
        }
        case 101: {
          Wheel4 = input.ReadFloat();
          break;
        }
      }
    }
  }
  #endif

}

[global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
public sealed partial class grSim_Commands : pb::IMessage<grSim_Commands>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<grSim_Commands> _parser = new pb::MessageParser<grSim_Commands>(() => new grSim_Commands());
  private pb::UnknownFieldSet _unknownFields;
  private int _hasBits0;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pb::MessageParser<grSim_Commands> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::GrSimCommandsReflection.Descriptor.MessageTypes[1]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public grSim_Commands() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public grSim_Commands(grSim_Commands other) : this() {
    _hasBits0 = other._hasBits0;
    timestamp_ = other.timestamp_;
    isteamyellow_ = other.isteamyellow_;
    robotCommands_ = other.robotCommands_.Clone();
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public grSim_Commands Clone() {
    return new grSim_Commands(this);
  }

  /// <summary>Field number for the "timestamp" field.</summary>
  public const int TimestampFieldNumber = 1;
  private readonly static double TimestampDefaultValue = 0D;

  private double timestamp_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public double Timestamp {
    get { if ((_hasBits0 & 1) != 0) { return timestamp_; } else { return TimestampDefaultValue; } }
    set {
      _hasBits0 |= 1;
      timestamp_ = value;
    }
  }
  /// <summary>Gets whether the "timestamp" field is set</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool HasTimestamp {
    get { return (_hasBits0 & 1) != 0; }
  }
  /// <summary>Clears the value of the "timestamp" field</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearTimestamp() {
    _hasBits0 &= ~1;
  }

  /// <summary>Field number for the "isteamyellow" field.</summary>
  public const int IsteamyellowFieldNumber = 2;
  private readonly static bool IsteamyellowDefaultValue = false;

  private bool isteamyellow_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Isteamyellow {
    get { if ((_hasBits0 & 2) != 0) { return isteamyellow_; } else { return IsteamyellowDefaultValue; } }
    set {
      _hasBits0 |= 2;
      isteamyellow_ = value;
    }
  }
  /// <summary>Gets whether the "isteamyellow" field is set</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool HasIsteamyellow {
    get { return (_hasBits0 & 2) != 0; }
  }
  /// <summary>Clears the value of the "isteamyellow" field</summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void ClearIsteamyellow() {
    _hasBits0 &= ~2;
  }

  /// <summary>Field number for the "robot_commands" field.</summary>
  public const int RobotCommandsFieldNumber = 3;
  private static readonly pb::FieldCodec<global::grSim_Robot_Command> _repeated_robotCommands_codec
      = pb::FieldCodec.ForMessage(26, global::grSim_Robot_Command.Parser);
  private readonly pbc::RepeatedField<global::grSim_Robot_Command> robotCommands_ = new pbc::RepeatedField<global::grSim_Robot_Command>();
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public pbc::RepeatedField<global::grSim_Robot_Command> RobotCommands {
    get { return robotCommands_; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override bool Equals(object other) {
    return Equals(other as grSim_Commands);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public bool Equals(grSim_Commands other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (!pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.Equals(Timestamp, other.Timestamp)) return false;
    if (Isteamyellow != other.Isteamyellow) return false;
    if(!robotCommands_.Equals(other.robotCommands_)) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public override int GetHashCode() {
    int hash = 1;
    if (HasTimestamp) hash ^= pbc::ProtobufEqualityComparers.BitwiseDoubleEqualityComparer.GetHashCode(Timestamp);
    if (HasIsteamyellow) hash ^= Isteamyellow.GetHashCode();
    hash ^= robotCommands_.GetHashCode();
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
    if (HasTimestamp) {
      output.WriteRawTag(9);
      output.WriteDouble(Timestamp);
    }
    if (HasIsteamyellow) {
      output.WriteRawTag(16);
      output.WriteBool(Isteamyellow);
    }
    robotCommands_.WriteTo(output, _repeated_robotCommands_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
    if (HasTimestamp) {
      output.WriteRawTag(9);
      output.WriteDouble(Timestamp);
    }
    if (HasIsteamyellow) {
      output.WriteRawTag(16);
      output.WriteBool(Isteamyellow);
    }
    robotCommands_.WriteTo(ref output, _repeated_robotCommands_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(ref output);
    }
  }
  #endif

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public int CalculateSize() {
    int size = 0;
    if (HasTimestamp) {
      size += 1 + 8;
    }
    if (HasIsteamyellow) {
      size += 1 + 1;
    }
    size += robotCommands_.CalculateSize(_repeated_robotCommands_codec);
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
  public void MergeFrom(grSim_Commands other) {
    if (other == null) {
      return;
    }
    if (other.HasTimestamp) {
      Timestamp = other.Timestamp;
    }
    if (other.HasIsteamyellow) {
      Isteamyellow = other.Isteamyellow;
    }
    robotCommands_.Add(other.robotCommands_);
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
    if ((tag & 7) == 4) {
      // Abort on any end group tag.
      return;
    }
    switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 9: {
          Timestamp = input.ReadDouble();
          break;
        }
        case 16: {
          Isteamyellow = input.ReadBool();
          break;
        }
        case 26: {
          robotCommands_.AddEntriesFrom(input, _repeated_robotCommands_codec);
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
    if ((tag & 7) == 4) {
      // Abort on any end group tag.
      return;
    }
    switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
          break;
        case 9: {
          Timestamp = input.ReadDouble();
          break;
        }
        case 16: {
          Isteamyellow = input.ReadBool();
          break;
        }
        case 26: {
          robotCommands_.AddEntriesFrom(ref input, _repeated_robotCommands_codec);
          break;
        }
      }
    }
  }
  #endif

}

#endregion


#endregion Designer generated code
