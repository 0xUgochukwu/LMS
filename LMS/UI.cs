using System;
using System.Drawing;
using System.Reflection;
using System.Threading;
using Colorful;
using static System.Net.Mime.MediaTypeNames;
using Console = Colorful.Console;

namespace LMS
{
    public class UI
    {
        static public string logo = "\n      ___           ___ \n     /  /\\         /  /\\\n    /  /::\\       /  /:/\n   /  /:/\\:\\     /  /:/ \n  /  /::\\ \\:\\   /  /:/  \n /__/:/\\:\\_\\:\\ /__/:/   \n \\__\\/  \\:\\/:/ \\  \\:\\   \n      \\__\\::/   \\  \\:\\  \n      /  /:/     \\  \\:\\ \n     /__/:/       \\  \\:\\\n     \\__\\/         \\__\\/\n";
        static string[,] sequence = sequence = new string[,] {
                { "/", "-", "\\", "|" },
                { ".   ", "..  ", "... ", "...." },
                { "=>   ", "==>  ", "===> ", "====>" },
            };
        static int Delay { get; set; } = 200;

        static int totalSequences = sequence.GetLength(0);
        static int counter = 0;


        static public void WriteLogo()
        {
            Console.WriteLine(logo, Color.Blue);

        }

        static public void TypeLine(string line, Color? color = null, int sleepTime = 50)
        {
            for (int i = 0; i < line.Length; i++)
            {
                Console.Write(line[i], color ?? Color.White);
                System.Threading.Thread.Sleep(sleepTime);
            }
        }

        static public void TypeLogo()
        {
            TypeLine(logo, Color.Blue, 10);
        }

        static public void Clear()
        {
            Console.Clear();
            WriteLogo();
        }


        static public void Spin(string displayMsg, int times, int sequenceCode = 0)
        {
            TypeLine(displayMsg);
            Clear();
            int timesCheck = 0;
            while (timesCheck++ < times)
            {
                counter++;

                

                sequenceCode = sequenceCode > totalSequences - 1 ? 0 : sequenceCode;

                int counterValue = counter % 4;

                string fullMessage = displayMsg + sequence[sequenceCode, counterValue];
                int msglength = fullMessage.Length;

                Console.Write(fullMessage);

                Console.SetCursorPosition(Console.CursorLeft - msglength, Console.CursorTop);
                Thread.Sleep(Delay);
            }
            
        }

        
    }
}

