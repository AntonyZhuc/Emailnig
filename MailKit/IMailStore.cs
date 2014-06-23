//
// IMailStore.cs
//
// Author: Jeffrey Stedfast <jeff@xamarin.com>
//
// Copyright (c) 2013-2014 Xamarin Inc. (www.xamarin.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

using System;
using System.Threading;
using System.Threading.Tasks;

namespace MailKit {
	/// <summary>
	/// An interface for retreiving messages from a message store such as IMAP.
	/// </summary>
	/// <remarks>
	/// Implemented by <see cref="MailKit.Net.Imap.ImapClient"/>.
	/// </remarks>
	public interface IMailStore : IMailService
	{
		/// <summary>
		/// Get the personal namespaces.
		/// </summary>
		/// <remarks>
		/// The personal folder namespaces contain a user's personal mailbox folders.
		/// </remarks>
		/// <value>The personal namespaces.</value>
		FolderNamespaceCollection PersonalNamespaces { get; }

		/// <summary>
		/// Get the shared namespaces.
		/// </summary>
		/// <remarks>
		/// The shared folder namespaces contain mailbox folders that are shared with the user.
		/// </remarks>
		/// <value>The shared namespaces.</value>
		FolderNamespaceCollection SharedNamespaces { get; }

		/// <summary>
		/// Get the other namespaces.
		/// </summary>
		/// <remarks>
		/// The other folder namespaces contain other mailbox folders.
		/// </remarks>
		/// <value>The other namespaces.</value>
		FolderNamespaceCollection OtherNamespaces { get; }

		/// <summary>
		/// Get the Inbox folder.
		/// </summary>
		/// <remarks>
		/// The Inbox folder is the default folder and is typically the folder
		/// where all new messages are delivered.
		/// </remarks>
		/// <value>The Inbox folder.</value>
		IMailFolder Inbox { get; }

		/// <summary>
		/// Enable the quick resynchronization feature.
		/// </summary>
		/// <remarks>
		/// <para>Enables quick resynchronization when a folder is opened using the
		/// <see cref="IMailFolder.Open(FolderAccess,UniqueId,ulong,UniqueId[],System.Threading.CancellationToken)"/>
		/// method.</para>
		/// <para>If this feature is enabled, the <see cref="IMailFolder.MessageExpunged"/> event
		/// is replaced with the <see cref="IMailFolder.MessagesVanished"/> event.</para>
		/// <para>This method needs to be called immediately after
		/// <see cref="IMailService.Authenticate(System.Net.ICredentials,System.Threading.CancellationToken)"/>,
		/// before the opening of any folders.</para>
		/// </remarks>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <exception cref="System.ObjectDisposedException">
		/// The <see cref="MailStore"/> has been disposed.
		/// </exception>
		/// <exception cref="System.InvalidOperationException">
		/// The <see cref="MailStore"/> is not connected, not authenticated, or a folder has been selected.
		/// </exception>
		/// <exception cref="System.NotSupportedException">
		/// The mail store does not support quick resynchronization.
		/// </exception>
		/// <exception cref="System.OperationCanceledException">
		/// The operation was canceled via the cancellation token.
		/// </exception>
		/// <exception cref="System.IO.IOException">
		/// An I/O error occurred.
		/// </exception>
		/// <exception cref="ProtocolException">
		/// A protocol error occurred.
		/// </exception>
		void EnableQuickResync (CancellationToken cancellationToken = default (CancellationToken));

