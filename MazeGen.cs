sealed class MazeGen(int width, int height) : IMazeGenerator
{
    private static readonly Vec2d[] Directions =
    [
        new Vec2d(0, -1),
        new Vec2d(1, 0),
        new Vec2d(0, 1),
        new Vec2d(-1, 0)
    ];

    private static readonly int[][] Orders =
    [
        [0, 1, 2, 3], [0, 1, 3, 2], [0, 2, 1, 3], [0, 2, 3, 1], [0, 3, 1, 2], [0, 3, 2, 1],
        [1, 0, 2, 3], [1, 0, 3, 2], [1, 2, 0, 3], [1, 2, 3, 0], [1, 3, 0, 2], [1, 3, 2, 0],
        [2, 0, 1, 3], [2, 0, 3, 1], [2, 1, 0, 3], [2, 1, 3, 0], [2, 3, 0, 1], [2, 3, 1, 0],
        [3, 0, 1, 2], [3, 0, 2, 1], [3, 1, 0, 2], [3, 1, 2, 0], [3, 2, 0, 1], [3, 2, 1, 0]
    ];

    public CellType[,] Generate()
    {
        var grid = new CellType[width, height];
        var rng = new Random();

        for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
                grid[x, y] = CellType.Wall;

        var start = Vec2d.Zero;
        GenerateRec(start);

        grid[start.X, start.Y] = CellType.Start;
        grid[(width - 1) & ~1, (height - 1) & ~1] = CellType.Exit;

        return grid;

        void GenerateRec(Vec2d position)
        {
            grid[position.X, position.Y] = CellType.Corridor;
            foreach (var dir in Orders[rng.Next(Orders.Length)])
            {
                var direction = Directions[dir];
                var next = position.Add(direction.Multiply(2));

                if (next.IsInBounds(width, height) && grid[next.X, next.Y] == CellType.Wall)
                {
                    grid[position.Midpoint(next).X, position.Midpoint(next).Y] = CellType.Corridor;
                    GenerateRec(next);
                }
            }
        }
    }
}
