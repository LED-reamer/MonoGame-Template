using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using UNOnline.Src.Common;
using UNOnline.Src.Game;

namespace UNOnline
{
    public class Game1 : Game
    {
        public Game1()
        {
            GLOB.CM = Content;
            GLOB.GR = new GraphicsDeviceManager(this);
            GLOB.WN = Window;


            GLOB.CM.RootDirectory = "Content";
            IsMouseVisible = true;
            GLOB.WN.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            GLOB.SCR.Init();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            GLOB.SB = new SpriteBatch(GraphicsDevice);
            //loading all scenes
            SCENES.LoadingScene.Load();



            GLOB.CurrentScene = SCENES.LoadingScene;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GLOB.ShouldExit)
            {
                Exit();
            }

            GLOB.GT = gameTime;
            GLOB.IsWindowFocused = IsActive;

            //update currently active scene
            GLOB.CurrentScene.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GLOB.SCR.Bind();

            GLOB.GR.GraphicsDevice.Clear(Color.DeepPink);

            //draw currently active scene
            GLOB.CurrentScene.Draw();

            GLOB.SCR.Unbind();
            GLOB.SCR.Draw();


            base.Draw(gameTime);
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            GLOB.ShouldExit = true;
            base.OnExiting(sender, args);
        }
    }
}