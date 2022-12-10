// See https://aka.ms/new-console-template for more information
string inputtext = System.IO.File.ReadAllText(@"input.txt");
string[] values = inputtext.Split(new[] { "\n" }, StringSplitOptions.None);

char[,] charMatrix = new char[400, 400];
//char[,] charMatrix = new char[5, 6];
//char[,] charMatrix = new char[30, 30];


(int x, int y) s = (120, 80);
//(int x, int y) s = (0, 4);
//(int x, int y) s = (0, 20);
//(int x, int y) s = (11, 20);

List<(int x, int y, string symbol)> shortRope = new();
shortRope.Add((s.x, s.y, "H"));
shortRope.Add((s.x, s.y, "T"));


List<(int x, int y, string symbol)> longRope = new();
longRope.Add((s.x, s.y, "H"));
longRope.Add((s.x, s.y, "1"));
longRope.Add((s.x, s.y, "2"));
longRope.Add((s.x, s.y, "3"));
longRope.Add((s.x, s.y, "4"));
longRope.Add((s.x, s.y, "5"));
longRope.Add((s.x, s.y, "6"));
longRope.Add((s.x, s.y, "7"));
longRope.Add((s.x, s.y, "8"));
longRope.Add((s.x, s.y, "9"));

InitMatrix(charMatrix, s);


int part1 = Simulate(values, charMatrix, shortRope);
InitMatrix(charMatrix, s);
int part2 = Simulate(values, charMatrix, longRope);


Console.WriteLine("Part1: " + part1);
Console.WriteLine("Part2: " + part2);

static void InitMatrix(char[,] matrix, (int x, int y) s)
{
    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            if (j == s.x && i == s.y)
            {
                matrix[i, j] = 's';
            }
            else
            {
                matrix[i, j] = '.';
            }
        }
    }
}

static int GetTailVisits(char[,] matrix)
{
    int tailVisits = 0;

    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            if (matrix[i, j] == '#' || matrix[i, j] == 's')
            {
                tailVisits++;
            }
        }
    }

    return tailVisits;
}

static void PrintMatrix(char[,] matrix, List<(int x, int y, string symbol)> knots)
{
    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            (int x, int y) point = (j, i);

            string symbol = matrix[point.y, point.x].ToString();

            foreach (var knot in knots)
            {                
                if (knot.x == point.x && knot.y == point.y)
                {
                    symbol = knot.symbol;
                    break;
                }
            }

            Console.Write($"{symbol} ");

        }
        Console.WriteLine();
    }
}

static int Simulate(string[] values, char[,] charMatrix, List<(int x, int y, string symbol)> knots)
{
    foreach (string str in values)
    {
        //PrintMatrix(charMatrix, H, T, s);
        //Console.WriteLine($"== {str} ==");


        string direction = str.Split(' ')[0];
        int steps = int.Parse(str.Split(' ')[1]);

        IEnumerable<int> range = Enumerable.Range(0, steps);


        foreach (int i in range)
        {
            knots[0] = direction switch
            {
                "U" => (knots[0].x, knots[0].y - 1, knots[0].symbol),
                "R" => (knots[0].x + 1, knots[0].y, knots[0].symbol),
                "D" => (knots[0].x, knots[0].y + 1, knots[0].symbol),
                "L" => (knots[0].x - 1, knots[0].y, knots[0].symbol)
            };

            for (int k = 1; k < knots.Count; k++)
            {
                knots[k] = MoveTail(knots[k - 1], knots[k]);
            }

            charMatrix[knots.Last() .y, knots.Last().x] = '#';

            //PrintMatrix(charMatrix, knots);
            //Console.ReadLine();
        }
    }

    return GetTailVisits(charMatrix);
}

static (int x, int y, string symbol) MoveTail((int x, int y, string symbol) lead, (int x, int y, string symbol) follow)
{
    var xDiff = follow.x - lead.x;
    var yDiff = follow.y - lead.y;

    if (Math.Abs(xDiff) > 1 && Math.Abs(yDiff) > 1)
    {
        var correctionX = xDiff < 0 ? xDiff + 1 : xDiff - 1;


        var correctionY = yDiff < 0 ? yDiff + 1 : yDiff - 1;
        follow = (follow.x - correctionX, follow.y - correctionY, follow.symbol);


    }
    else if (Math.Abs(xDiff) > 1)
    {
        var correction = xDiff < 0 ? xDiff + 1 : xDiff - 1;
        follow = (follow.x - correction, lead.y, follow.symbol);
    }
    else if (Math.Abs(yDiff) > 1)
    {
        var correction = yDiff < 0 ? yDiff + 1 : yDiff - 1;
        follow = (lead.x, follow.y - correction, follow.symbol);
    }

    return follow;
}