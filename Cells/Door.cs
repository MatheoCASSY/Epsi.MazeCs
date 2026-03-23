sealed class Door : Cell
{
    private readonly ICollectable _requiredKey = new Key();

    public override bool IsSolid => true;

    public override bool CanBeEnteredBy(IReadOnlyList<ICollectable> inventory)
    {
        return inventory.Any(item => ReferenceEquals(item, _requiredKey));
    }

    public override void Draw(IGridDisplay screen, Vec2d position)
    {
        screen.DrawText(position, "▓", ConsoleColor.DarkYellow);
    }

    public void PlaceKeyIn(Room room)
    {
        room.SetCollectable(_requiredKey);
    }
}
