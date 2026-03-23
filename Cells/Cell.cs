abstract class Cell
{
    public abstract bool IsSolid { get; }
    public virtual bool IsStart => false;
    public virtual bool IsExit => false;

    public virtual bool CanBeEnteredBy(IReadOnlyList<ICollectable> inventory)
    {
        return !IsSolid;
    }

    public abstract void Draw(IGridDisplay screen, Vec2d position);
}
