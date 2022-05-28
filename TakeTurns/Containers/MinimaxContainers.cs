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

    public class MinimaxOutput<GameSpace, AgentType, MoveType, EvalType> : GameState<GameSpace, AgentType, MoveType, EvalType> where AgentType : new()
    {
        public EvalType MinimaxEvaluation { get; set; }

        public MinimaxOutput(EvalType zero)
        {
            MinimaxEvaluation = zero;
            Moves = new List<MoveType>();
            Agent = new AgentType();
        }

        public MinimaxOutput(bool isMin, EvalType minValue, EvalType maxValue, GameSpace space)
        {
            MinimaxEvaluation = isMin ? maxValue : minValue;
            Space = space;
            Moves = new List<MoveType>(); //change this to just a MoveType. If the user wants, their MoveType can be a list...
            Agent = new AgentType();
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
