using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNOnline.Src.Common
{
    internal class Sprite
    {
        //basic rendering
        public Texture2D Texture;
        public bool topleftorientated = false;
        public Vector2 Position = Vector2.Zero;
        public Vector2 Origin = Vector2.Zero;
        public float Rotation = 0f;
        public SpriteEffects spriteEffects = SpriteEffects.None;
        public Color Color = Color.White;
        public float Scale = 1.0f;
        public float Layer = 0f;
        public bool hidden = false;
        public Rectangle Rectangle;
        public Rectangle SourceRectangle;

        //animation
        public Vector2 FrameDimenions = new Vector2(32, 32);
        public float AnimationTime = 100f;
        public int PlayAnimation = -1;
        public int TotalFrames;
        public int BeginFrame = 0;
        private float _timer = 0f;
        private int _currentFrame;

        public Sprite(string texture)
        {
            Texture = GLOB.CM.Load<Texture2D>(texture);
        }

        public void SetOriginToTextureMid()
        {
            Origin.X = Texture.Width / 2;
            Origin.Y = Texture.Height / 2;
        }
        public bool IsMouseOnTexture()//TODO also do rotating rectangles somehow?
        {
            if (Rectangle.Contains(GLOB.GetWorldMousePosition()))
                return true;
            else
                return false;
        }
        public void Rotate(float angle)
        {
            Rotation += MathHelper.ToRadians(angle);
        }
        public void RotateTo(Vector2 Pos)
        {
            Vector2 dPos = Pos - this.Position;
            dPos.Normalize();
            Rotation = (float)Math.Atan2((double)dPos.Y, (double)dPos.X) + MathHelper.PiOver2;//(float)Math.Atan2(dPos.Y, dPos.X);
        }
        public void Animation(int animation = 0, float time = 100f, float width = 32, float height = 32, int frames = 0, int beginframe = 0)
        {
            PlayAnimation = animation;
            AnimationTime = time;
            TotalFrames = frames;
            BeginFrame = beginframe;
            FrameDimenions = new Vector2(width, height);
        }
        public void Draw()
        {
            if (hidden) return;


            #region animation timers
            _timer += (float)GLOB.GT.ElapsedGameTime.TotalMilliseconds;
            if (_timer >= AnimationTime)
            {
                _timer = 0f;
                _currentFrame++;
            }

            if (_currentFrame == TotalFrames)
            {
                _timer = 0f;
                _currentFrame = 0;
            }
            #endregion

            if (topleftorientated)
            {
                Origin = Vector2.Zero;
            }

            if (PlayAnimation == -1)//no animation set
            {
                GLOB.SB.Draw(Texture, Position, null, Color, Rotation, Origin, Scale, spriteEffects, Layer);

                //Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
                //Rectangle = new Rectangle(((int)Position.X - Texture.Width / 2), ((int)Position.Y - Texture.Height / 2), Texture.Width * (int)Scale, Texture.Height * (int)Scale);
            }
            else//animate
            {
                SourceRectangle = new Rectangle((int)FrameDimenions.X * _currentFrame + (int)FrameDimenions.X * BeginFrame + BeginFrame - BeginFrame / 2, (int)FrameDimenions.Y * PlayAnimation + PlayAnimation - PlayAnimation / 2, (int)FrameDimenions.X, (int)FrameDimenions.Y);
                GLOB.SB.Draw(Texture, Position, SourceRectangle, Color, Rotation, Origin, Scale, spriteEffects, Layer);

                //Origin = new Vector2(SourceRectangle.Width / 2, SourceRectangle.Height / 2);
                //Rectangle = new Rectangle((int)Position.X - SourceRectangle.Width, (int)Position.Y - SourceRectangle.Height, SourceRectangle.Width * (int)Scale, SourceRectangle.Height * (int)Scale);
            }
        }
    }
}
