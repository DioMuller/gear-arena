using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GearArena.Behaviors;
using Microsoft.Xna.Framework;
using MonoGameLib.Core.Entities;
using MonoGameLib.Core.Input;
using MonoGameLib.Core.Sprites;

namespace GearArena.Entities
{
    public class Player : Entity
    {
        #region Constructor
        public Player() : base()
        {
            Behaviors.Add(new PhysicsBehavior(this) { IsActive = true, Mass = 100, Rotate = false, Gravity = new Vector2(0f, 9.8f) } );
            Behaviors.Add(new ControllableBehavior(this, new KeyboardInput()));
            Behaviors.Add(new CollidableBehavior(this));

            Sprite = new Sprite("images/sprites/mecha.png", new Point(32, 32), 100);

            Sprite.Animations.Add( new Animation("idle", 0, 0, 0) );
            Sprite.Animations.Add( new Animation("walking", 0, 0, 3) );

            Sprite.ChangeAnimation(0);
        }
        #endregion Constructor
    }
}
