﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MonoGameLib.Core.Entities;
using MonoGameLib.Core.Extensions;

namespace GearArena.Behaviors
{
    /// <summary>
    /// Solid body physics behavior.
    /// </summary>
    public class PhysicsBehavior : Behavior
    {
        #region Properties
        /// <summary>
        /// Body momentum.
        /// </summary>
        public Vector2 Momentum { get; set; }

        /// <summary>
        /// Gravity being applied on the body.
        /// </summary>
        public Vector2 Gravity { get; set; }

        /// <summary>
        /// Constant forces applied on the body.
        /// </summary>
        public Dictionary<string, Vector2> ConstantForces { get; set; }

        /// <summary>
        /// Forces applied once on the body.
        /// </summary>
        public Stack<Vector2> Forces { get; set; }

        /// <summary>
        /// Body Mass.
        /// </summary>
        public float Mass { get; set; }

        /// <summary>
        /// Does the entity rotate with the force?
        /// </summary>
        public bool Rotate { get; set; }

        /// <summary>
        /// Constant friction being applied on the body.
        /// </summary>
        public Vector2 Friction { get; set; }
        #endregion Properties

        #region Constructors
        public PhysicsBehavior(Entity parent) : base(parent)
        {
            Forces = new Stack<Vector2>();
            ConstantForces = new Dictionary<string,Vector2>();

            Friction = Vector2.One;
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Updates the Physics of the entity.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //ITEM: c) Implementou corretamente o cálculo de deslocamento da bala, levando em conta a inércia e fazendo uso de vetores;
            Vector2 forces = Vector2.Zero;
            Vector2 instantForces = Vector2.Zero;
            float secs = (float)gameTime.ElapsedGameTime.TotalSeconds; 

            #region Calculate Forces
            //Constant forces
            foreach( Vector2 force in ConstantForces.Values )
            {
                forces += force;
            }

            //Forces applied once
            while( Forces.Count > 0 )
            {
                instantForces += (Forces.Pop() / Mass); //Since it will be applied only once, must do a BIG BOOM.
            }
            #endregion Calculate Forces

            Vector2 acceleration = (forces / Mass) + Gravity;
            Vector2 accelSecs = acceleration * secs;

            Entity.Position += GlobalForces.MetersToPixels( (Momentum + accelSecs/2) * secs );
            Momentum += (accelSecs + instantForces);
            Momentum *= (Vector2.One - Friction);

            if( Rotate )
            {
                Entity.Rotation = Momentum.GetAngle();
            }
        }
        #endregion Methods
    }
}
