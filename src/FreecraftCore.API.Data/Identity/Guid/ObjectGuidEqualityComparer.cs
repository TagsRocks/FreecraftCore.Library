﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FreecraftCore
{
	/// <summary>
	/// Implements simple value-type style comparision semantics for the <see cref="ObjectGuid"/>
	/// type.
	/// </summary>
	public class ObjectGuidEqualityComparer<TType> : EqualityComparer<TType>
		where TType : ObjectGuid
	{
		/// <summary>
		/// Singleton instance of the <see cref="ObjectGuidEqualityComparer{TType}"/>.
		/// </summary>
		public static ObjectGuidEqualityComparer<TType> Instance { get; } = new ObjectGuidEqualityComparer<TType>();

		//Forces use of singleton
		protected ObjectGuidEqualityComparer()
		{

		}

		public override bool Equals(TType x, TType y)
		{
			if(x == null)
			{
				return y == null;
			}
			else if(y == null)
				return false;

			return x.RawGuidValue == y.RawGuidValue;
		}

		public override int GetHashCode(TType obj)
		{
			if(obj == null) throw new ArgumentNullException(nameof(obj));

			//Just return the ulong hash code.
			return obj.RawGuidValue.GetHashCode();
		}
	}
}
