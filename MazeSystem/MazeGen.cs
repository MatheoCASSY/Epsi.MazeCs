sealed class MazeGen(int width, int height, double coinProbability, double doorProbability) : IMazeGenerator
{
    private static readonly Vec2d[] Directions =
    [
        new Vec2d(0, -1),
        new Vec2d(1, 0),
        new Vec2d(0, 1),
        new Vec2d(-1, 0)
    ];

    private static readonly int[][] Orders =
    [
        [0, 1, 2, 3], [0, 1, 3, 2], [0, 2, 1, 3], [0, 2, 3, 1], [0, 3, 1, 2], [0, 3, 2, 1],
        [1, 0, 2, 3], [1, 0, 3, 2], [1, 2, 0, 3], [1, 2, 3, 0], [1, 3, 0, 2], [1, 3, 2, 0],
        [2, 0, 1, 3], [2, 0, 3, 1], [2, 1, 0, 3], [2, 1, 3, 0], [2, 3, 0, 1], [2, 3, 1, 0],
        [3, 0, 1, 2], [3, 0, 2, 1], [3, 1, 0, 2], [3, 1, 2, 0], [3, 2, 0, 1], [3, 2, 1, 0]
    ];

    public Cell[,] Generate()
    {
        if (coinProbability < 0 || coinProbability > 1)
            throw new ArgumentOutOfRangeException(nameof(coinProbability), "Coin probability must be between 0 and 1.");
        if (doorProbability < 0 || doorProbability > 1)
            throw new ArgumentOutOfRangeException(nameof(doorProbability), "Door probability must be between 0 and 1.");

        var grid = new Cell[width, height];
        var rng = new Random();
        var exit = new Vec2d((width - 1) & ~1, (height - 1) & ~1);

        for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
                grid[x, y] = new Wall();

        var start = Vec2d.Zero;
        GenerateRec(start);

        grid[start.X, start.Y] = new StartRoom((grid[start.X, start.Y] as Room)?.Collectable);
        grid[exit.X, exit.Y] = new ExitRoom((grid[exit.X, exit.Y] as Room)?.Collectable);

        return grid;

        bool GenerateRec(Vec2d position)
        {
            grid[position.X, position.Y] = CreateRoom(rng);
            var leadsToExit = position == exit;

            foreach (var dir in Orders[rng.Next(Orders.Length)])
            {
                var direction = Directions[dir];
                var next = position.Add(direction.Multiply(2));

                if (next.IsInBounds(width, height) && grid[next.X, next.Y].IsSolid)
                {
                    var passage = position.Midpoint(next);
                    Door? door = null;

                    if (rng.NextDouble() < doorProbability)
                    {
                        door = new Door();
                        grid[passage.X, passage.Y] = door;
                    }
                    else
                    {
                        grid[passage.X, passage.Y] = CreateRoom(rng);
                    }

                    var childLeadsToExit = GenerateRec(next);
                    if (childLeadsToExit && door is not null && grid[position.X, position.Y] is Room room)
                        door.PlaceKeyIn(room);

                    leadsToExit |= childLeadsToExit;
                }
            }

            return leadsToExit;
        }

        Room CreateRoom(Random random)
        {
            return random.NextDouble() < coinProbability
                ? new Room(new Coin())
                : new Room();
        }
    }
}
