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
            Keys.D0, Keys.D1, Keys.D2,
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
             
                 //   newChar += Keys.A.ToString();
                 //   break;
                case Keys.D0:
                    newChar += "0";
                    break;
                case Keys.D1:
                    newChar += "1";
                    break;
                case Keys.D2:
                    newChar += "2";
                    break;
                case Keys.D3:
                    newChar += "3";
                    break;
                case Keys.D4:
                    newChar += "4";
                    break;
                case Keys.D5:
                    newChar += "5";
                    break;
                case Keys.D6:
                    newChar += "6";
                    break;
                case Keys.D7:
                    newChar += "7";
                    break;
                case Keys.D8:
                    newChar += "8";
                    break;
                case Keys.D9:
                    newChar += "9";
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
                default:
                    {
                        newChar += key.ToString().ToLower();
                        break;
                    } 
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
