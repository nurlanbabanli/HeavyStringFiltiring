using Business.Abstract;
using Business.BackgroundWorkers;
using Business.Concrete;
using Business.Helper.DataHolders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

public class TextProcessingWorkerTest
{
    private IConfiguration _configuration;
    private ITextFilterService _textFilterService;

    [SetUp]
    public void Setup()
    {
        var basePath = Path.GetFullPath(Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\..\Api"));
        _configuration = new ConfigurationBuilder().SetBasePath(basePath).AddJsonFile("appsettings.json", optional: false).AddEnvironmentVariables().Build();

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<ITextFilterService, TextFilterManager>();
        var provider = serviceCollection.BuildServiceProvider();

        _textFilterService = provider.GetService<ITextFilterService>();
    }

    [Test]
    public async Task Test1()
    {
        var backgroundService = new TextProcessingWorker(_configuration, _textFilterService);
        using var cts = new CancellationTokenSource();

        TextHolder.TextsToFilterQueue.Clear();
        TextHolder.FilteredTexts.Clear();
        TextHolder.FilterWords.Clear();
        TextHolder.FilterWords.Add("book");
        TextHolder.FilterWords.Add("pen");
        TextHolder.FilterWords.Add("window");

        var fullText = "I have a book. I dont want to a pen. Please open the window";
        var expectedText = "I have a I dont want to a Please open the";

        TextHolder.TextsToFilterQueue.Enqueue(fullText);

        var taskToRun= backgroundService.StartAsync(cts.Token);
        await Task.Delay(TimeSpan.FromSeconds(3));
        cts.Cancel();
        await taskToRun;

        Assert.IsTrue(string.Compare(expectedText, TextHolder.FilteredTexts[0]) == 0);
   
    }
}
