using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GearArena.Behaviors;
using GearArena.Components;
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
        #region Attributes
        /// <summary>
        /// Initial propulstion time.
        /// </summary>
        public float _propulsionTime;
        #endregion Attributes

        #region Properties
        /// <summary>
        /// Ammo type.
        /// </summary>
        public AmmoType Type { get; private set; }
        #endregion Properties

        #region Constructor
        public Ammo(Level level, Vector2 initialForce, AmmoType type) : base()
        {
            Behaviors.Add(new PhysicsBehavior(this) { Mass = 3f - (float) type, Rotate = true, Gravity = new Vector2(0f, 9.8f), Friction = new Vector2( 0f, 0f ) });
            Behaviors.Add(new CollidableBehavior(this, level));

            GetBehavior<PhysicsBehavior>().Momentum = initialForce;

            Sprite = new Sprite("images/sprites/ammo.png", new Point(12, 12), 0);
            Sprite.Origin = new Vector2(6,6);

            Sprite.Animations.Add(new Animation("heavy", 0, 0, 0));
            Sprite.Animations.Add(new Animation("medium", 1, 0, 0));
            Sprite.Animations.Add(new Animation("light", 2, 0, 0));

            _propulsionTime = 1f;

            Sprite.ChangeAnimation((int) type);
        }
        #endregion Constructor
    }
}
