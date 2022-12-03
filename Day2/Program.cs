// See https://aka.ms/new-console-template for more information
string inputtext = System.IO.File.ReadAllText(@"input.txt");
string[] values = inputtext.Split(new[] { "\n" }, StringSplitOptions.None);

var roundsPartOne = new List<int>();

var roundsPartTwo = new List<int>();

foreach (string str in values)
{
    string opponentPlay = str[0] switch
    {
        'A' => "Rock",
        'B' => "Paper",
        'C' => "Scissor",
    };

    char myPlay = str[2];



    int partOneScore = (opponentPlay, myPlay) switch
    {
        ("Rock", 'X') => 3 + 1,
        ("Rock", 'Y') => 6 + 2,
        ("Rock", 'Z') => 0 + 3,

        ("Paper", 'X') => 0 + 1,
        ("Paper", 'Y') => 3 + 2,
        ("Paper", 'Z') => 6 + 3,

        ("Scissor", 'X') => 6 + 1,
        ("Scissor", 'Y') => 0 + 2,
        ("Scissor", 'Z') => 3 + 3,
    };

    roundsPartOne.Add(partOneScore);

    int partTwoScore = (opponentPlay, myPlay) switch
    {
        ("Rock", 'X') => 0 + 3,
        ("Rock", 'Y') => 3 + 1,
        ("Rock", 'Z') => 6 + 2,

        ("Paper", 'X') => 0 + 1,
        ("Paper", 'Y') => 3 + 2,
        ("Paper", 'Z') => 6 + 3,

        ("Scissor", 'X') => 0 + 2,
        ("Scissor", 'Y') => 3 + 3,
        ("Scissor", 'Z') => 6 + 1,
    };

    roundsPartTwo.Add(partTwoScore);
}

var part1 = roundsPartOne.Sum();

var part2 = roundsPartTwo.Sum();

Console.WriteLine(part1);
Console.WriteLine(part2);

Console.ReadLine();
