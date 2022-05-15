using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeTurns.Interfaces
{
    public interface IGameEvaluation<GameSpace, AgentType, EvalType, InputType>
    {
        EvalType GetGameEvaluation(GameSpace space);
        IList<InputType> GetPositions(GameSpace space, bool isMaxPlayer);
        bool EndGameReached(GameSpace space);
    }
}
