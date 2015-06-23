using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
