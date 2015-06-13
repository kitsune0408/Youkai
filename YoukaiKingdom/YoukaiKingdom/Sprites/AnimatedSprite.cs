using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using YoukaiKingdom.Helpers;
using YoukaiKingdom.GameLogic;
using YoukaiKingdom.GameScreens;

namespace YoukaiKingdom.Sprites
{
    class AnimatedSprite: Sprite
    {
        #region Fields

        Dictionary<AnimationKey, Animation> animations;
        AnimationKey currentAnimation;
        bool isAnimating;        
        float speed = 2.0f;

        #endregion

        #region Constructors

        public AnimatedSprite(Texture2D sprite, Dictionary<AnimationKey, Animation> animation):base(sprite)
        {
            this.mSpriteTexture = sprite;
            animations = new Dictionary<AnimationKey, Animation>();

            foreach (AnimationKey key in animation.Keys)
                animations.Add(key, (Animation)animation[key].Clone());
        }

        #endregion

        #region Properties

        public AnimationKey CurrentAnimation
        {
            get { return currentAnimation; }
            set { currentAnimation = value; }
        }

        public bool IsAnimating
        {
            get { return isAnimating; }
            set { isAnimating = value; }
        }

        public int Width
        {
            get { return animations[currentAnimation].FrameWidth; }
        }

        public int Height
        {
            get { return animations[currentAnimation].FrameHeight; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = MathHelper.Clamp(speed, 1.0f, 16.0f); }
        }

        #endregion

        #region Methods

        public override void Update(GameTime gameTime, GamePlayScreen mGame, Vector2 speed, Vector2 direction)
        {
            if (isAnimating)
            {
                animations[currentAnimation].Update(gameTime);
            }          
            base.Update(gameTime, mGame, speed, direction);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                this.mSpriteTexture,
                this.Position,
                animations[currentAnimation].CurrentFrameRect,
                Color.White);
        }

        #endregion

    }
}
