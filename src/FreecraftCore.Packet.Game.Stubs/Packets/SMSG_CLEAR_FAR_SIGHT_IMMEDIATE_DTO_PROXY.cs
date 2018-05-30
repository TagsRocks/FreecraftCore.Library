using FreecraftCore;
using FreecraftCore.Serializer;

[GamePayloadOperationCodeAttribute(NetworkOperationCode.SMSG_CLEAR_FAR_SIGHT_IMMEDIATE)]
[WireDataContractAttribute]
public sealed class SMSG_CLEAR_FAR_SIGHT_IMMEDIATE_DTO_PROXY : GamePacketPayload, IUnimplementedGamePacketPayload
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

    public SMSG_CLEAR_FAR_SIGHT_IMMEDIATE_DTO_PROXY()
    {
    }
}