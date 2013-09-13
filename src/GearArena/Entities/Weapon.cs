using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MonoGameLib.Core.Entities;
using MonoGameLib.Core.Sprites;
using MonoGameLib.Core.Extensions;
using MonoGameLib.Core;

namespace GearArena.Entities
{
    class Weapon : Entity
    {
        #region Constants
        private static Vector2 ParentCenter = new Vector2(0, 0);
        #endregion Constants

        #region Attributes
        private float _diff = 0.01f;
        #endregion Attributes

        #region Properties
        public Dictionary<AmmoType, int> Ammo { get; private set; }
        public AmmoType SelectedType { get; private set; }
        public float Force { get; private set; }
        #endregion Properties

        #region Constructor
        public Weapon() : base()
        {
            Sprite = new Sprite("images/sprites/weapon.png", new Point(12, 24), 0);
            Sprite.Origin = new Vector2(6, 24);
            Sprite.Animations.Add(new Animation("single", 0, 0, 0));
            Sprite.ChangeAnimation(0);

            Force = 0f;

            Ammo = new Dictionary<AmmoType, int>();
            Ammo.Add(AmmoType.Light, -1);
            Ammo.Add(AmmoType.Medium, 5);
            Ammo.Add(AmmoType.Heavy, 3);
        }
        #endregion Constructor

        #region Methods

        public void PrevAmmo()
        {
            int type = (int)SelectedType;
            SelectedType = SelectedType != AmmoType.Light ? (AmmoType)(type + 1) : AmmoType.Light;
        }

        public void NextAmmo()
        {
            int type = (int) SelectedType;
            SelectedType = SelectedType != AmmoType.Heavy? (AmmoType) (type - 1) : AmmoType.Heavy;
        }

        public void SelectAmmo(AmmoType type)
        {
            SelectedType = type;
        }

        public bool Shoot()
        {
            if( Ammo[SelectedType] > 0 || Ammo[SelectedType] == -1 )
            {
                if (this.Parent is Player)
                {
                    Vector2 direction = new Vector2(0, -1).RotateRadians(Rotation);
                    Vector2 force = direction * 100f * Force;
                    Ammo ammo = new Ammo((this.Parent as Player).Level, force, SelectedType) { Parent = this, Color = this.Color };

                    Vector2 positionCentered = (Parent.Position + ParentCenter); //Weapon origin position.
                    Vector2 weaponOffset = new Vector2(ammo.Size.X / 2, Size.Y + ammo.Size.Y).RotateRadians(Rotation); //Add ammo size as offset, relocates bullet Y.

                    ammo.Position = (positionCentered - weaponOffset);

                    Children.Add(ammo);

                    if( Ammo[SelectedType] != -1 ) //If not infinite
                    {
                        Ammo[SelectedType]--; //Used one ammo
                    }
                }

                return true;
            }

            return false;
        }

        public void ChangeForce()
        {
            Force += _diff;

            if( Force < 0f )
            {
                Force = 0f;
                _diff *= -1;
            }
            else if( Force > 1f )
            {
                Force = 1f;
                _diff *= -1;
            }
        }
        #endregion Methods
    }
}
