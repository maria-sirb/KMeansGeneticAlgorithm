using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAPExamen
{ 
    public static class Engine
    {
        public static List<PointF> pointsList;
        public static Random rnd = new Random();
        public static int pointsNr = 100;
        public static List<Solution> population = new List<Solution>();
        public static Panel panel1;
        public static ListBox listBox1;
        public static Graphics g;
        public static int populationCount = 100;

        public static void Init() 
        {

           GeneratePoints();
           GeneratePopulation();
           for(int i = 0; i < 30; i++)
           {
                //listBox1.Items.Add(i);
                SortPopulation();
                List<Solution> newPopulation = new List<Solution>();
                for(int j = 0; j < populationCount; j++)
                {
                    
                    Solution parent = population[rnd.Next(5)];
                    Solution child = population[j].Crossover(parent);
                   // listBox1.Items.Add(population[j].GetFitness());
                   // population[j].ViewSolution();
                    child.Mutate();
                   
                    newPopulation.Add(child);   

                }
                for (int j = 0; j < population.Count; j++)
                {
                    population[j] = newPopulation[j];
                }


           }
            SortPopulation();
            population[0].DrawSolution();


        }
        public static void SortPopulation()
        {
            population.Sort((Solution a, Solution b) => a.GetFitness().CompareTo(b.GetFitness()));
        }
        public static void GeneratePopulation()
        {
            for(int i = 0; i < populationCount; i++)
            {
                population.Add(new Solution());
            }
        }
        public static void GeneratePoints()
        {
            pointsList = new List<PointF>();
            for (int i = 0; i < pointsNr; i++)
            {
                PointF a = new PointF(rnd.Next(panel1.Width), rnd.Next(panel1.Height));
                g.DrawEllipse(Pens.Black, a.X, a.Y, 3, 3);
                g.FillEllipse(Brushes.Black, a.X, a.Y, 3, 3);
                pointsList.Add(a);
            }
        }
        public static void DrawPoints()
        {
            for (int i = 0; i < pointsNr; i++)
            {
                g.DrawEllipse(Pens.Black, pointsList[i].X, pointsList[i].Y, 3, 3);
                g.FillEllipse(Brushes.Black, pointsList[i].X, pointsList[i].Y, 3, 3);
               
            }
        }
    }
}
