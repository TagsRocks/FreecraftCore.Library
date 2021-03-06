using FreecraftCore;
using FreecraftCore.Serializer;

[GamePayloadOperationCodeAttribute(NetworkOperationCode.SMSG_BATTLEGROUND_PLAYER_JOINED)]
[WireDataContractAttribute]
public sealed class SMSG_BATTLEGROUND_PLAYER_JOINED_DTO_PROXY : GamePacketPayload, IUnimplementedGamePacketPayload
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

    public SMSG_BATTLEGROUND_PLAYER_JOINED_DTO_PROXY()
    {
    }
}