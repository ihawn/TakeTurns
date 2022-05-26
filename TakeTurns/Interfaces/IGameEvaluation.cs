using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeTurns.Enumerations;

namespace TakeTurns.Interfaces
{
    public interface IGameEvaluation<GameSpace, AgentType, EvalType, InputType>
    {
        EvalType GetGameEvaluation(GameSpace space);
        IList<InputType> GetPositions(GameSpace space, bool isMaxPlayer);
        (bool, EndState) EndGameReached(GameSpace space); //(end reached, did maximizing player win)
    }
}
