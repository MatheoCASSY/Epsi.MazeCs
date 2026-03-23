sealed class StartRoom(ICollectable? collectable = null) : Room(collectable)
{
    public override bool IsStart => true;
}
