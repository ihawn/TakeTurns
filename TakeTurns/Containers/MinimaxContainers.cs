using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeTurns.Interfaces;

namespace TakeTurns.Containers
{
    public class MinimaxInput<GameSpace, AgentType, EvalType> : GameState<GameSpace, AgentType, EvalType>
    {
        public MinimaxInput(GameSpace space, IList<AgentType> moves, AgentType agent)
        {
            Space = space;
            Moves = moves;
            Agent = agent;
        }

        public MinimaxInput(GameSpace space) { Space = space; }
    }

    public abstract class MinimaxOutput<GameSpace, AgentType, EvalType> : GameState<GameSpace, AgentType, EvalType>, IGameEvaluation<GameSpace, AgentType, EvalType>
    {
        public EvalType MinimaxEvaluation 
        { 
            get 
            { 
                return GetGameEvaluation(Space); 
            } 
            set { } 
        }

        public MinimaxOutput(EvalType zero)
        {
            MinimaxEvaluation = zero;
            Space = default(GameSpace);
            Moves = new List<AgentType>();
            Agent = default(AgentType);
        }

        public MinimaxOutput(bool isMin, EvalType minValue, EvalType maxValue, GameSpace space)
        {
            MinimaxEvaluation = isMin ? minValue : maxValue;
            Space = space;
            Moves = new List<AgentType>();
            Agent = default(AgentType);
        }

        public MinimaxOutput(EvalType evaluation, GameSpace space, IList<AgentType> moves, AgentType agent)
        {
            MinimaxEvaluation = evaluation;
            Space = space;
            Moves = moves;
            Agent = agent;
        }

        public abstract EvalType GetGameEvaluation(GameSpace space);
        public abstract IList<AgentType> GetPositions(GameSpace space);
    }

    public abstract class BranchResult<GameSpace, AgentType, EvalType> : GameState<GameSpace, AgentType, EvalType>
    {
        public IList<MinimaxInput<GameSpace, AgentType, EvalType>> Branches { get; set; }

        public BranchResult(List<MinimaxInput<GameSpace, AgentType, EvalType>> branches) { Branches = branches; }
    }
}
