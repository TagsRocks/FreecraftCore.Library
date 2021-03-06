﻿using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore.Serializer;

namespace FreecraftCore
{
	[WireDataContract]
	public sealed class CreatureQueryResponseInfo
	{
		[Encoding(EncodingType.ASCII)]
		[WireMember(1)]
		[KnownSize(4)]
		public string[] CreatureNames { get; }

		[Encoding(EncodingType.ASCII)]
		[WireMember(2)]
		public string AlternativeName { get; }

		[Encoding(EncodingType.ASCII)]
		[WireMember(3)]
		public string CursorName { get; }

		//TODO: Find the enum for this
		[WireMember(4)]
		public CreatureTypeFlags Flags { get; }

		[WireMember(5)]
		public CreatureType CreatureType { get; }

		[WireMember(6)]
		public CreatureFamily Family { get; }

		[WireMember(7)]
		public CreatureEliteType Classification { get; }

		[KnownSize(2)]
		[WireMember(8)]
		public int[] ProxyCreatureIds { get; }

		[KnownSize(4)]
		[WireMember(9)]
		public int[] CreatureDisplayIds { get; }

		[WireMember(10)]
		public float HealthMultiplier { get; }

		[WireMember(11)]
		public float EnergyMultiplier { get; }

		//TODO: What does this mean?
		[WireMember(12)]
		public bool IsLeader { get; }

		[KnownSize(6)]
		[WireMember(13)]
		public int[] QuestItemIds { get; }

		[WireMember(14)]
		public int CreatureMovementInfoId { get; }

		//TODO: Validate parameters
		//TODO: Ctor overloads
		/// <inheritdoc />
		public CreatureQueryResponseInfo(string[] creatureNames, string alternativeName, string cursorName, CreatureTypeFlags flags, CreatureType creatureType, CreatureFamily family, CreatureEliteType classification, int[] proxyCreatureIds, int[] creatureDisplayIds, float healthMultiplier, float energyMultiplier, bool isLeader, int[] questItemIds, int creatureMovementInfoId)
		{
			CreatureNames = creatureNames;
			AlternativeName = alternativeName;
			CursorName = cursorName;
			Flags = flags;
			CreatureType = creatureType;
			Family = family;
			Classification = classification;
			ProxyCreatureIds = proxyCreatureIds;
			CreatureDisplayIds = creatureDisplayIds;
			HealthMultiplier = healthMultiplier;
			EnergyMultiplier = energyMultiplier;
			IsLeader = isLeader;
			QuestItemIds = questItemIds;
			CreatureMovementInfoId = creatureMovementInfoId;
		}

		protected CreatureQueryResponseInfo()
		{
			
		}
	}
}
