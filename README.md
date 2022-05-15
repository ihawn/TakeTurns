# Take Turns
[![version](https://img.shields.io/badge/version-1.0.1-yellow.svg)](https://semver.org)
## A 2-Player, Turn-Base AI Engine

Take Turns is a fast, flexible, 2-player AI engine based off the classic minimax algorithm with alpha-beta pruning

- Compatible with Unity
- Fully customizable and suitable for any turn-based, 2-player game
- Simple, yet effective approach to turn-based AI

![checkers](https://github.com/ihawn/TakeTurns/blob/main/TakeTurns/Resources/checkers.gif)

## Installation
##### General Installation
This package is available in NuGet with a search or running ```Install-Package TakeTurns -Version 1.0.1``` in the Package manager console.
##### Unity Installation
Download ```TakeTurns.dll``` from [the release page](https://github.com/ihawn/TakeTurns/releases/tag/1.0.1) and place anywhere inside your Assets directory.



## Usage
See the example project for a specific implementation example of this package.
This package requires the following user-supplied classes which will be specific to your game.
Note that your class names need not match.
```sh
public class GameSpace
{
    // Class to store the game
    // Needs to have information on each game agent
    // Should be as simple to make deep-copying fast
    
    // For example, if  you were to make a chess AI with this package you would probably want
    // to have a matrix property to store each piece type and location
}

public class Agent
{
    // Class to store each piece
    // In the chess example, this could be as simple as an x, y coordinate to access the piecetype in GameSpace
}

public class Move
{
    // Class to store each move
    // Should contain enough information to modify GameSpace to reflect a unique movement of Agent
    // In the chess example, this would just be an x, y coordinate to specify which square to move to
}

public class OverloadContainer : TakeTurns<GameSpace, Agent, Move>
{
    // Class to store overloads
    // An instance of this class will also be needed to call the GetBestMove() method
}
```

Place the following 3 overloads in OverloadContainer:
```
public override float GetGameEvaluation(GameSpace space)
{
    // This method is very important and will be quite specific to your game
    // It should calculate a float which defines the "state" of your game
    // Player 1 will want to minimize this value while Player 2 will want to maximize it
    // Returning 0 would mean the game is currently tied
    // In the chess example, a simple evaluation calculation would be BlackPieceCount - WhitePieceCount
    // Obviously, black would want to maximize this value and white would want to minimize it
    // See https://en.wikipedia.org/wiki/Minimax for more info
}

public override bool EndGameReached(GameSpace space)
{
    // Returns true if space contains a finished game (there is a winner or a draw)
    // Note that this is not measuring the current game space but rather that passed game space
}

public override IList<MinimaxInput<GameSpace, Agent, Move, float>> GetPositions(Gamespace space, bool isMaxPlayer)
{
    // Generates a collection of MinimaxInput objects which represent each game state which is possible from the passed one (space)
    // MinimaxInput contains the following fields
    //    - GameSpace -> the gamespace generated from space
    //    - Agent     -> the agent which generated GameSpace
    //    - Move      -> the move which Agent took to generate GameSpace
}
```
**IMPORTANT:** Inside ```GetPosition()```, when computing a branch of space, you must deep-copy space before assigning it to the ```GameSpace``` property of the current ```MinimaxInput``` object. If you do not, you will modify the passed parameter space which will result in nonsense output. This is why your ```GameSpace``` class should contain the bare minimum amount of information needed. Preferrably, it should contain only primitives so that you may define the deep-copy in a constructor overload of ```GameSpace```.

And that's it! Obviously, games can be quite different which is why this user-supplied information is needed. The next move your computer player should make can be computed with the function 
```
OverloadContainer.GetBestMove(GameSpace g, int depth, bool isMaximizingPlayer)
```
The ```isMaximizingPlayer``` bool simply represents whose turn it is. It doesn't matter which player you choose to be your maximizing one as long as you are consistent when calling this function as well as when writing ```GetPositions```. Note too that depth must be odd (or else this function would return what move the other player should make).

```GetBestMove()``` returns a ```MinimaxOutput``` object which contains the following parameters:
- *Piece:*   The piece that should move
- *Move:*  The Move that Piece should make
- *GameSpace:*  The space of your game after Piece performs Move

Now you have all the information you need to move your computer player's piece!


## License

MIT
