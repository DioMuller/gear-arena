using GearArena.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GearArena.GUI
{
    class PlayerGUI
    {
        #region Attributes
        private SpriteFont _font;
        #endregion Attributes

        #region Properties
        public Player Player { get; private set; }
        #endregion Properties

        #region Constructor
        public PlayerGUI(Player player)
        {
            Player = player;
            _font = GameContent.LoadContent<SpriteFont>("fonts/DefaultFont");
        }
        #endregion Constructor

        #region Methods
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            string health = Player.Health.ToString();
            Vector2 fontSize = _font.MeasureString(health);
            Vector2 position = new Vector2((Player.Position.X + (Player.Sprite.FrameSize.X / 2) - (fontSize.X/2) ), Player.Position.Y - fontSize.Y - 5);

            spriteBatch.DrawString(_font, health, position, Color.Black);
        }
        #endregion Methods
    }
}
