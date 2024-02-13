using var input = new StreamReader(Console.OpenStandardInput());
using var output = new StreamWriter(Console.OpenStandardOutput());

int t = Convert.ToInt32(input.ReadLine());

for (int i = 0; i < t; i++)
{
    var arr = input.ReadLine();
    int cursor = 0;
    int line = 0;
    List<List<string>> field = new List<List<string>>(arr.Length);
    field.Insert(line, new List<string>(arr.Length + 1));

    for (int j = 0; j < arr.Length; j++)
    {
        char letter = arr[j];

        switch (letter)
        {
            case 'L':
                if (cursor > 0)
                {
                    cursor--;
                }

                break;
            case 'R':
                if (cursor < field[line].Count)
                {
                    cursor++;
                }

                break;
            case 'U':
                if (line > 0)
                {
                    line--;
                    if (field[line].Count < cursor)
                    {
                        MoveCursorToEnd(field, line, out cursor);
                    }
                }

                break;
            case 'D':
                if (line < field.Count - 1)
                {
                    line++;
                    if (field[line].Count < cursor)
                    {
                        MoveCursorToEnd(field, line, out cursor);
                    }
                }

                break;
            case 'B':
                cursor = 0;
                break;
            case 'E':
                if (field[line].Count > 0)
                {
                    MoveCursorToEnd(field, line, out cursor);
                }

                break;
            case 'N':
                field.Insert(line + 1, new List<string>(arr.Length + 1));
                for (int k = cursor; k < field[line].Count; k++)
                {
                    field[line + 1].Add(field[line][k]);
                }
                
                field[line].RemoveRange(cursor, field[line].Count - cursor);
                
                line++;

                cursor = 0;
                break;
            default:
                if (char.IsLower(letter) || char.IsDigit(letter))
                {
                    field[line].Insert(cursor, letter.ToString());
                    cursor++;
                }

                break;
        }
    }

    for (int j = 0; j < field.Count; j++)
    {
        for (int k = 0; k < field[j].Count; k++)
        {
            if (!string.IsNullOrEmpty(field[j][k]))
            {
                output.Write(field[j][k]);
            }
        }

        output.WriteLine("");
    }
    output.WriteLine("-");
}

void MoveCursorToEnd(List<List<string>> field, int line, out int cursor)
{
    int end = 0;
    for (int k = 0; k < field[line].Count; k++)
    {
        if (!string.IsNullOrEmpty(field[line][k]))
        {
            end = k + 1;
        }
        else
        {
            break;
        }
    }

    cursor = end;
}