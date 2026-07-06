# 🐑 Guard The Sheep

A small WPF puzzle game where your goal is to **trap the sheep** before it escapes the meadow.

## 📸 Preview


## In This Version (0.1)

- **The Meadow** is made up of a grid of square patches (7 × 7 by default).
- **The Sheep** starts somewhere on the meadow and can move in **4 directions** — up, down, left, and right (no diagonals).
- **Blockades** can be placed on any empty patch by clicking it. A patch that already holds a blockade, or the patch the sheep is standing on, cannot be blocked.
- After every blockade you place, the sheep takes **one step** along the shortest path toward the edge.
- **The Sheep escapes** when it reaches an **escape patch** on the border of the meadow — if that happens, **you lose**.
- **You win** when the sheep is completely surrounded and has **no valid move** left — the sheep is trapped.
- **New Game** resets the meadow with a fresh sheep position so you can play again.

## How To Play

1. Launch the app — the meadow and sheep are drawn automatically.
2. Click any empty green patch to place a 🚧 blockade.
3. Watch the sheep move one step after each of your placements.
4. Keep boxing the sheep in until it has nowhere to go.
5. Use the **🔄 New Game** button at any time to start over.

## Board Legend

| Symbol / Color | Meaning |
| --- | --- |
| 🐑 | The sheep |
| 🚧 | A blockade you placed |
| Green patches | Open meadow (clickable) |
| Darker green edge | Escape patches (the sheep wins if it reaches here) |

## Project Structure

- **GuardTheSheep** — Domain layer (entities such as `Meadow`, `Sheep`, `MeadowPatch`, and enums).
- **GuardTheSheep.Application** — Application services (`GameEngine`, pathfinding, factories).
- **GuardTheSheep.WPF** — The WPF user interface (`MainWindow`).

## Requirements

- .NET 10 (Windows)
- Windows OS (WPF)

## Running The Game

Open the solution (`GuardTheSheep.slnx`) in Visual Studio / VS Code and run the **GuardTheSheep.WPF** project, or from a terminal:

```powershell
dotnet run --project GuardTheSheep.WPF
```
