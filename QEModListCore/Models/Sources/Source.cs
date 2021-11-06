using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QEModList.Core.Models.Sources
{
    public abstract class Source
    {
        public abstract Task<List<Addon>> GetAddonsAsync(CancellationToken cancellationToken);
        public abstract Task<Stream> GetFileStreamAsync(string path,CancellationToken cancellationToken);
    }
}
