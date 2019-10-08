using System;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using static System.Console;
using static System.Environment;

namespace text_analytics
{
    class Program
    {
        private static readonly string subscriptionKey = GetEnvironmentVariable("TEXT_ANALYTICS_SUBSCRIPTION_KEY");
        private static readonly string endpoint = GetEnvironmentVariable("TEXT_ANALYTICS_ENDPOINT");

        static void Main(string[] args)
        {
            if (!string.IsNullOrEmpty(subscriptionKey) && !string.IsNullOrEmpty(endpoint))
            {
                string textToAnalyze = "今年最強クラスの台風が週末3連休を直撃か...影響とその対策は？";
                ApiKeyServiceClientCredentials credentials = new ApiKeyServiceClientCredentials(subscriptionKey);
                TextAnalyticsClient client = new TextAnalyticsClient(credentials)
                {
                    Endpoint = endpoint
                };
                OutputEncoding = System.Text.Encoding.UTF8;
                LanguageResult languageResult = client.DetectLanguage(textToAnalyze);
                Console.WriteLine($"Language: {languageResult.DetectedLanguages[0].Name}");
                SentimentResult sentimentResult = client.Sentiment(textToAnalyze, languageResult.DetectedLanguages[0].Iso6391Name);
                Console.WriteLine($"Sentiment Score: {sentimentResult.Score:0.00}");
                Write("Press any key to exit.");
                ReadKey();
            }
            else
            {
                throw new Exception("You must set both TEXT_ANALYTICS_SUBSCRIPTION_KEY and TEXT_ANALYTICS_ENDPOINT:: as environment variables.");
            }
        }
    }
}