		/// <summary>
		/// Asynchronously enable the quick resynchronization feature.
		/// </summary>
		/// <remarks>
		/// <para>Enables quick resynchronization when a folder is opened using the
		/// <see cref="IMailFolder.Open(FolderAccess,UniqueId,ulong,UniqueId[],System.Threading.CancellationToken)"/>
		/// method.</para>
		/// <para>If this feature is enabled, the <see cref="IMailFolder.MessageExpunged"/> event
		/// is replaced with the <see cref="IMailFolder.MessagesVanished"/> event.</para>
		/// <para>This method needs to be called immediately after
		/// <see cref="IMailService.Authenticate(System.Net.ICredentials,System.Threading.CancellationToken)"/>,
		/// before the opening of any folders.</para>
		/// </remarks>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <exception cref="System.ObjectDisposedException">
		/// The <see cref="MailStore"/> has been disposed.
		/// </exception>
		/// <exception cref="System.InvalidOperationException">
		/// The <see cref="MailStore"/> is not connected, not authenticated, or a folder has been selected.
		/// </exception>
		/// <exception cref="System.NotSupportedException">
		/// The mail store does not support quick resynchronization.
		/// </exception>
		/// <exception cref="System.OperationCanceledException">
		/// The operation was canceled via the cancellation token.
		/// </exception>
		/// <exception cref="System.IO.IOException">
		/// An I/O error occurred.
		/// </exception>
		/// <exception cref="ProtocolException">
		/// A protocol error occurred.
		/// </exception>
		Task EnableQuickResyncAsync (CancellationToken cancellationToken = default (CancellationToken));

		/// <summary>
		/// Get the specified special folder.
		/// </summary>
		/// <remarks>
		/// Not all message stores support the concept of special folders,
		/// so this method may return <c>null</c>.
		/// </remarks>
		/// <returns>The folder if available; otherwise <c>null</c>.</returns>
		/// <param name="folder">The type of special folder.</param>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// <paramref name="folder"/> is out of range.
		/// </exception>
		IMailFolder GetFolder (SpecialFolder folder);

		/// <summary>
		/// Get the folder for the specified namespace.
		/// </summary>
		/// <remarks>
		/// The main reason to get the toplevel folder in a namespace is
		/// to list its child folders.
		/// </remarks>
		/// <returns>The folder.</returns>
		/// <param name="namespace">The namespace.</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="namespace"/> is <c>null</c>.
		/// </exception>
		/// <exception cref="FolderNotFoundException">
		/// The folder could not be found.
		/// </exception>
		IMailFolder GetFolder (FolderNamespace @namespace);

		/// <summary>
		/// Get the folder for the specified path.
		/// </summary>
		/// <remarks>
		/// Gets the folder for the specified path.
		/// </remarks>
		/// <returns>The folder.</returns>
		/// <param name="path">The folder path.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="path"/> is <c>null</c>.
		/// </exception>
		/// <exception cref="System.OperationCanceledException">
		/// The operation was canceled via the cancellation token.
		/// </exception>
		/// <exception cref="FolderNotFoundException">
		/// The folder could not be found.
		/// </exception>
		IMailFolder GetFolder (string path, CancellationToken cancellationToken = default (CancellationToken));

		/// <summary>
		/// Asynchronously get the folder for the specified path.
		/// </summary>
		/// <remarks>
		/// Asynchronously gets the folder for the specified path.
		/// </remarks>
		/// <returns>The folder.</returns>
		/// <param name="path">The folder path.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <exception cref="System.ArgumentNullException">
		/// <paramref name="path"/> is <c>null</c>.
		/// </exception>
		/// <exception cref="System.OperationCanceledException">
		/// The operation was canceled via the cancellation token.
		/// </exception>
		/// <exception cref="FolderNotFoundException">
		/// The folder could not be found.
		/// </exception>
		Task<IMailFolder> GetFolderAsync (string path, CancellationToken cancellationToken = default (CancellationToken));

		/// <summary>
		/// Occurs when a remote message store receives an alert message from the server.
		/// </summary>
		/// <remarks>
		/// Some implementations, such as <see cref="MailKit.Net.Imap.ImapClient"/>,
		/// will emit Alert events when they receive alert messages from the server.
		/// </remarks>
		event EventHandler<AlertEventArgs> Alert;
	}
}
