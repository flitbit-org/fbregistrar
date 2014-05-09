#region COPYRIGHT© 2009-2014 Phillip Clark. All rights reserved.
// For licensing information see License.txt (MIT style licensing).
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

namespace FlitBit.Registrar
{
	/// <summary>
	/// Basic implementation of the registrar.
	/// </summary>
	/// <typeparam name="TKey">key type K</typeparam>
	/// <typeparam name="THandback">handback type H</typeparam>
	public class Registrar<TKey, THandback> : IRegistrar<TKey, THandback>
	{
	  readonly Object _sync = new Object();
	  readonly Dictionary<TKey, IRegistrationKey> _registrations = new Dictionary<TKey, IRegistrationKey>();
		int _revision;

		[Serializable]
		class RegistrationKey : IRegistrationKey<TKey, THandback>
		{
			internal RegistrationKey(IRegistrar<TKey, THandback> registrar, TKey key, THandback handback)
			{
				this.Registrar = registrar;
				this.Key = key;
				this.Handback = handback;
			}

			public bool IsCanceled { get; private set; }

			public IRegistrar<TKey, THandback> Registrar { get; private set; }

			public TKey Key { get; private set; }

			public THandback Handback { get; private set; }

			public void Cancel()
			{									
				if (!IsCanceled)
				{
					Registrar.CancelRegistration(this);
					this.IsCanceled = true;
				}
			}

			public Type KeyType { get { return typeof(TKey); } }

			public Type HandbackType { get { return typeof(THandback); } }

			public IRegistrar UntypedRegistrar
			{
				get
				{
					Contract.Ensures(Contract.Result<FlitBit.Registrar.IRegistrar>() == this.Registrar);
					return Registrar;
				}
			}

			public object UntypedKey { get { return Key; } }

			public object UntypedHandback { get { return Handback; } }

			public event EventHandler<RegistrationEventArgs<TKey, THandback>> OnAny;

			internal void NotifyRegistrationEvent(RegistrationEventArgs<TKey, THandback> e)
			{
				if (OnAny != null)
				{
					OnAny(Registrar, e);
				}
			}
		}

		/// <summary>
		/// Determines if a key has a registration.
		/// </summary>
		/// <param name="key">the key</param>
		/// <returns><em>true</em> if the registration is present; otherwise <em>false</em></returns>
		public bool IsRegistered(TKey key)
		{
			lock (_sync)
			{
				return _registrations.ContainsKey(key);
			}
		}

		/// <summary>
		/// Tries to get the current registration for a key.
		/// </summary>
		/// <param name="key">the key</param>
		/// <param name="registration">reference to a variable where the registration
		/// will be returned upon success.</param>
		/// <returns><em>true</em> if the registration is present; otherwise <em>false</em></returns>
		public bool TryGetRegistration(TKey key, out IRegistrationKey<TKey, THandback> registration)
		{
			lock (_sync)
			{
				if (_registrations.ContainsKey(key))
				{
					registration = (IRegistrationKey<TKey, THandback>)_registrations[key];
					return true;
				}
			}
			registration = default(IRegistrationKey<TKey, THandback>);
			return false;
		}

		/// <summary>
		/// Tries to register a key and handback.
		/// </summary>
		/// <param name="key">the key</param>
		/// <param name="handback">the handback</param>
		/// <param name="registration">reference to a variable where the registration
		/// will be written upon success.</param>
		/// <returns><em>true</em> if the registration is successful; otherwise <em>false</em></returns>
		public bool TryRegister(TKey key, THandback handback, out IRegistrationKey<TKey, THandback> registration)
		{
			var result = false;
			registration = default(IRegistrationKey<TKey, THandback>);

			lock (_sync)
			{
				if (!_registrations.ContainsKey(key))
				{
					registration = new RegistrationKey(this, key, handback);
					_registrations.Add(key, registration);
					_revision++;
					result = true;
				}
			}
			if (result)
			{ // notify registration observers outside the lock...
				NotifyRegistrationEvent(new RegistrationEventArgs<TKey, THandback>(registration, RegistrationEventKind.Registration), null);
			}
			return result;
		}

