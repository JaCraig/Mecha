using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mecha.Core.Generator.Helpers
{
    /// <summary>
    /// Faulty file stream
    /// </summary>
    /// <seealso cref="System.IO.FileStream"/>
    public class FaultyFileStream : FileStream
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FaultyFileStream"/> class.
        /// </summary>
        public FaultyFileStream()
            : base($"./Mecha/mock-{Guid.NewGuid()}.txt", FileMode.OpenOrCreate)
        {
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="FaultyFileStream"/> class.
        /// </summary>
        ~FaultyFileStream()
        {
            foreach (FileCurator.Interfaces.IFile File in new FileCurator.DirectoryInfo("./Mecha/").EnumerateFiles())
            {
                try
                {
                    _ = File.Delete();
                }
                catch { }
            }
        }

        /// <summary>
        /// Begins an asynchronous read operation. Consider using <see
        /// cref="M:System.IO.FileStream.ReadAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)"/> instead.
        /// </summary>
        /// <param name="array">The buffer to read data into.</param>
        /// <param name="offset">
        /// The byte offset in <paramref name="array"/> at which to begin reading.
        /// </param>
        /// <param name="numBytes">The maximum number of bytes to read.</param>
        /// <param name="callback">
        /// The method to be called when the asynchronous read operation is completed.
        /// </param>
        /// <param name="state">
        /// A user-provided object that distinguishes this particular asynchronous read request from
        /// other requests.
        /// </param>
        /// <returns>An object that references the asynchronous read.</returns>
        /// <exception cref="FileNotFoundException"></exception>
        public override IAsyncResult BeginRead(byte[] array, int offset, int numBytes, AsyncCallback callback, object state) => throw new FileNotFoundException();

        /// <summary>
        /// Begins an asynchronous write operation. Consider using <see
        /// cref="M:System.IO.FileStream.WriteAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)"/> instead.
        /// </summary>
        /// <param name="array">The buffer containing data to write to the current stream.</param>
        /// <param name="offset">
        /// The zero-based byte offset in <paramref name="array"/> at which to begin copying bytes
        /// to the current stream.
        /// </param>
        /// <param name="numBytes">The maximum number of bytes to write.</param>
        /// <param name="callback">
        /// The method to be called when the asynchronous write operation is completed.
        /// </param>
        /// <param name="state">
        /// A user-provided object that distinguishes this particular asynchronous write request
        /// from other requests.
        /// </param>
        /// <returns>An object that references the asynchronous write.</returns>
        /// <exception cref="EndOfStreamException"></exception>
        public override IAsyncResult BeginWrite(byte[] array, int offset, int numBytes, AsyncCallback callback, object state) => throw new FileNotFoundException();

        /// <summary>
        /// Closes the current stream and releases any resources (such as sockets and file handles)
        /// associated with the current stream. Instead of calling this method, ensure that the
        /// stream is properly disposed.
        /// </summary>
        /// <exception cref="EndOfStreamException"></exception>
        public override void Close() => throw new FileNotFoundException();

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
        /// <exception cref="EndOfStreamException"></exception>
        public override void CopyTo(Stream destination, int bufferSize) => throw new FileNotFoundException();

        /// <summary>
        /// Copies to asynchronous.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="EndOfStreamException"></exception>
        public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken) => throw new FileNotFoundException();

        /// <summary>
        /// Waits for the pending asynchronous read operation to complete. (Consider using <see
        /// cref="M:System.IO.FileStream.ReadAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)"/> instead.)
        /// </summary>
        /// <param name="asyncResult">
        /// The reference to the pending asynchronous request to wait for.
        /// </param>
        /// <returns>
        /// The number of bytes read from the stream, between 0 and the number of bytes you
        /// requested. Streams only return 0 at the end of the stream, otherwise, they should block
        /// until at least 1 byte is available.
        /// </returns>
        /// <exception cref="FileNotFoundException"></exception>
        public override int EndRead(IAsyncResult asyncResult) => throw new FileNotFoundException();

        /// <summary>
        /// Ends an asynchronous write operation and blocks until the I/O operation is complete.
        /// (Consider using <see
        /// cref="M:System.IO.FileStream.WriteAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)"/> instead.)
        /// </summary>
        /// <param name="asyncResult">The pending asynchronous I/O request.</param>
        /// <exception cref="FileNotFoundException"></exception>
        public override void EndWrite(IAsyncResult asyncResult) => throw new FileNotFoundException();

        /// <summary>
        /// Clears buffers for this stream and causes any buffered data to be written to the file.
        /// </summary>
        /// <exception cref="FileNotFoundException"></exception>
        public override void Flush() => throw new FileNotFoundException();

        /// <summary>
        /// Clears buffers for this stream and causes any buffered data to be written to the file,
        /// and also clears all intermediate file buffers.
        /// </summary>
        /// <param name="flushToDisk">
        /// <see langword="true"/> to flush all intermediate file buffers; otherwise, <see langword="false"/>.
        /// </param>
        /// <exception cref="FileNotFoundException"></exception>
        public override void Flush(bool flushToDisk) => throw new FileNotFoundException();

        /// <summary>
        /// Asynchronously clears all buffers for this stream, causes any buffered data to be
        /// written to the underlying device, and monitors cancellation requests.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous flush operation.</returns>
        /// <exception cref="FileNotFoundException"></exception>
        public override Task FlushAsync(CancellationToken cancellationToken) => throw new FileNotFoundException();

        /// <summary>
        /// Reads a block of bytes from the stream and writes the data in a given buffer.
        /// </summary>
        /// <param name="array">
        /// When this method returns, contains the specified byte array with the values between
        /// <paramref name="offset"/> and ( <paramref name="offset"/> + <paramref name="count"/> - 1
        /// <c>)</c> replaced by the bytes read from the current source.
        /// </param>
        /// <param name="offset">
        /// The byte offset in <paramref name="array"/> at which the read bytes will be placed.
        /// </param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <returns>
        /// The total number of bytes read into the buffer. This might be less than the number of
        /// bytes requested if that number of bytes are not currently available, or zero if the end
        /// of the stream is reached.
        /// </returns>
        /// <exception cref="FileNotFoundException"></exception>
        public override int Read(byte[] array, int offset, int count) => throw new FileNotFoundException();

        /// <summary>
        /// Reads the specified buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public override int Read(Span<byte> buffer) => throw new FileNotFoundException();

        /// <summary>
        /// Asynchronously reads a sequence of bytes from the current file stream and writes them to
        /// a byte array beginning at a specified offset, advances the position within the file
        /// stream by the number of bytes read, and monitors cancellation requests.
        /// </summary>
        /// <param name="buffer">The buffer to write the data into.</param>
        /// <param name="offset">
        /// The byte offset in <paramref name="buffer"/> at which to begin writing data from the stream.
        /// </param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that represents the asynchronous read operation and wraps the total number of
        /// bytes read into the buffer. The result value can be less than the number of bytes
        /// requested if the number of bytes currently available is less than the requested number,
        /// or it can be 0 (zero) if the end of the stream has been reached.
        /// </returns>
        /// <exception cref="FileNotFoundException"></exception>
        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) => throw new FileNotFoundException();

        /// <summary>
        /// Reads the asynchronous.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default) => throw new FileNotFoundException();

        /// <summary>
        /// Reads a byte from the file and advances the read position one byte.
        /// </summary>
        /// <returns>
        /// The byte, cast to an <see cref="T:System.Int32"/>, or -1 if the end of the stream has
        /// been reached.
        /// </returns>
        /// <exception cref="FileNotFoundException"></exception>
        public override int ReadByte() => throw new FileNotFoundException();

        /// <summary>
        /// Sets the current position of this stream to the given value.
        /// </summary>
        /// <param name="offset">
        /// The point relative to <paramref name="origin"/> from which to begin seeking.
        /// </param>
        /// <param name="origin">
        /// Specifies the beginning, the end, or the current position as a reference point for
        /// <paramref name="offset"/>, using a value of type <see cref="T:System.IO.SeekOrigin"/>.
        /// </param>
        /// <returns>The new position in the stream.</returns>
        /// <exception cref="FileNotFoundException"></exception>
        public override long Seek(long offset, SeekOrigin origin) => throw new FileNotFoundException();

        /// <summary>
        /// Writes a block of bytes to the file stream.
        /// </summary>
        /// <param name="array">The buffer containing data to write to the stream.</param>
        /// <param name="offset">
        /// The zero-based byte offset in <paramref name="array"/> from which to begin copying bytes
        /// to the stream.
        /// </param>
        /// <param name="count">The maximum number of bytes to write.</param>
        /// <exception cref="FileNotFoundException"></exception>
        public override void Write(byte[] array, int offset, int count) => throw new FileNotFoundException();

        /// <summary>
        /// Writes the specified buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <exception cref="FileNotFoundException"></exception>
        public override void Write(ReadOnlySpan<byte> buffer) => throw new FileNotFoundException();

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
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        /// <exception cref="FileNotFoundException"></exception>
        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) => throw new FileNotFoundException();

        /// <summary>
        /// Writes the asynchronous.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default) => throw new FileNotFoundException();

        /// <summary>
        /// Writes a byte to the current position in the file stream.
        /// </summary>
        /// <param name="value">A byte to write to the stream.</param>
        /// <exception cref="FileNotFoundException"></exception>
        public override void WriteByte(byte value) => throw new FileNotFoundException();
    }
}