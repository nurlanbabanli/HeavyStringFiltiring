using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos
{
    public class ChunkRequestDto
    {
        public string UploadId { get; set; }
        public int ChunkIndex { get; set; }
        public string Data { get; set; }
        public bool IsLastChunk { get; set; }
    }
}