		/// <summary>
		/// Tries to replace the current registration.
		/// </summary>
		/// <param name="current">the current</param>
		/// <param name="key">the key</param>
		/// <param name="handback">the handback</param>
		/// <param name="registration">reference to a variable where the new
		/// registration will be returned upon success.</param>
		/// <returns><em>true</em> if the registration is present; otherwise <em>false</em></returns>
		public bool TryReplaceRegistration(IRegistrationKey current, TKey key, THandback handback, out IRegistrationKey<TKey, THandback> registration)
		{
			var result = false;
			registration = default(IRegistrationKey<TKey, THandback>);

			var ours = current as RegistrationKey;
			if (ours != null && ReferenceEquals(this, ours.Registrar))
			{
				lock (_sync)
				{
					if (_registrations.ContainsKey(key))
					{
						var it = _registrations[key];
						if (ReferenceEquals(it, ours))
						{
							var evt = new RegistrationEventArgs<TKey, THandback>(ours, RegistrationEventKind.Replacing);
							NotifyRegistrationEvent(evt, ours);
							if (!evt.IsCanceled)
							{
								registration = new RegistrationKey(this, key, handback);
								_registrations[key] = registration;
								_revision++;
								NotifyRegistrationEvent(new RegistrationEventArgs<TKey, THandback>(ours, RegistrationEventKind.Replaced), ours);
								result = true;
							}
						}
					}
				}
			}
			if (result)
			{ // notify registration observers outside the lock...
				NotifyRegistrationEvent(new RegistrationEventArgs<TKey, THandback>(registration, RegistrationEventKind.Registration), null);
			}
			return result;
		}

		/// <summary>
		/// Cancels the registration given.
		/// </summary>
		/// <param name="registration">a registration</param>
		/// <returns><em>true</em> if the registration was canceled as a result of the call; otherwise <em>false</em>.</returns>
		public bool CancelRegistration(IRegistrationKey registration)
		{
			var ours = registration as RegistrationKey;
			if (ours != null && ReferenceEquals(this, ours.Registrar))
			{
				var key = ours.Key;
				lock (_sync)
				{
					if (_registrations.ContainsKey(key))
					{
						var current = _registrations[key];
						if (ReferenceEquals(current, registration))
						{
							var evt = new RegistrationEventArgs<TKey, THandback>(ours, RegistrationEventKind.Canceling);
							NotifyRegistrationEvent(evt, ours);
							if (!evt.IsCanceled)
							{
								_registrations.Remove(key);
								_revision++;
								NotifyRegistrationEvent(new RegistrationEventArgs<TKey, THandback>(ours, RegistrationEventKind.Canceled), ours);
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Event fired on any registration event.
		/// </summary>
		public event EventHandler<RegistrationEventArgs<TKey, THandback>> OnAny;
		/// <summary>
		/// Event fired when new registrations occur.
		/// </summary>
		public event EventHandler<RegistrationEventArgs<TKey, THandback>> OnNewRegistration;

		/// <summary>
		/// Allows subclasses to safely walk the registrations without
		/// blocking concurrent registrar operations.
		/// </summary>
		/// <param name="visitor">an action called for each regisration</param>
		protected void VisitEach(Action<IRegistrationKey<TKey, THandback>> visitor)
		{
			var snapshot = GetRegistrationSnapshot();
			foreach (var reg in snapshot)
			{
				visitor(reg);
			}
		}

		private void NotifyRegistrationEvent(RegistrationEventArgs<TKey, THandback> evt, RegistrationKey context)
		{
			Contract.Requires<ArgumentNullException>(evt != null);

			if (context != null)
			{
				context.NotifyRegistrationEvent(evt);
			}
			if (evt.Kind == RegistrationEventKind.Registration
				&& OnNewRegistration != null)
			{
				OnNewRegistration(this, evt);
			}
			if (OnAny != null)
			{
				OnAny(this, evt);
			}
		}
			
		int _snapshotRevision;
		IRegistrationKey<TKey, THandback>[] _snapshot = new IRegistrationKey<TKey, THandback>[0];
		private IEnumerable<IRegistrationKey<TKey, THandback>> GetRegistrationSnapshot()
		{
		  if (this._registrations == null)
		  {
		    return Enumerable.Empty<IRegistrationKey<TKey, THandback>>();
		  }

		  IRegistrationKey<TKey, THandback>[] result;
			lock (_sync)
			{
				if (_snapshotRevision != _revision)
				{
					_snapshot = _registrations.Values.Cast<IRegistrationKey<TKey, THandback>>().ToArray();
					_snapshotRevision = _revision;
				}
				result = _snapshot;
			}
			return result;
		}
	}

}
