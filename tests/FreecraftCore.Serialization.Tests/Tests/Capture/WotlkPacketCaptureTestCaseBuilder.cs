﻿using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;

namespace FreecraftCore
{
	public sealed class WotlkPacketCaptureTestCaseBuilder : PacketCaptureTestCaseCaptureBuilder
	{
		/// <inheritdoc />
		public override ExpansionType Expac { get; } = ExpansionType.WrathOfTheLichKing;

		public WotlkPacketCaptureTestCaseBuilder()
		{
			
		}

		/// <inheritdoc />
		public override IEnumerable<Type> BuildSerializableTypes()
		{
			//Then we want to register DTOs for unknown
			return GamePacketStubMetadataMarker.GamePacketPayloadStubTypes
				.Where(t => GamePacketMetadataMarker.UnimplementedOperationCodes.Value.Contains(AttributesUtil.GetAttribute<GamePayloadOperationCodeAttribute>((Type)t).OperationCode))
				.Concat(GamePacketMetadataMarker.SerializableTypes)
				.ToArray();
		}
	}
}