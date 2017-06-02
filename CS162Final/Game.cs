using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace nullEngine
{
    class Game
    {
        public static GameWindow window;
        public static int tick = 0;
        public static InputManager input;
        public static ButtonManager buttonMan;
        public static Queue<Action> renderQueue;
        public static TextureAtlas font;
        public static Texture2D buttonBackground;
        public static long frameTime;
        public static Random rng;

        public StateMachines.GameStateManager gStateManager;

        private Matrix4 projMatrix;

        static int worldx = 0;
        static int worldy = 0;
        public static int worldMaxX;
        public static int worldMaxY;

        public static Rectangle worldRect;
        public static Rectangle windowRect;

        public static int worldCenterX
        {
            get
            {
                return worldx + window.Width / 2;
            }
        }

        public static int worldCenterY
        {
            get
            {
                return worldy + window.Height / 2;
            }
        }

        private Stopwatch sw;

        public Game(GameWindow w)
        {
            rng = new Random();
            window = w;
            worldMaxX = int.MaxValue;
            worldMaxY = int.MaxValue;

            worldRect = new Rectangle(0, 0, worldMaxX, worldMaxY);
            windowRect = new Rectangle(worldx, worldy, window.Width, window.Height);

            window.Load += window_Load;
            window.UpdateFrame += window_UpdateFrame;
            window.RenderFrame += window_RenderFrame;

            input = new InputManager();
            buttonMan = new ButtonManager();

            //initialize global textures
            font = new TextureAtlas("Content/font.png", 16, 6, 8, 12, 0);
            buttonBackground = Managers.TextureManager.LoadTexture("Content/buttonBackground.png", false);

            //initialize game statemachine;
            gStateManager = new StateMachines.GameStateManager();

            //inititialize frame timer;
            sw = new Stopwatch();
            frameTime = 0;
            renderQueue = new Queue<Action>();
        }

        void window_Load(object sender, EventArgs e)
        {
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.AlphaTest);
            GL.AlphaFunc(AlphaFunction.Gequal, 0.5f);
        }

        void window_UpdateFrame(object sender, FrameEventArgs e)
        {
            projMatrix = Matrix4.CreateOrthographicOffCenter(worldx, window.Width + worldx, window.Height + worldy, worldy, 0, 1);
            windowRect.X = worldx;
            windowRect.Y = worldy;
            worldRect = new Rectangle(0, 0, worldMaxX, worldMaxY);
        }

        void window_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.ClearColor(Color.DimGray);
            GL.ClearDepth(1);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projMatrix);

            while (renderQueue.Count != 0)
            {
                renderQueue.Dequeue().Invoke();
            }

            window.SwapBuffers();

            //Do tick at END of frame.
            Tick();
            if(sw.IsRunning)
            {
                sw.Stop();
                frameTime = sw.ElapsedMilliseconds;
                sw.Reset();
                sw.Start();
            }
            else
            {
                sw.Start();
            }
        }

        public static Point ScreenToWorldSpace(Point p)
        {
            return new Point(p.X + worldx, p.Y + worldy);
        }

        public static void SetWindowCenter(int x, int y)
        {
            worldx = x - (window.Width / 2);
            worldy = y - (window.Height / 2);
            windowRect = new Rectangle(worldx, worldy, window.Width, window.Height);
        }

        void PrintHello()
        {
            Console.WriteLine("Hello World!");
        }

        void Tick()
        {
            tick++;
            if (tick > 19)
            {
                tick = 0;
            }
        }
    }
}
