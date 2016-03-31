using UnityEngine;
using System.Collections;

namespace Oculus2ARDrone
{
    public class Orientation
    {
        private float roll;
        private float pitch;
        private float yaw;
        private float gaz;

        public Orientation()
        {
            roll = 0;
            pitch = 0;
            yaw = 0;
            gaz = 0;
        }

        public float Roll
        {
            get { return roll; }
            set { this.roll = value; }
        }

        public float Pitch
        {
            get { return pitch; }
            set { this.pitch = value; }
        }

        public float Yaw
        {
            get { return yaw; }
            set { this.yaw = value; }
        }

        public float Gaz
        {
            get { return gaz; }
            set { this.gaz = value; }
        }
    }
}
