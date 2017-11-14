using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moleculs.Class
{
    public class Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /*public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }*/
    }
}
