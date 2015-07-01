using Microsoft.Xna.Framework.Graphics;


namespace YoukaiKingdom.Interfaces
{
    interface ISprite
    {       
        Texture2D mSpriteTexture {get;set;}
        Microsoft.Xna.Framework.Rectangle Size { get; set; }
        float Scale { get; set; }
    }
}
