#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using MonoGameLib.Tiled;
using MonoGameLib.Core;
using GearArena.Entities;
using GearArena.Behaviors;
using GearArena.Components;
#endregion

namespace GearArena
{
    public enum GameState
    {
        Title,
        Tutorial,
        Playing,
        GameOver
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameMain : Game
    {
        private GraphicsDeviceManager _graphics;

        private TitleScreen _titleScreen;
        private Controls _controls;
        private Level _level;
        private GameOver _gameOver;

        public GameMain()
            : base()
        {
            _graphics = new GraphicsDeviceManager(this);

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.IsFullScreen = false;

            _graphics.ApplyChanges();

            SoundManager.BGMFolder = "audio/bgm";
            SoundManager.SEFolder = "audio/se";

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            GameContent.Initialize(Content); 

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _titleScreen = new TitleScreen(this);
            _controls = new Controls(this);
            _level = new Level(this);
            _gameOver = new GameOver(this);

            Components.Add(_titleScreen);
            Components.Add(_controls);
            Components.Add(_level);
            Components.Add(_gameOver);

            ChangeState(GameState.Title);

            GlobalForces.Gravity = new Vector2(0.0f, 9.8f);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public void ChangeState(GameState state, object obj = null)
        {
            switch( state )
            {
                case GameState.Title:
                    _titleScreen.WaitNext = true; //So Enter doesn't skip directly to the Playing state.
                    break;
                case GameState.Tutorial:
                    _controls.WaitNext = true; //So Enter doesn't skip directly to the Playing state.
                    break;
                case GameState.Playing:
                    Components.Remove(_level);
                    _level = new Level(this);
                    Components.Add(_level);
                    break;
                case GameState.GameOver:
                    _gameOver.Text = (obj as Player).Tag + " is the winner!";
                    _gameOver.Background = (obj as Player).Color;
                    break;
            }

            _titleScreen.Enabled = (state == GameState.Title);
            _titleScreen.Visible = (state == GameState.Title);

            _controls.Enabled = (state == GameState.Tutorial);
            _controls.Visible = (state == GameState.Tutorial);

            _level.Enabled = (state == GameState.Playing);
            _level.Visible = (state == GameState.Playing);

            _gameOver.Enabled = (state == GameState.GameOver);
            _gameOver.Visible = (state == GameState.GameOver);
        }
    }
}
