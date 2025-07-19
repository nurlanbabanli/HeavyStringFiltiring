using Business.Abstract;
using Business.Concrete;
using Business.Helper.DataHolders;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace Tests;

public class TextFilterManagerTest
{
    private ITextFilterService _textFilterService;

    [SetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<ITextFilterService, TextFilterManager>();
        var provider = serviceCollection.BuildServiceProvider();

        _textFilterService = provider.GetService<ITextFilterService>();
    }

    [Test]
    public async Task Test1()
    {
        double similarity = 0.8;
        TextHolder.FilterWords.Clear();
        TextHolder.FilterWords.Add("book");
        TextHolder.FilterWords.Add("pen");
        TextHolder.FilterWords.Add("window");


        var fullText = "I have a book. I dont want to a pen. Please open the window";
        var expectedText = "I have a I dont want to a Please open the";

        var resultText = await _textFilterService.FilterText(fullText, similarity);

        Assert.IsTrue(string.Compare(expectedText, resultText) == 0);

    }
}
