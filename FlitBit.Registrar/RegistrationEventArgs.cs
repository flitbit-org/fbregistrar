using System;
using System.Diagnostics.Contracts;

namespace FlitBit.Registrar
{									
	/// <summary>
	/// Base (untyped) EventArgs for registration events.
	/// </summary>
	[Serializable]
	public class RegistrationEventArgs : EventArgs
	{
		/// <summary>
		/// Creates a new instance.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="kind"></param>
		public RegistrationEventArgs(IRegistrationKey key, RegistrationEventKind kind)
		{
			Contract.Requires<ArgumentNullException>(key != null);

			this.UntypedKey = key;
			this.Kind = kind;
			this.CanCancel = (kind == RegistrationEventKind.Replacing || kind == RegistrationEventKind.Canceling);
		}
		/// <summary>
		/// Gets the untyped registration.
		/// </summary>
		public IRegistrationKey UntypedKey { get; private set; }
		/// <summary>
		/// Gets the kind of registration event.
		/// </summary>
		public RegistrationEventKind Kind { get; private set; }

		/// <summary>
		/// Indicates whether the event can be canceled.
		/// </summary>
		public bool CanCancel { get; private set; }

		/// <summary>
		/// Cancels the event.
		/// </summary>
		/// <returns></returns>
		public void Cancel()
		{
			if (CanCancel)
			{
				IsCanceled = true;
			}
		}

		/// <summary>
		/// Indicates whether the event has been canceled.
		/// </summary>
		public bool IsCanceled { get; private set; }
	}
}
