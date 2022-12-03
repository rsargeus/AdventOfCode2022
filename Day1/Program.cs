int currentElf = 0; // Create variable to store the current Elf index in the elves array

string inputtext = System.IO.File.ReadAllText(@"input.txt");
string[] values = inputtext.Split(new[] { "\n" }, StringSplitOptions.None);

var elves = new Dictionary<int, List<int>>();

elves.Add(currentElf, new List<int>());
foreach (string str in values)
{
    if (str == "")
    {
        currentElf++;
        elves.Add(currentElf, new List<int>());
    }
    else
    {
        elves.Last().Value.Add(int.Parse(str));
    }
}

var part1 = elves.Max(e=> e.Value.Sum());

var part2 = elves.OrderByDescending(e => e.Value.Sum()).Take(3).SelectMany(e => e.Value).Sum();


Console.WriteLine(part1);
Console.WriteLine(part2);
Console.ReadLine();
