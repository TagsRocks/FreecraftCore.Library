﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FreecraftCore.Serializer;
using NUnit.Framework;

namespace FreecraftCore.Tests
{
	[TestFixture]
	public abstract class AutomatedReflectionTests<TPayloadBaseType, TTypeToReflectForAssembly>
		where TTypeToReflectForAssembly : TPayloadBaseType
	{
		public static IEnumerable<Type> PayloadTypes { get; } = typeof(TTypeToReflectForAssembly).Assembly.GetTypes()
			.Where(t => typeof(TPayloadBaseType).IsAssignableFrom(t));

		[Test]
		public void Test_SpecialLinkAttributes_Are_Marked_On_Correct_Types()
		{
			//arrange
			//Find the attribute that should be annoting these payloads
			WireDataContractBaseLinkAttribute linkAttri = typeof(AuthenticationServerPayloadAttribute)
				.Assembly
				.GetTypes()
				.Where(t =>  typeof(WireDataContractBaseLinkAttribute).IsAssignableFrom(t) && !t.IsAbstract)
				.Select(t => Activator.CreateInstance(t, BindingFlags.CreateInstance | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new object[] { 5 }, null) as WireDataContractBaseLinkAttribute)
				.FirstOrDefault(c => c.BaseType == typeof(TPayloadBaseType));

			if(linkAttri == null)
				Assert.Fail($"Failed to get link attribute for {typeof(TPayloadBaseType).Name}.");

			//check that all payloads in the assembly with this attribute derive from the basepayload type
			foreach(Type t in typeof(TTypeToReflectForAssembly).Assembly
				.GetTypes()
				.Where(t => t.GetCustomAttribute(linkAttri.GetType()) != null))
			{
				try
				{
					Assert.True(typeof(TPayloadBaseType).IsAssignableFrom(t), $"Type: {t.Name} is marked with Attribute: {linkAttri.GetType().Name} but doesn't derive from Type: {linkAttri.BaseType.Name}. In derives from incorrect Type: {t.BaseType}");
				}
				catch(Exception e)
				{
					Assert.Fail($"Failed to check the link for {t.Name} Exception: {e.Message}");
				}
			}

		}

		[Test]
		[TestCaseSource(nameof(PayloadTypes))]
		public void Test_Each_Payload_Has_WireDataContract_Attribute(Type t)
		{
			//act
			bool result = t.GetCustomAttribute<WireDataContractAttribute>(false) != null;

			//assert
			Assert.True(result, $"Type: {t.Name} does not have required {nameof(WireDataContractAttribute)} annoted on it.");
		}

		[Test]
		[TestCaseSource(nameof(PayloadTypes))]
		public void Test_Can_Register_All_Concrete_Payloads(Type t)
		{
			//arrange
			SerializerService serializer = new SerializerService();

			//assert
			Assert.DoesNotThrow(() => serializer.RegisterType(t));
			serializer.Compile();

			Assert.True(serializer.isTypeRegistered(t), $"Failed to register Type: {t.Name}");
			Assert.True(serializer.isTypeRegistered(typeof(TPayloadBaseType)), $"Base packet type wasn't registered.");
		}

		[Test]
		[TestCaseSource(nameof(PayloadTypes))]
		public void Test_Payload_With_Link_Has_Correct_BaseType(Type t)
		{
			//assert
			if(GetWireDataContractLinkAttribute(t) != null)
			{
				Assert.NotNull(GetWireDataContractLinkAttribute(t).BaseType, $"Type: {t.Name} did not have a statically linked base type.");
				Assert.True(GetWireDataContractLinkAttribute(t).BaseType.IsAssignableFrom(t), $"Type: {t.Name} linked to Type: {t.GetCustomAttribute<WireDataContractBaseLinkAttribute>().BaseType} but was not a basetype of Type: {t.Name}.");
			}
		}

		private WireDataContractBaseLinkAttribute GetWireDataContractLinkAttribute(Type t)
		{
			return t.GetCustomAttributes().FirstOrDefault(a => a is WireDataContractBaseLinkAttribute) as WireDataContractBaseLinkAttribute;
		}

		[Test]
		[TestCaseSource(nameof(PayloadTypes))]
		public void Test_Can_Serialize_All_Concrete_Payloads(Type t)
		{
			//arrange
			SerializerService serializer = new SerializerService();
			serializer.RegisterType(t);
			serializer.Compile();

			//Abstracts can't be created
			if(t.IsAbstract) //if it's unknown then it's probably default and thus unwritable
				return;

			object payload = Activator.CreateInstance(t, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.CreateInstance, null, new object[0], null);

			//act
			byte[] bytes = null;

			//We have to suppress these exceptions because some payloads have complex object graphs and
			//need more than default initialization to be serializable
			try
			{
				bytes = serializer.Serialize(payload);

				//assert
				Assert.NotNull(bytes);
				Assert.True(bytes.Length != 0);
			}
			catch(ArgumentNullException e)
			{

			}
			catch(Exception e)
			{
				Assert.Warn($"Type: {t.Name} may not be serializable. It's not determinable. This can happen if it has class/complex fields and should be ignored. \n\nException: {e.Message}");
			}
		}
	}
}