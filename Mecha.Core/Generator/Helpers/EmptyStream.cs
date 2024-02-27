using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mecha.Core.Generator.Helpers
{
    /// <summary>
    /// Empty stream (does nothing)
    /// </summary>
    /// <seealso cref="Stream"/>
    public class EmptyStream : Stream
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyStream"/> class.
        /// </summary>
        public EmptyStream()
        {
        }

        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the current stream
        /// supports reading.
        /// </summary>
        public override bool CanRead => true;

        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the current stream
        /// supports seeking.
        /// </summary>
        public override bool CanSeek => true;

        /// <summary>
        /// Gets a value that determines whether the current stream can time out.
        /// </summary>
        public override bool CanTimeout => true;

        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the current stream
        /// supports writing.
        /// </summary>
        public override bool CanWrite => true;

        /// <summary>
        /// When overridden in a derived class, gets the length in bytes of the stream.
        /// </summary>
        public override long Length => 10000;

        /// <summary>
        /// When overridden in a derived class, gets or sets the position within the current stream.
        /// </summary>
        public override long Position { get; set; }

        /// <summary>
        /// Gets or sets a value, in milliseconds, that determines how long the stream will attempt
        /// to read before timing out.
        /// </summary>
        public override int ReadTimeout { get; set; }

        /// <summary>
        /// Gets or sets a value, in milliseconds, that determines how long the stream will attempt
        /// to write before timing out.
        /// </summary>
        public override int WriteTimeout { get; set; }

        /// <summary>
        /// Begins an asynchronous read operation. (Consider using <see
        /// cref="M:System.IO.Stream.ReadAsync(System.Byte[],System.Int32,System.Int32)"/> instead.)
        /// </summary>
        /// <param name="buffer">The buffer to read the data into.</param>
        /// <param name="offset">
        /// The byte offset in <paramref name="buffer"/> at which to begin writing data read from
        /// the stream.
        /// </param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <param name="callback">
        /// An optional asynchronous callback, to be called when the read is complete.
        /// </param>
        /// <param name="state">
        /// A user-provided object that distinguishes this particular asynchronous read request from
        /// other requests.
        /// </param>
        /// <returns>
        /// An <see cref="T:System.IAsyncResult"/> that represents the asynchronous read, which
        /// could still be pending.
        /// </returns>
        public override IAsyncResult? BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state) => null;

        /// <summary>
        /// Begins an asynchronous write operation. (Consider using <see
        /// cref="M:System.IO.Stream.WriteAsync(System.Byte[],System.Int32,System.Int32)"/> instead.)
        /// </summary>
        /// <param name="buffer">The buffer to write data from.</param>
        /// <param name="offset">
        /// The byte offset in <paramref name="buffer"/> from which to begin writing.
        /// </param>
        /// <param name="count">The maximum number of bytes to write.</param>
        /// <param name="callback">
        /// An optional asynchronous callback, to be called when the write is complete.
        /// </param>
        /// <param name="state">
        /// A user-provided object that distinguishes this particular asynchronous write request
        /// from other requests.
        /// </param>
        /// <returns>
        /// An <see langword="IAsyncResult"/> that represents the asynchronous write, which could
        /// still be pending.
        /// </returns>
        public override IAsyncResult? BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state) => null;

        /// <summary>
        /// Closes the current stream and releases any resources (such as sockets and file handles)
        /// associated with the current stream. Instead of calling this method, ensure that the
        /// stream is properly disposed.
        /// </summary>
        public override void Close()
        {
        }

        /// <summary>
        /// Reads the bytes from the current stream and writes them to another stream, using a
        /// specified buffer size.
        /// </summary>
        /// <param name="destination">
        /// The stream to which the contents of the current stream will be copied.
        /// </param>
        /// <param name="bufferSize">
        /// The size of the buffer. This value must be greater than zero. The default size is 81920.
        /// </param>
        public override void CopyTo(Stream destination, int bufferSize)
        {
        }

        /// <summary>
        /// Asynchronously reads the bytes from the current stream and writes them to another
        /// stream, using a specified buffer size and cancellation token.
        /// </summary>
        /// <param name="destination">
        /// The stream to which the contents of the current stream will be copied.
        /// </param>
        /// <param name="bufferSize">
        /// The size, in bytes, of the buffer. This value must be greater than zero. The default
        /// size is 81920.
        /// </param>
        /// <param name="cancellationToken">
        /// The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None"/>.
        /// </param>
        /// <returns>A task that represents the asynchronous copy operation.</returns>
        public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken) => Task.CompletedTask;

        /// <summary>
        /// Waits for the pending asynchronous read to complete. (Consider using <see
        /// cref="M:System.IO.Stream.ReadAsync(System.Byte[],System.Int32,System.Int32)"/> instead.)
        /// </summary>
        /// <param name="asyncResult">The reference to the pending asynchronous request to finish.</param>
        /// <returns>
        /// The number of bytes read from the stream, between zero (0) and the number of bytes you
        /// requested. Streams return zero (0) only at the end of the stream, otherwise, they should
        /// block until at least one byte is available.
        /// </returns>
        public override int EndRead(IAsyncResult asyncResult) => 0;

        /// <summary>
        /// Ends an asynchronous write operation. (Consider using <see
        /// cref="M:System.IO.Stream.WriteAsync(System.Byte[],System.Int32,System.Int32)"/> instead.)
        /// </summary>
        /// <param name="asyncResult">A reference to the outstanding asynchronous I/O request.</param>
        public override void EndWrite(IAsyncResult asyncResult)
        {
        }

        /// <summary>
        /// When overridden in a derived class, clears all buffers for this stream and causes any
        /// buffered data to be written to the underlying device.
        /// </summary>
        public override void Flush()
        {
        }

        /// <summary>
        /// Asynchronously clears all buffers for this stream, causes any buffered data to be
        /// written to the underlying device, and monitors cancellation requests.
        /// </summary>
        /// <param name="cancellationToken">
        /// The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None"/>.
        /// </param>
        /// <returns>A task that represents the asynchronous flush operation.</returns>
        public override Task FlushAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        /// <summary>
        /// Obtains a lifetime service object to control the lifetime policy for this instance.
        /// </summary>
        /// <returns>
        /// An object of type <see cref="T:System.Runtime.Remoting.Lifetime.ILease"/> used to
        /// control the lifetime policy for this instance. This is the current lifetime service
        /// object for this instance if one exists; otherwise, a new lifetime service object
        /// initialized to the value of the <see
        /// cref="P:System.Runtime.Remoting.Lifetime.LifetimeServices.LeaseManagerPollTime"/> property.
        /// </returns>
        [Obsolete("This Remoting API is not supported and throws PlatformNotSupportedException.", DiagnosticId = "SYSLIB0010", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
        public override object? InitializeLifetimeService() => null;

        /// <summary>
        /// When overridden in a derived class, reads a sequence of bytes from the current stream
        /// and advances the position within the stream by the number of bytes read.
        /// </summary>
        /// <param name="buffer">
        /// An array of bytes. When this method returns, the buffer contains the specified byte
        /// array with the values between <paramref name="offset"/> and ( <paramref name="offset"/>
        /// + <paramref name="count"/> - 1) replaced by the bytes read from the current source.
        /// </param>
        /// <param name="offset">
        /// The zero-based byte offset in <paramref name="buffer"/> at which to begin storing the
        /// data read from the current stream.
        /// </param>
        /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
        /// <returns>
        /// The total number of bytes read into the buffer. This can be less than the number of
        /// bytes requested if that many bytes are not currently available, or zero (0) if the end
        /// of the stream has been reached.
        /// </returns>
        /// <exception cref="EndOfStreamException"></exception>
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (offset < 0)
                offset = 0;
            if (count < 0)
                count = 0;
            if (buffer is null || buffer.Length < offset + count || offset > buffer.Length)
                return 0;
            Array.Fill<byte>(buffer, 0, offset, count);
            return count;
        }

        /// <summary>
        /// When overridden in a derived class, reads a sequence of bytes from the current stream
        /// and advances the position within the stream by the number of bytes read.
        /// </summary>
        /// <param name="buffer">
        /// A region of memory. When this method returns, the contents of this region are replaced
        /// by the bytes read from the current source.
        /// </param>
        /// <returns>
        /// The total number of bytes read into the buffer. This can be less than the number of
        /// bytes allocated in the buffer if that many bytes are not currently available, or zero
        /// (0) if the end of the stream has been reached.
        /// </returns>
        public override int Read(Span<byte> buffer) => 0;

        /// <summary>
        /// Asynchronously reads a sequence of bytes from the current stream, advances the position
        /// within the stream by the number of bytes read, and monitors cancellation requests.
        /// </summary>
        /// <param name="buffer">The region of memory to write the data into.</param>
        /// <param name="cancellationToken">
        /// The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None"/>.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The value of its <see
        /// cref="P:System.Threading.Tasks.ValueTask`1.Result"/> property contains the total number
        /// of bytes read into the buffer. The result value can be less than the number of bytes
        /// allocated in the buffer if that many bytes are not currently available, or it can be 0
        /// (zero) if the end of the stream has been reached.
        /// </returns>
        public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default) => new(0);

        /// <summary>
        /// Asynchronously reads a sequence of bytes from the current stream, advances the position
        /// within the stream by the number of bytes read, and monitors cancellation requests.
        /// </summary>
        /// <param name="buffer">The buffer to write the data into.</param>
        /// <param name="offset">
        /// The byte offset in <paramref name="buffer"/> at which to begin writing data from the stream.
        /// </param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <param name="cancellationToken">
        /// The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None"/>.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The value of the parameter
        /// contains the total number of bytes read into the buffer. The result value can be less
        /// than the number of bytes requested if the number of bytes currently available is less
        /// than the requested number, or it can be 0 (zero) if the end of the stream has been reached.
        /// </returns>
        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) => Task.FromResult(0);

        /// <summary>
        /// Reads a byte from the stream and advances the position within the stream by one byte, or
        /// returns -1 if at the end of the stream.
        /// </summary>
        /// <returns>
        /// The unsigned byte cast to an <see langword="Int32"/>, or -1 if at the end of the stream.
        /// </returns>
        public override int ReadByte() => 0;

        /// <summary>
        /// When overridden in a derived class, sets the position within the current stream.
        /// </summary>
        /// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter.</param>
        /// <param name="origin">
        /// A value of type <see cref="T:System.IO.SeekOrigin"/> indicating the reference point used
        /// to obtain the new position.
        /// </param>
        /// <returns>The new position within the current stream.</returns>
        /// <exception cref="EndOfStreamException"></exception>
        public override long Seek(long offset, SeekOrigin origin) => offset;

        /// <summary>
        /// When overridden in a derived class, sets the length of the current stream.
        /// </summary>
        /// <param name="value">The desired length of the current stream in bytes.</param>
        public override void SetLength(long value)
        {
        }

        /// <summary>
        /// When overridden in a derived class, writes a sequence of bytes to the current stream and
        /// advances the current position within this stream by the number of bytes written.
        /// </summary>
        /// <param name="buffer">
        /// An array of bytes. This method copies <paramref name="count"/> bytes from <paramref
        /// name="buffer"/> to the current stream.
        /// </param>
        /// <param name="offset">
        /// The zero-based byte offset in <paramref name="buffer"/> at which to begin copying bytes
        /// to the current stream.
        /// </param>
        /// <param name="count">The number of bytes to be written to the current stream.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
        }

        /// <summary>
        /// When overridden in a derived class, writes a sequence of bytes to the current stream and
        /// advances the current position within this stream by the number of bytes written.
        /// </summary>
        /// <param name="buffer">
        /// A region of memory. This method copies the contents of this region to the current stream.
        /// </param>
        public override void Write(ReadOnlySpan<byte> buffer)
        {
        }

        /// <summary>
        /// Asynchronously writes a sequence of bytes to the current stream, advances the current
        /// position within this stream by the number of bytes written, and monitors cancellation requests.
        /// </summary>
        /// <param name="buffer">The region of memory to write data from.</param>
        /// <param name="cancellationToken">
        /// The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None"/>.
        /// </param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default) => new();

        /// <summary>
        /// Asynchronously writes a sequence of bytes to the current stream, advances the current
        /// position within this stream by the number of bytes written, and monitors cancellation requests.
        /// </summary>
        /// <param name="buffer">The buffer to write data from.</param>
        /// <param name="offset">
        /// The zero-based byte offset in <paramref name="buffer"/> from which to begin copying
        /// bytes to the stream.
        /// </param>
        /// <param name="count">The maximum number of bytes to write.</param>
        /// <param name="cancellationToken">
        /// The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None"/>.
        /// </param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) => Task.CompletedTask;

        /// <summary>
        /// Writes a byte to the current position in the stream and advances the position within the
        /// stream by one byte.
        /// </summary>
        /// <param name="value">The byte to write to the stream.</param>
        public override void WriteByte(byte value)
        {
        }

        /// <summary>
        /// Allocates a <see cref="T:System.Threading.WaitHandle"/> object.
        /// </summary>
        /// <returns>A reference to the allocated <see langword="WaitHandle"/>.</returns>
        [Obsolete("Reasons")]
        protected override WaitHandle? CreateWaitHandle() => null;
    }
}