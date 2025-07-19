using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helper.Filters
{
    internal static class LevenshteinHelper
    {

        internal static double CalculateSimilarity(string source, string target)
        {
            int distance = CalculateLevenshteinDistance(source, target);
            int maxLen = Math.Max(source.Length, target.Length);
            return maxLen == 0 ? 1.0 : 1.0 - ((double)distance / maxLen);
        }


        private static int CalculateLevenshteinDistance(string source, string target)
        {
            if(string.IsNullOrEmpty(source) && string.IsNullOrEmpty(target) ) return 0;
            if(string.IsNullOrEmpty(source) && !string.IsNullOrEmpty(target)) return target.Length;
            if (!string.IsNullOrEmpty(source) && string.IsNullOrEmpty(target)) return source.Length;

            int[,] dp = new int[source.Length + 1, target.Length + 1];

            for (int i = 0; i <= source.Length; i++) dp[i, 0] = i;
            for (int j = 0; j <= target.Length; j++) dp[0, j] = j;

            for (int i = 1; i <= source.Length; i++)
            {
                for (int j = 1; j <= target.Length; j++)
                {
                    int cost = source[i - 1] == target[j - 1] ? 0 : 1;

                    dp[i, j] = Math.Min(
                        Math.Min(dp[i - 1, j] + 1,
                                 dp[i, j - 1] + 1),
                        dp[i - 1, j - 1] + cost
                    );
                }
            }

            return dp[source.Length, target.Length];
        }
    }
}
