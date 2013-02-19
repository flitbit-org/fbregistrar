using System;
using System.Diagnostics.Contracts;

namespace FlitBit.Registrar
{		
	/// <summary>
	/// EventArgs for registration events.
	/// </summary>
	[Serializable]
	public class RegistrationEventArgs<K, H> : RegistrationEventArgs
	{
		/// <summary>
		/// Creates a new instance.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="kind"></param>
		public RegistrationEventArgs(IRegistrationKey<K, H> key, RegistrationEventKind kind)
			: base(key, kind)
		{
			Contract.Requires<ArgumentNullException>(key != null);

		}
		/// <summary>
		/// Gets the strongly typed registration key.
		/// </summary>
		public IRegistrationKey<K, H> Key
		{
			get
			{
				return (IRegistrationKey<K, H>)UntypedKey;
			}
		}
	}
	
}
