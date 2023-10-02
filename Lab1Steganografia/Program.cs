using Lab1Steganografia.Options;
using MatthiWare.CommandLine;
using System;
using System.Text;

namespace Lab1Steganografia
{   
    /// <summary>
    /// Приложение предназначено для шифровки сообщений с помощью стеганографии
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            var options = new CommandLineParserOptions
            {
                AppName = "Steganografia"
            };
            var parser = new CommandLineParser<ProgramOptions>(options);
            var result = parser.Parse(args);
            
            if (result.HasErrors)
            {
                Console.Error.WriteLine("Errors...");
                return;
            }
            if (result != null)
            {
                ProgramOptions programOptions = result.Result;
                Function function = new Function();

                if(programOptions.startCommand == "put")
                {
                    ///// Считывание сообщения //////
                    if (programOptions.message != null)
                    {
                        function.MessageConvertToBit(programOptions.message);
                    }
                    else
                    {
                        Console.Write("Введите сообщение: ");
                        try
                        {
                            function.outputText(Console.ReadLine(), "input.txt");
                            function.MessageConvertToBit("input.txt");
                        }
                        catch { Console.Error.WriteLine("Errors enter message.."); }

                    }

                    ///// Читаем текст из контейнера //////
                    function.inputContainerAndEncryption(programOptions.container);


                    ///// Запись закодированного сообщения //////
                    if (programOptions.stego != null)
                    {
                        function.outputText(function.encodedText, programOptions.stego);
                    }
                    else
                    {
                        Console.WriteLine("Вывод текста с зашифрованным сообщением: ");
                        Console.WriteLine(function.encodedText);

                    }
                    
                    //Console.WriteLine("Запись сообщения");
                    //Console.WriteLine($"message:{programOptions.message}");
                    //Console.WriteLine($"stego:{programOptions.stego}");
                    //Console.WriteLine($"container:{programOptions.container}");

                }

                if (programOptions.startCommand == "get")
                {

                    ///// Считывание текста с закодированным сообщением //////
                    string stegoStr;
                    if (programOptions.stego != null)
                    {
                        stegoStr = function.inputStego(programOptions.stego);
                    }
                    else
                    {
                        Console.WriteLine("Введите зашифрованное сообщение: ");
                        stegoStr = Console.ReadLine();
                    } 

                    ///// Запись или вывод расшифрованного сообщения //////
                    byte[] decryptedStr = function.decryptedMessage(stegoStr);
                    if (programOptions.message != null)
                    {
                        function.outputMessage(decryptedStr, programOptions.message);
                    }
                    else
                    {
                        Console.Write("Расшифрованное сообщение: ");
                        var str = Encoding.UTF8.GetString(decryptedStr, 0, decryptedStr.Length);
                        Console.WriteLine(str);  
                    }
                    //Console.WriteLine("Получение сообщения");
                    //Console.WriteLine($"message:{programOptions.message}");
                    //Console.WriteLine($"stego:{programOptions.stego}");
                    //Console.WriteLine($"container:{programOptions.container}");

                }

            }

        }
    }
}