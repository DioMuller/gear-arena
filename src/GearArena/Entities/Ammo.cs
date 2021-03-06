﻿using System;
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
using MonoGameLib.Core;

namespace GearArena.Entities
{

    //ITEM:     4. O jogador poderá escolher bala de massa 1, 2 ou 3. O dano será proporcional a bala:
    //              4.1. Bala de massa 3 tira 20. O jogador terá 3 balas desse tipo.
    //              4.2. Bala de massa 2 tira 10. O jogador terá 5 balas desse tipo.
    //              4.3. Bala de massa 1 tira 5. Infinitas balas desse tipo.
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
        ParticleEmiter _explosion;
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
            Behaviors.Add(new PhysicsBehavior(this) { Mass = 3f - (float) type, Rotate = true, Gravity = GlobalForces.Gravity, Friction = new Vector2( 0f, 0f ) });
            Behaviors.Add(new CollidableBehavior(this, level)
            {
                OnCollide = new OnCollideDelegate(OnCollision)
            });

            //ITEM: 3. Atuarão sobre a bala a força da propulsão do canhão e do vento. O vento terá uma chance de variar a cada tiro;
            GetBehavior<PhysicsBehavior>().Forces.Push(initialForce);
            GetBehavior<PhysicsBehavior>().ConstantForces["Wind"] = GlobalForces.Wind;

            Type = type;

            switch (type)
            {
                case AmmoType.Light:
                    Sprite = new Sprite("images/sprites/ammo-light.png", new Point(4, 6), 0);
                    break;
                case AmmoType.Medium:
                    Sprite = new Sprite("images/sprites/ammo-medium.png", new Point(6, 8), 0);
                    break;
                case AmmoType.Heavy:
                    Sprite = new Sprite("images/sprites/ammo-heavy.png", new Point(8, 11), 0);
                    break;
                default:
                    Sprite = new Sprite("images/sprites/ammo-light.png", new Point(4, 6), 0);
                    break;
            }
            
            Sprite.Animations.Add(new Animation("any", 0, 0, 0));
            Sprite.ChangeAnimation(0);

            #region Particle
            List<ParticleState> particleStates = new List<ParticleState>();
            particleStates.Add(new ParticleState() { StartTime = 0f, Color = Color.Transparent, Scale = .1f });
            particleStates.Add(new ParticleState() { StartTime = 100f, Color = Color.DarkGray * 0.3f, Scale = .8f });
            particleStates.Add(new ParticleState() { StartTime = 150f, Color = Color.Gray * 0.3f, Scale = 1f });
            particleStates.Add(new ParticleState() { StartTime = 200f, Color = Color.White * 0.3f, Scale = 1.2f });
            particleStates.Add(new ParticleState() { StartTime = 300f, Color = Color.White * 0.2f, Scale = 1.5f });
            particleStates.Add(new ParticleState() { StartTime = 400f, Color = Color.White * 0.1f, Scale = 1.8f });
            particleStates.Add(new ParticleState() { StartTime = 500f, Color = Color.White * 0.05f, Scale = 2f });

            _particles = new ParticleEmiter("images/particles/dust.png", particleStates) { ParticleMaxTime = 1000f, MillisecondsToEmit = 16f, OpeningAngle = 90f, ParticleSpeed = 0.1f };
            #endregion Particle
        }
        #endregion Constructor

        #region Methods
        public override void Update(GameTime gameTime)
        {
            PhysicsBehavior physics = GetBehavior<PhysicsBehavior>();

            if( physics != null )
            {
                _particles.Position = this.Position;
                _particles.Direction = new Vector2(0, -1).RotateRadians(physics.Momentum.GetAngle());

                _particles.Update(gameTime);

                if (_explosion != null)
                {
                    _explosion.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            _particles.Draw(gameTime, spriteBatch);

            if (_explosion != null)
            {
                _explosion.Draw(gameTime, spriteBatch);
            }

            base.Draw(gameTime, spriteBatch);
        }

        #region Destruction
        public void OnCollision(List<Entity> collided)
        {
            Visible = false;

            _particles.OnDecay = new OnDecayedDelegate(OnExplosionEnd); //It's here because the fog's duration is bigger than the explosion duration.
            _particles.DecayTime = 1000f;
            GetBehavior<PhysicsBehavior>().IsActive = false;
            GetBehavior<CollidableBehavior>().IsActive = false;

            SoundManager.PlaySound("Explosion");

            #region Create Explosion
            List<ParticleState> particleStates = new List<ParticleState>();
            particleStates.Add(new ParticleState() { StartTime = 0f, Color = Color.DarkRed, Scale = 1f });
            particleStates.Add(new ParticleState() { StartTime = 100f, Color = Color.Red * 0.3f, Scale = 1f });
            particleStates.Add(new ParticleState() { StartTime = 150f, Color = Color.DarkOrange * 0.3f, Scale = 1f });
            particleStates.Add(new ParticleState() { StartTime = 200f, Color = Color.Orange * 0.3f, Scale = 1.2f });
            particleStates.Add(new ParticleState() { StartTime = 300f, Color = Color.Yellow * 0.2f, Scale = 1.5f });
            particleStates.Add(new ParticleState() { StartTime = 400f, Color = Color.Gray * 0.1f, Scale = 1.8f });
            particleStates.Add(new ParticleState() { StartTime = 500f, Color = Color.White * 0.05f, Scale = 2f });

            _explosion = new ParticleEmiter("images/particles/dust.png", particleStates) { Direction = new Vector2(0, -1), ParticleMaxTime = 1000f, MillisecondsToEmit = 1f, OpeningAngle = 180f, ParticleSpeed = 1f, Position = this.Position, DecayTime = 600f };
            #endregion Create Explosion

            #region Collision with players
            foreach (Entity e in collided)
            {
                Player p = e as Player;

                if (p != null)
                {
                    //ITEM:     4. O jogador poderá escolher bala de massa 1, 2 ou 3. O dano será proporcional a bala:
                    //              4.1. Bala de massa 3 tira 20. O jogador terá 3 balas desse tipo.
                    //              4.2. Bala de massa 2 tira 10. O jogador terá 5 balas desse tipo.
                    //              4.3. Bala de massa 1 tira 5. Infinitas balas desse tipo.
                    int damage = (Type == AmmoType.Light) ? 5 : ((Type == AmmoType.Medium) ? 10 : 20);
                    p.Hit(damage);
                }
            }
            #endregion Collision with players
        }

        public void OnExplosionEnd()
        {
            this.Parent.RemoveChildren(this);
        }
        #endregion Destruction

        #endregion Methods
    }
}
