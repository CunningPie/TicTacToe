using System.Runtime.CompilerServices;

namespace TicTacToe;

public class Game
{
    private readonly Field _field;
    private readonly int _sizeX;
    private readonly int _sizeY;

    public Game(int sizeX, int sizeY)
    {
        _sizeX = sizeX;
        _sizeY = sizeY;

        _field = new Field(sizeX, sizeY);
    }
    
    /// <summary>
    /// Проверка диагонали из точки (х, у) вниз вправо на условие победы.
    /// </summary>
    private bool CheckDiagonalDown(int x, int y)
    {
        return (_field[x, y] == _field[x + 1, y + 1]) && (_field[x, y] == _field[x + 2, y + 2]);
    }
    
    /// <summary>
    /// Проверка диагонали из точки (х, у) вверх вправо на условие победы.
    /// </summary>
    private bool CheckDiagonalUp(int x, int y)
    {
        return (_field[x, y] == _field[x + 1, y - 1]) && (_field[x, y] == _field[x + 2, y - 2]);
    }
    
    /// <summary>
    /// Проверка вертикали из точки (х, у) на условие победы.
    /// </summary>
    private bool CheckVertical(int x, int y)
    {
        return (_field[x, y] == _field[x, y + 1]) && (_field[x, y] == _field[x, y + 2]);
    }
    
    /// <summary>
    /// Проверка горизонтали из точки (х, у) на условие победы.
    /// </summary>
    private bool CheckHorizontal(int x, int y)
    {
        return (_field[x, y] == _field[x + 1, y]) && (_field[x, y] == _field[x + 2, y]);
    }

    /// <summary>
    /// Проверка на пустые клетки.
    /// </summary>
    private bool CheckEmptySpace()
    {        
        for (var i = 0; i < _sizeX; i++)
            for (var j = 0; j < _sizeY; j++)
            { 
                if (_field[i, j] == 0)
                {
                    return true;
                }
            }

        return false;
    }

    /// <summary>
    /// Полная проверка условия победы после хода игрока в клетке (x, y) для произвольного размера поля. 
    /// </summary>
    private bool CheckWinConditionAfterTurn(int x, int y)
    {
        return
            CheckDiagonalDown(x - 2, y - 2) ||
            CheckHorizontal(x - 2, y) ||
            CheckDiagonalUp(x - 2, y + 2) ||
            CheckDiagonalDown(x - 1, y - 1) ||
            CheckHorizontal(x - 1, y) ||
            CheckDiagonalUp(x - 1, y + 1) ||
            CheckVertical(x, y - 2) ||
            CheckVertical(x, y - 1) ||
            CheckDiagonalUp(x, y) ||
            CheckHorizontal(x, y) ||
            CheckDiagonalDown(x, y) ||
            CheckVertical(x, y);
    }

    private int CheckGameStatus(int x, int y)
    {
        if (CheckWinConditionAfterTurn(x, y))
        {
            return 1;
        }
        if (!CheckEmptySpace() && !CheckWinConditionAfterTurn(x, y))
        {
            return 2;
        }

        return 0;
    }

    public int Move(int x, int y, int player)
    {
        if (_field[x, y] == 0)
        {
            _field[x, y] = player;
        }

        return CheckGameStatus(x, y);
    }

    public string GetField()
    {
        return _field.ToString();
    }
}