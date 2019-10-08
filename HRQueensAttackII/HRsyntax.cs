static int queensAttack(int n, int k, int r_q, int c_q, int[][] obstacles) {
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

    int centerAtk = (n - 1) * 4; 
    // calculates the maximum atk squares if Q is centered, -1 because we can't count the queen's square.
    // * 4 because that counts the Vertial, Horizontal, Positive Diagonal, and Negative Diagonal attack lines, in both directions

    int VertDist = Math.Abs((r_q - 1) - (n - r_q)); // calculates squares lost on diagonal attacks per each vertical step off center
    int HoriDist = Math.Abs((c_q - 1) - (n - c_q)); // calculates squares lost on diagonal attacks per each horizontal step off center
    // Still works for (n % 2 == 0) values where there isn't a true center, it will just have an additional lost square.

    int lostSquares = Math.Max(VertDist, HoriDist); 
    // Takes the larger of the two values because we want to account for the largest amount of lost squares

    int maxAtk = centerAtk - lostSquares;

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

    return maxAtk;
}
