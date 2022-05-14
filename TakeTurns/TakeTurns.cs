using System;
using System.Collections.Generic;
using TakeTurns.Containers;
using TakeTurns.Interfaces;

namespace TakeTurns
{
    public abstract class TakeTurns<GameSpace, AgentType> : IGameEvaluation<GameSpace, AgentType, float, MinimaxInput<GameSpace, AgentType, float>>
    {
        public abstract float GetGameEvaluation(GameSpace space);
        public abstract IList<MinimaxInput<GameSpace, AgentType, float>> GetPositions(GameSpace space, bool isMaxPlayer);
        public abstract int GetAgentCount(GameSpace space, bool isMaxPlayer);



        private MinimaxOutput<GameSpace, AgentType, float> Minimax(MinimaxInput<GameSpace, AgentType, float> input, 
                                                                      MinimaxOutput<GameSpace, AgentType, float> minEvaluation, 
                                                                      MinimaxOutput<GameSpace, AgentType, float> maxEvaluation, 
                                                                      IList<AgentType> originatingMoves, AgentType originatingPiece, 
                                                                      int depth, int originalDepth, bool isMaximizingPlayer, float alpha, float beta)
        {
            if (input == null || input.Space == null)
                return new MinimaxOutput<GameSpace, AgentType, float>();

            int blackCount = GetAgentCount(input.Space, true);
            int whiteCount = GetAgentCount(input.Space, false);
            if (depth == 0 || blackCount == 0 || whiteCount == 0)
                return new MinimaxOutput<GameSpace, AgentType, float>(GetGameEvaluation(input.Space), input.Space, originatingMoves, originatingPiece);

            IList<MinimaxInput<GameSpace, AgentType, float>> branches = GetPositions(input.Space, isMaximizingPlayer);

            if (isMaximizingPlayer)
            {
                foreach (var branch in branches)
                {
                    originatingMoves = depth == originalDepth ? branch.Moves : originatingMoves;
                    originatingPiece = depth == originalDepth ? branch.Agent : originatingPiece;

                    MinimaxOutput<GameSpace, AgentType, float> result = Minimax(branch, minEvaluation, maxEvaluation, originatingMoves, originatingPiece, depth - 1, originalDepth, false, alpha, beta);
                    maxEvaluation = result.MinimaxEvaluation > maxEvaluation.MinimaxEvaluation || (result.MinimaxEvaluation == maxEvaluation.MinimaxEvaluation && new Random().Next(1, 10) <= 5) ? result : maxEvaluation;

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
                    originatingMoves = depth == originalDepth ? branch.Moves : originatingMoves;
                    originatingPiece = depth == originalDepth ? branch.Agent : originatingPiece;

                    MinimaxOutput<GameSpace, AgentType, float> result = Minimax(branch, minEvaluation, maxEvaluation, originatingMoves, originatingPiece, depth - 1, originalDepth, true, alpha, beta);
                    minEvaluation = result.MinimaxEvaluation < minEvaluation.MinimaxEvaluation || (result.MinimaxEvaluation == minEvaluation.MinimaxEvaluation && new Random().Next(1, 10) <= 5) ? result : minEvaluation;

                    beta = Math.Min(beta, minEvaluation.MinimaxEvaluation);
                    if (beta <= alpha)
                        break;
                }
                return minEvaluation;
            }
        }
    }
}
