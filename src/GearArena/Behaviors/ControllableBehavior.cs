using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GearArena.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameLib.Core.Entities;
using MonoGameLib.Core.Input;

namespace GearArena.Behaviors
{
    class ControllableBehavior : Behavior
    {
        #region Attributes
        private GenericInput _input;
        private bool _ready;
        #endregion Attributes

        #region Constructors
        public ControllableBehavior(Entity parent, GenericInput input)
            : base(parent)
        {
            _input = input;
            _ready = false;
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

            if (direction.X != 0f)
            {
                Entity.Sprite.ChangeAnimation(1);
                Entity.GetBehavior<PhysicsBehavior>().ConstantForces["Accelerator"] = new Vector2(direction.X * 100f, 0f);
            }
            else
            {
                Entity.Sprite.ChangeAnimation(0);
                Entity.GetBehavior<PhysicsBehavior>().ConstantForces["Accelerator"] = Vector2.Zero;
            }

            if( direction.Y != 0f )
            {
                Weapon weapon = Entity.GetChildren<Weapon>();

                if( weapon != null )
                {
                    weapon.Rotation += direction.Y / 10f;
                }
            }

            if( _input.LeftBumper == ButtonState.Pressed )
            {
                _ready = true;
            }
            else if( _ready )
            {
                Weapon weapon = Entity.GetChildren<Weapon>();

                if (weapon != null)
                {
                    weapon.Shoot(50f, AmmoType.Light);
                }

                _ready = false;
            }
        }
        #endregion Methods
    }
}
