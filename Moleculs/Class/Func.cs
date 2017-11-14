using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace Moleculs.Class
{
    public static class Func
    {
        public static float Distance(Vector3 m1, Vector3 m2)
        {
            return (float)Math.Sqrt(Math.Pow(m1.X - m2.X, 2) + Math.Pow(m1.Y - m2.Y, 2) + Math.Pow(m1.Z - m2.Z, 2));
        }

        static float Force_(Molecul m1, Molecul m2)
        {
            float dist = Distance(m1.Position, m2.Position);
            //return (float)((Constant.Force_const * (1/dist) - 1)*(Math.Atan(100/(1*dist))));


            if (dist < Constant.MAX_DISTANCE)//Максимальная дистанция взаимодействия (2.45)
            {
                /*Gl.glBegin(Gl.GL_LINES);

                Gl.glColor3f(1f, 1f, 1f);//5
                Gl.glVertex3f(m1.Position.X, m1.Position.Y, m1.Position.Z);
                Gl.glVertex3f(m2.Position.X, m2.Position.Y, m2.Position.Z);
                Gl.glColor3f(1f, 0, 0);//5

                Gl.glEnd();*/


                return (float)((Constant._4 * Constant.Force_const * (Constant._1 / dist) - Constant._2) * (Constant._5 * Math.Atan(Constant._3 / (dist))));
            }
            else return 0;




            //return (float)((-Constant.Force_const * m1.weight * m2.weight)/(Math.Pow(dist, 2)));
        }

        static float Force(Molecul m1, Molecul m2)
        {
            float dist = Distance(m1.Position, m2.Position);

            if (dist < Constant.MAX_DISTANCE)//Максимальная дистанция взаимодействия (2.45)
            {
                Gl.glBegin(Gl.GL_LINES);

                Gl.glColor3f(1f, 1f, 1f);
                Gl.glVertex3f(m1.Position.X, m1.Position.Y, m1.Position.Z);
                Gl.glVertex3f(m2.Position.X, m2.Position.Y, m2.Position.Z);
                Gl.glColor3f(1f, 0, 0);

                Gl.glEnd();


                return (float)(4 * Constant._1 * (Math.Pow( Constant._2/dist, 12 ) - Math.Pow(Constant._2 / dist, 6)));
            }
            else return 0;
        }

        public static void acceleration(Molecul m1, Molecul m2)
        {
            float dx = m1.Position.X - m2.Position.X;
            float dy = m1.Position.Y - m2.Position.Y;
            float dz = m1.Position.Z - m2.Position.Z;

            float alpha_xy = (float)Math.Atan2(dy, dx);
            float alpha_xz = (float)Math.Atan2(dz, dx);
            float alpha_yz = (float)Math.Atan2(dy, dz);

            float force = 0.001f * Force(m1, m2);

            //float d = Math.Sign(dx);

            if ((Math.Abs(m1.Position.X) < 1) || (Math.Abs(m1.Position.Y) < 1 || (Math.Abs(m1.Position.Z) < 1)))
            {
                m1.Speed.X += (float)((m2.weight * force * Math.Cos(alpha_xy)) / m1.weight);
                m1.Speed.Y += (float)((m2.weight * force * Math.Sin(alpha_xy)) / m1.weight);
                m1.Speed.Z += (float)((m2.weight * force * Math.Sin(alpha_xz)) / m1.weight);
            } 

            if ((Math.Abs(m2.Position.X) < 1) || (Math.Abs(m2.Position.Y) < 1) || (Math.Abs(m2.Position.Z) < 1))
            {
                m2.Speed.X += (float)(-(m1.weight * force * Math.Cos(alpha_xy)) / m2.weight);
                m2.Speed.Y += (float)(-(m1.weight * force * Math.Sin(alpha_xy)) / m2.weight);
                m2.Speed.Z += (float)(-(m1.weight * force * Math.Sin(alpha_xz)) / m2.weight);
            }

            m1.Speed.X *= Constant.Speed_decr;
            m1.Speed.Y *= Constant.Speed_decr;
            m1.Speed.Z *= Constant.Speed_decr;

            m2.Speed.X *= Constant.Speed_decr;
            m2.Speed.Y *= Constant.Speed_decr;
            m2.Speed.Z *= Constant.Speed_decr;
        }

    }
}
