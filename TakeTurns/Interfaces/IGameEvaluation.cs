using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeTurns.Interfaces
{
    public interface IGameEvaluation<GameSpace, AgentType, EvalType>
    {
        EvalType GetGameEvaluation(GameSpace space);
        IList<AgentType> GetPositions(GameSpace space);
    }
}
