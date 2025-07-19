using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business.Helper.DataHolders
{
    internal static class TextHolder
    {
        internal static ConcurrentDictionary<int, string> ChunksByIndex = new ConcurrentDictionary<int, string>();
        internal static ConcurrentDictionary<string, ConcurrentDictionary<int, string>> ChunksByUploadId = new ConcurrentDictionary<string, ConcurrentDictionary<int, string>>();
        internal static ConcurrentQueue<string> TextsToFilterQueue = new ConcurrentQueue<string>();
        internal static List<string> FilteredTexts = new List<string>();

        internal static List<string> FilterWords = new List<string>() { "test", "apple", "software", "debug" };
    }
}
