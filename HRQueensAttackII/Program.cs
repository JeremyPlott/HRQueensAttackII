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

            int n = 88587; // board length (5)
            int k = 9; // obstacle count (4)
            int r_q = 20001; // Q column Y (4)
            int c_q = 20003; // Q row X (3)

            int[][] obstacles = new int[k][];
            int[] o1 = { 20001, 20002 }; //5,5
            int[] o2 = { 20001, 20004 }; //4,2
            int[] o3 = { 20000, 20003 }; //2,3
            int[] o4 = { 20002, 20003 }; //4,1
            int[] o5 = { 20000, 20004 };
            int[] o6 = { 20000, 20002 };
            int[] o7 = { 20002, 20004 };
            int[] o8 = { 20002, 20002 };
            int[] o9 = { 564, 323 };
            obstacles[0] = o1;
            obstacles[1] = o2;
            obstacles[2] = o3;
            obstacles[3] = o4;
            obstacles[4] = o5;
            obstacles[5] = o6;
            obstacles[6] = o7;
            obstacles[7] = o8;
            obstacles[8] = o9;           

            //expect 0


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
            IEnumerable<int[]> obsN = atkLinObs.Where(o => o[1] == c_q && o[0] - r_q > 0); // obs on same column && north of queen
            IEnumerable<int[]> obsS = atkLinObs.Where(o => o[1] == c_q && o[0] - r_q < 0);
            IEnumerable<int[]> obsE = atkLinObs.Where(o => o[0] == r_q && o[1] - c_q > 0);
            IEnumerable<int[]> obsW = atkLinObs.Where(o => o[0] == r_q && o[1] - c_q < 0);
                             
            IEnumerable<int[]> obsNE = atkLinObs.Where(o => Math.Abs(c_q - x) == Math.Abs(r_q - y) && o[0] - c_q > 0 && o[1] - r_q > 0); // obs on diagonal line && N && E
            IEnumerable<int[]> obsSE = atkLinObs.Where(o => Math.Abs(c_q - x) == Math.Abs(r_q - y) && o[0] - c_q < 0 && o[1] - r_q > 0);
            IEnumerable<int[]> obsNW = atkLinObs.Where(o => Math.Abs(c_q - x) == Math.Abs(r_q - y) && o[0] - c_q > 0 && o[1] - r_q < 0);
            IEnumerable<int[]> obsSW = atkLinObs.Where(o => Math.Abs(c_q - x) == Math.Abs(r_q - y) && o[0] - c_q < 0 && o[1] - r_q < 0);           

            int[] trueN = obsN.OrderBy(o => Math.Abs(o[0] - c_q)).FirstOrDefault(); // get the closest obstacle for each direction
            int[] trueS = obsS.OrderBy(o => Math.Abs(o[0] - c_q)).FirstOrDefault();
            int[] trueE = obsE.OrderBy(o => Math.Abs(o[1] - r_q)).FirstOrDefault();
            int[] trueW = obsW.OrderBy(o => Math.Abs(o[1] - r_q)).FirstOrDefault();
            int[] trueNE = obsNE.OrderBy(o => Math.Abs(o[0] - c_q)).FirstOrDefault();
            int[] trueSE = obsSE.OrderBy(o => Math.Abs(o[0] - c_q)).FirstOrDefault();
            int[] trueNW = obsNW.OrderBy(o => Math.Abs(o[1] - c_q)).FirstOrDefault();
            int[] trueSW = obsSW.OrderBy(o => Math.Abs(o[1] - c_q)).FirstOrDefault();

            List<int[]> trueObs = new List<int[]>();
            if (trueN != null) { trueObs.Add(trueN); }
            if (trueS != null) { trueObs.Add(trueS); }
            if (trueE != null) { trueObs.Add(trueE); }
            if (trueW != null) { trueObs.Add(trueW); }
            if (trueNE != null) { trueObs.Add(trueNE); }
            if (trueSE != null) { trueObs.Add(trueSE); }
            if (trueNW != null) { trueObs.Add(trueNW); }
            if (trueSW != null) { trueObs.Add(trueSW); }
      
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
