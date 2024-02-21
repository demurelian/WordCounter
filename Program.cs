using System.Text;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string inputFilePath = "E:\\Microsoft Visual Studio\\Projects\\DD-WordCounter\\DD-WordCounter\\text.txt";
        string resultFilePath = "E:\\Microsoft Visual Studio\\Projects\\DD-WordCounter\\DD-WordCounter\\result.txt";

        var words = ReadFileWordsToDictionary(inputFilePath);

        var sortedWordsWithFrequency = words.OrderByDescending(pair => pair.Value);

        WriteWordsToFile(sortedWordsWithFrequency, resultFilePath);

        Console.WriteLine("Программа завершена.");
    }

    static Dictionary<string, int> ReadFileWordsToDictionary(string filePath)
    {
        var wordsWithFrequency = new Dictionary<string, int>();

        try
        {
            using var reader = new StreamReader(filePath, Encoding.Default);
            while (reader.ReadLine() is { } line)
            {
                foreach(var word in CustomSplit(line))
                {
                    if (word.Length <= 0) continue;

                    var normalizedWord = word.ToLowerInvariant();
                    if (!wordsWithFrequency.TryAdd(normalizedWord,1))
                    {
                        wordsWithFrequency[normalizedWord]++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
        }

        return wordsWithFrequency;
    }

    static string[] CustomSplit(string text)
    {
        string pattern = @"[^a-zA-Zа-яА-ЯёЁ’-]| -|- |[a-zA-Z]&#\d{3}";
        var words = Regex.Split(text, pattern);
        return words;
    }

    static void WriteWordsToFile(IOrderedEnumerable<KeyValuePair<string, int>> sortedWords, string filePath)
    {
        try
        {
            using var writer = new StreamWriter(filePath);
            foreach (var item in sortedWords)
            {
                writer.WriteLine(item.Key + " " + item.Value);
            }

            Console.WriteLine($"Слова успешно записаны в файл: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при записи в файл: {ex.Message}");
        }
    }
}