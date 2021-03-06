﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GearArena.Behaviors;
using GearArena.Entities;
using GearArena.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLib.Core;
using MonoGameLib.Core.Entities;
using MonoGameLib.Tiled;

namespace GearArena.Components
{
    public enum TurnState
    {
        Playing,
        Shooting,
        NextTurn
    }

    public class Level : DrawableGameComponent
    {
        #region Constants
        private static List<int> IgnoredTerrains = new List<int>(new int[]{0,2});
        #endregion Constants

        #region Attributes
        private SpriteBatch _spriteBatch;

        private TurnState _currentState;
        private Map _map;
        private int _currentPlayer;
        private GameMain _game;
        private GameGUI _gui;

        private Texture2D _background;
        private Texture2D _clouds;

        private Vector2 _cloudPosition;
        private Vector2 _cloudSize;
        private Vector2 _cloudOffset;
        #endregion Attributes

        #region Properties
        public List<Player> Players { get; private set; }
        #endregion Properties

        #region Constructor
        public Level(Game game) : base(game) 
        { 
            _game = game as GameMain;
            _gui = new GameGUI();

            _background = GameContent.LoadContent<Texture2D>("images/backgrounds/background.png");
            _clouds = GameContent.LoadContent<Texture2D>("images/backgrounds/clouds.png");

            _cloudPosition = Vector2.Zero;
            _cloudSize = new Vector2(_clouds.Width, _clouds.Height);
            _cloudOffset = Vector2.UnitX * _cloudSize.X;
        }
        #endregion Constructor

        #region Methods

        #region Override Methods
        protected override void LoadContent()
        {
            Players = new List<Player>();

            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _map = MapLoader.LoadMap("Content/data/maps/earth01.tmx");

            //ITEM: 1. Dois canhões, um em cada lado da tela;
            Player player = new Player(this, new Vector2(100f, 350f), Color.Yellow) { Tag = "Player 1" };
            Player player2 = new Player(this, new Vector2(600f, 350f), Color.LightBlue) { Tag = "Player 2" };

            Players.Add(player);
            Players.Add(player2);

            _gui.Player1 = player;
            _gui.Player2 = player2;

            _currentPlayer = -1;
            _currentState = TurnState.NextTurn;
        }

        public override void Update(GameTime gameTime)
        {
            SoundManager.PlayBGM("Take the Lead");

            _cloudPosition += GlobalForces.Wind;

            if( _cloudPosition.X > _cloudSize.X ) _cloudPosition.X -= _cloudSize.X;
            else if (_cloudPosition.X < 0)  _cloudPosition.X += _cloudSize.X;

            switch( _currentState )
            {
                case TurnState.Playing:
                    foreach (Player en in Players)
                    {
                        en.Update(gameTime);
                    }
                    break;
                case TurnState.Shooting:
                    //TODO: Simplificar?
                    if( Players[_currentPlayer].Children.OfType<Weapon>().First().Children.OfType<Ammo>().Count() == 0 )
                    {
                        _currentState = TurnState.NextTurn;
                    }
                    
                    foreach (Player en in Players)
                    {
                        en.Update(gameTime);
                    }
                    break;
                case TurnState.NextTurn:
                    _currentPlayer = (_currentPlayer+1) % Players.Count;
                    _currentState = TurnState.Playing;

                    foreach (Player en in Players)
                    {
                        en.GetBehavior<ControllableBehavior>().IsActive = false;
                    }

                    //ITEM: 3. Atuarão sobre a bala a força da propulsão do canhão e do vento. O vento terá uma chance de variar a cada tiro;
                    //(VARIAÇÃO DO VENTO)
                    GlobalForces.Wind = new Vector2(RandomNumberGenerator.Next(-1f, 1f), 0f) * RandomNumberGenerator.Next(0f, 10f);

                    Players[_currentPlayer].GetBehavior<ControllableBehavior>().IsActive = true;

                    break;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_background, Vector2.Zero, Color.White);

            _spriteBatch.Draw(_clouds, _cloudPosition, Color.White);

            _spriteBatch.Draw(_clouds, _cloudPosition - _cloudOffset, Color.White);

            _map.Draw(gameTime, _spriteBatch, Vector2.Zero);

            foreach (Player en in Players)
            {
                en.Draw(gameTime, _spriteBatch);
            }
            
            _spriteBatch.End();

            _spriteBatch.Begin();
            _gui.Draw(gameTime, _spriteBatch, _game.Window.ClientBounds);
            _spriteBatch.End();
        }
        #endregion Override Methods

        #region Public Methods
        public bool Collides(Rectangle collisionRect)
        {
            return _map.Collides(collisionRect, IgnoredTerrains);
        }

        public void ChangeState(TurnState state)
        {
            if (state == TurnState.Shooting)
            {
                Players[_currentPlayer].GetBehavior<ControllableBehavior>().IsActive = false;
            }

            _currentState = state;
        }

        public void FinishLevel(Player loser)
        {
            _game.ChangeState(GameState.GameOver, Players.Where( (p) => p!= loser).First());
        }
        #endregion Public Methods

        #endregion Methods
    }
}
