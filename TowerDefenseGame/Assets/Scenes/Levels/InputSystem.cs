using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Game.Engine
{
    class InputSystem
    {

        private static InputSystem instance = null;
        public static InputSystem Instance
        {
            get
            {
                instance = (instance == null) ? new InputSystem() : instance;
                return instance;
            }
        }

        public enum eKey { Left, Right, Space };
        BitArray inputBits = new BitArray(3);

        public bool GetKeyState(eKey key) { return inputBits[(int)key]; }

        public void OnKeyDown(Key key)
        {
            switch (key)
            {
                case Key.Left:
                    inputBits[0] = true;
                    break;
                case Key.Right:
                    inputBits[1] = true;
                    break;
                case Key.Space:
                    inputBits[2] = true;
                    break;
            }
        }

        public void OnKeyUp(Key key)
        {
            switch (key)
            {
                case Key.Left:
                    inputBits[0] = false;
                    break;
                case Key.Right:
                    inputBits[1] = false;
                    break;
                case Key.Space:
                    inputBits[2] = false;
                    break;
            }

        }

    }
}