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
using GearArena.GUI;
using Microsoft.Xna.Framework.Graphics;

namespace GearArena.Entities
{
    public class Player : Entity
    {
        #region Properties
        public Level Level { get; private set; }
        public int Health { get; private set; }
        public Dictionary<AmmoType, int> Ammo { get; private set; }
        public AmmoType SelectedType { get; private set; }

        public bool Mirrored
        {
            get
            {
                return (Sprite.Effect == SpriteEffects.FlipHorizontally);
            }
            set
            {
                Sprite.Effect = (value) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            }
        }
        #endregion Properties

        #region Constructor
        public Player(Level level, Vector2 position) : base()
        {
            Level = level;

            Health = 100;

            Sprite = new Sprite("images/sprites/mecha.png", new Point(32, 32), 100);
            Sprite.Origin = new Vector2(16, 16);
            Position = position + Sprite.Origin;

            Sprite.Animations.Add( new Animation("idle", 0, 0, 0) );
            Sprite.Animations.Add( new Animation("walking", 0, 0, 3) );

            Ammo = new Dictionary<AmmoType,int>();
            Ammo.Add(AmmoType.Light, -1);
            Ammo.Add(AmmoType.Medium, 5);
            Ammo.Add(AmmoType.Heavy, 3);

            Sprite.ChangeAnimation(0);

            Behaviors.Add(new ControllableBehavior(this, new KeyboardInput()));
            Behaviors.Add(new CollidableBehavior(this, level));
            Behaviors.Add(new PhysicsBehavior(this) { Mass = 10f, Rotate = false, Gravity = new Vector2(0f, 9.8f), Friction = new Vector2(0.01f, 0f) });
            
            Children.Add(new Weapon() { Parent = this, Position = new Vector2(0,0), FollowParent = true } );
        }
        #endregion Constructor

        #region Methods
        public override void Draw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public void Hit(int health)
        {
            Health -= health;

            //TODO: Kill
            if( Health <= 0f ) Level.FinishLevel(this);
        }
        #endregion Methods
    }
}
