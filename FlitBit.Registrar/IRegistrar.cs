#region COPYRIGHT© 2009-2014 Phillip Clark. All rights reserved.
// For licensing information see License.txt (MIT style licensing).
#endregion

using System;
using System.Diagnostics.Contracts;

namespace FlitBit.Registrar
{
	/// <summary>
	/// Base (untyped) registrar.
	/// </summary>
	[ContractClass(typeof(CodeContracts.ContractForIRegistrar))]
	public interface IRegistrar
	{
		/// <summary>
		/// Cancels the registration given.
		/// </summary>
		/// <param name="key">a registration</param>
		/// <returns><em>true</em> if the registration was canceled as a result of the call; otherwise <em>false</em>.</returns>
		bool CancelRegistration(IRegistrationKey key);
	}

	namespace CodeContracts
	{
		/// <summary>
		/// CodeContracts Class for IRegistrar
		/// </summary>
		[ContractClassFor(typeof(IRegistrar))]
		internal abstract class ContractForIRegistrar : IRegistrar
		{
			public bool CancelRegistration(IRegistrationKey key)
			{
				Contract.Requires<ArgumentNullException>(key != null);

				throw new NotImplementedException();
			}
		}
	}
}