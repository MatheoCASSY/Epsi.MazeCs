sealed class Player(Vec2d startPosition, IController controller)
{
    private static readonly ConsoleColor PlayerColor = ConsoleColor.Yellow;

    public Vec2d Position { get; private set; } = startPosition;

    public bool IsExitRequested => controller.IsEscPressed;

    public bool TryMove(Maze maze, IGridDisplay screen, Vec2d offset)
    {
        controller.ReadInput();

        var delta =
            controller.IsUpPressed ? new Vec2d(0, -1) :
            controller.IsDownPressed ? new Vec2d(0, 1) :
            controller.IsLeftPressed ? new Vec2d(-1, 0) :
            controller.IsRightPressed ? new Vec2d(1, 0) :
            (Vec2d?)null;

        if (delta is not { } movement)
            return false;

        return TryMove(movement, maze, screen, offset);
    }

    public bool TryMove(Vec2d delta, Maze maze, IGridDisplay screen, Vec2d offset)
    {
        var next = Position.Add(delta);

        if (!maze.IsInBounds(next))
            return false;

        var nextCell = maze.GetCell(next);

        if (nextCell.IsSolid)
            return false;

        var reachedExit = nextCell.IsExit;

        maze.DrawCell(screen, offset, Position);
        Position = next;
        Draw(screen, offset);

        return reachedExit;
    }

    public void Draw(IGridDisplay screen, Vec2d offset)
    {
        screen.DrawText(offset.Add(Position), "@", PlayerColor);
    }
}
