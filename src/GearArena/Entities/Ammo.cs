using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GearArena.Behaviors;
using GearArena.Components;
using Microsoft.Xna.Framework;
using MonoGameLib.Core.Entities;
using MonoGameLib.Core.Sprites;
using MonoGameLib.Core.Particles;
using MonoGameLib.Core.Extensions;

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
        ParticleEmiter _particles;
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
            Sprite.Origin = new Vector2(12,12);

            Sprite.Animations.Add(new Animation("heavy", 0, 0, 0));
            Sprite.Animations.Add(new Animation("medium", 1, 0, 0));
            Sprite.Animations.Add(new Animation("light", 2, 0, 0));

            Sprite.ChangeAnimation((int) type);

            List<ParticleState> particleStates = new List<ParticleState>();
            particleStates.Add(new ParticleState() { StartTime = 0f, Color = Color.Yellow * 0.3f, Scale = .5f });
            particleStates.Add(new ParticleState() { StartTime = 300f, Color = Color.Red * 0.3f, Scale = 1f });
            particleStates.Add(new ParticleState() { StartTime = 500f, Color = Color.White * 0.2f, Scale = 2f });

            _particles = new ParticleEmiter("images/particles/dust.png", particleStates) { ParticleMaxTime = 5000f, MillisecondsToEmit = 16f, OpeningAngle = 30f, ParticleSpeed = 0.1f };
        }
        #endregion Constructor

        #region Methods
        public override void Update(GameTime gameTime)
        {
            _particles.Position = this.Position + new Vector2(6,6);
            _particles.Direction = new Vector2(0, -1).RotateRadians(GetBehavior<PhysicsBehavior>().Momentum.GetAngle());

            _particles.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            _particles.Draw(gameTime, spriteBatch);
            base.Draw(gameTime, spriteBatch);
        }
        #endregion Methods
    }
}
