//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: netconnect.proto
namespace ProtoMsg
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ConnectNotify")]
  public partial class ConnectNotify : global::ProtoBuf.IExtensible
  {
    public ConnectNotify() {}
    
    private int _nUserID;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"nUserID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int nUserID
    {
      get { return _nUserID; }
      set { _nUserID = value; }
    }
    private string _sRoomID;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"sRoomID", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string sRoomID
    {
      get { return _sRoomID; }
      set { _sRoomID = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"DisConnectNotify")]
  public partial class DisConnectNotify : global::ProtoBuf.IExtensible
  {
    public DisConnectNotify() {}
    
    private int _nUserID;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"nUserID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int nUserID
    {
      get { return _nUserID; }
      set { _nUserID = value; }
    }
    private string _sRoomID;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"sRoomID", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string sRoomID
    {
      get { return _sRoomID; }
      set { _sRoomID = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}