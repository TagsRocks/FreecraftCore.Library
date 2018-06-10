﻿using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore.Serializer;

namespace FreecraftCore
{
	[WireDataContract]
	[GamePayloadOperationCode(NetworkOperationCode.SMSG_ITEM_QUERY_SINGLE_RESPONSE)]
	public sealed class SMSG_ITEM_QUERY_SINGLE_RESPONSE_Payload : GamePacketPayload
	{
		[WireMember(1)]
		private uint PackedResponseId { get; }

		/// <summary>
		/// Indicates if the query was successful.
		/// </summary>
		public bool WasRequestSuccessful => (PackedResponseId & 0x80000000) == 0;

		[Optional(nameof(WasRequestSuccessful))]
		[WireMember(2)]
		public ItemQueryResponseInfo Result { get; }

		/// <inheritdoc />
		public SMSG_ITEM_QUERY_SINGLE_RESPONSE_Payload(uint packedResponseId, ItemQueryResponseInfo result)
		{
			PackedResponseId = packedResponseId;
			Result = result;
		}

		protected SMSG_ITEM_QUERY_SINGLE_RESPONSE_Payload()
		{
			
		}
	}
}