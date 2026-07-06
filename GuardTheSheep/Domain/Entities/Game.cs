using GuardTheSheep.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuardTheSheep.Domain.Entities;

public class Game
{
    public Meadow Meadow { get; }
    public Sheep Sheep { get; }
    public GameState CurrentState { get; private set; }

    public Game(Meadow meadow, Sheep sheep, GameState currentState)
    {
        Meadow = meadow;
        Sheep = sheep;
        CurrentState = currentState;
    }

    public void UpdateState(GameState state)
    {
        CurrentState = state;
    }
}
