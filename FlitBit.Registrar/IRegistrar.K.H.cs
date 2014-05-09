#region COPYRIGHT© 2009-2014 Phillip Clark. All rights reserved.
// For licensing information see License.txt (MIT style licensing).
#endregion

using System;
using System.Diagnostics.Contracts;

namespace FlitBit.Registrar
{		
	/// <summary>
	/// Strongly typed registrar; maintains registrations.
	/// </summary>
	/// <typeparam name="K">key type K</typeparam>
	/// <typeparam name="H">handback type H</typeparam>
	[ContractClass(typeof(CodeContracts.ContractForIRegistrar<,>))]
	public interface IRegistrar<K, H> : IRegistrar
	{
		/// <summary>
		/// Determines if a key has a registration.
		/// </summary>
		/// <param name="key">the key</param>
		/// <returns><em>true</em> if the registration is present; otherwise <em>false</em></returns>
		bool IsRegistered(K key);

		/// <summary>
		/// Tries to get the current registration for a key.
		/// </summary>
		/// <param name="key">the key</param>
		/// <param name="registration">reference to a variable where the registration
		/// will be returned upon success.</param>
		/// <returns><em>true</em> if the registration is present; otherwise <em>false</em></returns>
		bool TryGetRegistration(K key, out IRegistrationKey<K, H> registration);

		/// <summary>
		/// Tries to register a key and handback.
		/// </summary>
		/// <param name="key">the key</param>
		/// <param name="handback">the handback</param>
		/// <param name="registration">reference to a variable where the registration
		/// will be returned upon success.</param>
		/// <returns><em>true</em> if the registration is successful; otherwise <em>false</em></returns>
		bool TryRegister(K key, H handback, out IRegistrationKey<K, H> registration);

		/// <summary>
		/// Tries to replace the current registration.
		/// </summary>
		/// <param name="current">the current</param>
		/// <param name="key">the key</param>
		/// <param name="handback">the handback</param>
		/// <param name="registration">reference to a variable where the new
		/// registration will be returned upon success.</param>
		/// <returns><em>true</em> if the registration is present; otherwise <em>false</em></returns>
		bool TryReplaceRegistration(IRegistrationKey current, K key, H handback, out IRegistrationKey<K, H> registration);

		/// <summary>
		/// Event fired on any registration event.
		/// </summary>
		event EventHandler<RegistrationEventArgs<K, H>> OnAny;
		/// <summary>
		/// Event fired when new registrations occur.
		/// </summary>
		event EventHandler<RegistrationEventArgs<K, H>> OnNewRegistration;
	}

	namespace CodeContracts
	{
		/// <summary>
		/// CodeContracts Class for IRegistrar
		/// </summary>
		[ContractClassFor(typeof(IRegistrar<,>))]
		internal abstract class ContractForIRegistrar<K,H> : IRegistrar<K,H>
		{
			public bool IsRegistered(K key)
			{
				throw new NotImplementedException();
			}

			public bool TryGetRegistration(K key, out IRegistrationKey<K, H> registration)
			{

				throw new NotImplementedException();
			}

			public bool TryRegister(K key, H handback, out IRegistrationKey<K, H> registration)
			{
				throw new NotImplementedException();
			}

			public bool TryReplaceRegistration(IRegistrationKey current, K key, H handback, out IRegistrationKey<K, H> registration)
			{
				Contract.Requires<ArgumentNullException>(current != null);

				throw new NotImplementedException();
			}

			public event EventHandler<RegistrationEventArgs<K, H>> OnAny
			{
				add { }
				remove { }
			}

			public event EventHandler<RegistrationEventArgs<K, H>> OnNewRegistration
			{
				add { }
				remove { }
			}

			public bool CancelRegistration(IRegistrationKey key)
			{
				throw new NotImplementedException();
			}
		}
	}
}