using System;
using System.Diagnostics.Contracts;

namespace FlitBit.Registrar
{
	/// <summary>
	/// Base (untyped) registration key.
	/// </summary>
	[ContractClass(typeof(CodeContracts.ContractForIRegistrationKey))]
	public interface IRegistrationKey
	{
		/// <summary>
		/// Cancels a registration.
		/// </summary>
		void Cancel();
		/// <summary>
		/// Indicates whether the registration has been canceled.
		/// </summary>
		bool IsCanceled { get; }

		/// <summary>
		/// Gets the key's type.
		/// </summary>
		Type KeyType { get; }
		/// <summary>
		/// Gets the handback's type.
		/// </summary>
		Type HandbackType { get; }

		/// <summary>
		/// Gets a reference (untyped) to the registrar
		/// upon which this registration was made.
		/// </summary>
		IRegistrar UntypedRegistrar { get; }

		/// <summary>
		/// Gets a reference (untyped) to the key.
		/// </summary>
		object UntypedKey { get; }

		/// <summary>
		/// Gets a reference (untyped) to the handback.
		/// </summary>
		object UntypedHandback { get; }
	}

	namespace CodeContracts
	{
		/// <summary>
		/// CodeContracts Class for IRegistrationKey
		/// </summary>
		[ContractClassFor(typeof(IRegistrationKey))]
		internal abstract class ContractForIRegistrationKey : IRegistrationKey
		{								
			public void Cancel()
			{
				Contract.Ensures(this.IsCanceled == true);

				throw new NotImplementedException();
			}

			public bool IsCanceled
			{
				get { throw new NotImplementedException(); }
			}

			public Type KeyType
			{
				get { throw new NotImplementedException(); }
			}

			public Type HandbackType
			{
				get { throw new NotImplementedException(); }
			}

			public IRegistrar UntypedRegistrar
			{
				get
				{
					Contract.Ensures(Contract.Result<IRegistrar>() != null);

					throw new NotImplementedException();
				}
			}

			public object UntypedKey
			{
				get { throw new NotImplementedException(); }
			}

			public object UntypedHandback
			{
				get { throw new NotImplementedException(); }
			}
		}
	}
}
