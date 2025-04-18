// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: messages_robocup_ssl_wrapper_legacy.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace RoboCup2014Legacy.Wrapper {

  /// <summary>Holder for reflection information generated from messages_robocup_ssl_wrapper_legacy.proto</summary>
  public static partial class MessagesRobocupSslWrapperLegacyReflection {

    #region Descriptor
    /// <summary>File descriptor for messages_robocup_ssl_wrapper_legacy.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static MessagesRobocupSslWrapperLegacyReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CiltZXNzYWdlc19yb2JvY3VwX3NzbF93cmFwcGVyX2xlZ2FjeS5wcm90bxIZ",
            "Um9ib0N1cDIwMTRMZWdhY3kuV3JhcHBlchokbWVzc2FnZXNfcm9ib2N1cF9z",
            "c2xfZGV0ZWN0aW9uLnByb3RvGiptZXNzYWdlc19yb2JvY3VwX3NzbF9nZW9t",
            "ZXRyeV9sZWdhY3kucHJvdG8iewoRU1NMX1dyYXBwZXJQYWNrZXQSJgoJZGV0",
            "ZWN0aW9uGAEgASgLMhMuU1NMX0RldGVjdGlvbkZyYW1lEj4KCGdlb21ldHJ5",
            "GAIgASgLMiwuUm9ib0N1cDIwMTRMZWdhY3kuR2VvbWV0cnkuU1NMX0dlb21l",
            "dHJ5RGF0YQ=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::MessagesRobocupSslDetectionReflection.Descriptor, global::RoboCup2014Legacy.Geometry.MessagesRobocupSslGeometryLegacyReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::RoboCup2014Legacy.Wrapper.SSL_WrapperPacket), global::RoboCup2014Legacy.Wrapper.SSL_WrapperPacket.Parser, new[]{ "Detection", "Geometry" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
  public sealed partial class SSL_WrapperPacket : pb::IMessage<SSL_WrapperPacket>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<SSL_WrapperPacket> _parser = new pb::MessageParser<SSL_WrapperPacket>(() => new SSL_WrapperPacket());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<SSL_WrapperPacket> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::RoboCup2014Legacy.Wrapper.MessagesRobocupSslWrapperLegacyReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SSL_WrapperPacket() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SSL_WrapperPacket(SSL_WrapperPacket other) : this() {
      detection_ = other.detection_ != null ? other.detection_.Clone() : null;
      geometry_ = other.geometry_ != null ? other.geometry_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SSL_WrapperPacket Clone() {
      return new SSL_WrapperPacket(this);
    }

    /// <summary>Field number for the "detection" field.</summary>
    public const int DetectionFieldNumber = 1;
    private global::SSL_DetectionFrame detection_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::SSL_DetectionFrame Detection {
      get { return detection_; }
      set {
        detection_ = value;
      }
    }

    /// <summary>Field number for the "geometry" field.</summary>
    public const int GeometryFieldNumber = 2;
    private global::RoboCup2014Legacy.Geometry.SSL_GeometryData geometry_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::RoboCup2014Legacy.Geometry.SSL_GeometryData Geometry {
      get { return geometry_; }
      set {
        geometry_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as SSL_WrapperPacket);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(SSL_WrapperPacket other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Detection, other.Detection)) return false;
      if (!object.Equals(Geometry, other.Geometry)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (detection_ != null) hash ^= Detection.GetHashCode();
      if (geometry_ != null) hash ^= Geometry.GetHashCode();
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
      if (detection_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Detection);
      }
      if (geometry_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(Geometry);
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
      if (detection_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Detection);
      }
      if (geometry_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(Geometry);
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
      if (detection_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Detection);
      }
      if (geometry_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Geometry);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(SSL_WrapperPacket other) {
      if (other == null) {
        return;
      }
      if (other.detection_ != null) {
        if (detection_ == null) {
          Detection = new global::SSL_DetectionFrame();
        }
        Detection.MergeFrom(other.Detection);
      }
      if (other.geometry_ != null) {
        if (geometry_ == null) {
          Geometry = new global::RoboCup2014Legacy.Geometry.SSL_GeometryData();
        }
        Geometry.MergeFrom(other.Geometry);
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
          case 10: {
            if (detection_ == null) {
              Detection = new global::SSL_DetectionFrame();
            }
            input.ReadMessage(Detection);
            break;
          }
          case 18: {
            if (geometry_ == null) {
              Geometry = new global::RoboCup2014Legacy.Geometry.SSL_GeometryData();
            }
            input.ReadMessage(Geometry);
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
          case 10: {
            if (detection_ == null) {
              Detection = new global::SSL_DetectionFrame();
            }
            input.ReadMessage(Detection);
            break;
          }
          case 18: {
            if (geometry_ == null) {
              Geometry = new global::RoboCup2014Legacy.Geometry.SSL_GeometryData();
            }
            input.ReadMessage(Geometry);
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
