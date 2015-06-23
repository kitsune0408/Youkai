using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using YoukaiKingdom.GameLogic;
using YoukaiKingdom.GameScreens;
using YoukaiKingdom.Helpers;

namespace YoukaiKingdom.Sprites
{
    class SpecialEffectSprite: AnimatedSprite
    {
        public SpecialEffectSprite(Texture2D sprite, Dictionary<AnimationKey, Animation> animation) : 
            base(sprite, animation)
        {
        }

    }
}
