// See https://aka.ms/new-console-template for more information
using System.Text;
using System.Text.RegularExpressions;


string inputtext = System.IO.File.ReadAllText(@"input.txt");
string[] values = inputtext.Split(new[] { "\n" }, StringSplitOptions.None);

/*
var stacks = new Stack<char>[9]
{
    new Stack<char>(),
    new Stack<char>(),
    new Stack<char>(),
    new Stack<char>(),
    new Stack<char>(),
    new Stack<char>(),
    new Stack<char>(),
    new Stack<char>(),
    new Stack<char>()
};*/

/*
stacks[0].Push('Z');
stacks[0].Push('N');

stacks[1].Push('M');
stacks[1].Push('C');
stacks[1].Push('D');

stacks[2].Push('P');
*/



var stringArray = values.Take(9).ToArray();

char[,] charMatrix = new char[stringArray.Length, stringArray[0].Length];

for (int i = 0; i < stringArray.Length; i++)
{
    for (int j = 0; j < stringArray[i].Length; j++)
    {
        charMatrix[i, j] = stringArray[i][j];
    }
}

PrintMatrix(charMatrix);

var instructions = values.Skip(10);

var partOneStacks = GetStacks(charMatrix);

string part1 = MoveCrates(partOneStacks, instructions, false);

Console.WriteLine("Part1:" + part1);

var partTwoStacks = GetStacks(charMatrix);

string part2 = MoveCrates(partTwoStacks, instructions, true);

Console.WriteLine("Part2:" + part2);

static string ToString(Stack<char> stack)
{
    StringBuilder sb = new StringBuilder();
    while (stack.Count > 0)
    {
        sb.Append($"[{stack.Pop()}]");
    }

    return sb.ToString();

}

char[] GetColumn(char[,] matrix, int columnNumber)
{
    return Enumerable.Range(0, matrix.GetLength(0))
            .Select(x => matrix[x, columnNumber]).Where(c => !char.IsWhiteSpace(c))
            .ToArray();
}

static void PrintMatrix(char[,] transposedMatrix)
{
    for (int i = 0; i < transposedMatrix.GetLength(0); i++)
    {
        for (int j = 0; j < transposedMatrix.GetLength(1); j++)
        {
            Console.Write(transposedMatrix[i, j] + " ");
        }
        Console.WriteLine();
    }
}

static string MoveCrates(Stack<char>[] stacks, IEnumerable<string> instructions, bool multiplePickUp)
{
    foreach (string str in instructions)
    {
        var numbers = Regex.Split(str, @"\D+").Where(s => s != String.Empty).ToArray();

        var count = int.Parse(numbers[0]);
        var from = int.Parse(numbers[1]);
        var to = int.Parse(numbers[2]);


        var crateMover = new List<char>();

        for (int i = 0; i < count; i++)
        {
            var c = stacks[from - 1].Pop();

            crateMover.Add(c);
        }
        if (multiplePickUp)
        {
            crateMover.Reverse();
        }

        for (int i = 0; i < count; i++)
        {
            stacks[to - 1].Push(crateMover[i]);
        }
    }

    string result = "";

    for (int i = 0; i < stacks.Length; i++)
    {
        result += stacks[i].Peek();

        Console.WriteLine($"{i}:{ToString(stacks[i])}");
    }

    return result;
}

Stack<char>[] GetStacks(char[,] charMatrix)
{
    Stack<char>[] stacks = new Stack<char>[9];

    for (int i = 1; i < charMatrix.GetLength(1); i = i + 2)
    {
        char[] row = GetColumn(charMatrix, i);

        for (int j = row.Length - 2; j >= 0; j--)
        {
            int stackIndex = int.Parse(row.Last().ToString()) - 1;
            char c = row[j];

            if (stacks[stackIndex] == null)
            {
                stacks[stackIndex] = new Stack<char>();
            }

            stacks[stackIndex].Push(c);
        }
    }

    return stacks;
}