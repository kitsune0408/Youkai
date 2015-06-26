using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using YoukaiKingdom.GameLogic;
using YoukaiKingdom.GameScreens;
using YoukaiKingdom.Helpers;

namespace YoukaiKingdom.Sprites
{
    class SpecialEffectSprite : AnimatedSprite
    {
        private Animation animation;
        public bool IsOver;

        public SpecialEffectSprite(Texture2D sprite, Dictionary<AnimationKey, Animation> animation) :
            base(sprite, animation)
        {
        }

        public SpecialEffectSprite(Texture2D sprite, Animation animation)
        {
            this.mSpriteTexture = sprite;
            this.animation = animation;
            this.IsOver = true;
            this.STimer = new Timer();
            this.STimer.Elapsed += this.STimerElapsed;
        }

        public Sprite AffectedSprite;
        public Timer STimer { get; set; }

        private void STimerElapsed(object sender, ElapsedEventArgs e)
        {
            this.STimer.Stop();

            this.IsOver = true;
        }


        public void Update(GameTime gameTime)
        {
            if (this.IsOver == false)
            {
                this.animation.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (this.IsOver == false)
            {
                spriteBatch.Draw(
               this.mSpriteTexture,
               this.Position,
               this.animation.CurrentFrameRect,
               Color.White);
            }
        }

        public void StartTimer(int interval)
        {
            this.STimer.Interval = interval;
            this.IsOver = false;
            this.STimer.Start();
        }
    }
}
