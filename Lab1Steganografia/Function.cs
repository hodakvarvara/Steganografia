using System;
using System.Collections;
using System.Text;

namespace Lab1Steganografia
{
    public class Function
    {
        public List<bool> bitMessage = new List<bool>(); // сообщение в двоичном виде
        public string alpAngl = "ABCEHKMOPTXaceopxy";
        public string alpRus =  "АВСЕНКМОРТХасеорху";
        public string encodedText;

        /// <summary>
        /// Сообщение преобразуем в двоичную последовательность
        /// </summary>
        /// <param name="path"></param>
        public async void MessageConvertToBit(string path)
        {
            // чтение из файла
            try
            {
                byte[] bytes = File.ReadAllBytes(path);
                var bit = new BitArray(bytes); // сообщение в двоичном виде
                for(int i = 0; i < bit.Count; i++)
                {
                    bitMessage.Add(bit[i]);
                }
                bitMessage.Add(true);
                //// Console.WriteLine("Начальное сообщение");
                //foreach (var b in bitMessage) 
                //{ 
                //    Console.Write(b ? "1" : "0"); 
                //    Console.Write(" "); 
                //}
                //Console.WriteLine();
                //foreach (var b in bytes) 
                //{
                //    Console.Write(b); 
                //    Console.Write(" "); 
                //}
                //Console.WriteLine();

                //string text = ConvertBitArrayToText(k);

                //Console.WriteLine("Converted Text: " + text);

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error MessageConvertToBit: " + ex.Message);
            }
        }

        /// <summary>
        /// Читаем текст из stego
        /// </summary>
        /// <param name="path"></param>
        public string inputStego(string path)
        {
            string stegoMessage = "";
            // чтение из файла  по символьно
            try
            {
                if (new FileInfo(path).Length != 0)
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        int character;
                        while ((character = reader.Read()) != -1)
                        {
                            stegoMessage += (char)character;
                        }
                    }

                }   
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error inputContainer: " + ex.Message);
            }
            return stegoMessage;
        }

        /// <summary>
        /// Читаем текст из контейнера и шифруем
        /// </summary>
        /// <param name="path"></param>
        public  void inputContainerAndEncryption(string path)
        {
            // Преобразовываем в начальном тексте все
            // русско-английские буквы к английскому виду
            string inputTextToContainer = "";
            convertContainerToEngAlph(path, ref inputTextToContainer);

            try
            {
                int i = 0;
                foreach (char currentChar in inputTextToContainer)
                {  
                    // Меняем на русскую букву если:
                    // еще не кончилось сообщение
                    // текущая буква есть и в русском и в английском 
                    // если i символ сообщения == true т. е. 1
                    int ie = alpAngl.IndexOf(currentChar);
                    if (i != bitMessage.Count && ie >= 0)
                    {
                        if (bitMessage[i])
                        {
                            encodedText += alpRus[ie];
                        }
                        else
                        {
                            // Если i символ сообщения == false т. е. 0
                            // оставляем английскую букву
                            encodedText += currentChar;
                        }
                        i++;
                    }
                    else
                    {
                        // если не русско-анг буква оставляем как есть
                        encodedText += currentChar;
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error inputContainerAndEncryption: " + ex.Message);
            }
        }


        /// <summary>
        /// Преобразовываем контейнер ко всем английским буквам
        /// </summary>
        /// <param name="path"></param>
        /// <param name="inputTextToContainer"></param>
        public void convertContainerToEngAlph(string path, ref string inputTextToContainer)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    int character;
                    while ((character = reader.Read()) != -1)
                    {
                        char currentChar = (char)character;
                        int ir = alpRus.IndexOf(currentChar);
                        if (ir >= 0)
                        {
                            inputTextToContainer += alpAngl[ir];
                        }
                        else
                        {
                            inputTextToContainer += currentChar;
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error convertContainerToEngAlph: " + ex.Message);
            }
        }

        /// <summary>
        /// Текст записываем в файл
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task outputMessage(byte[] text, string path)
        {
            using (FileStream fstream = new FileStream(path, FileMode.Create))
            {
                // преобразуем строку в байты
                // byte[] buffer = Encoding.Default.GetBytes(text);
                // запись массива байтов в файл
                await fstream.WriteAsync(text, 0, text.Length);
            }
        }

        /// <summary>
        /// Текст записываем в файл
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task outputText(string text, string path)
        {
            using (FileStream fstream = new FileStream(path, FileMode.Create))
            {
                // преобразуем строку в байты
                byte[] buffer = Encoding.Default.GetBytes(text);
                // запись массива байтов в файл
                await fstream.WriteAsync(buffer, 0, text.Length);
            }
        }
        /// <summary>
        /// Расшифровываем сообщение из стегоконтейнера
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public byte[] decryptedMessage(string text)
        {
            List<bool> decryptedMessageList = new List<bool>(); // расшифр сообщение в двоичном виде
            byte[]? decryptedMessageStr = null;

            try
            {
                for (int j = 0; j < text.Length; j++)
                {
                    char currentChar = text[j];
                    // русская буква значит true
                    // английская буква значит false
                    if(alpAngl.Contains(currentChar) )
                    {
                        decryptedMessageList.Add(false);
                    }
                    else
                    {
                        if (alpRus.Contains(currentChar))
                        {
                            decryptedMessageList.Add(true);
                        }
                    }
                }

                // Удаляем ненужные нули 
                decryptedMessageList.Reverse();
                var k = 0;
                for(int i = 0; i<decryptedMessageList.Count; i++)
                {
                    if (decryptedMessageList[i] == true)
                    {
                        k = i;
                        break;
                    }
                }
                decryptedMessageList.Reverse();
              //  decryptedMessageList.RemoveRange(0, k - 1);
                // Преобразовываем List -> BitArray
                
                var count = decryptedMessageList.Count - k - 1;
                BitArray decryptedMessage = new BitArray(count); // расшифр сообщение в двоичном виде
                                                                 // Console.WriteLine("Восстановленное сообщение");
                for (int i = 0; i < count; i++)
                {
                    decryptedMessage[i] = decryptedMessageList[i];
                    //Console.Write(decryptedMessageList[i] ? "1" : "0");
                    //Console.Write(" ");
                }
                
                // Переводим двоичный код в текст
                decryptedMessageStr =  ConvertBitArrayToText(decryptedMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error decryptedMessage: " + ex.Message);
            }
            //Console.WriteLine($"Расшифрованное сообщение = {decryptedMessageStr}");
            return decryptedMessageStr;
        }


        /// <summary>
        /// Преобразование битовой последовательности 
        /// в текст
        /// </summary>
        /// <param name="bitArray"></param>
        /// <returns></returns>
        static byte[] ConvertBitArrayToText(BitArray bitArray)
        {
            //// Преобразование BitArray в байты
            byte[] bytes = new byte[(int)Math.Ceiling(bitArray.Length / 8.0)];
            bitArray.CopyTo(bytes, 0);
            
            // Декодирование байтов в текст
            return bytes;
        }
    }
}
