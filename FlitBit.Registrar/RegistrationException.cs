﻿#region COPYRIGHT© 2009-2014 Phillip Clark. All rights reserved.
// For licensing information see License.txt (MIT style licensing).
#endregion

using System;
using System.Runtime.Serialization;

namespace FlitBit.Registrar
{
	/// <summary>
	/// Base registration exception.
	/// </summary>
	[Serializable]
	public class RegistrationException : ApplicationException
	{
		/// <summary>
		/// Default constructor; creates a new instance.
		/// </summary>
		public RegistrationException()
		{
		}

		/// <summary>
		/// Creates a new instance using the error message given.
		/// </summary>
		/// <param name="errorMessage">An error message describing the exception.</param>
		public RegistrationException(string errorMessage)
			: base(errorMessage)
		{
		}

		/// <summary>
		/// Creates a new instance using the error message and cuase given.
		/// </summary>
		/// <param name="errorMessage">An error message describing the exception.</param>
		/// <param name="cause">An inner exception that caused this exception</param>
		public RegistrationException(string errorMessage, Exception cause)
			: base(errorMessage, cause)
		{
		}

		/// <summary>
		/// Used during serialization.
		/// </summary>
		/// <param name="si">SerializationInfo</param>
		/// <param name="sc">StreamingContext</param>
		protected RegistrationException(SerializationInfo si, StreamingContext sc)
			: base(si, sc)
		{
		}
	}
}
