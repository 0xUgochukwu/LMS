using System;
using System.Drawing;
using Colorful;
using Console = Colorful.Console;



namespace LMS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = ("Login");
            //Console.WriteLine("Hei", Color.Red);
            //UI.WriteLogo();
            int counter = 0;
            UI.WriteLogo();
            UI.Spin("Working", 100, sequenceCode: 1); counter++;
            
            //UI.TypeLine(UI.logo, Color.Blue);
            Console.ReadKey();
        }
    }
}

