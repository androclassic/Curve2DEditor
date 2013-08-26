using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CurveEditor
{

    public partial class Curve2D : Form
    {
        int cellsWidth;
        int cellsHeight;
        int cellSize;
        List<CurvePoint> points;

        bool tangentSelected;
        bool pointSelected;
        int indexSelected;

        public Curve2D()
        {
            InitializeComponent();
            cellsWidth = 35;
            cellsHeight = 15;
            cellSize = 25;
            this.Size = new System.Drawing.Size((cellSize * cellsWidth)+50, (cellSize * cellsHeight)+200);
            tangentSelected = false;
            pointSelected = false;
           
            indexSelected = 0;
            points = new List<CurvePoint>();
            points.Add(new CurvePoint(0, (cellSize * cellsHeight) / 2));
            points.Add(new CurvePoint((cellSize * cellsWidth), (cellSize * cellsHeight) / 2));

        }

        private static int CompareCurvePointByX(CurvePoint p1, CurvePoint p2)
        {
            if (p1.Point2D.X > p2.Point2D.X)
                return 1;
            else if( p1.Point2D.X < p2.Point2D.X )
                return -1;
            else
                return 0;
        }

        private void DisplayPointsText()
        {
            CurvePointsText.Text  = "";
            for (int i = 0; i < points.Count; i++)
            {
                PointF point = new PointF(points[i].Point2D.X / (cellsWidth * cellSize), points[i].Point2D.Y / (cellsHeight * cellSize));
                CurvePointsText.Text = CurvePointsText.Text + " " + point.ToString() + " " + points[i].GetTangentValue().ToString() + " / ";
            }
        }


        private void DrawGraph(System.Drawing.Graphics go)
        {
            go.Clear(Color.White);
            DrawLines(go);

            for (int i = 0; i < points.Count; i++)
            {
                points[i].DrawCurve2DPoint(go);
                if (i < points.Count - 1)
                    DrawCurve(go, 40, points[i].Point2D, points[i + 1].Point2D, points[i].GetTangentValue() , points[i + 1].GetTangentValue());
            
            }
        }


        public void DrawCurve(System.Drawing.Graphics go ,int steps, PointF P1, PointF P2, float T1, float T2)
        {
            PointF back = P1;

            for (int t = 0; t < steps; t++)
            {
                                    
                float s = t / (float)steps;    // scale s to go from 0 to 1
         
                double x = P1.X + (P2.X - P1.X) * s;

                double y = Hermite(s, P1.Y, P2.Y, T1 * (P2.X - P1.X), T2 * (P2.X - P1.X));         

                PointF front= new PointF((float)x, (float)y);

                Pen pen = new Pen(System.Drawing.Color.Blue, 1);
                go.DrawLine(pen, back, front);
                back = front;


            }
        }

        private double Hermite(float s, float P1, float P2, float T1, float T2)
        {
            double h1 = 2 * Math.Pow(s, 3) - 3 * Math.Pow(s, 2) + 1;          // calculate basis function 1
            double h2 = -2 * Math.Pow(s, 3) + 3 * Math.Pow(s, 2);              // calculate basis function 2
            double h3 = Math.Pow(s, 3) - 2 * Math.Pow(s, 2) + s;         // calculate basis function 3
            double h4 = Math.Pow(s, 3) - Math.Pow(s, 2);                   // calculate basis function 4


            return h1 * P1 +                    // multiply and sum all funtions
                       h2 * P2 +                    // together to build the interpolated
                       h3 * T1 +                    // point along the curve.
                       h4 * T2 ;

        }

        private void DrawLines(System.Drawing.Graphics go)
        {
            
            Pen pen = new Pen(System.Drawing.Color.Gray, 1);
            for (int i = 0; i < cellsWidth * cellSize; i += cellSize)
                go.DrawLine(pen, new Point(i, 0), new Point(i, (int)cellsHeight * cellSize));

            for (int i = 0; i < cellsHeight * cellSize; i += cellSize)
                go.DrawLine(pen, new Point(0, i), new Point((int)cellsWidth * cellSize, i));

        }

        public void AddPoint(int x, int y)
        {
            points.Add( new CurvePoint(x, y) );
        }

        private void framebuffer_Paint(object sender, PaintEventArgs e)
        {
            UpdateFrame(e.Graphics);
        }

        private void framebuffer_Click(object sender, EventArgs e)
        {

        }

        private void UpdatePoints()
        {

            CurvePoint currentPoint = points[indexSelected];
            points.Sort(CompareCurvePointByX);
            indexSelected = points.FindIndex(delegate(CurvePoint p) { return p.Point2D == currentPoint.Point2D; });
        }

        private void UpdateFrame(System.Drawing.Graphics go)
        {
            if (pointSelected)
                UpdatePoints();

            DisplayPointsText();
            DrawGraph(go);
            pointTtext.Text = (points[indexSelected].GetTangentValue()).ToString();
            pointXtext.Text = (points[indexSelected].Point2D.X).ToString();
            pointYtext.Text = (points[indexSelected].Point2D.Y).ToString();

            framebuffer.Invalidate();

        }

        private void framebuffer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            UpdatePoints();
            for (int i = 1; i <points.Count; i++)
            {
                if (points[i].Point2D.X > e.X)
                {
                    PointF p1 = points[i-1].Point2D;
                    PointF p2 = points[i].Point2D;


                    float s = (e.X - p1.X) / (p2.X - p1.X);
                    float y = (float)Hermite(s, p1.Y, p2.Y, points[i - 1].GetTangentValue() * (p2.X - p1.X), points[i].GetTangentValue()*(p2.X - p1.X));
                    points.Insert(i,new CurvePoint(e.X, y));
                    break;
                }
            }
            Refresh();
        }

        private void framebuffer_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private float SquaredDistance(PointF p1, PointF p2)
        {
            return (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y);
        }

        private void framebuffer_MouseMove(object sender, MouseEventArgs e)
        {
            if(tangentSelected)
            {
                points[indexSelected].UpdateTangentController(new Point(e.X, e.Y));
                Refresh();
            }
            if (pointSelected)
            {
                points[indexSelected].UpdatePointController(new Point(e.X, e.Y));
                Refresh();
            }
        }

        private void framebuffer_MouseUp(object sender, MouseEventArgs e)
        {
            tangentSelected = false;
            pointSelected = false;
            if (indexSelected < points.Count)
            {
                points[indexSelected].Select(-1);
                Refresh();
            }
        }

        private void framebuffer_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < points.Count; i++)
                if (SquaredDistance(new Point(e.X, e.Y), points[i].Tangent) < 100)
                {
                    indexSelected = i;
                    this.tangentSelected = true;
                    points[indexSelected].Select(0);
                    return;
                }
                else if (SquaredDistance(new Point(e.X, e.Y), points[i].Tangent2) < 100)
                    {
                        indexSelected = i;
                        this.tangentSelected = true;
                        points[indexSelected].Select(1);
                        return;
                    }
                else if (SquaredDistance(new Point(e.X, e.Y), points[i].Point2D) < 100)
                {
                    indexSelected = i;
                    this.pointSelected = true;
                    return;
                }
        
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

    }

    public class CurvePoint
    {
        PointF m_point;
        PointF[] m_tangentControl;
        
        static int distance = 50;
        int selectedIndex;

        public CurvePoint(PointF p)
        {
            m_point = p;
            m_tangentControl = new PointF[2];
            m_tangentControl[0] = new PointF(m_point.X + distance, m_point.Y);
            m_tangentControl[1] = new PointF(m_point.X - distance, m_point.Y);

            selectedIndex = -1;
        }
        public CurvePoint(float x, float y)
        {
            m_point = new PointF(x,y);
            m_tangentControl = new PointF[2];
            m_tangentControl[0] = new PointF(m_point.X + distance, m_point.Y);
            m_tangentControl[1] = new PointF(m_point.X - distance, m_point.Y);
            selectedIndex = -1;
        }

        //index -1 unselected
        public void Select(int index)
        {
            selectedIndex = index;
        }

        public PointF Point2D
        {
            get
            {
                return m_point;
            }
            set
            {
                m_point = value;
            }
        }

        public PointF Tangent
        {
            get
            {
                return m_tangentControl[0];
            }
            set
            {
                m_tangentControl[0] = value;
            }
        }

        public PointF Tangent2
        {
            get
            {
                return m_tangentControl[1];
            }
            set
            {
                m_tangentControl[1] = value;
            }
        }

        
        public void DrawCurve2DPoint(System.Drawing.Graphics go)
        {
            Pen pen = new Pen(Color.FromArgb(128, 255,0,0), 3);
            go.DrawLine(pen, m_point, m_tangentControl[0]);
            go.DrawLine(pen, m_point, m_tangentControl[1]);

            int drawSize = 4;
            go.DrawEllipse(pen, new Rectangle(new Point((int)m_tangentControl[0].X - drawSize / 2, (int)m_tangentControl[0].Y - drawSize / 2), new Size(drawSize, drawSize)));
            go.DrawEllipse(pen, new Rectangle(new Point((int)m_tangentControl[1].X - drawSize / 2, (int)m_tangentControl[1].Y - drawSize / 2), new Size(drawSize, drawSize)));

            go.DrawRectangle(new Pen(System.Drawing.Color.Blue, 3), new Rectangle(new Point((int)m_point.X - drawSize / 2, (int)m_point.Y - drawSize / 2), new Size(drawSize, drawSize)));

             if (selectedIndex != -1)
                 go.DrawEllipse(new Pen(Color.FromArgb(128, 0, 255, 0), 2), new Rectangle(new Point((int)m_tangentControl[selectedIndex].X - drawSize, (int)m_tangentControl[selectedIndex].Y - drawSize), new Size(drawSize * 2, drawSize * 2)));
            
           
        }

        public void UpdatePointController(Point mousePosition)
        {
            Size move = new Size(mousePosition.X - (int)m_point.X, mousePosition.Y - (int)m_point.Y);
            m_point += move;
            m_tangentControl[0] += move;
            m_tangentControl[1] += move;
            

        }
        public void UpdateTangentController(Point mousePosition)
        {
            float x = mousePosition.X - m_point.X;
            float y = mousePosition.Y - m_point.Y;

            if (selectedIndex == -1)
                return;
            if (selectedIndex == 0 && x < 1)
                x=1;
            if (selectedIndex == 1 && x > -1)
                x=-1;


            float magnitude = (float)Math.Sqrt( (double)(x * x + y * y) );
            x *= distance / magnitude;
            y *= distance / magnitude;

            if (selectedIndex == 0)
            {
                m_tangentControl[0] = m_point + new Size((int)x, (int)y);
                m_tangentControl[1] = m_point - new Size((int)x, (int)y);
            }
            else
            {
                m_tangentControl[0] = m_point - new Size((int)x, (int)y);
                m_tangentControl[1] = m_point + new Size((int)x, (int)y);
            }
        }

        public float GetTangentValue()
        {
            float value = (float)( m_tangentControl[1].Y - m_point.Y ) / (float)(m_tangentControl[1].X - m_point.X);

            if (value > distance)
                return distance;
            else if (value < -distance)
                return -distance ;
            else
                return value ;

        }

    
    }



}
