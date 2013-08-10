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
    public enum AmmoType
    {
        Light = 2,
        Medium = 1,
        Heavy = 0
    }

    public class Ammo : Entity
    {
        #region Properties
        /// <summary>
        /// Ammo type.
        /// </summary>
        public AmmoType Type { get; private set; }
        #endregion Properties

        #region Constructor
        public Ammo(Vector2 initialForce, AmmoType type) : base()
        {
            Behaviors.Add(new PhysicsBehavior(this) { Mass = 3f - (float) type, Rotate = true, Gravity = new Vector2(0f, 9.8f), Friction = new Vector2( 0f, 0f ) });
            Behaviors.Add(new CollidableBehavior(this));

            GetBehavior<PhysicsBehavior>().ConstantForces["Propulsion"] = initialForce;

            Sprite = new Sprite("images/sprites/ammo.png", new Point(12, 12), 0);

            Sprite.Animations.Add(new Animation("heavy", 0, 0, 0));
            Sprite.Animations.Add(new Animation("medium", 1, 0, 0));
            Sprite.Animations.Add(new Animation("light", 2, 0, 0));

            Sprite.ChangeAnimation((int) type);
        }
        #endregion Constructor
    }
}
