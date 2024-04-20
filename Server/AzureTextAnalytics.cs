using Azure;
using Azure.AI.TextAnalytics;

namespace Server;

internal sealed class AzureTextAnalytics
{
    void AnalyzeText(string text)
    {
        string key = "0034de55083a4fe2aa95ca2260c631b9";
        Uri endpoint = new Uri("https://sinanhack.cognitiveservices.azure.com/");

        AzureKeyCredential credentials = new AzureKeyCredential(key);
        TextAnalyticsClient textClient = new TextAnalyticsClient(endpoint, credentials);

        Response<KeyPhraseCollection> keyPhrasesResponse = textClient.ExtractKeyPhrases(text, "bg");
    }
}