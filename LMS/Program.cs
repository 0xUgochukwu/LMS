using System;
using System.Drawing;
using Colorful;
using Console = Colorful.Console;
using System.Threading;
using System.Threading.Tasks;



namespace LMS
{
    class Program
    {
        
        static void Main(string[] args)
        {


            //Console.Title = ("Login");
            ////Console.WriteLine("Hei", Color.Red);
            ////UI.WriteLogo();
            //int counter = 0;
            //UI.WriteLogo();
            //UI.Spin("Working", 100, sequenceCode: 1);

            //UI.AnimateLogo(5);
            //UI.Load(20, displayMsg: "Welcome To Aptech Library", sequenceCode: 1);
            //Console.WriteLine("Hello");
            //UI.TypeLine("...");

            //UI.ProgressBar(100);
            UI.AnimateLogo(5);

            string password = UI.getPassword();

            if (password == "0000")
            {
                UI.Escape(); UI.TypeLine("Correct", Color.Green);
            }
            else
            {
                UI.Escape();UI.TypeLine("Wrong", Color.Red);
            }
            //UI.TypeLine(UI.logo, Color.Blue);
            Console.ReadKey();

            Console.WriteLine("LMS Loading...");

        }
    }
}

