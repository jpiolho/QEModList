using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QEModList.Core.Models
{
    public class ZipFileStream : Stream
    {
        private bool _disposeArchive;
        private ZipArchive _archive;
        private Stream _stream;

        public Stream BaseStream { get; set; }
        public ZipFileStream(ZipArchive archive,Stream stream,bool disposeArchive=false)
        {
            BaseStream = stream;

            _archive = archive;
            _stream = stream;
            _disposeArchive = disposeArchive;
        }

        public override bool CanRead => _stream.CanRead;

        public override bool CanSeek => _stream.CanSeek;

        public override bool CanWrite => _stream.CanWrite;

        public override long Length => _stream.Length;

        public override long Position { get => _stream.Position; set => _stream.Position = value; }

        public override void Flush() => _stream.Flush();

        public override int Read(byte[] buffer, int offset, int count) => _stream.Read(buffer, offset, count);

        public override long Seek(long offset, SeekOrigin origin) => _stream.Seek(offset, origin);

        public override void SetLength(long value) => _stream.SetLength(value);

        public override void Write(byte[] buffer, int offset, int count) => _stream.Write(buffer, offset, count);
        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state) => _stream.BeginRead(buffer, offset, count, callback, state);
        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state) => _stream.BeginWrite(buffer, offset, count, callback, state);
        public override bool CanTimeout => _stream.CanTimeout;
        public override void CopyTo(Stream destination, int bufferSize) => _stream.CopyTo(destination, bufferSize);
        public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken) => _stream.CopyToAsync(destination, bufferSize, cancellationToken);

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var copyOfStream = _stream;
                _stream = null;

                copyOfStream.Close();

                var copyOfArchive = _archive;
                _archive = null;
                if (_disposeArchive)
                    copyOfArchive.Dispose();
            }

            base.Dispose(disposing);
        }

        public override int EndRead(IAsyncResult asyncResult) => _stream.EndRead(asyncResult);
        public override void EndWrite(IAsyncResult asyncResult) => _stream.EndWrite(asyncResult);
        public override Task FlushAsync(CancellationToken cancellationToken) => _stream.FlushAsync(cancellationToken);
        public override int Read(Span<byte> buffer) => _stream.Read(buffer);
        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) => _stream.ReadAsync(buffer, offset, count, cancellationToken);
        public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default) => _stream.ReadAsync(buffer, cancellationToken);
        public override int ReadByte() => _stream.ReadByte();
        public override int ReadTimeout { get => _stream.ReadTimeout; set => _stream.ReadTimeout = value; }
        public override void Write(ReadOnlySpan<byte> buffer) => _stream.Write(buffer);
        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) => _stream.WriteAsync(buffer, offset, count, cancellationToken);
        public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default) => _stream.WriteAsync(buffer, cancellationToken);
        public override void WriteByte(byte value) => _stream.WriteByte(value);
        public override int WriteTimeout { get => _stream.WriteTimeout; set => _stream.WriteTimeout = value; }

        public override void Close()
        {
            Dispose(true);
        }
    }
}
