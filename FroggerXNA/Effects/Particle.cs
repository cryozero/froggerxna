using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Frogger.Effects
{
    /// <summary>
    /// Represents a particle
    /// </summary>
    struct Particle
    {
        #region Variables

        private Vector2 mLocation;
        private double mLifeTime;
        private Vector2 mDirection;

        #endregion

        #region Properties

        /// <summary>
        /// get or set the location of the particle
        /// </summary>
        public Vector2 Location
        {
            get { return mLocation; }
            set { mLocation = value; }
        }

        /// <summary>
        /// Get or Set the direction
        /// </summary>
        public Vector2 Direction
        {
            get { return mDirection; }
            set { mDirection = value; }
        }
        
        /// <summary>
        /// Get or Set the lifetime of the particle
        /// </summary>
        public double LifeTime
        {
            get { return mLifeTime; }
            set { mLifeTime = value; }
        }


        #endregion

    }
}
