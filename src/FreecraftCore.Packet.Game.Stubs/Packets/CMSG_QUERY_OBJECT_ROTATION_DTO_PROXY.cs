using FreecraftCore;
using FreecraftCore.Serializer;

[GamePayloadOperationCodeAttribute(NetworkOperationCode.CMSG_QUERY_OBJECT_ROTATION)]
[WireDataContractAttribute]
public sealed class CMSG_QUERY_OBJECT_ROTATION_DTO_PROXY : GamePacketPayload, IUnimplementedGamePacketPayload
{
    [ReadToEndAttribute]
    [WireMemberAttribute(1)]
    private byte[] _Data;
    public byte[] Data
    {
        get
        {
            return _Data;
        }

        set
        {
            _Data = value;
        }
    }

    public CMSG_QUERY_OBJECT_ROTATION_DTO_PROXY()
    {
    }
}