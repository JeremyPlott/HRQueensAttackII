using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

namespace HRQueensAttackII {
    class Program {
        static void Main(string[] args) {

            // input format
            // static int queensAttack(int n, int k, int r_q, int c_q, int[][] obstacles)
            // n = board length
            // k = # of obstacles
            // r_q / c_q = queens location, row column
            // following [x][x] = obstacle locations

            // calculate maximum atk squares
            // foreach o[x][y] in obstacles
            // check if o is on an atk line
            // adjust max atk as necessary

            int n = 5; // board length
            int k = 3; // obstacle count
            int r_q = 4; // Q column Y
            int c_q = 3; // Q row X

            int[][] obstacles = new int[k][];
            int[] o1 = { 5, 5 };
            int[] o2 = { 4, 2 };
            int[] o3 = { 2, 3 };
            obstacles[0] = o1;
            obstacles[1] = o2;
            obstacles[2] = o3;


            /*
            // simple way to calculate the maximum atk for all 8 directions
            // maximum atk squares on a line
            int N = r_q - 1;
            int E = n - c_q;
            int S = n - r_q;
            int W = c_q - 1;
            int NE = Math.Min(N, E); // diagonal atk range == shortest distance to the vertical / horizontal wall in that direction
            int SE = Math.Min(S, E);
            int SW = Math.Min(S, W);
            int NW = Math.Min(N, W);

            int maxAtk = N + NE + E + SE + S + SW + W + NW;
            */


            // alternative to calculate maximum atk using four straight lines 

            // for values of n > 1
            int centerAtk = (n - 1) * 4; // calculates the maximum atk squares if Q is centered, -1 because we can't count the queen's square.
            // * 4 because that counts the Vertial, Horizontal, Positive Diagonal, and Negative Diagonal attack lines, in both directions

            int VertDist = Math.Abs((r_q - 1) - (n - r_q)); // calculates squares lost on diagonal attacks per each vertical step off center
            int HoriDist = Math.Abs((c_q - 1) - (n - c_q)); // calculates squares lost on diagonal attacks per each horizontal step off center
            // Still works for (n % 2 == 0) values where there isn't a true center, it will just have an additional lost square.

            int lostSquares = Math.Max(VertDist, HoriDist); // Takes the larger of the two values because we want to account for the largest amount of lost squares

            int maxAtk = centerAtk - lostSquares;
            //Console.WriteLine($"Center atk {centerAtk}");
            //Console.WriteLine($"VertDist {VertDist}");
            //Console.WriteLine($"HoriDist {HoriDist}");
            //Console.WriteLine($"LostSquares {lostSquares}");
            //Console.WriteLine($"maxAtk {maxAtk}");


            //y = mx + b
            //m = slope
            //b = y intercept

            // need to extract x,y from each obstacle
            // foreach obstacle(x,y) in obstacles


            int x = 0;
            int y = 0;

            // all obstacles on an atk line
            IEnumerable<int[]> atkLinObs = obstacles.Where(o => o[0] == r_q || o[1] == c_q || Math.Abs(c_q - o[1]) == Math.Abs(r_q - o[0]));

            // splitting by cardinal directions
            IEnumerable<int[]> obsN = obstacles.Where(o => o[1] == c_q && o[1] - c_q > 0).; // obs on same column && north of queen
            IEnumerable<int[]> obsS = obstacles.Where(o => o[1] == c_q && o[1] - c_q < 0);
            IEnumerable<int[]> obsE = obstacles.Where(o => o[0] == r_q && o[0] - r_q > 0);
            IEnumerable<int[]> obsW = obstacles.Where(o => o[0] == r_q && o[0] - r_q < 0);

            IEnumerable<int[]> obsNE = obstacles.Where(o => Math.Abs(c_q - x) == Math.Abs(r_q - y) && o[1] - c_q > 0 && o[0] - r_q > 0); // obs on diagonal line && N && E
            IEnumerable<int[]> obsSE = obstacles.Where(o => Math.Abs(c_q - x) == Math.Abs(r_q - y) && o[1] - c_q < 0 && o[0] - r_q > 0);
            IEnumerable<int[]> obsNW = obstacles.Where(o => Math.Abs(c_q - x) == Math.Abs(r_q - y) && o[1] - c_q > 0 && o[0] - r_q < 0);
            IEnumerable<int[]> obsSW = obstacles.Where(o => Math.Abs(c_q - x) == Math.Abs(r_q - y) && o[1] - c_q < 0 && o[0] - r_q < 0);

            var trueN = obsN.OrderBy(o => Math.Abs(o[1] - c_q)).First(); // get the closest obstacle for each direction
            var trueS = obsN.OrderBy(o => Math.Abs(o[1] - c_q)).First();
            var trueE = obsN.OrderBy(o => Math.Abs(o[0] - r_q)).First();
            var trueW = obsN.OrderBy(o => Math.Abs(o[0] - r_q)).First();
            var trueNE = obsNE.OrderBy(o => Math.Abs(o[1] - c_q)).First();
            var trueSE = obsSE.OrderBy(o => Math.Abs(o[1] - c_q)).First();
            var trueNW = obsNW.OrderBy(o => Math.Abs(o[0] - c_q)).First();
            var trueSW = obsSW.OrderBy(o => Math.Abs(o[0] - c_q)).First();

            List<int[]> trueObs = new List<int[]>();
            trueObs.Add(trueN);
            trueObs.Add(trueS);
            trueObs.Add(trueE);
            trueObs.Add(trueW);
            trueObs.Add(trueNE);
            trueObs.Add(trueSE);
            trueObs.Add(trueNW);
            trueObs.Add(trueSW);

            foreach (var o in trueObs) {
                y = o[0]; // row r_q
                x = o[1]; // column c_q

                int obsDistRow = Math.Abs(c_q - x);
                int obsDistCol = Math.Abs(r_q - y);

                int lostHori = Math.Min(x, (n + 1) - x);
                int lostVert = Math.Min(y, (n + 1) - y);

                int obsLostSq = 0;

                // vertical line
                if (x == c_q) { // if obstacle and queen are in same column
                    obsLostSq = lostVert;
                }

                // horizontal line
                if (y == r_q) { // if obstacle and queen are in same row
                    obsLostSq = lostHori;
                }
                
                bool InTheWay = (obsDistCol == obsDistRow); // Checks if obstacle(x,y) falls on Queen diagonal line for both positive and negative diagonals
                // It should be equal if they are on the same line since slope is |1|

                if (InTheWay) {                    
                    obsLostSq = Math.Min(lostVert, lostHori);
                }

                maxAtk -= obsLostSq;                
            }
            Console.WriteLine(maxAtk);
        }
    }
}
