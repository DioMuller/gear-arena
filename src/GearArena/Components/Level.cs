using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GearArena.Behaviors;
using GearArena.Entities;
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
        #endregion Attributes

        #region Properties
        public List<Player> Players { get; private set; }
        #endregion Properties

        #region Constructor
        public Level(Game game) : base(game) {}
        #endregion Constructor

        #region Methods

        #region Override Methods
        protected override void LoadContent()
        {
            Players = new List<Player>();

            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _map = MapLoader.LoadMap("Content/data/maps/earth01.tmx");
            Player player = new Player(this) { Position = new Vector2(100f, 100f) };
            Player player2 = new Player(this) { Position = new Vector2(500f, 100f) };

            Players.Add(player);
            Players.Add(player2);

            _currentPlayer = -1;
            _currentState = TurnState.NextTurn;
        }

        public override void Update(GameTime gameTime)
        {
            SoundManager.PlayBGM("Cold Funk");

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

                    GlobalForces.Wind = new Vector2(RandomNumberGenerator.Next(-1f, 1f), 0f);

                    Players[_currentPlayer].GetBehavior<ControllableBehavior>().IsActive = true;

                    break;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Matrix.CreateScale(2f));
            _map.Draw(gameTime, _spriteBatch, Vector2.Zero);

            foreach (Player en in Players)
            {
                en.Draw(gameTime, _spriteBatch);
            }
            
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
        #endregion Public Methods

        #endregion Methods
    }
}
