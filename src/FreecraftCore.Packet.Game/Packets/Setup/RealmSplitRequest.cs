﻿using FreecraftCore.Serializer;

namespace FreecraftCore
{
	/// <summary>
	/// Sent by the connecting user during character list.
	/// Sent with <see cref="NetworkOperationCode.CMSG_REALM_SPLIT"/>.
	/// </summary>
	[WireDataContract]
	[GamePayloadOperationCode(NetworkOperationCode.CMSG_REALM_SPLIT)]
	public sealed class RealmSplitRequest : GamePacketPayload
	{
		/// <summary>
		/// Unknown data. Trinitycore has a 4 byte field here.
		/// </summary>
		[WireMember(1)]
		public int Unk { get; }

		/// <inheritdoc />
		public RealmSplitRequest(int unk)
		{
			Unk = unk;
		}

		public RealmSplitRequest()
		{
			
		}
	}
}
