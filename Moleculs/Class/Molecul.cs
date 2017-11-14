using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using Tao.FreeGlut;


namespace Moleculs.Class
{
    public class Molecul
    {
        public Vector3 Position = new Vector3();
        public Vector3 Speed = new Vector3();
        public float weight = 1;
        public bool stat = false;

        public Molecul(Vector3 position)
        {
            Position = position;

            Speed = new Vector3(0, 0, 0);
            weight = 10;
            stat = false;

        }

        public Molecul(Vector3 position, float w)
        {
            Position = position;

            Speed = new Vector3(0, 0, 0);
            weight = w;
            stat = false;

        }

        public Molecul(Vector3 position, Vector3 speed)
        {
            Position = position;
            Speed = speed;
        }

        public Molecul(Vector3 position, Vector3 speed, float w, bool st)
        {
            Position = position;
            Speed = speed;
            weight = w;
            stat = st;
        }


        public void Draw()
        {
            Gl.glPushMatrix();
            Gl.glTranslatef(Position.X, Position.Y, Position.Z);

            if (stat == true) Gl.glColor3f(0, 0, 1);
            else Gl.glColor3f(1, 0, 0);

            Glut.glutSolidSphere(weight*0.001f, 16, 16);

            Gl.glPopMatrix();
        }


        public void Step()
        {
            if (stat == false)
            {
                Position.X += Speed.X;
                Position.Y += Speed.Y;
                //Position.Z += Speed.Z; //z coord
            }
        }
    }
}
