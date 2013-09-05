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

        Texture2D _ammoLight;
        Texture2D _ammoMedium;
        Texture2D _ammoHeavy;
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

            _ammoLight = GameContent.LoadContent<Texture2D>("images/sprites/ammo-light.png");
            _ammoMedium = GameContent.LoadContent<Texture2D>("images/sprites/ammo-medium.png");
            _ammoHeavy = GameContent.LoadContent<Texture2D>("images/sprites/ammo-heavy.png");
        }
        #endregion Constructors

        #region Methods
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Rectangle window)
        {
            Rectangle position_p1 = new Rectangle(window.X, (int) (window.Height * .8f), (int) (window.Width * 0.4f), window.Height / 5);
            Rectangle position_cn = new Rectangle((int)(window.Width * 0.4), (int)(window.Height * .8f), (int)(window.Width * 0.2f), window.Height / 5);
            Rectangle position_p2 = new Rectangle((int) (window.Width * 0.6), (int)(window.Height * .8f), (int)(window.Width * 0.4f), window.Height / 5);

            #region Player 1
            spriteBatch.Draw(_background, position_p1, Color.DarkGray);

            if (Player1 != null)
            {
                Vector2 p1_pos = new Vector2((float)(position_p1.X + position_p1.Width * 0.1f), (float)(position_p1.Y + position_p1.Height * 0.1f));
                spriteBatch.DrawString(_font, "Health: " + Player1.Health, p1_pos, Color.White);

                #region Ammo
                Weapon weapon = Player1.GetChildren<Weapon>();

                if( weapon != null )
                {
                    p1_pos += Vector2.UnitY * 30;
                    spriteBatch.Draw(_ammoLight, p1_pos, null, weapon.SelectedType == AmmoType.Light ? Color.White : Color.Black, 0f, Vector2.Zero, Vector2.One * 4, SpriteEffects.None, 0f);
                    p1_pos +=Vector2.UnitX * 30;
                    spriteBatch.DrawString(_font, "-", p1_pos, Color.White);

                    p1_pos += Vector2.UnitX * 30;
                    spriteBatch.Draw(_ammoMedium, p1_pos, null, weapon.SelectedType == AmmoType.Medium ? Color.White : Color.Black, 0f, Vector2.Zero, Vector2.One * 4, SpriteEffects.None, 0f);
                    p1_pos += Vector2.UnitX * 30;
                    spriteBatch.DrawString(_font, weapon.Ammo[AmmoType.Medium].ToString(), p1_pos, Color.White);

                    p1_pos += Vector2.UnitX * 30;
                    spriteBatch.Draw(_ammoHeavy, p1_pos, null, weapon.SelectedType == AmmoType.Heavy ? Color.White : Color.Black, 0f, Vector2.Zero, Vector2.One * 4, SpriteEffects.None, 0f);
                    p1_pos += Vector2.UnitX * 30;
                    spriteBatch.DrawString(_font, weapon.Ammo[AmmoType.Heavy].ToString(), p1_pos, Color.White);

                }
                #endregion Ammo
            }
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
            spriteBatch.Draw(_background, position_p2, Color.DarkGray);

            if (Player2 != null)
            {
                Vector2 p2_pos = new Vector2((float)(position_p2.X + position_p2.Width * 0.1f), (float)(position_p2.Y + position_p2.Height * 0.1f));
                spriteBatch.DrawString(_font, "Health: " + Player2.Health, p2_pos, Color.White);

                #region Ammo
                Weapon weapon = Player2.GetChildren<Weapon>();

                if (weapon != null)
                {
                    p2_pos += Vector2.UnitY * 30;
                    spriteBatch.Draw(_ammoLight, p2_pos, null, weapon.SelectedType == AmmoType.Light ? Color.White : Color.Black, 0f, Vector2.Zero, Vector2.One * 4, SpriteEffects.None, 0f);
                    p2_pos += Vector2.UnitX * 30;
                    spriteBatch.DrawString(_font, "-", p2_pos, Color.White);

                    p2_pos += Vector2.UnitX * 30;
                    spriteBatch.Draw(_ammoMedium, p2_pos, null, weapon.SelectedType == AmmoType.Medium ? Color.White : Color.Black, 0f, Vector2.Zero, Vector2.One * 4, SpriteEffects.None, 0f);
                    p2_pos += Vector2.UnitX * 30;
                    spriteBatch.DrawString(_font, weapon.Ammo[AmmoType.Medium].ToString(), p2_pos, Color.White);

                    p2_pos += Vector2.UnitX * 30;
                    spriteBatch.Draw(_ammoHeavy, p2_pos, null, weapon.SelectedType == AmmoType.Heavy ? Color.White : Color.Black, 0f, Vector2.Zero, Vector2.One * 4, SpriteEffects.None, 0f);
                    p2_pos += Vector2.UnitX * 30;
                    spriteBatch.DrawString(_font, weapon.Ammo[AmmoType.Heavy].ToString(), p2_pos, Color.White);

                }
                #endregion Ammo
            }
            #endregion Player 2
        }
        #endregion Methods
    }
}
