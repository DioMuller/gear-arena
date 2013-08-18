using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GearArena
{
    public static class GlobalForces
    {
        #region Constants
        /// <summary>
        /// Pixels per Meter.
        /// </summary>
        private const float PPM = 4f;

        /// <summary>
        /// Meters per Pixel.
        /// </summary>
        private const float MPP = 1f/PPM;
        #endregion Constants

        #region Properties
        public static Vector2 Gravity { get; set; }
        public static Vector2 Wind { get; set; }
        #endregion Properties

        #region Methods
        public static float MetersToPixels(float value)
        {
            return value / MPP;
        }

        public static Vector2 MetersToPixels(Vector2 vector)
        {
            return vector / MPP;
        }
        #endregion Methods
    }
}
