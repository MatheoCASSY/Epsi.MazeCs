class Room(ICollectable? collectable = null) : Cell
{
    public override bool IsSolid => false;
    private ICollectable? _collectable = collectable;
    public ICollectable? Collectable => _collectable;

    public void SetCollectable(ICollectable collectable)
    {
        _collectable = collectable;
    }

    public ICollectable? TakeCollectable()
    {
        var item = _collectable;
        _collectable = null;
        return item;
    }

    public override void Draw(IGridDisplay screen, Vec2d position)
    {
        if (_collectable is not null)
        {
            screen.DrawText(position, _collectable.Glyph, _collectable.Color);
            return;
        }

        screen.DrawText(position, "·", ConsoleColor.DarkBlue);
    }
}
