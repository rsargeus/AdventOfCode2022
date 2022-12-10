


// See https://aka.ms/new-console-template for more information
string inputtext = System.IO.File.ReadAllText(@"input.txt");
string[] values = inputtext.Split(new[] { "\n" }, StringSplitOptions.None);

Tree[,] charMatrix = new Tree[values.Length, values[0].Length];

for (int i = 0; i < values.Length; i++)
{
    for (int j = 0; j < values[i].Length; j++)
    {

        int height = int.Parse(new string(new char[] { values[i][j] }));

        charMatrix[i, j] = new Tree { Height = height, Visible=false };
    }
}


Enumerable.Range(0, charMatrix.GetLength(0)).ToList().ForEach(t => UpdateColumn(charMatrix, t));

Enumerable.Range(0, charMatrix.GetLength(0)).ToList().ForEach(t => UpdateColumnDescending(charMatrix, t));

Enumerable.Range(0, charMatrix.GetLength(0)).ToList().ForEach(t => UpdateRow(charMatrix, t));

Enumerable.Range(0, charMatrix.GetLength(0)).ToList().ForEach(t => UpdateRowDescending(charMatrix, t));

PrintMatrix(charMatrix);

static void PrintMatrix(Tree[,] matrix)
{
    int visibleCounter = 0;
    int highestScenicScore = 0;

    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        for (int j = 0; j < matrix.GetLength(1); j++)
        {

            int scenicScore = matrix[i, j].VievingDistanceUp * matrix[i, j].VievingDistanceLeft * matrix[i, j].VievingDistanceDown * matrix[i, j].VievingDistanceRight;
            //Console.Write($"{matrix[i, j].Height},{matrix[i, j].Visible},{scenicScore}:{matrix[i, j].VievingDistanceUp},{matrix[i, j].VievingDistanceLeft},{matrix[i, j].VievingDistanceDown},{matrix[i, j].VievingDistanceRight} ");
            //Console.Write($"{matrix[i, j].Height},{matrix[i, j].Visible},{matrix[i, j].VievingDistanceDown} ");
            Console.Write($"{matrix[i, j].Height},{matrix[i, j].Visible},{scenicScore} ");

            if (matrix[i, j].Visible)
            {
                visibleCounter++;
            }

            if (scenicScore > highestScenicScore)
            {
                highestScenicScore = scenicScore;
            }
        }
        Console.WriteLine();
    }

    Console.WriteLine("Part 1: " + visibleCounter);
    Console.WriteLine("Part 2: " + highestScenicScore);

}

static void UpdateRow(Tree[,] charMatrix, int rowNo)
{
    int highestTree = -1;

    for (int i = 0; i < charMatrix.GetLength(1); i++)
    {
        var currentTree = charMatrix[rowNo, i];

        if (currentTree.Height > highestTree)
        {
            currentTree.Visible = true;
            highestTree = currentTree.Height;
        }

        for (int j = i-1; j >= 0; j--)
        {
            if (charMatrix[rowNo, j].Height >= currentTree.Height)
            {
                currentTree.VievingDistanceLeft = i-j;
                break;
            }
            if (j == 0)
            {
                currentTree.VievingDistanceLeft = i;
            }
        }
    }
}

static void UpdateRowDescending(Tree[,] charMatrix, int rowNo)
{
    int highestTree = -1;

    for (int i = charMatrix.GetLength(0) - 1; i >= 0; i--)
    {
        var currentTree = charMatrix[rowNo, i];

        if (currentTree.Height > highestTree)
        {
            currentTree.Visible = true;
            highestTree = currentTree.Height;
        }

        for (int j = i + 1; j < charMatrix.GetLength(1); j++)
        {
            if (charMatrix[rowNo, j].Height >= currentTree.Height)
            {
                currentTree.VievingDistanceRight = j-i;
                break;
            }
            if (j == charMatrix.GetLength(1)-1)
            {
                currentTree.VievingDistanceRight = charMatrix.GetLength(0) - 1-i;
            }
        }
    }
}

static void UpdateColumn(Tree[,] charMatrix,int columnNo)
{
    int highestTree = -1;

    for (int i = 0; i < charMatrix.GetLength(0); i++)
    {
        var currentTree = charMatrix[i, columnNo];

        if (currentTree.Height > highestTree)
        {
            currentTree.Visible = true;
            highestTree = currentTree.Height;
        }

        for (int j = i - 1; j >= 0; j--)
        {
            if (charMatrix[j, columnNo].Height >= currentTree.Height)
            {
                currentTree.VievingDistanceUp = i - j;
                break;
            }
            if (j == 0)
            {
                currentTree.VievingDistanceUp = i;
            }
        }
    }
}

static void UpdateColumnDescending(Tree[,] charMatrix, int columnNo)
{
    int highestTree = -1;

    for (int i = charMatrix.GetLength(0)-1; i >= 0; i--)
    {
        var currentTree = charMatrix[i, columnNo];

        if (currentTree.Height > highestTree)
        {
            currentTree.Visible = true;
            highestTree = currentTree.Height;
        }

        for (int j = i + 1; j < charMatrix.GetLength(0); j++)
        {
            if (charMatrix[j, columnNo].Height >= currentTree.Height)
            {
                currentTree.VievingDistanceDown = j - i;
                break;
            }
            if (j == charMatrix.GetLength(0) - 1)
            {
                currentTree.VievingDistanceDown = charMatrix.GetLength(0) - 1 - i;
            }
        }
    }
}

class Tree
{
    public int Height { get; set; }
    public bool Visible { get; set; }
    public int VievingDistanceUp { get; set; }
    public int VievingDistanceRight { get; set; }
    public int VievingDistanceDown { get; set; }
    public int VievingDistanceLeft { get; set; }
}