sealed class Maze
{
    private readonly Cell[,] _grid;

    public int Width  { get; }
    public int Height { get; }
    public Vec2d StartPosition { get; }

    public Maze(IMazeGenerator gen)
    {
        _grid = gen.Generate();
        Width  = _grid.GetLength(0);
        Height = _grid.GetLength(1);
        StartPosition = FindStart();
    }

    public Cell GetCell(Vec2d position) => _grid[position.X, position.Y];

    public bool IsInBounds(Vec2d position) => position.IsInBounds(Width, Height);

    public void Draw(IGridDisplay screen, Vec2d offset)
    {
        for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
                DrawCell(screen, offset, new Vec2d(x, y));
    }

    public void DrawCell(IGridDisplay screen, Vec2d offset, Vec2d position)
    {
        var cell = GetCell(position);
        cell.Draw(screen, offset.Add(position));
    }

    private Vec2d FindStart()
    {
        for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
                if (_grid[x, y].IsStart)
                    return new Vec2d(x, y);
        return Vec2d.Zero;
    }
}
