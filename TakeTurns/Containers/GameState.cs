using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeTurns.Interfaces;

namespace TakeTurns.Containers
{
    public abstract class GameState<GameSpace, AgentType, MoveType, EvalType>
    {
        public virtual GameSpace Space { get; set; }
        public virtual MoveType Move { get; set; }
        public virtual AgentType Agent { get; set; }
    }
}
