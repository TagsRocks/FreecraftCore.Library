using FreecraftCore;
using FreecraftCore.Serializer;

[GamePayloadOperationCodeAttribute(NetworkOperationCode.SMSG_LEARNED_SPELL)]
[WireDataContractAttribute]
public sealed class SMSG_LEARNED_SPELL_DTO_PROXY : GamePacketPayload, IUnimplementedGamePacketPayload
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

    public SMSG_LEARNED_SPELL_DTO_PROXY()
    {
    }
}