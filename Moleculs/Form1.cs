using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.FreeGlut;
using Moleculs.Class;

namespace Moleculs
{
    public partial class Form1 : Form
    {
        List<Molecul> Moleculs = new List<Molecul>();
        Random rnd = new Random();


        #region TrackPads varibles


        Point Track_Pad_position = new Point(40, 40);
        PointF rotate = new PointF(0, 0);
        PointF position = new PointF(0, 0);


        //Чем больше тем медленнее!!!
        float TrackPad_Speed = 100f;


        float mouse_old_position = 0;
        float mouse_delta = 0;
        int TrackPad_delta_position = 60;
        

        float pos_x = 0;
        float pos_y = 0;
        float pos_z = 0;

        float rot_x = 0;
        float rot_y = 0;
        float rot_z = 0;

        float default_rot_x = 0;
        float default_rot_y = 0;
        float default_rot_z = 0;

        bool mouse_down_pos_x;
        bool mouse_down_pos_y;
        bool mouse_down_pos_z;

        bool mouse_down_rot_x;
        bool mouse_down_rot_y;
        bool mouse_down_rot_z;

        bool mouse_down_default_rot_x;
        bool mouse_down_default_rot_y;
        bool mouse_down_default_rot_z;

        #endregion


        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();
            moveControllerInit();



            //Moleculs.Add(new Molecul(new Vector3(0, 0, 0), new Vector3(0, 0, 0), 100000));

            //Moleculs.Add(new Molecul(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0, 0f, 0), 10f, false));
            /*//Moleculs.Add(new Molecul(new Vector3(0.1f, -0.8f, 0.0f), new Vector3(0, 0.1f, 0), 10f, false));
            Moleculs.Add(new Molecul(new Vector3(0.2f, 0.0f, 0.0f), new Vector3(0, 0f, 0), 5f, false));
            Moleculs.Add(new Molecul(new Vector3(0.2f, 0.2f, 0.0f), new Vector3(0, 0f, 0), 5f, false));
            Moleculs.Add(new Molecul(new Vector3(0.0f, 0.0f, 0.2f), new Vector3(0, 0f, 0), 5f, false));
            Moleculs.Add(new Molecul(new Vector3(0.0f, 0.2f, 0.2f), new Vector3(0, 0f, 0), 5f, false));
            Moleculs.Add(new Molecul(new Vector3(0.2f, 0.0f, 0.2f), new Vector3(0, 0f, 0), 5f, false));
            Moleculs.Add(new Molecul(new Vector3(0.2f, 0.2f, 0.2f), new Vector3(0, 0f, 0), 5f, false));*/
            //Moleculs.Add(new Molecul(new Vector3(0.5f, -0.0f, 0), new Vector3(0, 0.002f, 0), 1f));
            //Moleculs.Add(new Molecul(new Vector3(0.9f, -0.0f, 0), new Vector3(0, 0.003f, 0), 1f));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

            Gl.glClearColor(0, 0, 0, 1);

            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            //Gl.glOrtho(1, 1, 1, 1, 1, -1);
            //Gl.glOrtho(-0.99, 1, -0.99, 1, -1, 1);


            Glu.gluPerspective(45, (float)AnT.Width / (float)AnT.Height, 0.1, 300);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_COLOR_MATERIAL);
            Gl.glEnable(Gl.GL_DOUBLE);

            float[] light0_pos = { 0f, 0f, 0f, 1f };


            /*
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT_MODEL_LOCAL_VIEWER);
            Gl.glEnable(Gl.GL_LIGHT0);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, light0_pos);
            */

            Draw();

