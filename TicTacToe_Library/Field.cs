using System.Text;

namespace TicTacToe;

public class Field
{
    private readonly int[,] _field;

    public Field(int sizeX, int sizeY)
    {
        _field = new int[sizeX, sizeY];

        for (var i = 0; i < sizeX; i++)
        {
            for (var j = 0; j < sizeY; j++)
            {
                _field[i, j] = 0;
            }
        }
    }
    
    /// <summary>
    /// Проверка на выход за границу поля.
    /// </summary>
    private bool CheckPosition(int x, int y)
    {
        return x >= 0 && x < _field.GetLength(0) && y >= 0 && y < _field.GetLength(1);
    }
    
    public int this[int i, int j]
    {
        get => CheckPosition(i, j) ? _field[i, j] : (int)0;
        set => _field[i, j] = value;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        foreach (var num in _field)
        {
            sb.Append(num.ToString());
        }

        return sb.ToString();
    }
}