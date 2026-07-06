using GuardTheSheep.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuardTheSheep.Domain.Entities;

public class Sheep{
    public GridPosition CurrentPosition { get; set; }

    public Sheep(GridPosition initialPosition)
    {
        this.CurrentPosition = initialPosition; 
    }

    public void MoveTo(GridPosition newPosition)
    {
        this.CurrentPosition = newPosition;
    }
}
