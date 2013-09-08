using Microsoft.Xna.Framework;
using MonoGameLib.GUI.Base;
using MonoGameLib.GUI.Components;
using MonoGameLib.GUI.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoGameLib.Core;
using Microsoft.Xna.Framework.Input;

namespace GearArena.Components
{
    public class GameOver : Window
    {
        public string Text 
        {
            get
            {
                return Container.Children.OfType<Label>().First().Text;
            }
            set
            {
                Container.Children.OfType<Label>().First().Text = value;
            }
        }

        public GameOver(Game game)
            : base(game)
        {
            Rectangle rect = game.Window.ClientBounds;

            Container = new Canvas(new Point(rect.X, rect.Y), new Point(rect.Width, rect.Height));

            Container.AddChildren(new Label(String.Empty, "fonts/DefaultFont") { PercentPosition = new Vector2(.5f, .5f), HorizontalOrigin = HorizontalAlign.Center, VerticalOrigin = VerticalAlign.Middle, Color = Color.Black});
            Container.AddChildren(new Label("(Press START or ENTER to go back to the Main Menu)", "fonts/DefaultFont") { PercentPosition = new Vector2(.5f, .55f), HorizontalOrigin = HorizontalAlign.Center, VerticalOrigin = VerticalAlign.Middle, Color = Color.Black });
        }

        public override void Update(GameTime gameTime)
        {
            SoundManager.PlayBGM("Wah Game Loop");
            base.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Start) || Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                (Game as GameMain).ChangeState(GameState.Title);
            }
        }
    }
}
