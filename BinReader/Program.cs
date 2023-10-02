namespace BinReader
{
    /// <summary>
    /// Приложение предназначено для вывода байтов текста из файла txt
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: path/to/file");
                return;
            }
            if (!File.Exists(args[0]))
            {
                Console.WriteLine($"File {args[0]}");
                return;
            }
            var data = File.ReadAllBytes(args[0]);
            for (int i = 0; i < data.Length; i += 10)
            {
                for (int j = i; j < i + 10 && j < data.Length; ++j)
                {
                    Console.Write($" {data[j]:X}");
                }
                Console.WriteLine();
            }
        }
    }
}
