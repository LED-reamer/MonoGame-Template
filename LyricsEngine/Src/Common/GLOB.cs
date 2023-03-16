using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNOnline.Src.Common
{
    internal class GLOB
    {
        public const int VWidth = 1920;
        public const int VHeight = 1080;

        public static bool IsWindowFocused = false;
        public static float MasterVolume = 0.1f;
        public static bool ShouldExit = false;

        public static ContentManager CM;
        public static GameWindow WN;
        public static SpriteBatch SB;
        public static GraphicsDeviceManager GR;
        public static GameTime GT = new GameTime();

        public static RatioScreen SCR = new RatioScreen();

        public static Scene CurrentScene = new Scene();
        public static SoundEffectInstance CurrentSound;

        #region useful functions
        public static float PI = 3.141f;
        public static double GetAngle(Vector2 p1, Vector2 p2)
        {
            double angle = Math.Atan2(p2.Y - p1.Y, p2.X - p1.X) * 180 / PI;
            return angle;
        }
        public static Vector2 GetWorldMousePosition()
        {
            Point windowPosition = Mouse.GetState().Position;

            float sx = windowPosition.X - SCR.dst.X;
            float sy = windowPosition.Y - SCR.dst.Y;

            sx /= (float)SCR.dst.Width;
            sy /= (float)SCR.dst.Height;

            sx *= (float)VWidth;
            sy *= (float)VHeight;

            return new Vector2(sx, sy);//Vector2(sx - Camera.Transform.Translation.X, sy - Camera.Transform.Translation.Y);
        }
        #endregion
    }
}
