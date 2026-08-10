// int[] nums = [1, 2, 3, 4];

// var subsets = Subsets(nums);
// var permute = Permute(nums);

// static List<List<int>> Subsets(int[] nums)
// {
//     List<List<int>> results = [];
//     Backtrack_subset(nums, 0, [], results);
//     return results;
// }


// static void Backtrack_subset(int[] nums, int start, List<int> current, List<List<int>> results)
// {
//     results.Add([.. current]);

//     for (int i = start; i < nums.Length; i++)
//     {
//         current.Add(nums[i]);
//         Backtrack_subset(nums, i + 1, current, results);
//         current.RemoveAt(current.Count - 1);
//     }
// }

// static List<List<int>> Permute(int[] nums)
// {
//     var results = new List<List<int>>();
//     Backtrack_permute(nums, new bool[nums.Length], [], results);
//     return results;
// }

// static void Backtrack_permute(int[] nums, bool[] used, List<int> current, List<List<int>> results)
// {
//     if(current.Count == nums.Length)
//     {
//         results.Add([.. current]);
//         return;
//     }

//     for (int i = 0; i < nums.Length; i++)
//     {
//         if (used[i])
//             continue;
        
//         current.Add(nums[i]);
//         used[i] = true;

//         Backtrack_permute(nums, used, current, results);

//         used[i] = false;
//         current.RemoveAt(current.Count - 1);
//     }
// }

// static int SolveNQueens(int n)
// {
//     var results = new List<List<int>>();
    
//     Backtrack(new int[n], new bool[n], [], results);

//     return results.Count;
// }

// static void Backtrack(int[] queens, bool[] used, List<int> current, List<List<int>> results)
// {
//      if(current.Count == queens.Length)
//     {
//         results.Add([.. current]);
//         return;
//     }

//     for (int i = 0; i < queens.Length; i++)
//     {
//         if (used[i])
//             continue;
        
//         current.Add(queens[i]);
//         used[i] = true;

//         Backtrack(queens, used, current, results);

//         used[i] = false;
//         current.RemoveAt(current.Count - 1);
//     }   
// }

// static bool IsQueenSighted(int[] queens, bool[] used, int row, int col)
// {
    
// }

// ulong board = 0u;
// int boardIdx = 9;
// var piece = Piece.RED;

// var isValidPlacement = GameHelper.IsValidPiecePlacement(gameConfig, board, _workingPieces[piece], boardIdx);

// var placedPiece = _workingPieces[piece] << boardIdx;
// Console.WriteLine($"({(isValidPlacement ? "Valid" : "Invalid")}) Placing {piece} at {boardIdx}\n");
// board |= placedPiece;

// PrintHelper.PrintBoard(gameConfig, board);



// foreach (var piece in _workingPieces)
// {
//     Console.WriteLine($"\n ~ {Enum.GetName(piece.Key)} ~ ");

//     Console.WriteLine();
//     PrintHelper.PrintBoard(gameConfig, piece.Value);
// }