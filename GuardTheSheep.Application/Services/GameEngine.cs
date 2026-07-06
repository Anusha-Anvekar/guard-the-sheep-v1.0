using GuardTheSheep.Application.Factories;
using GuardTheSheep.Domain.Domain.Services;
using GuardTheSheep.Domain.Entities;
using GuardTheSheep.Domain.Enum;
using GuardTheSheep.Domain.Helper;
using GuardTheSheep.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuardTheSheep.Application.Services;

public class GameEngine
{
    private GameFactory gameFactory = new();
    private readonly SheepPathFindingService pathFinder = new();
    public Game CurrentGame { get; private set; }

    public GameState GetGameState() 
    {
        return CurrentGame.CurrentState;
    }

   public void StartNewGame(int rows, int columns)
    {
        CurrentGame = gameFactory.CreateGame(rows, columns);
    }

    public void PlaceBlockade(GridPosition position)
    {
        if (CurrentGame == null)
        {
            Console.WriteLine("No game in progress");
            return;
        }
        // Place blockade on board
        bool success = CurrentGame.Meadow.PlaceBlockade(position);
        if (!success)
        {
            Console.WriteLine("Failed to place blockade");
        }

        //Calculate sheep next move
        GridPosition? nextMove =  pathFinder.FindNextMove(CurrentGame.Meadow,CurrentGame.Sheep);
    
        //If no move → sheep is trapped → player wins
        if (nextMove == null)
        {
            CurrentGame.UpdateState(GameState.Won);
            return;
        }
        //move sheep
        MoveSheep(nextMove);

        // check escape condition
        MeadowPatch? currentSheepPatch = CurrentGame.Meadow.GetPatch(nextMove);

        if (currentSheepPatch != null && currentSheepPatch.Type == MeadowPatchType.Escape)
        {
            CurrentGame.UpdateState(GameState.Lost);
        }
    }

    public void MoveSheep(GridPosition newPosition)
    {
        if (CurrentGame == null)
        {
            Console.WriteLine("No game in progress");
            return;
        }

        bool success = SheepMovementService.MoveSheep(CurrentGame.Sheep, CurrentGame.Meadow, newPosition);
        if (!success)
        {
            Console.WriteLine("Failed to move sheep");
        }
    }
}
