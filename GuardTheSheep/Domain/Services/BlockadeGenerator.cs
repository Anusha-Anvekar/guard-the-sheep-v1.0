using GuardTheSheep.Domain.Entities;
using GuardTheSheep.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuardTheSheep.Domain.Domain.Services;

public class BlockadeGenerator
{
    Random random = new Random();
    public void InitialBlockadeGenerator(Meadow meadow,Sheep sheep,int initialBlockadeCount)
    {
        while (initialBlockadeCount > 0)
        {
            int x = random.Next(meadow.Columns);
            int y = random.Next(meadow.Rows);

            GridPosition position = new GridPosition(x, y);

            if (position.IsGridPositionEqual(sheep.CurrentPosition)) continue;

            if (meadow.PlaceBlockade(position)) initialBlockadeCount--;
        }
    }
}
