using FreecraftCore;
using FreecraftCore.Serializer;

[GamePayloadOperationCodeAttribute(NetworkOperationCode.SMSG_INVALIDATE_PLAYER)]
[WireDataContractAttribute]
public sealed class SMSG_INVALIDATE_PLAYER_DTO_PROXY : GamePacketPayload, IUnimplementedGamePacketPayload
{
    [ReadToEndAttribute]
    [WireMemberAttribute(1)]
    private byte[] _Data;
    public byte[] Data
    {
        get
        {
            return Data;
        }

        set
        {
            Data = value;
        }
    }

    public SMSG_INVALIDATE_PLAYER_DTO_PROXY()
    {
    }
}