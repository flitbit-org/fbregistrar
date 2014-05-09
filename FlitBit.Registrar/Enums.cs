#region COPYRIGHT© 2009-2014 Phillip Clark. All rights reserved.
// For licensing information see License.txt (MIT style licensing).
#endregion

namespace FlitBit.Registrar
{
	/// <summary>
	/// Registration event kinds.
	/// </summary>
	public enum RegistrationEventKind
	{
		/// <summary>
		/// None.
		/// </summary>
		None = 0,
		/// <summary>
		/// Indicates a new registration occurred.
		/// </summary>
		Registration = 1,
		/// <summary>
		/// Indicates the registrar is replacing the registration.
		/// </summary>
		Replacing = 2,
		/// <summary>
		/// Indicates the registrar has replaced the registration.
		/// </summary>
		Replaced = 3,
		/// <summary>
		/// Indicates the registration is being canceled.
		/// </summary>
		Canceling = 4,
		/// <summary>
		/// Indicates the registration is canceled.
		/// </summary>
		Canceled = 5,
	}
}
