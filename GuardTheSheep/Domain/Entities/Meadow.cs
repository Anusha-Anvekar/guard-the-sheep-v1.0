using GuardTheSheep.Domain.Enum;
using GuardTheSheep.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuardTheSheep.Domain.Entities;

public class Meadow
{
    public int Rows { get; set; }
    public int Columns { get; set; }

    private List<MeadowPatch> Patches { get; }

    public Meadow(int rows, int columns, List<MeadowPatch> patches)
    {
        Rows = rows;
        Columns = columns;
        Patches = patches;
    }

    public MeadowPatch? GetPatch(GridPosition gridPosition)
    {
        foreach (MeadowPatch patch in Patches)
        {
            if (patch.PatchPosition.IsGridPositionEqual(gridPosition)) return patch;        
        }
        return null;
    }

    public bool IsValidPatch(GridPosition gridPosition)
    {
        return gridPosition.X >= 0 &&
               gridPosition.X < Columns &&
               gridPosition.Y >= 0 &&
               gridPosition.Y < Rows;
    }

    public bool PlaceBlockade(GridPosition position)
    {
        MeadowPatch patch = GetPatch(position);
        if (patch==null)
        {
            Console.WriteLine("Patch does not exist");
            return false;
        }
        if (patch.IsBlocked())
        {
            Console.WriteLine("Patch is already blocked");
            return false;
        }
        patch.Type = MeadowPatchType.Blocked;
        return true;
    }

    public List<MeadowPatch> GetNeighbors(GridPosition position)
    {
        List<MeadowPatch> neighbors = new();

        GridPosition[] positions = 
        {
            new GridPosition(position.X-1, position.Y),
            new GridPosition(position.X + 1, position.Y),
            new GridPosition(position.X, position.Y - 1),
            new GridPosition(position.X, position.Y + 1)
        };

        foreach (GridPosition pos in positions)
        {
            if (!IsValidPatch(pos)) continue;

            MeadowPatch patch = GetPatch(pos);

            if (patch != null) neighbors.Add(patch);
        }
        return neighbors;
    }
}
