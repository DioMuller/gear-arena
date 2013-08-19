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

namespace GearArena.Entities
{
    public class Player : Entity
    {
        #region Attributes
        private PlayerGUI _gui;
        #endregion Attributes

        #region Properties
        public Level Level { get; private set; }
        public int Health { get; private set; }
        #endregion Properties

        #region Constructor
        public Player(Level level) : base()
        {
            Behaviors.Add(new ControllableBehavior(this, new KeyboardInput()));
            Behaviors.Add(new CollidableBehavior(this, level));
            Behaviors.Add(new PhysicsBehavior(this) { Mass = 10f, Rotate = false, Gravity = new Vector2(0f, 9.8f), Friction = new Vector2(0.01f, 0f) } );

            Level = level;

            _gui = new PlayerGUI(this);

            Health = 100;

            Sprite = new Sprite("images/sprites/mecha.png", new Point(32, 32), 100);

            Sprite.Animations.Add( new Animation("idle", 0, 0, 0) );
            Sprite.Animations.Add( new Animation("walking", 0, 0, 3) );

            Sprite.ChangeAnimation(0);
            
            Children.Add(new Weapon() { Parent = this, Position = new Vector2(8,16), FollowParent = true } );
        }
        #endregion Constructor

        #region Methods
        public override void Draw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            _gui.Draw(gameTime, spriteBatch);
        }

        public void Hit(int health)
        {
            Health -= health;

            //TODO: Kill
        }
        #endregion Methods
    }
}
