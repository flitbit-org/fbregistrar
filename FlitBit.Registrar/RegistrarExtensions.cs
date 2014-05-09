#region COPYRIGHT© 2009-2014 Phillip Clark. All rights reserved.
// For licensing information see License.txt (MIT style licensing).
#endregion

using System;
using System.Diagnostics.Contracts;

namespace FlitBit.Registrar
{
	/// <summary>
	/// Contains utility methods for registrars.
	/// </summary>
	public static class RegistrarExtensions
	{
		/// <summary>
		/// Adds a registration for the key and handback given.
		/// </summary>
		/// <typeparam name="TKey">key type K</typeparam>
		/// <typeparam name="THandback">handback type H</typeparam>
		/// <param name="registrar">the registrar</param>
		/// <param name="key">the key</param>
		/// <param name="handback">the handback</param>
		public static void Register<TKey, THandback>(this IRegistrar<TKey, THandback> registrar, TKey key, THandback handback)
		{
			Contract.Requires<ArgumentNullException>(registrar != null);

			IRegistrationKey<TKey, THandback> reg;
			if (!registrar.TryRegister(key, handback, out reg))
				throw new RegistrationException(String.Concat("Already registered: ", Convert.ToString(key)));
		}

		/// <summary>
		/// Unregisters the current registration associated with the key given.
		/// </summary>
		/// <typeparam name="TKey">key type K</typeparam>
		/// <typeparam name="THandback">handback type H</typeparam>
		/// <param name="registrar">the registrar</param>
		/// <param name="key">the key</param>
		public static void Unregister<TKey, THandback>(this IRegistrar<TKey, THandback> registrar, TKey key)
		{
			Contract.Requires<ArgumentNullException>(registrar != null);

			IRegistrationKey<TKey, THandback> reg;
			if (registrar.TryGetRegistration(key, out reg))
			{
				if (!registrar.CancelRegistration(reg))
					throw new RegistrationException(String.Concat("Unregister was canceled: ", Convert.ToString(key)));
			}
		}

	}
}
