#region COPYRIGHT© 2009-2014 Phillip Clark. All rights reserved.
// For licensing information see License.txt (MIT style licensing).
#endregion

using System;
using System.Diagnostics.Contracts;

namespace FlitBit.Registrar
{		
	/// <summary>
	/// Represents a strongly typed registration of key type K with
	/// a registrar.
	/// </summary>
	/// <typeparam name="TKey">key type K</typeparam>
	/// <typeparam name="THandback">handback type H</typeparam>
	[ContractClass(typeof(CodeContracts.ContractForIRegistrationKey<,>))]
	public interface IRegistrationKey<TKey, THandback> : IRegistrationKey
	{
		/// <summary>
		/// Gets the strongly typed registrar upon which this
		/// registration was made.
		/// </summary>
		IRegistrar<TKey, THandback> Registrar { get; }
		/// <summary>
		/// Gets the strongly typed key.
		/// </summary>
		TKey Key { get; }
		/// <summary>
		/// Gets the strongly typed handback.
		/// </summary>
		THandback Handback { get; }

		/// <summary>
		/// Gets and sets the registration event handler.
		/// </summary>
		event EventHandler<RegistrationEventArgs<TKey, THandback>> OnAny;
	}

	namespace CodeContracts
	{
		/// <summary>
		/// CodeContracts Class for IRegistrationKey&gt;,>
		/// </summary>
		[ContractClassFor(typeof(IRegistrationKey<,>))]
		internal abstract class ContractForIRegistrationKey<TKey,THandback> : IRegistrationKey<TKey,THandback>
		{
			public IRegistrar<TKey, THandback> Registrar
			{
				get
				{
					Contract.Ensures(Contract.Result<IRegistrar<TKey,THandback>>() != null);

					throw new NotImplementedException();
				}
			}

			public TKey Key
			{
				get { throw new NotImplementedException(); }
			}

			public THandback Handback
			{
				get { throw new NotImplementedException(); }
			}

			public event EventHandler<RegistrationEventArgs<TKey, THandback>> OnAny
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
