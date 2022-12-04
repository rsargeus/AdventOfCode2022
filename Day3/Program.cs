// See https://aka.ms/new-console-template for more information
using System.Linq;

string inputtext = System.IO.File.ReadAllText(@"input.txt");
string[] values = inputtext.Split(new[] { "\n" }, StringSplitOptions.None);

List<(char, int)> part1 = new List<(char,int)>();
List<(char, int)> part2 = new List<(char, int)>();


for (int s = 0;s<values.Length; s++)
{

    if (s % 3 == 0)
    {
        var group1 = values[s].ToCharArray();
        var group2 = values[s + 1].ToCharArray();
        var group3 = values[s + 2].ToCharArray();

        for (int x = 0; x < group1.Length; x++)
        {

            char c = group1[x];
            if (group2.Contains(c) && group3.Contains(c))
            {
                int priority = GetPriority(c);
                part2.Add((c, priority));
                break;
            }
        }
            
    }

    int divider = values[s].Length / 2;

    string compartment1 = values[s].Substring(0, divider);
    char[] compartment2 = values[s].ToCharArray(divider, divider);


    for (int i = 0; i < divider; i++)
    {

        if (compartment1.Contains(compartment2[i]))
        {
            char c = compartment2[i];
            int priority = GetPriority(c);
            part1.Add((c, priority));
            break;
        }
    }
}

Console.WriteLine(part1.Sum(p=>p.Item2));

Console.WriteLine(part2.Sum(p => p.Item2));

Console.ReadLine();

static int GetPriority(char c)
{
    int priority;

    if (Char.IsLower(c))
    {
        priority = c - 'a' + 1;
    }
    else
    {
        priority = c - 'A' + 27;
    }

    return priority;
}