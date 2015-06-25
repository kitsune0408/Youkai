using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using YoukaiKingdom.Helpers;
using YoukaiKingdom.Logic.Models.Inventory;

namespace YoukaiKingdom.Sprites
{
    public class InteractionSprite: Sprite
    {
        private Texture2D mTexture;
        private int width;
        private int height;

        public InteractionSprite(Texture2D tex, InteractionType type ) : base(tex)
        {
            this.mTexture = tex;
            this.width = this.mTexture.Width / 2;
            this.height = this.mTexture.Height;
            this.BeenInteractedWith = false;
            this.InteractionType = type;
        }
        public bool BeenInteractedWith { get; set; }
        public Treasure Treasure { get; set; }
        public InteractionType InteractionType { get; private set; }

        public override void SetCollisionRectangle()
        {
            this.collisionRectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.width, this.height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {       
            spriteBatch.Draw(mSpriteTexture,
                new Rectangle((int)this.Position.X, (int)this.Position.Y, this.width, this.height),
                new Rectangle(BeenInteractedWith ? (mSpriteTexture.Width / 2):0, 0,        
                    mSpriteTexture.Width/2, mSpriteTexture.Height), Color.White);     
        }

    }
}
