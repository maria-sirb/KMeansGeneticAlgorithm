using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAPExamen
{
    public class Solution
    {
        public List<List<PointF>> solution;
        public List<PointF> gravityCenters = new List<PointF>();
        public List<Color> colors = new List<Color> {Color.Red, Color.Orange, Color.Green, Color.Purple };
        public Solution()
        {
           gravityCenters = new List<PointF>();
           for(int i = 0; i < 4; i++)
           {
                Point gc = new Point(Engine.rnd.Next(Engine.panel1.Width), Engine.rnd.Next(Engine.panel1.Height));
                gravityCenters.Add(gc);
              //  Engine.listBox1.Items.Add(gc);
            }
            GroupPoints();
            SetGravityCenters();

        }
        public Solution (List<PointF> gravityCenters)
        {
            this.gravityCenters = gravityCenters;
            GroupPoints();
            SetGravityCenters();
        }
        private void GroupPoints()
        {
            ClearSolution();
           // Engine.listBox1.Items.Add(availablePoints.Count);
            for (int i = 0; i < Engine.pointsList.Count; i++) 
            {
                double minDistance = int.MaxValue;
                int centerNo = 0;
                for(int j = 0; j < gravityCenters.Count; j++)
                {
                    double distance = GetDistance(Engine.pointsList[i], gravityCenters[j]);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        centerNo = j;
                    }
                }
               // Engine.listBox1.Items.Add(availablePoints[i]);
               // Engine.listBox1.Items.Add(centerNo);
                solution[centerNo].Add(Engine.pointsList[i]);
            }
        }
        public Solution Crossover(Solution parent)
        {
            List<PointF> newCenters = new List<PointF>();
            int cutIndex = Engine.rnd.Next(4);
            for(int i = 0; i < cutIndex; i++)
            {
                newCenters.Add(gravityCenters[i]);
            }
            for(int i = cutIndex; i < 4; i++)
            {
                newCenters.Add(parent.gravityCenters[i]);
            }
            Solution child = new Solution(newCenters);
            return child;
        }
        public void Mutate()
        {
            int centerToChange = Engine.rnd.Next(4);
            gravityCenters[centerToChange] = new PointF(Engine.rnd.Next(Engine.panel1.Width), Engine.rnd.Next(Engine.panel1.Height));
            GroupPoints();
            SetGravityCenters();
        }
        public double GetFitness()
        {
            double totalDistance = 0;
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < solution[i].Count; j++)
                {
                    totalDistance += GetDistance(solution[i][j], gravityCenters[i]);
                }
            }
            return totalDistance;
        }
        private void SetGravityCenters()
        {
            gravityCenters = new List<PointF>();
            for (int i = 0; i < 4; i++)
            {
                PointF gc = GetGravityCenter(solution[i]);
                gravityCenters.Add(gc);
            }
        }
       
        private double GetDistance(PointF a, PointF b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
        private PointF GetGravityCenter(List<PointF> points)
        {
            float gcX = 0, gcY = 0;
            for(int i = 0; i < points.Count; i++)
            {
                gcX += points[i].X;
                gcY += points[i].Y;
            }
            PointF cg = new PointF(gcX / points.Count, gcY / points.Count);
            return cg;
        }
        public void DrawSolution()
        {
            for (int i = 0; i < 4; i++)
            {
                Engine.g.DrawEllipse(new Pen(colors[i]), gravityCenters[i].X, gravityCenters[i].Y, 6, 6);
                Engine.g.FillEllipse(new SolidBrush(colors[i]), gravityCenters[i].X, gravityCenters[i].Y, 6, 6);
                for (int j = 0; j < solution[i].Count; j++)
                {
                    Engine.g.DrawEllipse(new Pen(colors[i]), solution[i][j].X, solution[i][j].Y, 4, 4);
                    Engine.g.FillEllipse(new SolidBrush(colors[i]), solution[i][j].X, solution[i][j].Y, 4, 4);
                    Engine.g.DrawLine(new Pen(colors[i], 1), solution[i][j], gravityCenters[i]);
                }

            }
        }
        public void ClearSolution()
        {
            solution = new List<List<PointF>>();
            for (int i = 0; i < 4; i++)
            {
                solution.Add(new List<PointF>());
            }
        }
        public void ViewSolution()
        {
           for(int i = 0; i < 4; i++)
            {
                Engine.listBox1.Items.Add(gravityCenters[i]);
            }
        }
    }
}
