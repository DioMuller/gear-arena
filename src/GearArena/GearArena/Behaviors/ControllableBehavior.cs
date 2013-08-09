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
        /// Updates the Rigid Body entity.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Entity.GetBehavior<SolidBodyBehavior>().ConstantForces["Accelerator"] = _input.LeftDirectional * 100f;
        }
        #endregion Methods
    }
}
