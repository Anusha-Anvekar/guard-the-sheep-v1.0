using GuardTheSheep.Domain.Entities;
using GuardTheSheep.Domain.Enum;
using GuardTheSheep.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace GuardTheSheep.Domain.Domain.Services;

// We will be using BFS to find shortest escape path
public class SheepPathFindingService
{
    public GridPosition? FindNextMove(Meadow meadow, Sheep sheep)
    {
        // Start position of sheep
        GridPosition start = sheep.CurrentPosition;

        //child -> parent map, previous position to new position
        Dictionary<GridPosition, GridPosition?> parentMap = new();

        Queue<GridPosition> queue = new(); //for BFS
        HashSet<GridPosition> visited = new(); // prevents revisiting same nodes

        queue.Enqueue(start);
        visited.Add(start);

        parentMap[start] = null;

        // holds escape location once found
        GridPosition? escapeNode = null; 

        //BFS STARTS
        while (queue.Count > 0)
        {
            GridPosition current = queue.Dequeue();

            MeadowPatch? currentPatch = meadow.GetPatch(current);

            // If this is an escape tile, stop BFS
            if (currentPatch != null && currentPatch.Type == MeadowPatchType.Escape)
            {
                escapeNode = current;
                break;
            }

            // Explore all valid neighbors 
            foreach (MeadowPatch neighborPatch in meadow.GetNeighbors(current))
            {
                GridPosition neighborPos = neighborPatch.PatchPosition;

                // Skip already visited nodes
                if (visited.Contains(neighborPos))
                    continue;

                // Skip blocked tiles
                if (neighborPatch.Type == MeadowPatchType.Blocked)
                    continue;

                // Mark as visited
                visited.Add(neighborPos);

                // Record how we reached this node
                parentMap[neighborPos] = current;

                // Add to BFS queue
                queue.Enqueue(neighborPos);
            }            

        }
        //NO PATH FOUND
        if (escapeNode == null) return null;

        //PATH FOUND
        /*RECONSTRUCT ENTIRE PATH FIRST ESCAPE FOUND TO SHEEP
        RETURN ONLY FIRST STEP TAKEN BY SHEEP*/
        var x =  GetNextStep(parentMap, start, escapeNode);
        return x;
    }

    /*RECONSTRUCT ENTIRE PATH FIRST ESCAPE FOUND TO SHEEP
    RETURN ONLY FIRST STEP TAKEN BY SHEEP*/
    private GridPosition? GetNextStep(Dictionary<GridPosition, GridPosition?> parentMap,GridPosition sheepPosition, GridPosition escape)
    {
        GridPosition current = escape;
        GridPosition? previous = null;

        // Move backwards until we reach start
        while (!current.Equals(sheepPosition))
        {
            previous = current;
            current = parentMap[current];
        }

        // 'previous' is the first step from sheep toward escape
        return previous;
    }
}
