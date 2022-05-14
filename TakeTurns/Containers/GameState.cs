using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeTurns.Containers
{
    public abstract class GameState<GameSpace, AgentType, EvalType>
    {
        public virtual GameSpace Space { get; set; }
        public virtual IList<AgentType> Moves { get; set; }
        public virtual AgentType Agent { get; set; }
    }
}
