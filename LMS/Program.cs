﻿using System;
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


            //UI.GeneralMenu();

            string password = UI.getPassword();

            if (password == "1234")
            {
                UI.Escape(); UI.TypeLine("Okay", Color.Green);
            }
            else
            {
                UI.Escape(); UI.throwError("Wrong");
            }

        }
    }
}

