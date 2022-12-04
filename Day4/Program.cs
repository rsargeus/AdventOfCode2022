// See https://aka.ms/new-console-template for more information
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

string inputtext = System.IO.File.ReadAllText(@"input.txt");
string[] values = inputtext.Split(new[] { "\n" }, StringSplitOptions.None);

int partOneSequenceContainsCounter = 0;
int partTwoSequenceOverlapsCounter = 0;

foreach (string str in values)
{

    bool sequenceContains = false;
    bool sequenceOverlaps = false;

    string rangeOneString = str.Split(',')[0];
    string rangeTwoString = str.Split(',')[1];

    var range1 = GetStartEnd(rangeOneString);
    var range2 = GetStartEnd(rangeTwoString);


    // Range 1 contains range 2
    if (range1.Start <= range2.Start && range1.End >= range2.End)
    {
        sequenceContains = true;
    }

    // Range 2 contains range 1
    if (range2.Start <= range1.Start && range2.End >= range1.End)
    {
        sequenceContains = true;
    }

    // Range 1 min value overlaps with range 2
    if (Overlaps(range1.Start, range2))
    {
        sequenceOverlaps = true;
    }

    // Range 1 max value overlaps with range 2
    if (Overlaps(range1.End, range2))
    {
        sequenceOverlaps = true;
    }

    // Range 2 min value overlaps with range 1
    if (Overlaps(range2.Start, range1))
    {
        sequenceOverlaps = true;
    }

    // Range 2 max value overlaps with range 1
    if (Overlaps(range2.End, range1))
    {
        sequenceOverlaps = true;
    }

    if (sequenceOverlaps)
    {
        partTwoSequenceOverlapsCounter++;
    }


    if (sequenceContains)
    {
        partOneSequenceContainsCounter++;
    }
}

Console.WriteLine(partOneSequenceContainsCounter);
Console.WriteLine(partTwoSequenceOverlapsCounter);
Console.ReadLine();

static (int Start, int End) GetStartEnd(string range1)
{
    int rangeOneStart = int.Parse( range1.Split('-')[0] );
    int rangeOneEnd = int.Parse(range1.Split('-')[1]);

    return (rangeOneStart, rangeOneEnd);
}


static bool Overlaps(int value, (int Start, int End) range)
{
    return value >= range.Start && value <= range.End;
}