using System;
using System.IO;
using System.Threading.Tasks;

namespace DWAsync
{
    public class Program
    {
        static async Task Main()
        {
            await ReadCharacters();
        }

        static async Task ReadCharacters()
        {
            var dirInfo = new DirectoryInfo(@"..\..\..\Files");
            var files = dirInfo.GetFiles("*", SearchOption.TopDirectoryOnly);
            //Console.WriteLine(f.Length.ToString());

            for (int i = 1; i <= files.Length; i++)
            {
                using (StreamReader reader = new StreamReader(@$"..\..\..\Files\{i}.txt"))
                {
                    Console.WriteLine("Async Read File has started");
                    String result = await reader.ReadToEndAsync();
                    await WriteFileAsync(@$"..\..\..\Files\", $"{i}-coppy.txt", result);
                    Console.WriteLine("Async Read File has completed");
                }
            }
        }

        static async Task WriteFileAsync(string dir, string file, string content)
        {
            Console.WriteLine("Async Write File has started");
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(dir, file)))
            {
                await outputFile.WriteAsync(content);
            }
            Console.WriteLine("Async Write File has completed");
        }

    }
}
