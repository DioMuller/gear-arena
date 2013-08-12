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
            Vector2 direction = new Vector2(0, -1).RotateRadians(Rotation);
            Vector2 force = direction * force_n;
            Vector2 ammoOffset = direction * 32;

            if( this.Parent is Player )
            {
                Children.Add(new Ammo((this.Parent as Player).Level, force, type) { Parent = this, Position = ammoOffset + (Position + Parent.Position) });
            }
        }
        #endregion Methods
    }
}
