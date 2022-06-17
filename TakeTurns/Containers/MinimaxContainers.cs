using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeTurns.Interfaces;

namespace TakeTurns.Containers
{
    public class MinimaxInput<GameSpace, AgentType, MoveType, EvalType> : GameState<GameSpace, AgentType, MoveType, EvalType>
    {
        public MinimaxInput(GameSpace space, AgentType agent, MoveType move)
        {
            Space = space;
            Agent = agent;
            Move = move;
        }

        public MinimaxInput(GameSpace space) { Space = space; }
    }

    public class MinimaxOutput<GameSpace, AgentType, MoveType, EvalType> : GameState<GameSpace, AgentType, MoveType, EvalType> where AgentType : new() where MoveType : new()
    {
        public EvalType MinimaxEvaluation { get; set; }

        public MinimaxOutput(EvalType zero)
        {
            MinimaxEvaluation = zero;
            Agent = new AgentType();
            Move = new MoveType();
        }

        public MinimaxOutput(bool isMin, EvalType minValue, EvalType maxValue, GameSpace space)
        {
            MinimaxEvaluation = isMin ? maxValue : minValue;
            Space = space;
            Agent = new AgentType();
            Move = new MoveType();
        }

        public MinimaxOutput(EvalType evaluation, GameSpace space, AgentType agent, MoveType move)
        {
            MinimaxEvaluation = evaluation;
            Space = space;
            Agent = agent;
            Move = move;
        }
    }
}
