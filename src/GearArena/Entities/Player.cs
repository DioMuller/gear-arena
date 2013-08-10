using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GearArena.Behaviors;
using GearArena.Components;
using Microsoft.Xna.Framework;
using MonoGameLib.Core.Entities;
using MonoGameLib.Core.Input;
using MonoGameLib.Core.Sprites;

namespace GearArena.Entities
{
    public class Player : Entity
    {
        #region Properties
        public Level Level { get; private set; }
        #endregion Properties

        #region Constructor
        public Player(Level level) : base()
        {
            Behaviors.Add(new PhysicsBehavior(this) { Mass = 10f, Rotate = false, Gravity = new Vector2(0f, 9.8f), Friction = new Vector2(0.01f, 0f) } );
            Behaviors.Add(new ControllableBehavior(this, new KeyboardInput()));
            Behaviors.Add(new CollidableBehavior(this, level));

            Level = level;

            Sprite = new Sprite("images/sprites/mecha.png", new Point(32, 32), 100);

            Sprite.Animations.Add( new Animation("idle", 0, 0, 0) );
            Sprite.Animations.Add( new Animation("walking", 0, 0, 3) );

            Sprite.ChangeAnimation(0);
            
            Children.Add(new Weapon() { Parent = this, Position = new Vector2(8,16), FollowParent = true } );
        }
        #endregion Constructor
    }
}
