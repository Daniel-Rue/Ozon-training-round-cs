
using var input = new StreamReader(Console.OpenStandardInput());
using var output = new StreamWriter(Console.OpenStandardOutput());

int t = Convert.ToInt32(input.ReadLine());
for (int i = 0; i < t; i++)
{
    int n = Convert.ToInt32(input.ReadLine());
    string[] s = input.ReadLine().Split(new []{' '});
    int start = 1001;
    int dif = 0;
    int sign = 0;
    List<int> result = new List<int>();
    
    for (int j = 0; j < n; j++)
    {
        if (start == 1001)
        {
            start = int.Parse(s[j]);
        }
        
        if (j + 1 != n && int.Parse(s[j+1]) - int.Parse(s[j]) == -1 && sign != 1)
        {
            if (sign != -1)
            { ;
                dif--;
                sign = -1;
            }
            else
            {
                dif--;
            }
        }
        else if (j + 1 != n && int.Parse(s[j+1]) - int.Parse(s[j]) == 1 && sign != -1)
        {
            if (sign != 1)
            {
                dif++;
                sign = 1;
            }
            else
            {
                dif++;
            }
        }
        else
        {
            result.Add(start);
            result.Add(dif);
            start = 1001;
            dif = 0;
            sign = 0;
        }
    }

    output.WriteLine(result.Count);
    foreach (var num in result)
    {
        output.Write(num + " ");
    }
    output.WriteLine();
}


