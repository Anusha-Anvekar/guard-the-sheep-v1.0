using System;
using System.Collections.Generic;
using System.Text;

namespace GuardTheSheep.Domain.Helper;

public class GridPosition
{
    public int X { get; }
    public int Y { get; }

    public GridPosition(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool IsGridPositionEqual(GridPosition other)
    {
        //return X.Equals(other.X) && Y.Equals(other.Y);
        return Equals(other);
    }

    public override bool Equals(object? obj)
    {
        if (!(obj is GridPosition))
                return false;

        GridPosition other = (GridPosition)obj;

        return X == other.X && Y == other.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}
