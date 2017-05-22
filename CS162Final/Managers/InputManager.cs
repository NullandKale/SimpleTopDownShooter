using OpenTK;
using OpenTK.Input;
using System.Drawing;

namespace nullEngine
{
    class InputManager
    {
        private KeyboardState lastKeyState;
        private KeyboardState keyState;

        private MouseState lastMouseState;
        private MouseState currentMouseState;

        public Point mousePos;

        public InputManager()
        {
            mousePos = new Point();
            Game.window.UpdateFrame += update;
        }

        public void update(object sender, FrameEventArgs e)
        {
            if(keyState != null)
            {
                lastKeyState = keyState;
            }
            keyState = Keyboard.GetState();

            if (currentMouseState != null)
            {
                lastMouseState = currentMouseState;
            }
            currentMouseState = Mouse.GetCursorState();
            mousePos = Game.window.PointToClient(new Point(currentMouseState.X, currentMouseState.Y));

        }

        public bool isClickedFalling(MouseButton b)
        {
            return currentMouseState.IsButtonUp(b) && lastMouseState.IsButtonDown(b);
        }

        public bool isClickedRising(MouseButton b)
        {
            return currentMouseState.IsButtonDown(b) && lastMouseState.IsButtonUp(b);
        }

        public bool KeyRisingEdge(Key k)
        {
            if(!isKeystateValid())
            {
                return false;
            }
            else
            {
                return lastKeyState.IsKeyDown(k) && keyState.IsKeyUp(k);
            }
        }

        public bool KeyFallingEdge(Key k)
        {
            if (!isKeystateValid())
            {
                return false;
            }
            else
            {
                return lastKeyState.IsKeyUp(k) && keyState.IsKeyDown(k);
            }
        }

        public bool KeyHeld(Key k)
        {
            if (!isKeystateValid())
            {
                return false;
            }
            else
            {
                return lastKeyState.IsKeyDown(k) && keyState.IsKeyDown(k);
            }
        }

        private bool isKeystateValid()
        {
            return keyState != null && lastKeyState != null;
        }
    }
}
