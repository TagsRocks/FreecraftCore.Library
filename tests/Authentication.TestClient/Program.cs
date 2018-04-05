﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Common.Logging.Simple;
using FreecraftCore;
using FreecraftCore.API.Common;
using FreecraftCore.Crypto;
using FreecraftCore.Handlers;
using FreecraftCore.Packet.Auth;
using FreecraftCore.Serializer;
using GladNet;

namespace Authentication.TestClient
{
	class Program
	{
		public static SerializerService Serializer { get; private set; }

		public static int Count = 0;

		static async Task Main(string[] args)
		{
			Serializer = new SerializerService();

			typeof(AuthLogonChallengeRequest).Assembly
				.GetTypes()
				.Where(t => typeof(AuthenticationServerPayload).IsAssignableFrom(t) || typeof(AuthenticationClientPayload).IsAssignableFrom(t))
				.ToList()
				.ForEach(t =>
				{
					Serializer.RegisterType(t);
				});

			Serializer.Compile();

			List<IManagedNetworkClient<AuthenticationClientPayload, AuthenticationServerPayload>> clients
				 = new List<IManagedNetworkClient<AuthenticationClientPayload, AuthenticationServerPayload>>(1000);

			List<ConfiguredTaskAwaitable> tasks = new List<ConfiguredTaskAwaitable>(1000);

			Console.ReadKey();
			for(int i = 0; i < 1000; i++)
				clients.Add(BuildClient());

			for(int i = 0; i < 1000; i++)
			{
				int j = i;
				tasks.Add(Task.Run(() => AsyncMain(clients[j])).ConfigureAwait(false));
			}

			for(int i = 0; i < tasks.Count; i++)
				await tasks[i];

			Console.ReadKey();
		}

		private static IManagedNetworkClient<AuthenticationClientPayload, AuthenticationServerPayload> BuildClient()
		{
			//The auth server is encryptionless and headerless
			IManagedNetworkClient<AuthenticationClientPayload, AuthenticationServerPayload> client = new DotNetTcpClientNetworkClient()
				.AddHeaderlessNetworkMessageReading(new FreecraftCoreGladNetSerializerAdapter(Serializer))
				.For<AuthenticationServerPayload, AuthenticationClientPayload, IAuthenticationPayload>()
				.Build()
				.AsManaged(new ConsoleOutLogger("ConsoleLogger", LogLevel.All, true, false, false, null));

			return client;
		}

		private static async Task AsyncMain(IManagedNetworkClient<AuthenticationClientPayload, AuthenticationServerPayload> client)
		{
			try
			{
				/*AuthLogonChallengeRequest challengeRequest = new AuthLogonChallengeRequest(ProtocolVersion.ProtocolVersionTwo, GameType.WoW, ExpansionType.WrathOfTheLichKing, 3, 5,
					ClientBuild.Wotlk_3_3_5a, PlatformType.x86, OperatingSystemType.Win, LocaleType.enUS,
					IPAddress.Parse("127.0.0.1"), "Glader");

				while(true)
				{
					byte[] bytes = Serializer.Serialize(challengeRequest);
					AuthenticationClientPayload result = Serializer.Deserialize<AuthenticationClientPayload>(bytes);
					await Task.Delay(10);
				}*/

				if(!await client.ConnectAsync("127.0.0.1", 3724).ConfigureAwait(false))
					Console.WriteLine("Failed to connect");

				await client.SendMessage(new AuthLogonChallengeRequest(ProtocolVersion.ProtocolVersionTwo, GameType.WoW, ExpansionType.WrathOfTheLichKing, 3, 5,
						ClientBuild.Wotlk_3_3_5a, PlatformType.x86, OperatingSystemType.Win, LocaleType.enUS,
						IPAddress.Parse("127.0.0.1"), "Glader"))
					.ConfigureAwait(false);

				int count = Interlocked.Add(ref Count, 1);

				Console.WriteLine(count);
			}
			catch(Exception e)
			{
				Console.WriteLine($"Error: {e.Message}");
			}
			

			//var response = (await client.ReadMessageAsync()).Payload;

			//Console.WriteLine("Recieved payload");

			/*AuthenticationLogonChallengeResponseMessageHandler handler = new AuthenticationLogonChallengeResponseMessageHandler();

			if(response is AuthLogonChallengeResponse challengeResponse)
			{
				Console.WriteLine($"Response: Valid: {challengeResponse.isValid} Result: {challengeResponse.Result} SRP: {challengeResponse.Challenge}");

				await handler.HandleMessage(new DefaultPeerMessageContext<AuthenticationClientPayload>(client, client, new PayloadInterceptMessageSendService<AuthenticationClientPayload>(client, client)), challengeResponse);
			}
			else
				Console.WriteLine($"Recieved Payload of Type: {response.GetType().Name}");*/
		}
	}
}
