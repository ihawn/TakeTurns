using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeTurns.Containers
{
    public abstract class GameState<GameSpace, AgentType>
    {
        public virtual GameSpace Space { get; set; }
        public virtual IList<AgentType> Agents { get; set; }
        public virtual AgentType CurrentAgent { get; set; }
    }
}
