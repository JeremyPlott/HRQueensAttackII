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

            int n = 100; // board length (5)
            int k = 100; // obstacle count (4)
            int r_q = 48; // Q column Y (4)
            int c_q = 81; // Q row X (3)

            int[][] obstacles = new int[k][];
            int[] o1 = { 54, 87 };
            int[] o2 = { 64, 97};
            int[] o3 = { 42, 75};
            int[] o4 = { 32, 65};
            int[] o5 = { 42, 87};
            int[] o6 = { 32, 97};
            int[] o7 = { 54, 75};
            int[] o8 = { 64, 65};
            int[] o9 = { 48, 87};
            int[] o10 = { 48, 75};
            int[] o11 = { 54, 81};
            int[] o12 = { 42, 81};
            int[] o13 = { 45, 17};
            int[] o14 = { 14, 24};
            int[] o15 = { 35, 15};
            int[] o16 = { 95, 64};
            int[] o17 = { 63, 87};
            int[] o18 = { 25, 72};
            int[] o19 = { 71, 38};
            int[] o20 = { 96, 97};
            int[] o21 = { 16, 30};
            int[] o22 = { 60, 34};
            int[] o23 = { 31, 67};
            int[] o24 = { 26, 82};
            int[] o25 = { 20, 93};
            int[] o26 = { 81, 38};
            int[] o27 = { 51, 94};
            int[] o28 = { 75, 41};
            int[] o29 = { 79, 84};
            int[] o30 = { 79, 65};
            int[] o31 = { 76, 80};
            int[] o32 = { 52, 87};
            int[] o33 = { 81, 54};
            int[] o34 = { 89, 52};
            int[] o35 = { 20, 31};
            int[] o36 = { 10, 41};
            int[] o37 = { 32, 73};
            int[] o38 = { 83, 98};
            int[] o39 = { 87, 61};
            int[] o40 = { 82, 52};
            int[] o41 = { 80, 64};
            int[] o42 = { 82, 46};
            int[] o43 = { 49, 21};
            int[] o44 = { 73, 86};
            int[] o45 = { 37, 70};
            int[] o46 = { 43, 12};
            int[] o47 = { 94, 28};
            int[] o48 = { 10, 93};
            int[] o49 = { 52, 25};
            int[] o50 = { 52, 68};
            int[] o51 = { 52, 23};
            int[] o52 = { 60, 91};
            int[] o53 = { 79, 17};
            int[] o54 = { 93, 82};
            int[] o55 = { 94, 74};
            int[] o56 = { 12, 18};
            int[] o57 = { 75, 64};
            int[] o58 = { 69, 69};
            int[] o59 = { 50, 61};
            int[] o60 = { 61, 61};
            int[] o61 = { 46, 57};
            int[] o62 = { 67, 45};
            int[] o63 = { 96, 64};
            int[] o64 = { 83, 89};
            int[] o65 = { 58, 87};
            int[] o66 = { 76, 53};
            int[] o67 = { 79, 21};
            int[] o68 = { 94, 70};
            int[] o69 = { 16, 10};
            int[] o70 = { 50, 82};
            int[] o71 = { 92, 20};
            int[] o72 = { 40, 51};
            int[] o73 = { 49, 28};
            int[] o74 = { 51, 82};
            int[] o75 = { 35, 16};
            int[] o76 = { 15, 86};
            int[] o77 = { 78, 89};
            int[] o78 = { 41, 98};
            int[] o79 = { 70, 46};
            int[] o80 = { 79, 79};
            int[] o81 = { 24, 40};
            int[] o82 = { 91, 13};
            int[] o83 = { 59, 73};
            int[] o84 = { 35, 32};
            int[] o85 = { 40, 31};
            int[] o86 = { 14, 31};
            int[] o87 = { 71, 35};
            int[] o88 = { 96, 18};
            int[] o89 = { 27, 39};
            int[] o90 = { 28, 38};
            int[] o91 = { 41, 36};
            int[] o92 = { 31, 63};
            int[] o93 = { 52, 48};
            int[] o94 = { 81, 25};
            int[] o95 = { 49, 90};
            int[] o96 = { 32, 65};
            int[] o97 = { 25, 45};
            int[] o98 = { 63, 94};
            int[] o99 = { 89, 50};
            int[] o100 = { 48, 90};
            obstacles[0] = o1;
            obstacles[1] = o2;
            obstacles[2] = o3;
            obstacles[3] = o4;
            obstacles[4] = o5;
            obstacles[5] = o6;
            obstacles[6] = o7;
            obstacles[7] = o8;
            obstacles[8] = o9;
            obstacles[9] = o10;
            obstacles[10] = o11;
            obstacles[11] = o12;
            obstacles[12] = o13;
            obstacles[13] = o14;
            obstacles[14] = o15;
            obstacles[15] = o16;
            obstacles[16] = o17;
            obstacles[17] = o18;
            obstacles[18] = o19;
            obstacles[19] = o20;
            obstacles[20] = o21;
            obstacles[21] = o22;
            obstacles[22] = o23;
            obstacles[23] = o24;
            obstacles[24] = o25;
            obstacles[25] = o26;
            obstacles[26] = o27;
            obstacles[27] = o28;
            obstacles[28] = o29;
            obstacles[29] = o30;
            obstacles[30] = o31;
            obstacles[31] = o32;
            obstacles[32] = o33;
            obstacles[33] = o34;
            obstacles[34] = o35;
            obstacles[35] = o36;
            obstacles[36] = o37;
            obstacles[37] = o38;
            obstacles[38] = o39;
            obstacles[39] = o40;
            obstacles[40] = o41;
            obstacles[41] = o42;
            obstacles[42] = o43;
            obstacles[43] = o44;
            obstacles[44] = o45;
            obstacles[45] = o46;
            obstacles[46] = o47;
            obstacles[47] = o48;
            obstacles[48] = o49;
            obstacles[49] = o50;
            obstacles[50] = o51;
            obstacles[51] = o52;
            obstacles[52] = o53;
            obstacles[53] = o54;
            obstacles[54] = o55;
            obstacles[55] = o56;
            obstacles[56] = o57;
            obstacles[57] = o58;
            obstacles[58] = o59;
            obstacles[59] = o60;
            obstacles[60] = o61;
            obstacles[61] = o62;
            obstacles[62] = o63;
            obstacles[63] = o64;
            obstacles[64] = o65;
            obstacles[65] = o66;
            obstacles[66] = o67;
            obstacles[67] = o68;
            obstacles[68] = o69;
            obstacles[69] = o70;
            obstacles[70] = o71;
            obstacles[71] = o72;
            obstacles[72] = o73;
            obstacles[73] = o74;
            obstacles[74] = o75;
            obstacles[75] = o76;
            obstacles[76] = o77;
            obstacles[77] = o78;
            obstacles[78] = o79;
            obstacles[79] = o80;
            obstacles[80] = o81;
            obstacles[81] = o82;
            obstacles[82] = o83;
            obstacles[83] = o84;
            obstacles[84] = o85;
            obstacles[85] = o86;
            obstacles[86] = o87;
            obstacles[87] = o88;
            obstacles[88] = o89;
            obstacles[89] = o90;
            obstacles[90] = o91;
            obstacles[91] = o92;
            obstacles[92] = o93;
            obstacles[93] = o94;
            obstacles[94] = o95;
            obstacles[95] = o96;
            obstacles[96] = o97;
            obstacles[97] = o98;
            obstacles[98] = o99;
            obstacles[99] = o100;

            //expect 40

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

            // all obstacles on an atk line
            IEnumerable<int[]> atkLinObs = obstacles.Where(o => o[0] == r_q || o[1] == c_q || Math.Abs(c_q - o[1]) == Math.Abs(r_q - o[0]));

            // splitting by cardinal directions
            IEnumerable<int[]> obsN = atkLinObs.Where(o => o[1] == c_q && o[0] - r_q > 0); // obs on same column && north of queen
            IEnumerable<int[]> obsS = atkLinObs.Where(o => o[1] == c_q && o[0] - r_q < 0);
            IEnumerable<int[]> obsE = atkLinObs.Where(o => o[0] == r_q && o[1] - c_q > 0);
            IEnumerable<int[]> obsW = atkLinObs.Where(o => o[0] == r_q && o[1] - c_q < 0);
                             
            IEnumerable<int[]> obsNE = atkLinObs.Where(o => Math.Abs(c_q - o[1]) == Math.Abs(r_q - o[0]) && o[0] - r_q > 0 && o[1] - c_q > 0); // obs on diagonal line && N && E
            IEnumerable<int[]> obsNW = atkLinObs.Where(o => Math.Abs(c_q - o[1]) == Math.Abs(r_q - o[0]) && o[0] - r_q > 0 && o[1] - c_q < 0);
            IEnumerable<int[]> obsSE = atkLinObs.Where(o => Math.Abs(c_q - o[1]) == Math.Abs(r_q - o[0]) && o[0] - r_q < 0 && o[1] - c_q > 0);
            IEnumerable<int[]> obsSW = atkLinObs.Where(o => Math.Abs(c_q - o[1]) == Math.Abs(r_q - o[0]) && o[0] - r_q < 0 && o[1] - c_q < 0);           

            int[] trueN = obsN.OrderBy(o => Math.Abs(o[0] - r_q)).FirstOrDefault(); // get the closest obstacle for each direction
            int[] trueS = obsS.OrderBy(o => Math.Abs(o[0] - r_q)).FirstOrDefault();
            int[] trueE = obsE.OrderBy(o => Math.Abs(o[1] - c_q)).FirstOrDefault();
            int[] trueW = obsW.OrderBy(o => Math.Abs(o[1] - c_q)).FirstOrDefault();
            int[] trueNE = obsNE.OrderBy(o => Math.Abs(o[0] - r_q)).FirstOrDefault();
            int[] trueNW = obsNW.OrderBy(o => Math.Abs(o[1] - c_q)).FirstOrDefault();
            int[] trueSE = obsSE.OrderBy(o => Math.Abs(o[0] - r_q)).FirstOrDefault();
            int[] trueSW = obsSW.OrderBy(o => Math.Abs(o[1] - c_q)).FirstOrDefault();

            if (trueN != null) {
                maxAtk -= (n + 1) - trueN[0];
            }
            if (trueS != null) {
                maxAtk -= trueS[0];
            }
            if (trueE != null) {
                maxAtk -= (n + 1) - trueE[1];
            }
            if (trueW != null) {
                maxAtk -= trueW[1];
            }
            if (trueNE != null) {
                maxAtk -= Math.Min((n + 1) - trueNE[0], (n + 1) - trueNE[1]);
            }
            if (trueNW != null) {
                maxAtk -= Math.Min((n + 1) - trueNW[0], trueNW[1]);
            }
            if (trueSE != null) {
                maxAtk -= Math.Min(trueSE[0], (n + 1) - trueSE[1]);
            }
            if (trueSW != null) {
                maxAtk -= Math.Min(trueSW[0], trueSW[1]);
            }                     
            Console.WriteLine(maxAtk);            
        }
    }
}
