using MatthiWare.CommandLine.Core.Attributes;

namespace Lab1Steganografia.Options
{
    public class ProgramOptions
    {
        [OptionOrder(0), Name("m"), Description("Путь к сообщению")]
        public string? message { get; set; } // путь к файлу, содержащему сообщение

        [OptionOrder(1), Name("s"), Description("Путь к стегоконтейнеру")]
        public string stego { get; set; } // путь по которому нужно записать стегоконтейнер

        [OptionOrder(1), Name("c"), Description("Путь к контейнеру")]
        public string container { get; set; } // путь к файлу-контейнеру

        [ Name("start"), Description("Если вы хотите зашифровать сообщение необходимо указать параметр -put, расшифровать -get")]
        public string? startCommand { get; set; } // put get
    }
}
