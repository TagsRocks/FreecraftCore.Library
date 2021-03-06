﻿using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace FreecraftCore
{
	[WireDataContract]
	[GamePayloadOperationCode(NetworkOperationCode.SMSG_DESTROY_OBJECT)]
	public sealed class SMSG_DESTROY_OBJECT_Payload : GamePacketPayload
	{
		[WireMember(1)]
		public ObjectGuid DestroyedObject { get; }

		[WireMember(2)]
		public bool IsForDeath { get; }

		/// <inheritdoc />
		public SMSG_DESTROY_OBJECT_Payload([NotNull] ObjectGuid destroyedObject, bool isForDeath)
		{
			DestroyedObject = destroyedObject ?? throw new ArgumentNullException(nameof(destroyedObject));
			IsForDeath = isForDeath;
		}

		protected SMSG_DESTROY_OBJECT_Payload()
		{
			
		}
	}
}
