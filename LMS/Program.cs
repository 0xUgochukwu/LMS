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

            UI.LoadBooks();
            UI.GeneralMenu();

        }
    }
}

