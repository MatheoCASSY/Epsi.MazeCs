class Room(ICollectable? collectable = null) : Cell
{
    public override bool IsSolid => false;
    public ICollectable? Collectable { get; } = collectable;

    public override void Draw(IGridDisplay screen, Vec2d position)
    {
        if (Collectable is not null)
        {
            screen.DrawText(position, "¤", ConsoleColor.Yellow);
            return;
        }

        screen.DrawText(position, "·", ConsoleColor.DarkBlue);
    }
}
