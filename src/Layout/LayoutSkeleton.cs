#region Copyright & License
//
// Copyright 2001-2004 The Apache Software Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

using System;
using System.IO;

using log4net;
using log4net.Core;

namespace log4net.Layout
{
	/// <summary>
	/// Extend this abstract class to create your own log layout format.
	/// </summary>
	/// <remarks>
	/// <para>This is the base implementation of the <see cref="ILayout"/>
	/// interface. Most layout objects should extend this class.</para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public abstract class LayoutSkeleton : ILayout, IOptionHandler
	{
		#region Member Variables

		/// <summary>
		/// The header text
		/// </summary>
		/// <remarks>
		/// <para>See <see cref="Header"/> for more information.</para>
		/// </remarks>
		private string m_header = null;

		/// <summary>
		/// The footer text
		/// </summary>
		/// <remarks>
		/// <para>See <see cref="Footer"/> for more information.</para>
		/// </remarks>
		private string m_footer = null;

		#endregion

		#region Constructors

		/// <summary>
		/// Empty default constructor
		/// </summary>
		/// <remarks>
		/// Empty default constructor
		/// </remarks>
		protected LayoutSkeleton()
		{
		}

		#endregion

		#region Implementation of IOptionHandler

		/// <summary>
		/// Activate the options that were previously set with calls to option setters.
		/// </summary>
		/// <remarks>
		/// <para>
		/// This is part of the <see cref="IOptionHandler"/> delayed object
		/// activation scheme. The <see cref="ActivateOptions"/> method must 
		/// be called on this object after the configuration properties have
		/// been set. Until <see cref="ActivateOptions"/> is called this
		/// object is in an undefined state and must not be used. 
		/// </para>
		/// <para>
		/// If any of the configuration properties are modified then 
		/// <see cref="ActivateOptions"/> must be called again.
		/// </para>
 		/// <para>
 		/// This method must be implemented by the subclass.
 		/// </para>
		/// </remarks>
		abstract public void ActivateOptions();

		#endregion

		#region Implementation of ILayout

		/// <summary>
		/// Implement this method to create your own layout format.
		/// </summary>
		/// <param name="loggingEvent">The event to format</param>
		/// <returns>returns the formatted event</returns>
		/// <remarks>
		/// <para>This method is called by an appender to format
		/// the <paramref name="loggingEvent"/> as a string.</para>
		/// 
 		/// <para>This method must be implemented by the subclass.</para>
		/// </remarks>
		[Obsolete("Use Format(TextWriter,LoggingEvent)")]
		public string Format(LoggingEvent loggingEvent)
		{
			StringWriter writer = new StringWriter(System.Globalization.CultureInfo.InvariantCulture);
			this.Format(writer, loggingEvent);
			return writer.ToString();
		}

		/// <summary>
		/// Implement this method to create your own layout format.
		/// </summary>
		/// <param name="writer">The TextWriter to write the formatted event to</param>
		/// <param name="loggingEvent">The event to format</param>
		/// <remarks>
		/// <para>This method is called by an appender to format
		/// the <paramref name="loggingEvent"/> as text.</para>
		/// </remarks>
		abstract public void Format(TextWriter writer, LoggingEvent loggingEvent);

		/// <summary>
		/// The content type output by this layout. 
		/// </summary>
		/// <value>The content type is <c>"text/plain"</c></value>
		/// <remarks>
		/// <para>The content type output by this layout.</para>
		/// <para>This base class uses the value <c>"text/plain"</c>.
		/// To change this value a subclass must override this
		/// property.</para>
		/// </remarks>
		virtual public string ContentType
		{
			get { return "text/plain"; }
		}

		/// <summary>
		/// The header for the layout format.
		/// </summary>
		/// <value>the layout header</value>
		/// <remarks>
		/// <para>The Header text will be appended before any logging events
		/// are formatted and appended.</para>
		/// </remarks>
		virtual public string Header
		{
			get { return m_header; }
			set { m_header = value; }
		}

		/// <summary>
		/// The footer for the layout format.
		/// </summary>
		/// <value>the layout footer</value>
		/// <remarks>
		/// <para>The Footer text will be appended after all the logging events
		/// have been formatted and appended.</para>
		/// </remarks>
		virtual public string Footer
		{
			get { return m_footer; }
			set { m_footer = value; }
		}

		/// <summary>
		/// Flag indicating if this layout handles exceptions
		/// </summary>
		/// <value><c>false</c> if this layout handles exceptions</value>
		/// <remarks>
		/// <para>If this layout handles the exception object contained within
		/// <see cref="LoggingEvent"/>, then the layout should return
		/// <c>false</c>. Otherwise, if the layout ignores the exception
		/// object, then the layout should return <c>true</c>.</para>
		/// 
		/// <para>This method must be implemented by the subclass.</para>
		/// </remarks>
		abstract public	bool IgnoresException { get; }

		#endregion
	}
}