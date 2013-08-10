using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GearArena.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLib.Core;
using MonoGameLib.Core.Entities;
using MonoGameLib.Tiled;

namespace GearArena.Components
{
    public class Level : DrawableGameComponent
    {
        #region Constants
        private static List<int> IgnoredTerrains = new List<int>(new int[]{0,2});
        #endregion Constants

        #region Attributes
        private SpriteBatch _spriteBatch;

        private Map _map;
        #endregion Attributes

        #region Properties
        public List<Entity> Entities { get; private set; }
        #endregion Properties

        #region Constructor
        public Level(Game game) : base(game) {}
        #endregion Constructor

        #region Methods

        #region Override Methods
        protected override void LoadContent()
        {
            Entities = new List<Entity>();

            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _map = MapLoader.LoadMap("Content/data/maps/earth01.tmx");
            Player player = new Player(this) { Position = new Vector2(100f, 100f) };

            Entities.Add(player);
        }

        public override void Update(GameTime gameTime)
        {
            SoundManager.PlayBGM("Cold Funk");

            foreach( Entity en in Entities )
            {
                en.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, null, Matrix.CreateScale(2f));
            _map.Draw(gameTime, _spriteBatch, Vector2.Zero);
            
            foreach( Entity en in Entities )
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
        #endregion Public Methods

        #endregion Methods
    }
}
