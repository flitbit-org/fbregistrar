#region COPYRIGHT© 2009-2014 Phillip Clark. All rights reserved.
// For licensing information see License.txt (MIT style licensing).
#endregion

using System;
using System.Diagnostics.Contracts;

namespace FlitBit.Registrar
{		
	/// <summary>
	/// EventArgs for registration events.
	/// </summary>
	[Serializable]
	public class RegistrationEventArgs<TKey, THandback> : RegistrationEventArgs
	{
		/// <summary>
		/// Creates a new instance.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="kind"></param>
		public RegistrationEventArgs(IRegistrationKey<TKey, THandback> key, RegistrationEventKind kind)
			: base(key, kind)
		{
			Contract.Requires<ArgumentNullException>(key != null);

		}
		/// <summary>
		/// Gets the strongly typed registration key.
		/// </summary>
		public IRegistrationKey<TKey, THandback> Key
		{
			get
			{
				return (IRegistrationKey<TKey, THandback>)UntypedKey;
			}
		}
	}
	
}
