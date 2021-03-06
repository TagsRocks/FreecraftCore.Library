using FreecraftCore;
using FreecraftCore.Serializer;

[GamePayloadOperationCodeAttribute(NetworkOperationCode.CMSG_AUCTION_LIST_PENDING_SALES)]
[WireDataContractAttribute]
public sealed class CMSG_AUCTION_LIST_PENDING_SALES_DTO_PROXY : GamePacketPayload, IUnimplementedGamePacketPayload
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

    public CMSG_AUCTION_LIST_PENDING_SALES_DTO_PROXY()
    {
    }
}