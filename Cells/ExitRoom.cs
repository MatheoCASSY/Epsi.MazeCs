sealed class ExitRoom(ICollectable? collectable = null) : Room(collectable)
{
    public override bool IsExit => true;

    public override void Draw(IGridDisplay screen, Vec2d position)
    {
        screen.DrawText(position, "★", ConsoleColor.Green);
    }
}
