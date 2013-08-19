using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MonoGameLib.Core.Entities;
using GearArena.Components;

namespace GearArena.Behaviors
{
    #region Delegates
    public delegate void OnCollideDelegate(List<Entity> entities);
    #endregion Delegates

    /// <summary>
    /// Class that represents an collidible behavior.
    /// </summary>
    public class CollidableBehavior : Behavior
    {
        #region Attributes
        private Vector2 _oldPosition;
        private Level _level;
        private PhysicsBehavior _physics;
        #endregion Attributes

        #region Properties
        public Rectangle CollisionRect
        {
            get
            {
                return new Rectangle((int)Entity.Position.X, (int)Entity.Position.Y, Entity.Sprite.FrameSize.X, Entity.Sprite.FrameSize.Y);
            }
        }

        public PhysicsBehavior Physics
        {
            get
            {
                if (_physics == null)
                {
                    _physics = Entity.GetBehavior<PhysicsBehavior>();
                }

                return _physics;
            }
        }
        #endregion Properties

        #region Delegates
        public OnCollideDelegate OnCollide { get; set; }
        #endregion Delegates

        #region Constructor
        /// <summary>
        /// Basically, the same constructor as Entity.
        /// </summary>
        public CollidableBehavior(Entity parent, Level level) : base(parent) 
        {
            _oldPosition = Vector2.Zero;
            _level = level;
        }
        #endregion Constructor

        #region Methods
        public override void Update(GameTime gameTime)
        {
            Vector2 newPosition = Entity.Position;

            #region Check X
            Entity.Position = new Vector2(newPosition.X, _oldPosition.Y);

            if (_level != null && _level.Collides(CollisionRect))
            {
                if (OnCollide != null) OnCollide(new List<Entity>());
                else Entity.Position = _oldPosition;

                //If collided on X, stops X momentum.
                //That way, the gravity (and other forces to that) will not accumulate.
                if (Physics != null) Physics.Momentum = new Vector2(0f, Physics.Momentum.Y);
            }
            else
            {
                _oldPosition = Entity.Position;
            }
            #endregion Check X

            #region Check Y
            Entity.Position = new Vector2(_oldPosition.X, newPosition.Y);

            if (_level != null && _level.Collides(CollisionRect))
            {
                if (OnCollide != null) OnCollide(new List<Entity>());
                else Entity.Position = _oldPosition;

                //If collided on Y, stops Y momentum.
                //That way, the gravity (and other forces to that) will not accumulate. 
                if (Physics != null) Physics.Momentum = new Vector2(Physics.Momentum.X, 0f);
            }
            else
            {
                _oldPosition = Entity.Position;
            }
            #endregion Check Y

            #region Check Entities
            List<Entity> collided = new List<Entity>();

            foreach (Entity e in _level.Players)
            {
                CollidableBehavior collision = e.GetBehavior<CollidableBehavior>();

                if (collision != null)
                {
                    if (collision.CollisionRect.Intersects(CollisionRect))
                    {
                        collided.Add(collision.Entity);
                    }
                }
            }

            if (collided.Count != 0)
            {
                if (OnCollide != null) OnCollide(collided);
                else Entity.Position = _oldPosition;
            }
            #endregion Check Entities
        }
        #endregion Methods
    }
}
