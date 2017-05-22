using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nullEngine
{
    class Program
    {
      static void Main(string[] args)
        {
            OpenTK.GameWindow window = new OpenTK.GameWindow(1600, 900, new OpenTK.Graphics.GraphicsMode(32,8,0,0));
            Game game = new Game(window);
            window.Run(1.0 / 60.0);
        }
        public static void exit()
        {
            Environment.Exit(0);
        }
    }
}
