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
        #region Fields
        private Keys[] keysToCheck =
        {
            Keys.D0, Keys.D1, Keys.D2, Keys.D3,
            Keys.D4, Keys.D5, Keys.D6, Keys.D7,
            Keys.D8, Keys.D9,
            Keys.A, Keys.B, Keys.C, Keys.D, Keys.E,
            Keys.F, Keys.G, Keys.H, Keys.I, Keys.J,
            Keys.K, Keys.L, Keys.M, Keys.N, Keys.O,
            Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T,
            Keys.U, Keys.V, Keys.W, Keys.X, Keys.Y,
            Keys.Z, Keys.Back, Keys.Space };
        private KeyboardState currentKeyboardState;
        private KeyboardState lastKeyboardState;
        private bool _boxSelected;
        private IInputTextbox _box;
        #endregion

        #region Constructors

        public KeyboardInput(TextBox inputTextBox)
        {
            PrintedText = "";
            Subscriber = inputTextBox;
        }
        #endregion

        #region Properties

        public string PrintedText { get; set; }
        
        internal IInputTextbox Subscriber
        {
            get { return _box; }
            set
            {
                _box = value;
            }
        }

        #endregion

        #region Methods
        public void Update(GameTime gameTime, TextBox inpuTextBox)
        {
            currentKeyboardState = Keyboard.GetState();
            _boxSelected = inpuTextBox.Selected;
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
            if (PrintedText.Length >= 15 && key != Keys.Back)
                return;
            switch (key)
            {
             
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
                    if (_boxSelected)
                    {
                        if (PrintedText.Length != 0)
                            PrintedText = PrintedText.Remove(PrintedText.Length - 1);
                        _box.RecieveTextInput(PrintedText);
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
            
            if (_boxSelected)
            {
                PrintedText += newChar;
                _box.RecieveTextInput(PrintedText);
            }
        }

        private bool CheckKey(Keys key)
        {
            return lastKeyboardState.IsKeyDown(key) && currentKeyboardState.IsKeyUp(key);
        }
        #endregion
    }
}
