using System;
using System.Diagnostics.Contracts;

namespace FlitBit.Registrar
{		
	/// <summary>
	/// Represents a strongly typed registration of key type K with
	/// a registrar.
	/// </summary>
	/// <typeparam name="K">key type K</typeparam>
	/// <typeparam name="H">handback type H</typeparam>
	[ContractClass(typeof(CodeContracts.ContractForIRegistrationKey<,>))]
	public interface IRegistrationKey<K, H> : IRegistrationKey
	{
		/// <summary>
		/// Gets the strongly typed registrar upon which this
		/// registration was made.
		/// </summary>
		IRegistrar<K, H> Registrar { get; }
		/// <summary>
		/// Gets the strongly typed key.
		/// </summary>
		K Key { get; }
		/// <summary>
		/// Gets the strongly typed handback.
		/// </summary>
		H Handback { get; }

		/// <summary>
		/// Gets and sets the registration event handler.
		/// </summary>
		event EventHandler<RegistrationEventArgs<K, H>> OnAny;
	}

	namespace CodeContracts
	{
		/// <summary>
		/// CodeContracts Class for IRegistrationKey&gt;,>
		/// </summary>
		[ContractClassFor(typeof(IRegistrationKey<,>))]
		internal abstract class ContractForIRegistrationKey<K,H> : IRegistrationKey<K,H>
		{
			public IRegistrar<K, H> Registrar
			{
				get
				{
					Contract.Ensures(Contract.Result<IRegistrar<K,H>>() != null);

					throw new NotImplementedException();
				}
			}

			public K Key
			{
				get { throw new NotImplementedException(); }
			}

			public H Handback
			{
				get { throw new NotImplementedException(); }
			}

			public event EventHandler<RegistrationEventArgs<K, H>> OnAny
			{
				add { }
				remove { }
			}

			public void Cancel()
			{
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
				get { throw new NotImplementedException(); }
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
