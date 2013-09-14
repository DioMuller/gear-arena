using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GearArena.Components;
using GearArena.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameLib.Core;
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
            Vector2 angle = _input.RightDirectional;
            Weapon weapon = Entity.GetChildren<Weapon>();

            //ITEM:     2. Na hora do tiro, permita que o jogador escolha:
            //              2.1. Inclinação do canhão: Que dá o ângulo da bala;
            //              2.2. Força: Enquanto pressionar tecla/mouse a força variará de 0 até o máximo do canhão. O jogador não sabe que máximo é esse;
            //              2.3 Que bala usar (ver abaixo);

            if( (Entity as Player).Mirrored ) direction.Y *= -1;

            if (direction.X != 0f)
            {
                
                (Entity as Player).Mirrored = (direction.X < 0f);
                Entity.GetBehavior<PhysicsBehavior>().ConstantForces["Accelerator"] = new Vector2(direction.X * 100f, 0f);

                if (!(Entity as Player).IsFlying) 
                {
                    Entity.Sprite.ChangeAnimation(1); 
                    SoundManager.PlaySound("Walk");
                }
            }
            else
            {
                Entity.Sprite.ChangeAnimation(0);
                Entity.GetBehavior<PhysicsBehavior>().ConstantForces["Accelerator"] = Vector2.Zero;
            }

            if (angle.Y != 0f)
            {
                if( weapon != null )
                {
                    weapon.Rotation += (angle.Y / 10f) % 2f;
                    SoundManager.PlaySound("RotateWeapon");
                }
            }

            if( _input.LeftTrigger > 0.4f && (Entity as Player).Fuel > 0 )
            {
                (Entity as Player).GetBehavior<PhysicsBehavior>().ConstantForces["Propulsion"] = new Vector2(0f, -200f);
                (Entity as Player).IsFlying = true;
                SoundManager.PlaySound("Jetpack");
            }
            else
            {
                (Entity as Player).GetBehavior<PhysicsBehavior>().ConstantForces["Propulsion"] = new Vector2(0f, 0f);
                (Entity as Player).IsFlying = false;
            }

            if( _input.RightTrigger > 0.4f )
            {
                _ready = true;
                weapon.ChangeForce();
            }
            else if( _ready )
            {
                if (weapon != null)
                {
                    if( weapon.Shoot() )
                    {
                        (Entity as Player).Level.ChangeState(TurnState.Shooting);
                        SoundManager.PlaySound("Shoot");
                    }
                    else
                    {
                        SoundManager.PlaySound("NoAmmo");
                    }
                }

                _ready = false;
            }

            if (_input.LeftBumper == ButtonState.Pressed)
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

            if (_input.RightBumper == ButtonState.Pressed)
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

            if( _input.DirectionLeft == ButtonState.Pressed )
            {
                weapon.SelectAmmo(AmmoType.Light);
            }
            if (_input.DirectionUp == ButtonState.Pressed)
            {
                weapon.SelectAmmo(AmmoType.Medium);
            }
            if (_input.DirectionRight == ButtonState.Pressed)
            {
                weapon.SelectAmmo(AmmoType.Heavy);
            }
        }
        #endregion Methods
    }
}
