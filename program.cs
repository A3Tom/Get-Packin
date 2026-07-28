ulong board = 0b00000000_00000000_00000000;

void printBoard(ulong board)
{
    const int width = 8;
    const int mask = 0b1;

    for (int row = 0; row < 3; row++)
    {
        Console.Write($"[{row}]:");
        
        for(int cell = 0; cell < width; cell++)
        {
            var idx = (row * width) + cell;
            var hing = (board >> idx) & mask;
            Console.Write($"{hing}|");
        }

        Console.WriteLine();
    }
}

printBoard(board);