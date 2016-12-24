﻿using FreecraftCore.API.Common;
using FreecraftCore.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreecraftCore.Packet.Auth
{
	/// <summary>
	/// Response payload contains the realm list.
	/// Response to the request <see cref="AuthRealmListRequest"/>.
	/// </summary>
	[WireDataContract]
	[AuthenticationPayload(Common.AuthOperationCode.REALM_LIST)]
	public class AuthRealmListResponse : IAuthenticationPayload
	{
		/// <summary>
		/// The size of the payload.
		/// </summary>
		[WireMember(1)]
		private ushort payloadSize { get; set; }

		//Unknown field. Trinitycore always sends 0.
		//I think EmberEmu said it's expected as 0 in the client? Can't recall
		[WireMember(2)]
		private int unknownOne { get; set; }

		/// <summary>
		/// Realm information.
		/// </summary>
		[SendSize(SendSizeAttribute.SizeType.UShort)] //in 2.x and 3.x this is ushort but in 1.12.1 it's a uint32
		[WireMember(3)]
		private RealmInfo[] realms { get; set; }

		/// <summary>
		/// Collection of realm's.
		/// </summary>
		public IEnumerable<RealmInfo> Realms { get { return realms; } }

		//2.x and 3.x clients send byte 16 and 0
		//1.12.1 clients send 0 and 2.
		//EmberEmu has no information on what it is.
		[WireMember(4)]
		private short unknownTwo { get; set; }

		public AuthRealmListResponse()
		{

		}
	}
}
