//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: leave_room.proto
namespace ProtoMsg
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LeaveRoomRequest")]
  public partial class LeaveRoomRequest : global::ProtoBuf.IExtensible
  {
    public LeaveRoomRequest() {}
    
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
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LeaveRoomResponse")]
  public partial class LeaveRoomResponse : global::ProtoBuf.IExtensible
  {
    public LeaveRoomResponse() {}
    
    private int _nErrorCode;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"nErrorCode", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int nErrorCode
    {
      get { return _nErrorCode; }
      set { _nErrorCode = value; }
    }
    private bool _bKick = default(bool);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"bKick", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool bKick
    {
      get { return _bKick; }
      set { _bKick = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LeaveRoomNotify")]
  public partial class LeaveRoomNotify : global::ProtoBuf.IExtensible
  {
    public LeaveRoomNotify() {}
    
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
    private bool _bKick = default(bool);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"bKick", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool bKick
    {
      get { return _bKick; }
      set { _bKick = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}