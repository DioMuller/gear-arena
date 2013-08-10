using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MonoGameLib.Core.Entities;
using MonoGameLib.Core.Input;

namespace GearArena.Behaviors
{
    class ControllableBehavior : Behavior
    {
        #region Attributes
        private GenericInput _input;
        #endregion Attributes

        #region Constructors
        public ControllableBehavior(Entity parent, GenericInput input)
            : base(parent)
        {
            _input = input;
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Updates the Controllable entity.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Vector2 direction = _input.LeftDirectional;

            if (direction != Vector2.Zero)
            {
                Entity.Sprite.ChangeAnimation(1);
                Entity.GetBehavior<PhysicsBehavior>().ConstantForces["Accelerator"] = _input.LeftDirectional * 1000f;
            }
            else
            {
                Entity.Sprite.ChangeAnimation(0);
                Entity.GetBehavior<PhysicsBehavior>().ConstantForces["Accelerator"] = Vector2.Zero;
            }
        }
        #endregion Methods
    }
}
