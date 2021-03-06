﻿using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore
{
	[WireDataContract]
	[GamePayloadOperationCode(NetworkOperationCode.SMSG_ITEM_QUERY_SINGLE_RESPONSE)]
	public sealed class SMSG_ITEM_QUERY_SINGLE_RESPONSE_Payload : GamePacketPayload, IQueryResponsePayload<ItemQueryResponseInfo>
	{
		[WireMember(1)]
		private uint PackedResponseId { get; }

		/// <inheritdoc />
		public int QueryId => (int)(PackedResponseId & ~0x80000000);

		/// <summary>
		/// Indicates if the query was successful.
		/// </summary>
		public bool IsSuccessful => (PackedResponseId & 0x80000000) == 0;

		[Optional(nameof(IsSuccessful))]
		[WireMember(2)]
		public ItemQueryResponseInfo Result { get; }

		/// <inheritdoc />
		public SMSG_ITEM_QUERY_SINGLE_RESPONSE_Payload(int queryId, [NotNull] ItemQueryResponseInfo result)
		{
			//No bitwise math needed since success if 0x0
			PackedResponseId = (uint)queryId;
			Result = result ?? throw new ArgumentNullException(nameof(result));
		}

		/// <summary>
		/// Creates a failing response.
		/// </summary>
		/// <param name="queryId">The query id.</param>
		public SMSG_ITEM_QUERY_SINGLE_RESPONSE_Payload(uint queryId)
		{
			PackedResponseId = queryId & 0x80000000;
		}

		protected SMSG_ITEM_QUERY_SINGLE_RESPONSE_Payload()
		{
			
		}
	}
}
