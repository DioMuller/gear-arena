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
using MonoGameLib.Core.Particles;
using MonoGameLib.Core.Extensions;

namespace GearArena.Entities
{
    public class Player : Entity
    {
        #region Attributes
        private ParticleEmiter _particles;
        #endregion Attributes

        #region Properties
        public Level Level { get; private set; }
        public int Health { get; private set; }

        public int Fuel { get; private set; }

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

        public bool IsFlying { get; set; }
        #endregion Properties

        #region Constructor
        public Player(Level level, Vector2 position, Color color) : base()
        {
            Level = level;

            Color = color;

            Health = 100;

            Fuel = 100;

            Sprite = new Sprite("images/sprites/mecha.png", new Point(32, 32), 100);
            Sprite.Origin = new Vector2(16, 16);
            Position = position + Sprite.Origin;

            Sprite.Animations.Add( new Animation("idle", 0, 0, 0) );
            Sprite.Animations.Add( new Animation("walking", 0, 0, 3) );

            Sprite.ChangeAnimation(0);

            Behaviors.Add(new ControllableBehavior(this, new CustomKeyboardInput()));
            Behaviors.Add(new CollidableBehavior(this, level));
            Behaviors.Add(new PhysicsBehavior(this) { Mass = 10f, Rotate = false, Gravity = new Vector2(0f, 9.8f), Friction = new Vector2(0.01f, 0f) });
            
            Children.Add(new Weapon() { Parent = this, Position = new Vector2(0,0), FollowParent = true, Color = this.Color } );

            #region Particle
            List<ParticleState> particleStates = new List<ParticleState>();
            particleStates.Add(new ParticleState() { StartTime = 0f, Color = Color.DarkBlue, Scale = .1f });
            particleStates.Add(new ParticleState() { StartTime = 100f, Color = Color.Blue * 0.3f, Scale = .8f });
            particleStates.Add(new ParticleState() { StartTime = 150f, Color = Color.Yellow * 0.3f, Scale = 1f });
            particleStates.Add(new ParticleState() { StartTime = 200f, Color = Color.Red * 0.3f, Scale = 1.2f });
            particleStates.Add(new ParticleState() { StartTime = 300f, Color = Color.DarkGray * 0.2f, Scale = 1.5f });
            particleStates.Add(new ParticleState() { StartTime = 400f, Color = Color.Gray * 0.1f, Scale = 1.8f });
            particleStates.Add(new ParticleState() { StartTime = 500f, Color = Color.White * 0.05f, Scale = 2f });

            _particles = new ParticleEmiter("images/particles/dust.png", particleStates) { ParticleMaxTime = 1000f, MillisecondsToEmit = 16f, OpeningAngle = 30f, ParticleSpeed = 0.1f };
            #endregion Particle
        }
        #endregion Constructor

        #region Methods
        public override void Update(GameTime gameTime)
        {
            PhysicsBehavior physics = GetBehavior<PhysicsBehavior>();

            if( physics != null )
            {
                _particles.Position = this.Position + (Vector2.UnitX * ( Mirrored ? Sprite.Origin.X : -Sprite.Origin.X));
                _particles.Direction = new Vector2(0, -1).RotateRadians(physics.Momentum.GetAngle());

                _particles.Update(gameTime);
            }

            if( IsFlying )
            {
                Fuel--;
                _particles.Enabled = true;
            }
            else
            {
                _particles.Enabled = false;
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            _particles.Draw(gameTime, spriteBatch);
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
