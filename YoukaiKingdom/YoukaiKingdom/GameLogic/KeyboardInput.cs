using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using YoukaiKingdom.GameScreens;
using YoukaiKingdom.Interfaces;

namespace YoukaiKingdom.GameLogic
{
    public class KeyboardInput
    {
        public string printedText;

        Keys[] keysToCheck = new Keys[] {
            Keys.A, Keys.B, Keys.C, Keys.D, Keys.E,
            Keys.F, Keys.G, Keys.H, Keys.I, Keys.J,
            Keys.K, Keys.L, Keys.M, Keys.N, Keys.O,
            Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T,
            Keys.U, Keys.V, Keys.W, Keys.X, Keys.Y,
            Keys.Z, Keys.Back, Keys.Space };
        KeyboardState currentKeyboardState;
        KeyboardState lastKeyboardState;
        private BaseGameScreen parentScreen;
        private bool BoxSelected;
        //public string PrintedText ;

        IInputTextbox _box;
        internal IInputTextbox Subscriber
        {
            get { return _box; }
            set
            {
                _box = value;
            }
        }



        public KeyboardInput(BaseGameScreen screen, TextBox inputTextBox)
        {
            parentScreen = screen;
            printedText = "";
            Subscriber = inputTextBox;
        }

        public void Update(GameTime gameTime, TextBox inpuTextBox)
        {
            currentKeyboardState = Keyboard.GetState();
            BoxSelected = inpuTextBox.Selected;
            foreach (Keys key in keysToCheck)
            {
                if (CheckKey(key))
                {
                  AddKeyToText(key);
                    break;
                }
            }            
            lastKeyboardState = currentKeyboardState;
        }
        private void AddKeyToText(Keys key)
        {
            string newChar = "";
            if (printedText.Length >= 20 && key != Keys.Back)
                return;
            switch (key)
            {
                case Keys.A:
                    newChar += "a";
                    break;
                case Keys.B:
                    newChar += "b";
                    break;
                case Keys.C:
                    newChar += "c";
                    break;
                case Keys.D:
                    newChar += "d";
                    break;
                case Keys.E:
                    newChar += "e";
                    break;
                case Keys.F:
                    newChar += "f";
                    break;
                case Keys.G:
                    newChar += "g";
                    break;
                case Keys.H:
                    newChar += "h";
                    break;
                case Keys.I:
                    newChar += "i";
                    break;
                case Keys.J:
                    newChar += "j";
                    break;
                case Keys.K:
                    newChar += "k";
                    break;
                case Keys.L:
                    newChar += "l";
                    break;
                case Keys.M:
                    newChar += "m";
                    break;
                case Keys.N:
                    newChar += "n";
                    break;
                case Keys.O:
                    newChar += "o";
                    break;
                case Keys.P:
                    newChar += "p";
                    break;
                case Keys.Q:
                    newChar += "q";
                    break;
                case Keys.R:
                    newChar += "r";
                    break;
                case Keys.S:
                    newChar += "s";
                    break;
                case Keys.T:
                    newChar += "t";
                    break;
                case Keys.U:
                    newChar += "u";
                    break;
                case Keys.V:
                    newChar += "v";
                    break;
                case Keys.W:
                    newChar += "w";
                    break;
                case Keys.X:
                    newChar += "x";
                    break;
                case Keys.Y:
                    newChar += "y";
                    break;
                case Keys.Z:
                    newChar += "z";
                    break;
                case Keys.Space:
                    newChar += " ";
                    break;
                case Keys.Back:
                    if (BoxSelected)
                    {
                        if (printedText.Length != 0)
                            printedText = printedText.Remove(printedText.Length - 1);
                        _box.RecieveTextInput(printedText);
                    }
                    return;
            }
            if (currentKeyboardState.IsKeyDown(Keys.RightShift) ||
                currentKeyboardState.IsKeyDown(Keys.LeftShift))
            {
                newChar = newChar.ToUpper();
            }
            
            if (BoxSelected)
            {
                printedText += newChar;
                _box.RecieveTextInput(printedText);
            }
        }

        private bool CheckKey(Keys theKey)
        {
            return lastKeyboardState.IsKeyDown(theKey) && currentKeyboardState.IsKeyUp(theKey);
        }

    }
}
