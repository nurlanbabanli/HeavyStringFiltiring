using Business.Abstract;
using Business.Helper.DataHolders;
using Business.Helper.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TextFilterManager : ITextFilterService
    {
        public Task<string> FilterText(string text, double similarityThreshold)
        {
            var wordsFromText = text.Split(new[] { ' ', '\n', '\r', '\t', '.', ',', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);
            var resultBuilder=new StringBuilder();


            foreach (var wordFromText in wordsFromText)
            {
                bool isSmilarityFound=false;

                foreach (var filterWord in TextHolder.FilterWords)
                {
                    var similarityScore = LevenshteinHelper.CalculateSimilarity(wordFromText, filterWord);
                    if (similarityScore >= similarityThreshold)
                        isSmilarityFound = true;
                }

                if(!isSmilarityFound)
                    resultBuilder.Append(wordFromText).Append(' ');
            }

            var resultText = resultBuilder.ToString().TrimEnd();

            return Task.FromResult(resultText);
        }
    }
}
