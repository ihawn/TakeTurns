using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeTurns.Containers
{
    public class MinimaxInput : GameState<object, object>
    {
        public MinimaxInput(object space, IList<object> moves, object agent)
        {
            Space = space;
            Moves = moves;
            Agent = agent;
        }

        public MinimaxInput(object space)
        {
            Space = space;
        }
    }

    public class MinimaxOutput : GameState<object, object>
    {
        public int MinimaxEvaluation { get; set; }

        public MinimaxOutput()
        {
            MinimaxEvaluation = 0;
            Space = null;
            Moves = new List<object>();
            Agent = new object();
        }

        public MinimaxOutput(bool isMin, object space)
        {
            MinimaxEvaluation = isMin ? int.MaxValue : int.MinValue;
            Space = space;
            Moves = new List<object>();
            Agent = new object();
        }

        public MinimaxOutput(int evaluation, object space, List<object> moves, object agent)
        {
            MinimaxEvaluation = evaluation;
            Space = space;
            Moves = Moves;
            Agent = agent;
        }
    }
}
