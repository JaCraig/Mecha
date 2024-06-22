using Mecha.Core.Generator.Helpers;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Mecha.Core.Tests.Generator.Helpers
{
    public class FaultyFileStreamTests
    {
        public FaultyFileStreamTests()
        {
            _TestClass = new FaultyFileStream();
        }

        private readonly FaultyFileStream _TestClass;

        [Fact]
        public async Task AllMethodsThrowFileNotFound()
        {
            _ = Assert.Throws<FileNotFoundException>(() => _TestClass.BeginRead(null!, 0, 0, (_) => { }, null!));
            _ = Assert.Throws<FileNotFoundException>(() => _TestClass.BeginWrite(null!, 0, 0, (_) => { }, null!));
            _ = Assert.Throws<FileNotFoundException>(_TestClass.Close);
            _ = Assert.Throws<FileNotFoundException>(() => _TestClass.CopyTo(null!, 0));
            _ = await Assert.ThrowsAsync<FileNotFoundException>(() => _TestClass.CopyToAsync(null!, 0, CancellationToken.None));
            _ = Assert.Throws<FileNotFoundException>(() => _TestClass.EndRead(null!));
            _ = Assert.Throws<FileNotFoundException>(() => _TestClass.EndWrite(null!));
            _ = Assert.Throws<FileNotFoundException>(_TestClass.Flush);
            _ = Assert.Throws<FileNotFoundException>(() => _TestClass.Flush(true));
            _ = await Assert.ThrowsAsync<FileNotFoundException>(() => _TestClass.FlushAsync(CancellationToken.None));
            _ = await Assert.ThrowsAsync<FileNotFoundException>(_TestClass.FlushAsync);
            _ = Assert.Throws<FileNotFoundException>(() => _TestClass.Read(null!, 0, 0));
            _ = Assert.Throws<FileNotFoundException>(() => _TestClass.Read(new byte[256]));
            _ = await Assert.ThrowsAsync<FileNotFoundException>(() => _TestClass.ReadAsync(null!, 0, 0, CancellationToken.None));
            _ = await Assert.ThrowsAsync<FileNotFoundException>(() => _TestClass.ReadAsync(null!, 0, 0));
            _ = await Assert.ThrowsAsync<FileNotFoundException>(async () => await _TestClass.ReadAsync(new byte[256]));
            _ = Assert.Throws<FileNotFoundException>(() => _TestClass.ReadByte());
            _ = Assert.Throws<FileNotFoundException>(() => _TestClass.Seek(0, SeekOrigin.Begin));
            _ = Assert.Throws<FileNotFoundException>(() => _TestClass.Write(null!, 0, 0));
            _ = Assert.Throws<FileNotFoundException>(() => _TestClass.Write(new byte[256]!));
            _ = await Assert.ThrowsAsync<FileNotFoundException>(() => _TestClass.WriteAsync(null!, 0, 0, CancellationToken.None));
            _ = await Assert.ThrowsAsync<FileNotFoundException>(() => _TestClass.WriteAsync(null!, 0, 0));
            _ = await Assert.ThrowsAsync<FileNotFoundException>(async () => await _TestClass.WriteAsync(new byte[256]));
            byte TempByte = 0;
            _ = Assert.Throws<FileNotFoundException>(() => _TestClass.WriteByte(TempByte));
        }
    }
}