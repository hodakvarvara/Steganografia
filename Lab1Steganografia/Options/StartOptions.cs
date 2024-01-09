using MatthiWare.CommandLine.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1Steganografia.Options
{
    public class StartOptions
    {
        
        [OptionOrder(0), Name("put"), Description("opisanie put")]
        public string? put { get; set; }

    }
}