            button3.PerformClick();
        }

        void Draw()
        {
            #region setup

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glLoadIdentity();

            Gl.glTranslatef(0f, 0f, -2f);





            
            #region GL Movement

            Gl.glRotatef((default_rot_x += mouse_delta * Convert.ToByte(mouse_down_default_rot_x)), 1, 0, 0);
            Gl.glRotatef((default_rot_y += mouse_delta * Convert.ToByte(mouse_down_default_rot_y)), 0, 1, 0);
            Gl.glRotatef((default_rot_z += mouse_delta * Convert.ToByte(mouse_down_default_rot_z)), 0, 0, 1);

            Gl.glTranslatef((pos_x += mouse_delta * Convert.ToByte(mouse_down_pos_x)), 0, 0);
            Gl.glTranslatef(0, (pos_y += mouse_delta * Convert.ToByte(mouse_down_pos_y)), 0);
            Gl.glTranslatef(0, 0, (pos_z += mouse_delta * Convert.ToByte(mouse_down_pos_z)));

            Gl.glRotatef((rot_x += mouse_delta * Convert.ToByte(mouse_down_rot_x)), 1, 0, 0);
            Gl.glRotatef((rot_y += mouse_delta * Convert.ToByte(mouse_down_rot_y)), 0, 1, 0);
            Gl.glRotatef((rot_z += mouse_delta * Convert.ToByte(mouse_down_rot_z)), 0, 0, 1);

            Show_data();

            #endregion


            #region Graphic
            
            /*
            Gl.glColor3f(0, 0, 1);
            Gl.glPointSize(5);
            Gl.glBegin(Gl.GL_POINTS);

            for (int i = 0; i < Moleculs.Count; i++)
            {
                for (int j = i + 1; j < Moleculs.Count; j++)
                {
                    float dist = Func.Distance(Moleculs[i].Position, Moleculs[j].Position);
                    float yyy = (float)((Constant._4 * Constant.Force_const * (Constant._1 / dist) - Constant._2) * (Constant._5 * Math.Atan(Constant._3 / (dist))));
                    Gl.glVertex2f(dist / 10f, yyy);
                }
            }
            Gl.glEnd();

            Gl.glColor3f(0, 1, 0);
            Gl.glBegin(Gl.GL_LINE_STRIP);

            for (int i = 0; i < 1000; i++)
            {
                float xx = i/10f;
                float yy = (float)((Constant._4 * Constant.Force_const * (Constant._1 / xx) - Constant._2) * (Constant._5 * Math.Atan(Constant._3 / (xx))));

                Gl.glVertex2f(xx/10f, yy);
            }

            Gl.glEnd();



            //Gl.glVertex2f(0, 0);

            */
            
            
            #endregion

            #endregion

            #region GUI
            /*

            Gl.glBegin(Gl.GL_LINES);

            Gl.glColor3f(0.8f, 0.8f, 0.8f);//8
            Gl.glVertex2f(-1, 0);
            Gl.glVertex2f(1, 0);

            Gl.glVertex2f(0, -1);
            Gl.glVertex2f(0, 1);

            Gl.glColor3f(0.5f, 0.5f, 0.5f);//5
            for (int ii = -10; ii < 10; ii++)
            {
                Gl.glVertex2f(-1, ii / 10f);
                Gl.glVertex2f(1, ii / 10f);
            }
            for (int ii = -10; ii < 10; ii++)
            {
                Gl.glVertex2f(ii / 10f, -1);
                Gl.glVertex2f(ii / 10f, 1);
            }

            Gl.glColor3f(1, 1, 1);
            Gl.glEnd();

            */
            #endregion

            Gl.glColor3f(1, 0, 0);

            foreach (var molecul in Moleculs)
            {
                if(cb_Simulation.Checked)
                    molecul.Step();
            }

            for (int i = 0; i < Moleculs.Count; i++)
            {
                for (int j = i + 1; j < Moleculs.Count; j++)
                {
                    Func.acceleration(Moleculs[i], Moleculs[j]);
                }
            }


            /*
            for (int i = 0; i < Moleculs.Count; i++)
            {
                int[] m = new int[4] { -1, -1, -1, -1 };
                float[] dist = new float[4] { 99, 99, 99, 99 };

                for (int j = i+1; j < Moleculs.Count; j++)
                {
                    float d = Func.Distance(Moleculs[i].Position, Moleculs[j].Position);
                    if (d < Constant.MAX_DISTANCE)
                    {
                        if (d < dist[0])
                        {
                            dist[0] = d;
                            m[0] = j;
                        }
                        else if (d < dist[1])
                        {
                            dist[1] = d;
                            m[1] = j;
                        }
                        else if (d < dist[2])
                        {
                            dist[2] = d;
                            m[2] = j;
                        }
                        else if (d < dist[3])
                        {
                            dist[3] = d;
                            m[3] = j;
                        }
                    }
                    //Func.acceleration(Moleculs[i], Moleculs[j]);
                }*/
                
                /*for (int j = 1; j < 4; j++)
                {
                    if (m[j] != -1)
                    {
                        Func.acceleration(Moleculs[i], Moleculs[m[j]]);

                            /*Gl.glBegin(Gl.GL_LINES);

                            Gl.glColor3f(1f, 1f, 1f);//5
                            Gl.glVertex3f(Moleculs[i].Position.X, Moleculs[i].Position.Y, Moleculs[i].Position.Z);
                            Gl.glVertex3f(Moleculs[m[j]].Position.X, Moleculs[m[j]].Position.Y, Moleculs[m[j]].Position.Z);
                            Gl.glColor3f(1f, 0, 0);//5

                            Gl.glEnd();*/
                    /*}
                }
            }*/
            

            foreach (var molecul in Moleculs)
            {
                molecul.Draw();
            }

            Gl.glFlush();
            AnT.Invalidate();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            Draw();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Draw();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            float x = rnd.Next(-30, 30) / 100f;
            float y = rnd.Next(-30, 30) / 100f;
            Moleculs.Add(new Molecul(new Vector3(x, y, 0), new Vector3(0, 0f, 0), 10f, false));
            //Moleculs.Add(new Molecul(new Vector3(x+0.01f, y, 0), new Vector3(0, 0f, 0), 0.1f, false));
            //Moleculs.Add(new Molecul(new Vector3(x+0.02f, y, 0), new Vector3(0, 0f, 0), 0.1f, false));
            //Moleculs.Add(new Molecul(new Vector3(x+0.03f, y, 0), new Vector3(0, 0f, 0), 0.1f, false));
        }




        #region move controller

        void moveControllerInit()
        {
            

            btn_TrackPad_pos_x.MouseDown += btn_TrackPad_pos_x_MouseDown;
            btn_TrackPad_pos_x.MouseMove += btn_TrackPad_pos_x_MouseMove;
            btn_TrackPad_pos_x.MouseUp += btn_TrackPad_pos_x_MouseUp;

            btn_TrackPad_pos_y.MouseDown += btn_TrackPad_pos_y_MouseDown;
            btn_TrackPad_pos_y.MouseMove += btn_TrackPad_pos_y_MouseMove;
            btn_TrackPad_pos_y.MouseUp += btn_TrackPad_pos_y_MouseUp;

            btn_TrackPad_pos_z.MouseDown += btn_TrackPad_pos_z_MouseDown;
            btn_TrackPad_pos_z.MouseMove += btn_TrackPad_pos_z_MouseMove;
            btn_TrackPad_pos_z.MouseUp += btn_TrackPad_pos_z_MouseUp;


            btn_TrackPad_rot_x.MouseDown += btn_TrackPad_rot_x_MouseDown;
            btn_TrackPad_rot_x.MouseMove += btn_TrackPad_rot_x_MouseMove;
            btn_TrackPad_rot_x.MouseUp += btn_TrackPad_rot_x_MouseUp;

            btn_TrackPad_rot_y.MouseDown += btn_TrackPad_rot_y_MouseDown;
            btn_TrackPad_rot_y.MouseMove += btn_TrackPad_rot_y_MouseMove;
            btn_TrackPad_rot_y.MouseUp += btn_TrackPad_rot_y_MouseUp;

            btn_TrackPad_rot_z.MouseDown += btn_TrackPad_rot_z_MouseDown;
            btn_TrackPad_rot_z.MouseMove += btn_TrackPad_rot_z_MouseMove;
            btn_TrackPad_rot_z.MouseUp += btn_TrackPad_rot_z_MouseUp;


            btn_TrackPad_default_rot_x.MouseDown += btn_TrackPad_default_rot_x_MouseDown;
            btn_TrackPad_default_rot_x.MouseMove += btn_TrackPad_default_rot_x_MouseMove;
            btn_TrackPad_default_rot_x.MouseUp += btn_TrackPad_default_rot_x_MouseUp;

            btn_TrackPad_default_rot_y.MouseDown += btn_TrackPad_default_rot_y_MouseDown;
            btn_TrackPad_default_rot_y.MouseMove += btn_TrackPad_default_rot_y_MouseMove;
            btn_TrackPad_default_rot_y.MouseUp += btn_TrackPad_default_rot_y_MouseUp;

            btn_TrackPad_default_rot_z.MouseDown += btn_TrackPad_default_rot_z_MouseDown;
            btn_TrackPad_default_rot_z.MouseMove += btn_TrackPad_default_rot_z_MouseMove;
            btn_TrackPad_default_rot_z.MouseUp += btn_TrackPad_default_rot_z_MouseUp;

        }

        private void Show_data()
        {
            lbl_data_pos_x.Text = Convert.ToString(pos_x);
            lbl_data_pos_y.Text = Convert.ToString(pos_y);
            lbl_data_pos_z.Text = Convert.ToString(pos_z);

            lbl_data_rot_x.Text = Convert.ToString(rot_x);
            lbl_data_rot_y.Text = Convert.ToString(rot_y);
            lbl_data_rot_z.Text = Convert.ToString(rot_z);

            lbl_default_data_rot_x.Text = Convert.ToString(default_rot_x);
            lbl_default_data_rot_y.Text = Convert.ToString(default_rot_y);
            lbl_default_data_rot_z.Text = Convert.ToString(default_rot_z);
        }

        #region TrackPad position
        #region TrackPad position X
        private void btn_TrackPad_pos_x_MouseDown(object sender, MouseEventArgs e)
        {
            timer.Enabled = true;

            mouse_down_pos_x = true;
            mouse_old_position = Cursor.Position.Y;
        }
        private void btn_TrackPad_pos_x_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouse_down_pos_x)
            {
                Track_Pad_position.Y = (Cursor.Position.Y - this.Location.Y - TrackPad_delta_position) - gb_trackPad_pos_x.Location.Y;

                mouse_delta = ((mouse_old_position - Cursor.Position.Y) / TrackPad_Speed);

                if (Track_Pad_position.Y < 5) Track_Pad_position.Y = 5;
                if (Track_Pad_position.Y > 80) Track_Pad_position.Y = 80;

                btn_TrackPad_pos_x.Top = Track_Pad_position.Y;
            }
        }
        private void btn_TrackPad_pos_x_MouseUp(object sender, MouseEventArgs e)
        {
            timer.Enabled = false;

            mouse_down_pos_x = false;
            btn_TrackPad_pos_x.Top = 40;
            mouse_delta = 0;
        }
        #endregion
        #region TrackPad position Y
        private void btn_TrackPad_pos_y_MouseDown(object sender, MouseEventArgs e)
        {
            timer.Enabled = true;

            mouse_down_pos_y = true;//
            mouse_old_position = Cursor.Position.Y;
        }
        private void btn_TrackPad_pos_y_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouse_down_pos_y)
            {
                Track_Pad_position.Y = (Cursor.Position.Y - this.Location.Y - TrackPad_delta_position) - gb_trackPad_pos_x.Location.Y;

                mouse_delta = ((mouse_old_position - Cursor.Position.Y) / TrackPad_Speed);

                if (Track_Pad_position.Y < 5) Track_Pad_position.Y = 5;
                if (Track_Pad_position.Y > 80) Track_Pad_position.Y = 80;

                btn_TrackPad_pos_y.Top = Track_Pad_position.Y;//
            }
        }
        private void btn_TrackPad_pos_y_MouseUp(object sender, MouseEventArgs e)
        {
            timer.Enabled = false;

            mouse_down_pos_y = false;//
            btn_TrackPad_pos_y.Top = 40;//
            mouse_delta = 0;
        }
        #endregion
        #region TrackPad position Z
        private void btn_TrackPad_pos_z_MouseDown(object sender, MouseEventArgs e)
        {
            timer.Enabled = true;

            mouse_down_pos_z = true;//
            mouse_old_position = Cursor.Position.Y;
        }
        private void btn_TrackPad_pos_z_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouse_down_pos_z)//
            {
                Track_Pad_position.Y = (Cursor.Position.Y - this.Location.Y - TrackPad_delta_position) - gb_trackPad_pos_x.Location.Y;

                mouse_delta = ((mouse_old_position - Cursor.Position.Y) / TrackPad_Speed);

                if (Track_Pad_position.Y < 5) Track_Pad_position.Y = 5;
                if (Track_Pad_position.Y > 80) Track_Pad_position.Y = 80;

                btn_TrackPad_pos_z.Top = Track_Pad_position.Y;//
            }
        }
        private void btn_TrackPad_pos_z_MouseUp(object sender, MouseEventArgs e)
        {
            timer.Enabled = false;

            mouse_down_pos_z = false;//
            btn_TrackPad_pos_z.Top = 40;//
            mouse_delta = 0;
        }
        #endregion
        #endregion
        #region TrackPad rotation
        #region TrackPad rotation X
        private void btn_TrackPad_rot_x_MouseDown(object sender, MouseEventArgs e)
        {
            timer.Enabled = true;

            mouse_down_rot_x = true;//
            mouse_old_position = Cursor.Position.Y;
        }
        private void btn_TrackPad_rot_x_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouse_down_rot_x)//
            {
                Track_Pad_position.Y = (Cursor.Position.Y - this.Location.Y - TrackPad_delta_position) - gb_trackPad_rot_x.Location.Y;//

                mouse_delta = ((mouse_old_position - Cursor.Position.Y) / TrackPad_Speed);

                if (Track_Pad_position.Y < 5) Track_Pad_position.Y = 5;
                if (Track_Pad_position.Y > 80) Track_Pad_position.Y = 80;

                btn_TrackPad_rot_x.Top = Track_Pad_position.Y;//
            }
        }
        private void btn_TrackPad_rot_x_MouseUp(object sender, MouseEventArgs e)
        {
            timer.Enabled = false;

            mouse_down_rot_x = false;//
            btn_TrackPad_rot_x.Top = 40;//
            mouse_delta = 0;
        }
        #endregion
        #region TrackPad rotation Y
        private void btn_TrackPad_rot_y_MouseDown(object sender, MouseEventArgs e)
        {
            timer.Enabled = true;

            mouse_down_rot_y = true;//
            mouse_old_position = Cursor.Position.Y;
        }
        private void btn_TrackPad_rot_y_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouse_down_rot_y)//
            {
                Track_Pad_position.Y = (Cursor.Position.Y - this.Location.Y - TrackPad_delta_position) - gb_trackPad_rot_y.Location.Y;//

                mouse_delta = ((mouse_old_position - Cursor.Position.Y) / TrackPad_Speed);

                if (Track_Pad_position.Y < 5) Track_Pad_position.Y = 5;
                if (Track_Pad_position.Y > 80) Track_Pad_position.Y = 80;

                btn_TrackPad_rot_y.Top = Track_Pad_position.Y;//
            }
        }
        private void btn_TrackPad_rot_y_MouseUp(object sender, MouseEventArgs e)
        {
            timer.Enabled = false;

            mouse_down_rot_y = false;//
            btn_TrackPad_rot_y.Top = 40;//
            mouse_delta = 0;
        }
        #endregion
        #region TrackPad rotation Z
        private void btn_TrackPad_rot_z_MouseDown(object sender, MouseEventArgs e)
        {
            timer.Enabled = true;

            mouse_down_rot_z = true;//
            mouse_old_position = Cursor.Position.Y;
        }
        private void btn_TrackPad_rot_z_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouse_down_rot_z)//
            {
                Track_Pad_position.Y = (Cursor.Position.Y - this.Location.Y - TrackPad_delta_position) - gb_trackPad_rot_z.Location.Y;//

                mouse_delta = ((mouse_old_position - Cursor.Position.Y) / TrackPad_Speed);

                if (Track_Pad_position.Y < 5) Track_Pad_position.Y = 5;
                if (Track_Pad_position.Y > 80) Track_Pad_position.Y = 80;

                btn_TrackPad_rot_z.Top = Track_Pad_position.Y;//
            }
        }
        private void btn_TrackPad_rot_z_MouseUp(object sender, MouseEventArgs e)
        {
            timer.Enabled = false;

            mouse_down_rot_z = false;//
            btn_TrackPad_rot_z.Top = 40;//
            mouse_delta = 0;
        }
        #endregion
        #endregion
        #region TrackPad default rotation
        #region TrackPad default rotation X
        private void btn_TrackPad_default_rot_x_MouseDown(object sender, MouseEventArgs e)
        {
            timer.Enabled = true;

            mouse_down_default_rot_x = true;//
            mouse_old_position = Cursor.Position.Y;
        }
        private void btn_TrackPad_default_rot_x_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouse_down_default_rot_x)//
            {
                Track_Pad_position.Y = (Cursor.Position.Y - this.Location.Y - TrackPad_delta_position) - gb_TrackPad_default_rot_x.Location.Y;//

                mouse_delta = ((mouse_old_position - Cursor.Position.Y) / TrackPad_Speed);

                if (Track_Pad_position.Y < 5) Track_Pad_position.Y = 5;
                if (Track_Pad_position.Y > 80) Track_Pad_position.Y = 80;

                btn_TrackPad_default_rot_x.Top = Track_Pad_position.Y;//
            }
        }
        private void btn_TrackPad_default_rot_x_MouseUp(object sender, MouseEventArgs e)
        {
            timer.Enabled = false;

            mouse_down_default_rot_x = false;//
            btn_TrackPad_default_rot_x.Top = 40;//
            mouse_delta = 0;
        }
        #endregion
        #region TrackPad default rotation Y
        private void btn_TrackPad_default_rot_y_MouseDown(object sender, MouseEventArgs e)
        {
            timer.Enabled = true;

            mouse_down_default_rot_y = true;//
            mouse_old_position = Cursor.Position.Y;
        }
        private void btn_TrackPad_default_rot_y_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouse_down_default_rot_y)//
            {
                Track_Pad_position.Y = (Cursor.Position.Y - this.Location.Y - TrackPad_delta_position) - gb_TrackPad_default_rot_y.Location.Y;//

                mouse_delta = ((mouse_old_position - Cursor.Position.Y) / TrackPad_Speed);

                if (Track_Pad_position.Y < 5) Track_Pad_position.Y = 5;
                if (Track_Pad_position.Y > 80) Track_Pad_position.Y = 80;

                btn_TrackPad_default_rot_y.Top = Track_Pad_position.Y;//
            }
        }
        private void btn_TrackPad_default_rot_y_MouseUp(object sender, MouseEventArgs e)
        {
            timer.Enabled = false;

            mouse_down_default_rot_y = false;//
            btn_TrackPad_default_rot_y.Top = 40;//
            mouse_delta = 0;
        }
        #endregion
        #region TrackPad default rotation Z
        private void btn_TrackPad_default_rot_z_MouseDown(object sender, MouseEventArgs e)
        {
            timer.Enabled = true;

            mouse_down_default_rot_z = true;//
            mouse_old_position = Cursor.Position.Y;
        }
        private void btn_TrackPad_default_rot_z_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouse_down_default_rot_z)//
            {
                Track_Pad_position.Y = (Cursor.Position.Y - this.Location.Y - TrackPad_delta_position) - gb_TrackPad_default_rot_z.Location.Y;//

                mouse_delta = ((mouse_old_position - Cursor.Position.Y) / TrackPad_Speed);

                if (Track_Pad_position.Y < 5) Track_Pad_position.Y = 5;
                if (Track_Pad_position.Y > 80) Track_Pad_position.Y = 80;

                btn_TrackPad_default_rot_z.Top = Track_Pad_position.Y;//
            }
        }
        private void btn_TrackPad_default_rot_z_MouseUp(object sender, MouseEventArgs e)
        {
            timer.Enabled = false;

            mouse_down_default_rot_z = false;//
            btn_TrackPad_default_rot_z.Top = 40;//
            mouse_delta = 0;
        }
        #endregion
        #endregion
        #region Timer

        private void timer_Tick(object sender, EventArgs e)
        {
            Draw();
        }

        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            Constant._1 = (float)Convert.ToDouble(textBox1.Text);
            Constant._2 = (float)Convert.ToDouble(textBox2.Text);
            Constant._3 = (float)Convert.ToDouble(textBox3.Text);
            Constant._4 = (float)Convert.ToDouble(textBox4.Text);
            Constant._5 = (float)Convert.ToDouble(textBox5.Text);
        }

        #endregion

        private void btn_create_moleculs_arr_Click(object sender, EventArgs e)
        {
            /*
            //===========1================
            Moleculs.Add(new Molecul(new Vector3(0.1f, 0.1f, 0), new Vector3(0, 0f, 0), 10f, false));
            Moleculs.Add(new Molecul(new Vector3(0.3f, 0.1f, 0), new Vector3(0, 0f, 0), 10f, false));
            Moleculs.Add(new Molecul(new Vector3(0.2f, 0.0f, 0), new Vector3(0, 0f, 0), 10f, false));
            Moleculs.Add(new Molecul(new Vector3(0.1f, 0.2f, 0), new Vector3(0, 0f, 0), 10f, false));
            Moleculs.Add(new Molecul(new Vector3(0.3f, 0.2f, 0), new Vector3(0, 0f, 0), 10f, false));
            Moleculs.Add(new Molecul(new Vector3(0.2f, 0.3f, 0), new Vector3(0, 0f, 0), 10f, false));

            //===========2================
            Moleculs.Add(new Molecul(new Vector3(0.1f, 0.1f, 0), new Vector3(0, 0f, 0), 10f, false));
            Moleculs.Add(new Molecul(new Vector3(0.3f, 0.1f, 0), new Vector3(0, 0f, 0), 10f, false));
            Moleculs.Add(new Molecul(new Vector3(0.2f, 0.0f, 0), new Vector3(0, 0f, 0), 10f, false));
            Moleculs.Add(new Molecul(new Vector3(0.1f, 0.2f, 0), new Vector3(0, 0f, 0), 10f, false));
            Moleculs.Add(new Molecul(new Vector3(0.3f, 0.2f, 0), new Vector3(0, 0f, 0), 10f, false));
            Moleculs.Add(new Molecul(new Vector3(0.2f, 0.3f, 0), new Vector3(0, 0f, 0), 10f, false));
            */


            float start_pos_x = -0.6f;
            float start_pos_y = -0.6f;

            
            float pos_y = start_pos_y;
            for (int j = 0; j < 3; j++)
            {

                float pos_x = start_pos_x;
                for (int i = 0; i < 8; i++)
                {
                    Moleculs.Add(new Molecul(new Vector3(pos_x - 0.1f, pos_y + 0.1f, 0), new Vector3(0, 0f, 0), 10f, false));
                    Moleculs.Add(new Molecul(new Vector3(pos_x - 0.1f, pos_y + 0.2f, 0), new Vector3(0, 0f, 0), 10f, false));
                    Moleculs.Add(new Molecul(new Vector3(pos_x, pos_y, 0), new Vector3(0, 0f, 0), 10f, false));
                    Moleculs.Add(new Molecul(new Vector3(pos_x, pos_y + 0.3f, 0), new Vector3(0, 0f, 0), 10f, false));

                    pos_x += 0.2f;
                }

                Moleculs.RemoveAt(Moleculs.Count - 1);
                Moleculs.RemoveAt(Moleculs.Count - 1);

                pos_y += 0.4f;
            }

            //Moleculs[2].stat = true;
            //Moleculs[26].stat = true;
            //Moleculs[63].stat = true;
            //Moleculs[Moleculs.Count-3].stat = true;

            /*
            for (int i = 0; i < 7; i++)
            {
                Moleculs[2+4*i].stat = true;
                Moleculs[63 + 4 * i].stat = true;
            }
            for (int i = 0; i < 3; i++)
            {
                Moleculs[1 + 30 * i].stat = true;
                Moleculs[0 + 30 * i].stat = true;

                Moleculs[29 + 30 * i].stat = true;
                Moleculs[28 + 30 * i].stat = true;
            }*/

            /*foreach (var item in Moleculs)
            {
                if (item.Position.X < -0.58f) item.stat = true;
                if (item.Position.X > 0.58f) item.stat = true;

                if (item.Position.Y < -0.45f) item.stat = true;
                if (item.Position.Y > 0.35f) item.stat = true;
            }*/



        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            Moleculs = new List<Molecul>();
        }

        private void btn_apply_Click(object sender, EventArgs e)
        {
            Constant.MAX_DISTANCE = (float)Convert.ToDouble(tb_max_distance.Text);
        }

        private void btn_Create_Click(object sender, EventArgs e)
        {
            float X = (float)Convert.ToDouble(tb_Mol_X.Text);
            float Y = (float)Convert.ToDouble(tb_Mol_Y.Text);
            float Z = (float)Convert.ToDouble(tb_Mol_Z.Text);

            float X_speed = (float)Convert.ToDouble(tb_Mol_X_Speed.Text);
            float Y_speed = (float)Convert.ToDouble(tb_Mol_Y_Speed.Text);
            float Z_speed = (float)Convert.ToDouble(tb_Mol_Z_Speed.Text);

            float Mass = (float)Convert.ToDouble(tb_Mol_mass.Text);



            Moleculs.Add(new Molecul(new Vector3(X, Y, Z), new Vector3(X_speed, Y_speed, Z_speed), Mass, cb_Mol_Static.Checked));

        }

        private void btn_Clear_Din_Click(object sender, EventArgs e)
        {
            Moleculs.RemoveAll(mol => mol.stat == false);
        }

        private void btn_Clear_Static_Click(object sender, EventArgs e)
        {
            Moleculs.RemoveAll(mol => mol.stat == true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Moleculs.Add(new Molecul(new Vector3(0.0f, 0.0f, 0), 100));
            Moleculs.Add(new Molecul(new Vector3(-0.2f, -0.2f, 0), 10));
            Moleculs.Add(new Molecul(new Vector3(0.2f, -0.2f, 0), 10));
        }
    }
}
