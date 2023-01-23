using System;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Timers;
using Colorful;
using static System.Net.Mime.MediaTypeNames;
using Console = Colorful.Console;

namespace LMS
{
    public class UI
    {

        static string[] logos = new string[]
        {
            "\n      ___           ___ \n     /  /\\         /  /\\\n    /  /::\\       /  /:/\n   /  /:/\\:\\     /  /:/ \n  /  /::\\ \\:\\   /  /:/  \n /__/:/\\:\\_\\:\\ /__/:/   \n \\__\\/  \\:\\/:/ \\  \\:\\   \n      \\__\\::/   \\  \\:\\  \n      /  /:/     \\  \\:\\ \n     /__/:/       \\  \\:\\\n     \\__\\/         \\__\\/\n",
            "\n      ___                   \n     /  /\\                  \n    /  /::\\                 \n   /  /:/\\:\\    ___     ___ \n  /  /:/~/::\\  /__/\\   /  /\\\n /__/:/ /:/\\:\\ \\  \\:\\ /  /:/\n \\  \\:\\/:/__\\/  \\  \\:\\  /:/ \n  \\  \\::/        \\  \\:\\/:/  \n   \\  \\:\\         \\  \\::/   \n    \\  \\:\\         \\__\\/    \n     \\__\\/                  \n",
            "\n      ___                   \n     /\\  \\                  \n    /::\\  \\                 \n   /:/\\:\\  \\                \n  /:/ /::\\  \\   ___     ___ \n /:/_/:/\\:\\__\\ /\\  \\   /\\__\\\n \\:\\/:/  \\/__/ \\:\\  \\ /:/  /\n  \\::/__/       \\:\\  /:/  / \n   \\:\\  \\        \\:\\/:/  /  \n    \\:\\__\\        \\::/  /   \n     \\/__/         \\/__/    \n",
            "\n      ___           ___ \n     /\\  \\         /\\__\\\n    /::\\  \\       /:/  /\n   /:/\\:\\  \\     /:/  / \n  /::\\~\\:\\  \\   /:/  /  \n /:/\\:\\ \\:\\__\\ /:/__/   \n \\/__\\:\\/:/  / \\:\\  \\   \n      \\::/  /   \\:\\  \\  \n      /:/  /     \\:\\  \\ \n     /:/  /       \\:\\__\\\n     \\/__/         \\/__/\n"
        };
        static string[,] sequence = sequence = new string[,] {
                { "/", "-", "\\", "|" },
                { ".   ", "..  ", "... ", "...." },
                { "=>   ", "==>  ", "===> ", "====>" },
            };
        static int Delay { get; set; } = 200;

        static int totalSequences = sequence.GetLength(0);
        static int counter = 0;


        static public void WriteLogo(Color? color = null)
        {
            Console.WriteLine(logos[0], color ?? Color.Blue);

        }


        static public void AnimateLogo(int j = 20)
        {
           

            for (int i = 0; i <= j; i++)
            {
                Console.Write(logos[i % 4], i % 2 == 0 ? Color.Blue : Color.White);
                Thread.Sleep(500);
                Console.Clear();
            }

        }

        static public void TypeLine(string line, Color? color = null, int sleepTime = 50)
        {
            for (int i = 0; i < line.Length; i++)
            {
                Console.Write(line[i], color ?? Color.White);
                System.Threading.Thread.Sleep(sleepTime);
            }
        }

        static public void TypeLineL(string line, Color? color = null, int sleepTime = 50)
        {
            WriteLogo();
            for (int i = 0; i < line.Length; i++)
            {
                
                Console.Write(line[i], color ?? Color.White);
                System.Threading.Thread.Sleep(sleepTime);
            }
        }

        static public void TypeLogo()
        {
            TypeLine(logos[0], Color.Blue, 10);
        }

        static public void Clear()
        {
            Console.Clear();
            WriteLogo();
        }

        static public void Load(int times, string displayMsg = "", int sequenceCode = 0, Color? color = null)
        {
            Clear();
            TypeLineL(displayMsg);
            Clear();

            for (int i = 0; i < times; i++)
            {
                Clear();
                Console.Write(displayMsg);
                TypeLine(sequence[sequenceCode, i % 4], color ?? Color.White);
                System.Threading.Thread.Sleep(200);

            }

            Clear();
        }


        static public void Load2(int times, string displayMsg = "", int sequenceCode = 0)
        {
            Clear();
            TypeLineL(displayMsg);
            Clear();
            int timesCheck = 0;
            while (timesCheck++ < times + displayMsg.Length)
            {

                counter++;

                

                sequenceCode = sequenceCode > totalSequences - 1 ? 0 : sequenceCode;

                int counterValue = counter % 4;

                Clear();
                Console.Write(displayMsg + sequence[sequenceCode, counterValue]);

                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop);
                //Console.CursorVisible = false;
                Thread.Sleep(Delay);
            }


            Clear();
            
            
        }


        public static void ProgressBar(string displayMsg = "", int interval = 20, Color? color = null)
        {
            string spaces = "                         ";
            string loader = "";
            int count = 0;
            for (int i = 0; i < 101; i++)
            {
                Clear();

                Console.Write($"{displayMsg}");
                Console.Write($"[");
                Console.Write(loader + spaces, color ?? Color.Green);
                Console.Write($"] {sequence[0, count % 4]}  {i}%");
                Thread.Sleep(interval);

                if (i % 2 == 0) count++;
                if (i % 4 == 0 && i != 100)
                {
                    loader += "+";
                    spaces = spaces.Remove(spaces.Length - 1);
                    
                }
            }
        }


        public static string getPassword()
        {
            TypeLineL("Enter your password: ");
            string inputPassword = "";

            while (true)
            {
                ConsoleKeyInfo inputKey = Console.ReadKey(true);

                if (inputKey.Key == ConsoleKey.Enter)
                {
                    break;
                }

                inputPassword += inputKey.KeyChar;
                Console.Write("*");
            }

            return inputPassword;
        }


        
        public static void throwError(string errorMsg)
        {
            Clear();
            TypeLineL(errorMsg, color: Color.Red);

        }


        public static void ShowSuccess(string successMsg)
        {
            Clear();
            TypeLineL(successMsg, color: Color.Green);

        }

        static public void Escape( )
        {
            Console.WriteLine();
        }


        static public void passwordErr() {
            throwError("Invalid Password...");
        }

        public static void GeneralMenu() { }
        public static void ClientMenu(Client client) { }
        public static void AdminMenu(Admin admin) { }
    }
}

