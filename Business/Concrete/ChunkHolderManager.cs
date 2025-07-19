using Business.Abstract;
using Business.Helper.DataHolders;
using Dtos;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ChunkHolderManager : IChunkHolderService
    {
        public bool AddChunkDictionary(ChunkRequestDto chunkRequestDto)
        {
            if (TextHolder.ChunksByUploadId.TryGetValue(chunkRequestDto.UploadId, out var chunksByIndex))
            {
                chunksByIndex[chunkRequestDto.ChunkIndex] = chunkRequestDto.Data;
            }
            else
            {
                var newChunksByIndex=new ConcurrentDictionary<int, string>();
                newChunksByIndex[chunkRequestDto.ChunkIndex]= chunkRequestDto.Data;

                TextHolder.ChunksByUploadId[chunkRequestDto.UploadId]= newChunksByIndex;
            }


            if (chunkRequestDto.IsLastChunk)
            {
                AddToFullTextQueue(chunkRequestDto.UploadId);
                return true;
            }

            return false;
        }


        private void AddToFullTextQueue(string completedChunkId)
        {
            if(TextHolder.ChunksByUploadId.TryGetValue(completedChunkId, out var chunksDictionary))
            {
                var stringBuilder=new StringBuilder();
                foreach (var chunk in chunksDictionary.OrderBy(x => x.Key))
                    stringBuilder.Append(chunk.Value).Append(' ');

                var fullText=stringBuilder.ToString().TrimEnd();
                TextHolder.ChunksByUploadId.TryRemove(completedChunkId, out _);
                TextHolder.TextsToFilterQueue.Enqueue(fullText);
            }          
        }
    }
   
}
