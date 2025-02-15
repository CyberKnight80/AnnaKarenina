using System.Diagnostics;
using System.Text;

namespace AnnaKarenina;

class Program
{
    static void Main(string[] args)
    {
        string filePath = "sample.txt"; // Replace with your file path
        string text = File.ReadAllText(filePath, Encoding.ASCII);

        // Measure time for Dictionary
        var stopwatch = Stopwatch.StartNew();
        var dictionary = new Dictionary<string, int>();
        var frequencyDict = GetWordFrequency(dictionary, text);
        stopwatch.Stop();
        Console.WriteLine($"Time taken using Dictionary: {stopwatch.ElapsedMilliseconds} ms");
        PrintFrequency("Dictionary", frequencyDict);

        // Measure time for SortedDictionary
        stopwatch.Restart();
        var sortedDictionary = new SortedDictionary<string, int>();
        var frequencySortedDict = GetWordFrequency(sortedDictionary, text);
        stopwatch.Stop();
        Console.WriteLine($"Time taken using SortedDictionary: {stopwatch.ElapsedMilliseconds} ms");
        PrintFrequency("SortedDictionary", frequencySortedDict);

        // Measure time for SortedList
        stopwatch.Restart();
        var sortedList = new SortedList<string, int>();
        var frequencySortedList = GetWordFrequency(sortedList, text);
        stopwatch.Stop();
        Console.WriteLine($"Time taken using SortedList: {stopwatch.ElapsedMilliseconds} ms");
        PrintFrequency("SortedList", frequencySortedList);
    }

    static IDictionary<string, int> GetWordFrequency(IDictionary<string, int> frequencyDict, string text)
    {
        

        foreach (var word in text.Split(new[] { ' ', '\n', '\r', '\t', '.', ',', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries))
        {
            if (frequencyDict.ContainsKey(word))
            {
                frequencyDict[word]++;
            }
            else
            {
                frequencyDict[word] = 1;
            }
        }

        return frequencyDict;
    }

    static void PrintFrequency(string methodName, IDictionary<string, int> frequencyDict)
    {
        Console.WriteLine($"\nTop 10 most common words using {methodName}:");

        // Order the dictionary by frequency in descending order and take the top 10
        var topWords = frequencyDict
            .OrderByDescending(pair => pair.Value)
            .Take(10);

        // Print each word and its frequency
        foreach (var pair in topWords)
        {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }
    }
}