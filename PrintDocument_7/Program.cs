using System.Text;

using var input = new StreamReader(Console.OpenStandardInput());
using var output = new StreamWriter(Console.OpenStandardOutput());

int t = Convert.ToInt32(input.ReadLine());

for (int i = 0; i < t; i++)
{
    int k = Convert.ToInt32(input.ReadLine());
    bool[] exNums = new bool[k + 1];

    var arr = input.ReadLine().Split(",");

    foreach (var cell in arr)
    {
        if (!int.TryParse(cell, out int num))
        {
            var interval = cell.Split('-');
            for (int l = int.Parse(interval[0]); l < int.Parse(interval[1]) + 1; l++)
            {
                exNums[l] = true;
            }
        }
        else
        {
            exNums[num] = true;
        }
    }

    List<int> finalArr = new List<int>();
    
    for (int j = 1; j < exNums.Length; j++)
    {
        if (!exNums[j])
        {
            finalArr.Add(j);
        }
    }
    finalArr.Sort();

    StringBuilder result = new StringBuilder();
    int start = finalArr[0]; 
    int end = start;

    for (int j = 1; j < finalArr.Count; j++)
    {
        if (finalArr[j] == end + 1)
        {
            end = finalArr[j];
        }
        else
        {
            result.Append(start == end ? $"{start}," : $"{start}-{end},");
            start = finalArr[j];
            end = start;
        }
    }
    
    result.Append(start == end ? $"{start}" : $"{start}-{end}");
    output.WriteLine(result.ToString().TrimEnd(new char[] { ',' }));
}