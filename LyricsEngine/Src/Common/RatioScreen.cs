using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNOnline.Src.Common
{
    internal class RatioScreen
    {
        private RenderTarget2D _scene;
        public Rectangle dst;

        public void Init()
        {
            _scene = new RenderTarget2D(GLOB.GR.GraphicsDevice, GLOB.VWidth, GLOB.VHeight);
        }

        public void Bind()
        {
            GLOB.GR.GraphicsDevice.SetRenderTarget(_scene);
        }

        public void Unbind()
        {
            GLOB.GR.GraphicsDevice.SetRenderTarget(null);
        }

        public void Draw()
        {
            GLOB.GR.GraphicsDevice.SetRenderTarget(null);
            // draw render target
            float outputAspect =GLOB.WN.ClientBounds.Width / (float)GLOB.WN.ClientBounds.Height;
            float preferredAspect = GLOB.VWidth / (float)GLOB.VHeight;

            if (outputAspect <= preferredAspect)
            {
                // output is taller than it is wider, bars on top/bottom
                int presentHeight = (int)((GLOB.WN.ClientBounds.Width / preferredAspect) + 0.5f);
                int barHeight = (GLOB.WN.ClientBounds.Height - presentHeight) / 2;

                dst = new Rectangle(0, barHeight, GLOB.WN.ClientBounds.Width, presentHeight);
            }
            else
            {
                // output is wider than it is tall, bars left/right
                int presentWidth = (int)((GLOB.WN.ClientBounds.Height * preferredAspect) + 0.5f);
                int barWidth = (GLOB.WN.ClientBounds.Width - presentWidth) / 2;

                dst = new Rectangle(barWidth, 0, presentWidth, GLOB.WN.ClientBounds.Height);
            }

            

            // clear to get black bars
            GLOB.GR.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 1.0f, 0);

            //_spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, samplerState: SamplerState.PointClamp);
            GLOB.SB.Begin(SpriteSortMode.Deferred, BlendState.Opaque);
            GLOB.SB.Draw(_scene, dst, Color.White);
            GLOB.SB.End();
        }
    }
}
