// See https://aka.ms/new-console-template for more information
string inputtext = System.IO.File.ReadAllText(@"input.txt");
string[] values = inputtext.Split(new[] { "\n" }, StringSplitOptions.None);

int xRegister = 1;
List<int?> cycles = new();

foreach (string str in values)
{
    if (str == "noop")
    {
        cycles.Add(null);
    }
    else
    {
        int x = int.Parse(str.Split(' ')[1]);
        cycles.Add(null);
        cycles.Add(x);
    }
}

int signaltregnthSum = 0;

for (int i = 1; i <= cycles.Count; i++)
{
    int? signaltregnth = null;

    if ((i-20) % 40 == 0)
    {
        signaltregnth = i * xRegister;
        signaltregnthSum += signaltregnth.Value;
    }

    int column = (i-1) % 40;

    if (
        column == xRegister -1 ||
        column == xRegister ||
        column == xRegister + 1
        )
    {
        Console.Write("#");
    }
    else
    {
        Console.Write(".");
    }

    if (i % 40 == 0)
    {
        Console.WriteLine();
    }

    if (cycles[i-1].HasValue)
    {
        xRegister += cycles[i-1].Value;
    }
}

Console.WriteLine("Part1: " + signaltregnthSum);
