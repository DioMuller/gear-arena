using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MonoGameLib.Core.Entities;

namespace GearArena.Entities
{
    #region Delegates
    /// <summary>
    /// Checks if new position is valid.
    /// </summary>
    /// <returns>Valid movement.</returns>
    public delegate bool CheckCollisionDelegate();
    #endregion Delegates

    /// <summary>
    /// Class that represents an collidible entity.
    /// </summary>
    public class CollidableEntity : Entity
    {
        #region Properties
        public Rectangle CollisionRect
        {
            get
            {
                return new Rectangle((int) Position.X, (int) Position.Y, Sprite.FrameSize.X, Sprite.FrameSize.Y);
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
        public CollidableEntity() : base() { }
        #endregion Constructor

        #region Methods
        public override void Update(GameTime gameTime)
        {
            Vector2 oldPosition = Position;

            base.Update(gameTime);

            if( CheckCollision != null && CheckCollision() )
            {
                Position = oldPosition;
            }
        }
        #endregion Methods
    }
}
