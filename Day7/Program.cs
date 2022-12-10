// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;

string inputtext = System.IO.File.ReadAllText(@"input.txt");
string[] values = inputtext.Split(new[] { "\n" }, StringSplitOptions.None);

Folder home = new Folder() { Name = "/"};

Folder currentFolder = home;

foreach (string str in values)
{

    if (str == "$ cd /")
    {
        currentFolder = home;

    }
    else if (str == "$ ls")
    {

    }
    else if (str.StartsWith("dir"))
    {
        string folderName = str.Split(' ')[1];
        currentFolder.Folders.Add(new Folder { Name = folderName, Parent = currentFolder });
    }
    else if (str == "$ cd ..")
    {
        currentFolder = currentFolder.Parent;
    }
    else if (str.StartsWith("$ cd"))
    {
        string folderName = str.Split(' ')[2];
        currentFolder = currentFolder.Folders.Single(f => f.Name == folderName);
    }
    else
    {
        int fileSize = int.Parse(str.Split(' ')[0]);
        string fileName = str.Split(' ')[1];
        currentFolder.Files.Add((fileName, fileSize));
    }
}

CalculateSize(home);

PrintFolder(home, 1);

List<int> list = new List<int>();



GetBelow(list, home, 100000);

list.ForEach(i => Console.WriteLine(i));

Console.WriteLine($"Part 1: {list.Sum()}");

list = new List<int>();
GetBelow(list, home, int.MaxValue);

int mustDelete = home.Size - (70000000 - 30000000);

int part2 = list.Where(i => i > mustDelete).OrderBy(i => i).First();

Console.WriteLine($"Part 2: {part2}" );

void CalculateSize(Folder folder)
{
    folder.Folders.ForEach(f => CalculateSize(f));

    int fileSize = folder.Files.Sum(f => f.size);
    int folderSize = folder.Folders.Sum(f => f.Size);

    folder.Size = fileSize + folderSize;
}

void GetBelow(List<int> list, Folder folder, int limit)
{
    if (folder.Size < limit)
    {
        list.Add(folder.Size);
    }

    folder.Folders.ForEach(f => GetBelow(list, f, limit));
}

void PrintFolder(Folder folder, int indent)
{
    var indentString = new string(' ', indent);

    Console.WriteLine($"{indentString}- {folder.Name} (dir, {folder.Size})");

    indent++;
    folder.Folders.ForEach(f => PrintFolder(f, indent));
    folder.Files.ForEach(f => PrintFile(f, indent));
}

    
void PrintFile((string name, int size) file, int indent)
{
    var indentString = new string(' ', indent);

    Console.WriteLine($"{indentString}- {file.name} (file, {file.size})");
}

class Folder
{
    internal Folder()
    {
        Folders = new List<Folder>();
        Files = new List<(string name, int size)>();
    }

    internal string Name { get; set; }
    internal int Size { get; set; }
    internal Folder Parent { get; set; }
    internal List<Folder> Folders { get; }
    internal List<(string name, int size)> Files { get; }
}