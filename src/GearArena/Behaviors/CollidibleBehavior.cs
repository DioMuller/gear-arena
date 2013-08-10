using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MonoGameLib.Core.Entities;

namespace GearArena.Behaviors
{
    #region Delegates
    /// <summary>
    /// Checks if new position is valid.
    /// </summary>
    /// <returns>Valid movement.</returns>
    public delegate bool CheckCollisionDelegate();
    #endregion Delegates

    /// <summary>
    /// Class that represents an collidible behavior.
    /// </summary>
    public class CollidableBehavior : Behavior
    {
        #region Attributes
        public Vector2 _oldPosition;
        #endregion Attributes

        #region Properties
        public Rectangle CollisionRect
        {
            get
            {
                return new Rectangle((int)Entity.Position.X, (int)Entity.Position.Y, Entity.Sprite.FrameSize.X, Entity.Sprite.FrameSize.Y);
            }
        }
        #endregion Properties

        #region Delegates
        public CheckCollisionDelegate CheckCollision { get; set; }
        #endregion Delegates

        #region Constructor
        /// <summary>
        /// Basically, the same constructor as Entity.
        /// </summary>
        public CollidableBehavior(Entity parent) : base(parent) 
        {
            _oldPosition = Vector2.Zero;
        }
        #endregion Constructor

        #region Methods
        public override void Update(GameTime gameTime)
        {
            Vector2 newPosition = Entity.Position;

            #region Check X
            Entity.Position = new Vector2(newPosition.X, _oldPosition.Y);

            if (CheckCollision != null && CheckCollision())
            {
                Entity.Position = _oldPosition;
            }
            else
            {
                _oldPosition = Entity.Position;
            }
            #endregion Check X

            #region Check Y
            Entity.Position = new Vector2(_oldPosition.X, newPosition.Y);

            if (CheckCollision != null && CheckCollision())
            {
                Entity.Position = _oldPosition;
            }
            else
            {
                _oldPosition = Entity.Position;
            }
            #endregion Check Y
        }
        #endregion Methods
    }
}
