using FreecraftCore;
using FreecraftCore.Serializer;

[GamePayloadOperationCodeAttribute(NetworkOperationCode.SMSG_AVAILABLE_VOICE_CHANNEL)]
[WireDataContractAttribute]
public sealed class SMSG_AVAILABLE_VOICE_CHANNEL_DTO_PROXY : GamePacketPayload, IUnimplementedGamePacketPayload
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

    public SMSG_AVAILABLE_VOICE_CHANNEL_DTO_PROXY()
    {
    }
}