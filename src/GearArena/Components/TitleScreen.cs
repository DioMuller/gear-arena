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
    public class TitleScreen : Window
    {
        public bool WaitNext { get; set; }

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

        public TitleScreen(Game game)
            : base(game)
        {
            Rectangle rect = game.Window.ClientBounds;

            Container = new Canvas(new Point(rect.X, rect.Y), new Point(rect.Width, rect.Height));

            Container.AddChildren(new Image("images/title.png") { Position = new Point(0,0) });
            Container.AddChildren(new Label("Press START or ENTER to play", "fonts/DefaultFont") { PercentPosition = new Vector2(.5f, .5f), HorizontalOrigin = HorizontalAlign.Center, VerticalOrigin = VerticalAlign.Middle, Color = Color.White });
            Container.AddChildren(new Label("Diogo Muller de Miranda", "fonts/DefaultFont") { PercentPosition = new Vector2(.5f, .9f), HorizontalOrigin = HorizontalAlign.Center, VerticalOrigin = VerticalAlign.Middle, Color = Color.White});
            Container.AddChildren(new Label("Tileset by Melanie Young Yee - Music by Kevin MacLeod", "fonts/DefaultFont") { PercentPosition = new Vector2(.5f, .95f), HorizontalOrigin = HorizontalAlign.Center, VerticalOrigin = VerticalAlign.Middle, Color = Color.White });
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            SoundManager.PlayBGM("Exhilarate");

            if( GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Start) || Keyboard.GetState().IsKeyDown(Keys.Enter) )
            {
                if(!WaitNext) (Game as GameMain).ChangeState(GameState.Playing);
            }
            else
            {
                WaitNext = false;
            }
        }
    }
}
