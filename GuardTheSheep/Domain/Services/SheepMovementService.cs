using GuardTheSheep.Domain.Entities;
using GuardTheSheep.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuardTheSheep.Domain.Services;

public static class SheepMovementService
{
    public static bool MoveSheep(Sheep sheep,Meadow meadow, GridPosition newPosition)
    {
        if(!meadow.IsValidPatch(newPosition)) return false;

        MeadowPatch patch = meadow.GetPatch(newPosition);
        if (patch == null) 
        {
            Console.WriteLine("Invalid position for sheep to move");
            return false; 
        }
        if (!patch.IsWalkable())
        {
            Console.WriteLine("Patch is blocked");
            return false; 
        }
        sheep.MoveTo(newPosition);
        return true;
    }
}
