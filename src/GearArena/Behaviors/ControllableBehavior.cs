using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GearArena.Components;
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
        private bool _onHoldP;
        private bool _onHoldN;
        #endregion Attributes

        #region Constructors
        public ControllableBehavior(Entity parent, GenericInput input)
            : base(parent)
        {
            _input = input;
            _ready = false;
            _onHoldP = _onHoldN = false;
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
            Weapon weapon = Entity.GetChildren<Weapon>();

            if (direction.X != 0f)
            {
                Entity.Sprite.ChangeAnimation(1);
                (Entity as Player).Mirrored = (direction.X < 0f);
                Entity.GetBehavior<PhysicsBehavior>().ConstantForces["Accelerator"] = new Vector2(direction.X * 100f, 0f);
            }
            else
            {
                Entity.Sprite.ChangeAnimation(0);
                Entity.GetBehavior<PhysicsBehavior>().ConstantForces["Accelerator"] = Vector2.Zero;
            }

            if( direction.Y != 0f )
            {
                if( weapon != null )
                {
                    weapon.Rotation += (direction.Y / 10f) % 2f;
                }
            }

            if( _input.FaceButtonA == ButtonState.Pressed )
            {
                (Entity as Player).GetBehavior<PhysicsBehavior>().ConstantForces["Propulsion"] = new Vector2(0f, -2000f);
            }
            else
            {
                (Entity as Player).GetBehavior<PhysicsBehavior>().ConstantForces["Propulsion"] = new Vector2(0f, 0f);
            }

            if( _input.LeftBumper == ButtonState.Pressed )
            {
                _ready = true;
                weapon.ChangeForce();
            }
            else if( _ready )
            {
                if (weapon != null)
                {
                    weapon.Shoot();

                    (Entity as Player).Level.ChangeState(TurnState.Shooting);
                }

                _ready = false;
            }

            if (_input.FaceButtonB == ButtonState.Pressed)
            {
                if( !_onHoldP )
                {
                    weapon.PrevAmmo();
                    _onHoldP = true;
                }
            }
            else
            {
                _onHoldP = false;
            }

            if (_input.FaceButtonX == ButtonState.Pressed)
            {
                if( !_onHoldN )
                {
                    weapon.NextAmmo();
                    _onHoldN = true;
                }
            }
            else
            {
                _onHoldN = false;
            }
        }
        #endregion Methods
    }
}
