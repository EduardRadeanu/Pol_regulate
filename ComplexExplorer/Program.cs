﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace ComplexExplorer
{
    
    static class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.Run(new PoligoaneRegulate());
           
            
        }
    }
}
