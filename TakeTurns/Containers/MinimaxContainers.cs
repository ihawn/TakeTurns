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
        public MinimaxInput(GameSpace space, IList<MoveType> moves, AgentType agent)
        {
            Space = space;
            Moves = moves;
            Agent = agent;
        }

        public MinimaxInput(GameSpace space) { Space = space; }
    }

    public class MinimaxOutput<GameSpace, AgentType, MoveType, EvalType> : GameState<GameSpace, AgentType, MoveType, EvalType>
    {
        public EvalType MinimaxEvaluation { get; set; }

        public MinimaxOutput() { }

        public MinimaxOutput(EvalType zero)
        {
            MinimaxEvaluation = zero;
            Space = default(GameSpace);
            Moves = new List<MoveType>();
            Agent = default(AgentType);
        }

        public MinimaxOutput(bool isMin, EvalType minValue, EvalType maxValue, GameSpace space)
        {
            MinimaxEvaluation = isMin ? minValue : maxValue;
            Space = space;
            Moves = new List<MoveType>();
            Agent = default(AgentType);
        }

        public MinimaxOutput(EvalType evaluation, GameSpace space, IList<MoveType> moves, AgentType agent)
        {
            MinimaxEvaluation = evaluation;
            Space = space;
            Moves = moves;
            Agent = agent;
        }
    }
}
