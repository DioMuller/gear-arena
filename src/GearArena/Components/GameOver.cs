using Microsoft.Xna.Framework;
using MonoGameLib.GUI.Base;
using MonoGameLib.GUI.Components;
using MonoGameLib.GUI.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GearArena.Components
{
    public class GameOver : Window
    {
        public GameOver(Game game)
            : base(game)
        {
            Rectangle rect = game.Window.ClientBounds;

            Container = new Canvas(new Point(rect.X, rect.Y), new Point(rect.Width, rect.Height));

            Container.AddChildren(new Image("images/sprites/mecha.png") { PercentPosition = new Vector2(.5f, .5f), HorizontalOrigin = HorizontalAlign.Center, VerticalOrigin = VerticalAlign.Middle});
        }
    }
}
