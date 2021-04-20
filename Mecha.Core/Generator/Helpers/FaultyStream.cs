using System.IO;

namespace Mecha.Core.Generator.Helpers
{
    /// <summary>
    /// Stream that throws faults
    /// </summary>
    /// <seealso cref="Stream"/>
    public class FaultyStream : Stream
    {
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
        /// When overridden in a derived class, clears all buffers for this stream and causes any
        /// buffered data to be written to the underlying device.
        /// </summary>
        /// <exception cref="EndOfStreamException"></exception>
        public override void Flush()
        {
            throw new EndOfStreamException();
        }

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
            throw new EndOfStreamException();
        }

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
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new EndOfStreamException();
        }

        /// <summary>
        /// When overridden in a derived class, sets the length of the current stream.
        /// </summary>
        /// <param name="value">The desired length of the current stream in bytes.</param>
        /// <exception cref="EndOfStreamException"></exception>
        public override void SetLength(long value)
        {
            throw new EndOfStreamException();
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
        /// <exception cref="EndOfStreamException"></exception>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new EndOfStreamException();
        }
    }
}