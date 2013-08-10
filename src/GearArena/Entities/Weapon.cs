using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MonoGameLib.Core.Entities;
using MonoGameLib.Core.Sprites;
using MonoGameLib.Core.Extensions;

namespace GearArena.Entities
{
    class Weapon : Entity
    {
        #region Constructor
        public Weapon() : base()
        {
            Sprite = new Sprite("images/sprites/weapon.png", new Point(16, 16), 0);
            Sprite.Origin = new Vector2(8, 16);
            Sprite.Animations.Add(new Animation("single", 0, 0, 0));
            Sprite.ChangeAnimation(0);
        }
        #endregion Constructor

        #region Methods
        public void Shoot(float force_n, AmmoType type)
        {
            float offsetAngle = Sprite.Origin.GetAngle();
            Vector2 ammoOffset = new Vector2(0, 1).RotateRadians(Rotation - offsetAngle);
            Vector2 force = ammoOffset * force_n;

            Children.Add(new Ammo( force, type ) { Parent = this, Position = ammoOffset * 20 + (Position + Parent.Position) } );
        }
        #endregion Methods
    }
}
