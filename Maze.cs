sealed class Maze
{
    private static readonly ConsoleColor WallColor     = ConsoleColor.DarkGray;
    private static readonly ConsoleColor CorridorColor = ConsoleColor.DarkBlue;
    private static readonly ConsoleColor ExitColor     = ConsoleColor.Green;

    private readonly CellType[,] _grid;

    public int Width  { get; }
    public int Height { get; }
    public Vec2d StartPosition { get; }

    public Maze(MazeGen gen)
    {
        _grid = gen.Generate();
        Width  = _grid.GetLength(0);
        Height = _grid.GetLength(1);
        StartPosition = FindStart();
    }

    public CellType GetCell(Vec2d position) => _grid[position.X, position.Y];

    public bool IsInBounds(Vec2d position) => position.IsInBounds(Width, Height);

    public void Draw(ConsoleScreen screen, Vec2d offset)
    {
        for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
                DrawCell(screen, offset, new Vec2d(x, y));
    }

    public void DrawCell(ConsoleScreen screen, Vec2d offset, Vec2d position)
    {
        var cell = GetCell(position);
        screen.DrawText(
            offset.Add(position),
            cell switch
            {
                CellType.Wall => "█",
                CellType.Exit => "★",
                _             => "·"
            },
            cell switch
            {
                CellType.Wall => WallColor,
                CellType.Exit => ExitColor,
                _             => CorridorColor
            });
    }

    private Vec2d FindStart()
    {
        for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
                if (_grid[x, y] == CellType.Start)
                    return new Vec2d(x, y);
        return Vec2d.Zero;
    }
}
