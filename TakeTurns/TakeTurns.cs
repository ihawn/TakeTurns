using System;
using System.Collections.Generic;
using TakeTurns.Containers;
using TakeTurns.Interfaces;
using TakeTurns.Enumerations;

namespace TakeTurns
{
    public abstract class TakeTurns<GameSpace, AgentType, MoveType> : IGameEvaluation<GameSpace, AgentType, float, MinimaxInput<GameSpace, AgentType, MoveType, float>> where AgentType : new() where MoveType : new()
    {
        public abstract float GetGameEvaluation(GameSpace space);
        public abstract IList<MinimaxInput<GameSpace, AgentType, MoveType, float>> GetPositions(GameSpace space, bool isMaxPlayer);
        public abstract (bool, EndState) EndGameReached(GameSpace space);

        public MinimaxOutput<GameSpace, AgentType, MoveType, float> GetBestMove(GameSpace Game, int depth, bool isMaximizingPlayer)
        {
            if (depth % 2 == 0) { throw new ArgumentException("Depth Must Be Odd"); }
            MinimaxOutput<GameSpace, AgentType, MoveType, float> minEvaluation = new MinimaxOutput<GameSpace, AgentType, MoveType, float>(true, float.MinValue, float.MaxValue, Game);
            MinimaxOutput<GameSpace, AgentType, MoveType, float> maxEvaluation = new MinimaxOutput<GameSpace, AgentType, MoveType, float>(false, float.MinValue, float.MaxValue, Game);
            MinimaxInput<GameSpace, AgentType, MoveType, float> input = new MinimaxInput<GameSpace, AgentType, MoveType, float>(Game);

            MinimaxOutput<GameSpace, AgentType, MoveType, float> minimaxResult = Minimax(input, minEvaluation, maxEvaluation, new MoveType(), new AgentType(), depth, depth, isMaximizingPlayer, float.MinValue, float.MaxValue);

            while(minimaxResult.Move == null && depth > 2)
            {
                depth -= 2;
                minEvaluation = new MinimaxOutput<GameSpace, AgentType, MoveType, float>(true, float.MinValue, float.MaxValue, Game);
                maxEvaluation = new MinimaxOutput<GameSpace, AgentType, MoveType, float>(false, float.MinValue, float.MaxValue, Game);
                minimaxResult = Minimax(input, minEvaluation, maxEvaluation, new MoveType(), new AgentType(), depth, depth, isMaximizingPlayer, float.MinValue, float.MaxValue);
            }

            return minimaxResult;
        }

        private MinimaxOutput<GameSpace, AgentType, MoveType, float> Minimax(MinimaxInput<GameSpace, AgentType, MoveType, float> input, 
                                                                   MinimaxOutput<GameSpace, AgentType, MoveType, float> minEvaluation, 
                                                                   MinimaxOutput<GameSpace, AgentType, MoveType, float> maxEvaluation, 
                                                                   MoveType originatingMoves, AgentType originatingPiece, 
                                                                   int depth, int originalDepth, bool isMaximizingPlayer, float alpha, float beta)
        {
            if (input == null || input.Space == null)
                return new MinimaxOutput<GameSpace, AgentType, MoveType, float>(0);

            if (depth == 0 || EndGameReached(input.Space).Item1)
                return new MinimaxOutput<GameSpace, AgentType, MoveType, float>(GetGameEvaluation(input.Space), input.Space, originatingPiece, originatingMoves);

            IList<MinimaxInput<GameSpace, AgentType, MoveType, float>> branches = GetPositions(input.Space, isMaximizingPlayer);

            if (isMaximizingPlayer)
            {
                foreach (var branch in branches)
                {
                    originatingMoves = depth == originalDepth ? branch.Move : originatingMoves;
                    originatingPiece = depth == originalDepth ? branch.Agent : originatingPiece;

                    MinimaxOutput<GameSpace, AgentType, MoveType, float> result = Minimax(branch, minEvaluation, maxEvaluation, originatingMoves, originatingPiece, depth - 1, originalDepth, false, alpha, beta);
                    maxEvaluation = result.MinimaxEvaluation > maxEvaluation.MinimaxEvaluation ? result : maxEvaluation;

                    alpha = Math.Max(alpha, maxEvaluation.MinimaxEvaluation);
                    if (beta <= alpha)
                        break;
                }
                return maxEvaluation;
            }
            else
            {
                foreach (var branch in branches)
                {
                    originatingMoves = depth == originalDepth ? branch.Move : originatingMoves;
                    originatingPiece = depth == originalDepth ? branch.Agent : originatingPiece;

                    MinimaxOutput<GameSpace, AgentType, MoveType, float> result = Minimax(branch, minEvaluation, maxEvaluation, originatingMoves, originatingPiece, depth - 1, originalDepth, true, alpha, beta);
                    minEvaluation = result.MinimaxEvaluation < minEvaluation.MinimaxEvaluation ? result : minEvaluation;

                    beta = Math.Min(beta, minEvaluation.MinimaxEvaluation);
                    if (beta <= alpha)
                        break;
                }
                return minEvaluation;
            }
        }
    }
}
