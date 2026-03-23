sealed class Player(Vec2d startPosition, IController controller)
{
    private static readonly ConsoleColor PlayerColor = ConsoleColor.Yellow;
    private readonly List<ICollectable> _inventory = [];

    public event Action<int, int>? PointsIncreased;
    public event Action<IReadOnlyList<ICollectable>>? InventoryChanged;

    public Vec2d Position { get; private set; } = startPosition;
    public int Points { get; private set; }
    public IReadOnlyList<ICollectable> Inventory => _inventory;

    public bool IsExitRequested => controller.IsEscPressed;

    public bool TryMove(Maze maze, IGridDisplay screen, Vec2d offset)
    {
        controller.ReadInput();

        if (controller.IsPickupPressed)
        {
            TryPickup(maze, screen, offset);
            return false;
        }

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

        if (!nextCell.CanBeEnteredBy(_inventory))
            return false;

        var reachedExit = nextCell.IsExit;

        maze.DrawCell(screen, offset, Position);
        Position = next;
        Draw(screen, offset);

        return reachedExit;
    }

    private void TryPickup(Maze maze, IGridDisplay screen, Vec2d offset)
    {
        if (maze.GetCell(Position) is not Room room)
            return;

        if (room.TakeCollectable() is not { } collectable)
            return;

        if (collectable.Points > 0)
        {
            Points += collectable.Points;
            PointsIncreased?.Invoke(collectable.Points, Points);
        }

        if (collectable.IsPersistent)
        {
            _inventory.Add(collectable);
            InventoryChanged?.Invoke(Inventory);
        }

        maze.DrawCell(screen, offset, Position);
        Draw(screen, offset);
    }

    public void Draw(IGridDisplay screen, Vec2d offset)
    {
        screen.DrawText(offset.Add(Position), "@", PlayerColor);
    }
}
