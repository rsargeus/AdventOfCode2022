// See https://aka.ms/new-console-template for more information
string inputtext = System.IO.File.ReadAllText(@"input.txt");
string[] values = inputtext.Split(new[] { "\n" }, StringSplitOptions.None);


string datastream = values[0];

int offset = 14;

for (int i = 0; i < datastream.Length - offset; i++)
{
    var msg = datastream.Substring(i, offset);

    int distinct = msg.Distinct().Count();

    Console.WriteLine(distinct + " " + msg + ":" + (i+offset));
    if (distinct == offset)
    {
        break;
    }
}
