using Microsoft.Xna.Framework.Input;

namespace YoukaiKingdom.Interfaces
{
    internal interface IInputTextbox 
    {
        void RecieveTextInput(char inputChar);
        void RecieveTextInput(string text);
        bool Selected { get; set; }
    }
}
