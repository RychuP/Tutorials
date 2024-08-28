using System.Text;

namespace ConsoleTable;

public class Table
{
    readonly string[] _headers;
    readonly int[] _columnWidths;
    readonly string[,] _data;

    public Table(string[] headers, string[,] data)
    {
        if (headers.Length != data.GetLength(1))
        {
            throw new ArgumentException("Amount of data elements must match the amount of headers.");
        }
        _headers = headers;
        _data = data;
        _columnWidths = new int[headers.Length];

        for (int i = 0; i < _headers.Length; i++)
        {
            _columnWidths[i] = _headers[i].Length;
        }
    }

    public void SetColWidth(int width)
    {
        Array.Fill(_columnWidths, width);
    }

    public void SetColWidth(int index, int width)
    {
        if (index < 0 || index >= _columnWidths.Length)
        {
            throw new ArgumentOutOfRangeException($"Index {index} is outside the table limits.");
        }
        _columnWidths[index] = width;
    }

    public override string ToString()
    {
        StringBuilder sb = new("|");
            
        // build a header string
        for (int i = 0, count = _headers.Length; i < count; i++)
        {

            sb.Append(GetCell(_headers[i], _columnWidths[i]));
        }
        string line = new('-', sb.Length);
        sb.Insert(0, line + Environment.NewLine);
        sb.Append(Environment.NewLine + line);

        // add rows of data
        for (int x = 0, countx = _data.GetLength(0); x < countx; x++)
        {
            sb.Append("\n|");
            for (int y = 0, county = _data.GetLength(1); y < county; y++)
            {
                sb.Append(GetCell(_data[x, y], _columnWidths[y]));
            }
        }
        sb.AppendLine(Environment.NewLine + line);

        return sb.ToString();
    }

    string GetCell(string item, int length)
    {
        item = item.Length > length ? item.Substring(0, length) : item;
        return string.Format(" {0, -" + length + "} |", item);
    }
}