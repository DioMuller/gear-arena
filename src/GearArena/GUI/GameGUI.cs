using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GearArena.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLib.Core;
using MonoGameLib.Core.Extensions;

namespace GearArena.GUI
{
    class GameGUI
    {
        #region Attributes
        private Texture2D _background;
        private SpriteFont _font;
        private Texture2D _arrow;
        #endregion Attributes

        #region Properties
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        #endregion Properties

        #region Constructors
        public GameGUI()
        {
            _background = GameContent.LoadContent<Texture2D>("images/gui/background.png");
            _arrow = GameContent.LoadContent<Texture2D>("images/gui/arrow.png");
            _font = GameContent.LoadContent<SpriteFont>("fonts/DefaultFont");
        }
        #endregion Constructors

        #region Methods
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Rectangle window)
        {
            Rectangle position_p1 = new Rectangle(window.X, (int) (window.Height * .8f), (int) (window.Width * 0.4f), window.Height / 5);
            Rectangle position_cn = new Rectangle((int)(window.Width * 0.4), (int)(window.Height * .8f), (int)(window.Width * 0.2f), window.Height / 5);
            Rectangle position_p2 = new Rectangle((int) (window.Width * 0.6), (int)(window.Height * .8f), (int)(window.Width * 0.4f), window.Height / 5);

            #region Player 1
            spriteBatch.Draw(_background, position_p1, Color.Red);
            #endregion Player 1

            #region Center
            spriteBatch.Draw(_background, position_cn, Color.White);

            #region Wind
            Vector2 arrow_pos = new Vector2( (float) (position_cn.X + position_cn.Width * 0.2f), (float) (position_cn.Y + position_cn.Height * 0.2f) );
            spriteBatch.Draw(_arrow, arrow_pos, null, Color.CornflowerBlue, GlobalForces.Wind.GetAngle(), new Vector2(16, 16), 1f, SpriteEffects.None, 0f); 
            spriteBatch.DrawString(_font, "W", arrow_pos + new Vector2(-8,-16), Color.White);
            spriteBatch.DrawString(_font, Math.Abs(Math.Round(GlobalForces.Wind.X + GlobalForces.Wind.Y, 1, MidpointRounding.AwayFromZero)) + " m/s", arrow_pos + new Vector2(40, -16), Color.White);
            #endregion Wind

            #region Gravity
            arrow_pos = new Vector2((float)(position_cn.X + position_cn.Width * 0.2f), (float)(position_cn.Y + position_cn.Height * 0.4f));
            spriteBatch.Draw(_arrow, arrow_pos, null, Color.DarkRed, GlobalForces.Gravity.GetAngle(), new Vector2(16, 16), 1f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(_font, "G", arrow_pos + new Vector2(-8, -16), Color.White);
            spriteBatch.DrawString(_font, Math.Abs(Math.Round(GlobalForces.Gravity.X + GlobalForces.Gravity.Y, 1, MidpointRounding.AwayFromZero)) + " m/s", arrow_pos + new Vector2(40, -16), Color.White);
            #endregion Gravity



            #endregion Center

            #region Player 2
            spriteBatch.Draw(_background, position_p2, Color.Blue);
            #endregion Player 2
        }
        #endregion Methods
    }
}
