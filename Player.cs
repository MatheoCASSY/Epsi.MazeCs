sealed class Player(Vec2d startPosition)
{
    private static readonly ConsoleColor PlayerColor = ConsoleColor.Yellow;

    public Vec2d Position { get; private set; } = startPosition;

    public bool TryMove(Vec2d delta, Maze maze, ConsoleScreen screen, Vec2d offset)
    {
        var next = Position.Add(delta);

        if (!maze.IsInBounds(next) || maze.GetCell(next) == CellType.Wall)
            return false;

        var reachedExit = maze.GetCell(next) == CellType.Exit;

        maze.DrawCell(screen, offset, Position);
        Position = next;
        Draw(screen, offset);

        return reachedExit;
    }

    public void Draw(ConsoleScreen screen, Vec2d offset)
    {
        screen.DrawText(offset.Add(Position), "@", PlayerColor);
    }
}
