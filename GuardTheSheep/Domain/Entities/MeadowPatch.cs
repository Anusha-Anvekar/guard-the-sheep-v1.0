using GuardTheSheep.Domain.Enum;
using GuardTheSheep.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace GuardTheSheep.Domain.Entities;

public class MeadowPatch
{
    public MeadowPatchType Type { get; set; }
    public GridPosition PatchPosition { get; set; }

    public MeadowPatch(GridPosition gridPosition, MeadowPatchType type)
    {
        this.PatchPosition = gridPosition; 
        this.Type = type;
    }

    public bool IsWalkable()
    {
        return Type != MeadowPatchType.Blocked;
    }

    public bool IsBlocked()
    {        
        return Type == MeadowPatchType.Blocked;
    }
}
