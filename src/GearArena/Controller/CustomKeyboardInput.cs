﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGameLib.Core.Input
{
    public class CustomKeyboardInput : GenericInput
    {
        #region Constructor
        /// <summary>
        /// Creates Keyboard Input instance for player [index].
        /// </summary>
        public CustomKeyboardInput()
            : base()
        {
        }
        #endregion Constructor

        #region Properties
        /// <summary>
        /// Left Directional/Stick
        /// </summary>
        public override Vector2 LeftDirectional
        {
            get
            {
                Vector2 direction = Vector2.Zero;

                if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up)) direction.Y -= 1f;
                if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down)) direction.Y += 1f;
                if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left)) direction.X -= 1f;
                if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right)) direction.X += 1f;

                if (Math.Abs(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X) > 0.4)
                {
                    direction = (direction + GamePad.GetState(PlayerIndex.One).ThumbSticks.Left);
                    direction.Normalize();
                }
                return direction;
            }
        }
        /// <summary>
        /// Right Directional/Stick
        /// </summary>
        public override Vector2 RightDirectional
        {
            get
            {
                Vector2 direction = Vector2.Zero;

                if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up)) direction.Y -= 1f;
                if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down)) direction.Y += 1f;
                if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left)) direction.X -= 1f;
                if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right)) direction.X += 1f;

                if( Math.Abs(GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y )> 0.4 )
                {
                    direction = (direction + GamePad.GetState(PlayerIndex.One).ThumbSticks.Right);
                    direction.Normalize();
                }
                return direction;
            }
        }

        /// <summary>
        /// D-Pad Left direction.
        /// </summary>
        public override ButtonState DirectionLeft
        {
            get
            {
                return Keyboard.GetState().IsKeyDown(Keys.D1) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadLeft) ? ButtonState.Pressed : ButtonState.Released;
            }
        }
        /// <summary>
        /// D-Pad Right direction.
        /// </summary>
        public override ButtonState DirectionRight
        {
            get
            {
                return Keyboard.GetState().IsKeyDown(Keys.D3) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadRight) ? ButtonState.Pressed : ButtonState.Released;
            }
        }
        /// <summary>
        /// D-Pad Up direction.
        /// </summary>
        public override ButtonState DirectionUp
        {
            get
            {
                return Keyboard.GetState().IsKeyDown(Keys.D2) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadUp) ? ButtonState.Pressed : ButtonState.Released;
            }
        }
        /// <summary>
        /// D-Pad Down direction.
        /// </summary>
        public override ButtonState DirectionDown
        {
            get
            {
                return Keyboard.GetState().IsKeyDown(Keys.NumPad4) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadDown) ? ButtonState.Pressed : ButtonState.Released;
            }
        }

        /// <summary>
        /// Face Button on the A position (On the Xbox 360 Controller). 
        /// </summary>
        public override ButtonState FaceButtonA
        {
            get
            {
                return Keyboard.GetState().IsKeyDown(Keys.Z) ? ButtonState.Pressed : ButtonState.Released;
            }
        }
        /// <summary>
        /// Face Button on the B position (On the Xbox 360 Controller). 
        /// </summary>
        public override ButtonState FaceButtonB
        {
            get
            {
                return Keyboard.GetState().IsKeyDown(Keys.X) ? ButtonState.Pressed : ButtonState.Released;
            }
        }
        /// <summary>
        /// Face Button on the X position (On the Xbox 360 Controller). 
        /// </summary>
        public override ButtonState FaceButtonX
        {
            get
            {
                return Keyboard.GetState().IsKeyDown(Keys.C) ? ButtonState.Pressed : ButtonState.Released;
            }
        }
        /// <summary>
        /// Face Button on the Y position (On the Xbox 360 Controller). 
        /// </summary>
        public override ButtonState FaceButtonY
        {
            get
            {
                return Keyboard.GetState().IsKeyDown(Keys.V) ? ButtonState.Pressed : ButtonState.Released;
            }
        }

        /// <summary>
        /// Start Button
        /// </summary>
        public override ButtonState StartButton
        {
            get
            {
                return Keyboard.GetState().IsKeyDown(Keys.Enter) ? ButtonState.Pressed : ButtonState.Released;
            }
        }

        /// <summary>
        /// Select Button
        /// </summary>
        public override ButtonState SelectButton
        {
            get
            {
                return Keyboard.GetState().IsKeyDown(Keys.Escape) ? ButtonState.Pressed : ButtonState.Released;
            }
        }

        /// <summary>
        /// Button on the Left Bumper position. 
        /// </summary>
        public override ButtonState LeftBumper
        {
            get
            {
                return Keyboard.GetState().IsKeyDown(Keys.Q) || Keyboard.GetState().IsKeyDown(Keys.OemComma) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.LeftShoulder) ? ButtonState.Pressed : ButtonState.Released;
            }
        }
        /// <summary>
        /// Button on the Right Bumper position. 
        /// </summary>
        public override ButtonState RightBumper
        {
            get
            {
                return Keyboard.GetState().IsKeyDown(Keys.E) || Keyboard.GetState().IsKeyDown(Keys.OemPeriod) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.RightShoulder) ? ButtonState.Pressed : ButtonState.Released;
            }
        }

        /// <summary>
        /// Left directional click.
        /// </summary>
        public override ButtonState LeftClick
        {
            get
            {
                return Mouse.GetState().LeftButton;
            }
        }

        /// <summary>
        /// Right directional click.
        /// </summary>
        public override ButtonState RightClick
        {
            get
            {
                return Mouse.GetState().RightButton;
            }
        }

        /// <summary>
        /// Button on the Left Trigger position. 
        /// </summary>
        public override float LeftTrigger
        {
            get
            {
                return Math.Max(Keyboard.GetState().IsKeyDown(Keys.LeftShift) ? 1f : 0f, GamePad.GetState(PlayerIndex.One).Triggers.Left);
            }
        }
        /// <summary>
        /// Button on the Right Trigger position. 
        /// </summary>
        public override float RightTrigger
        {
            get
            {
                return Math.Max(Keyboard.GetState().IsKeyDown(Keys.Space) ? 1f : 0f, GamePad.GetState(PlayerIndex.One).Triggers.Right);
            }
        }
        #endregion Properties
    }
}
