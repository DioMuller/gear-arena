using System;
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
    class PhysicsBehavior : Behavior
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
        #endregion Properties

        #region Constructors
        public PhysicsBehavior(Entity parent) : base(parent)
        {
            Forces = new Stack<Vector2>();
            ConstantForces = new Dictionary<string,Vector2>();
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Updates the Rigid Body entity.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Vector2 forces = Vector2.Zero;
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
                forces += Forces.Pop();
            }
            #endregion Calculate Forces

            Vector2 acceleration = (forces / Mass) + Gravity;
            Vector2 accelSecs = acceleration * secs;

            Entity.Position += (Momentum + accelSecs/2) * secs;
            Momentum += accelSecs;

            if( Rotate )
            {
                Entity.Rotation = Momentum.GetAngle();
            }
        }
        #endregion Methods
    }
}
