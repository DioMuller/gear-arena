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
        #region Constants
        private static Vector2 ParentCenter = new Vector2(16, 16);
        #endregion Constants

        #region Constructor
        public Weapon() : base()
        {
            Sprite = new Sprite("images/sprites/weapon.png", new Point(12, 24), 0);
            Sprite.Origin = new Vector2(6, 24);
            Sprite.Animations.Add(new Animation("single", 0, 0, 0));
            Sprite.ChangeAnimation(0);
        }
        #endregion Constructor

        #region Methods
        public void Shoot(float force_n, AmmoType type)
        { 
            Vector2 direction = new Vector2(0, -1).RotateRadians(Rotation);
            Vector2 force = direction * force_n;
            Ammo ammo = new Ammo((this.Parent as Player).Level, force, type) { Parent = this };

            Vector2 positionCentered = (Parent.Position + ParentCenter); //Weapon origin position.
            Vector2 weaponOffset = new Vector2(ammo.Size.X / 2, Size.Y + ammo.Size.Y).RotateRadians(Rotation); //Add ammo size as offset, relocates bullet Y.

            ammo.Position = (positionCentered - weaponOffset);

            if( this.Parent is Player )
            {
                Children.Add(ammo);
            }
        }
        #endregion Methods
    }
}
