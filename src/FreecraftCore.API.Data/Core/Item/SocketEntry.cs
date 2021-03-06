﻿using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore.Serializer;

namespace FreecraftCore
{
	[WireDataContract]
	public sealed class SocketEntry
	{
		[WireMember(1)]
		public SocketColor Color { get; }

		[WireMember(2)]
		public uint SocketContent { get; }

		/// <inheritdoc />
		public SocketEntry(SocketColor color, uint socketContent)
		{
			Color = color;
			SocketContent = socketContent;
		}

		protected SocketEntry()
		{
			
		}
	}
}
