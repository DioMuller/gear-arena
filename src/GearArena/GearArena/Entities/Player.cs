using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GearArena.Behaviors;
using Microsoft.Xna.Framework;
using MonoGameLib.Core.Entities;
using MonoGameLib.Core.Sprites;

namespace GearArena.Entities
{
    public class Player : CollidableEntity
    {
        #region Constructor
        public Player() : base()
        {
            Behaviors.Add(new SolidBodyBehavior(this) { IsActive = true, Mass = 100, Rotate = false, Gravity = new Vector2(0f, 9.8f) } );
            
            Sprite = new Sprite("images/sprites/mecha.png", new Point(32, 32), 100);

            Sprite.Animations.Add( new Animation("idle", 0, 0, 0) );
            Sprite.Animations.Add( new Animation("walking", 0, 0, 3) );

            Sprite.ChangeAnimation(0);
        }
        #endregion Constructor
    }
}
