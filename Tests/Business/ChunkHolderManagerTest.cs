using Business.Abstract;
using Business.Concrete;
using Business.Helper.DataHolders;
using Dtos;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Tests;

public class ChunkHolderManagerTest
{
    private IChunkHolderService _chunkHolderService;

    [SetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton<IChunkHolderService, ChunkHolderManager>();
        var provider = serviceCollection.BuildServiceProvider();

        _chunkHolderService = provider.GetService<IChunkHolderService>();
    }

    [Test]
    public void AddChunkDictionaryTest1()
    {
        var stringBuilder = new StringBuilder();
        var uploadId = "c1";
        var data1 = "This is my test app";
        var data2 = "I can debug it.";
        var data3 = "This is going to be fun.";
        var fullText = stringBuilder.Append(data1).Append(' ').Append(data2).Append(' ').Append(data3).ToString();

        TextHolder.TextsToFilterQueue.Clear();
        TextHolder.ChunksByUploadId.Clear();

        var chunk1 = new ChunkRequestDto
        {
            UploadId = uploadId,
            ChunkIndex = 1,
            Data = data1,
            IsLastChunk = false
        };

        var chunk2 = new ChunkRequestDto
        {
            UploadId = uploadId,
            ChunkIndex = 2,
            Data = data2,
            IsLastChunk = false
        };

        var chunk3 = new ChunkRequestDto
        {
            UploadId = uploadId,
            ChunkIndex = 3,
            Data = data3,
            IsLastChunk = true
        };

       

        var result = _chunkHolderService.AddChunkDictionary(chunk1);
        Assert.IsFalse(result);

        result = _chunkHolderService.AddChunkDictionary(chunk2);
        Assert.IsFalse(result);

        result = _chunkHolderService.AddChunkDictionary(chunk3);
        Assert.IsTrue(result);

        Assert.IsTrue(TextHolder.TextsToFilterQueue.TryDequeue(out var text));

        Assert.IsTrue(string.Compare(text, fullText) == 0);

        Assert.IsFalse(TextHolder.ChunksByUploadId.TryGetValue(uploadId, out var _));
    }

}
