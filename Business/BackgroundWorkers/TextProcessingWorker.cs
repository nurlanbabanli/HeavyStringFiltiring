using Business.Abstract;
using Business.Helper.DataHolders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BackgroundWorkers
{
    public class TextProcessingWorker : BackgroundService
    {
        private double SimilarityThreshold = 0.1;
        private IConfiguration _onfiguration { get; }
        private ITextFilterService _textFilterService;

        public TextProcessingWorker(IConfiguration configuration, ITextFilterService textFilterService)
        {
            _onfiguration = configuration;
            _textFilterService = textFilterService;

            double.TryParse(_onfiguration["Settings:SimilarityThreshold"]!, CultureInfo.InvariantCulture, out SimilarityThreshold);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));

                if (!TextHolder.TextsToFilterQueue.TryDequeue(out var text)) continue;
                var resultText = await _textFilterService.FilterText(text, SimilarityThreshold);
                TextHolder.FilteredTexts.Add(resultText);
            }
        }
    }
}
