using GuardTheSheep.Domain.Domain.Services;
using GuardTheSheep.Domain.Entities;
using GuardTheSheep.Domain.Enum;
using GuardTheSheep.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuardTheSheep.Application.Factories;

public class GameFactory
{
    /* To start the game we will need to initialize the following entities
    Meadow
    Sheep */
    public Game CreateGame(int rows, int columns)
    {       
        Meadow meadow = CreateMeadow(rows, columns);

        Sheep sheep = CreateSheep(rows, columns);

        meadow = PlaceInitialBlockades(meadow, sheep);
        return new Game(meadow, sheep,GameState.InProgress);
    }

    private Meadow CreateMeadow(int rows,int columns)
    {
        List<MeadowPatch> patches = new();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GridPosition gridPosition = new GridPosition(i, j);

                if (i == 0 || j == 0 || i == rows - 1 || j == columns - 1)
                    patches.Add(new MeadowPatch(gridPosition, MeadowPatchType.Escape));
                else
                    patches.Add(new MeadowPatch(gridPosition, MeadowPatchType.Empty));
            }
        }
        return new Meadow(rows, columns, patches);
    }

    private Sheep CreateSheep(int rows, int columns)
    {
        return new Sheep(new GridPosition(rows / 2,columns / 2));
    }  
    
    private Meadow PlaceInitialBlockades(Meadow meadow, Sheep sheep)
    {
        BlockadeGenerator blockadeGenerator = new BlockadeGenerator();
        blockadeGenerator.InitialBlockadeGenerator(meadow,sheep, initialBlockadeCount: 8); // parametreize it later
        return meadow;
    }
}
