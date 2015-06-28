using Microsoft.Xna.Framework.Input;

namespace YoukaiKingdom.Interfaces
{
    internal interface IInputTextbox 
    {
        void RecieveTextInput(string text);
        bool Selected { get; set; }
    }
}
