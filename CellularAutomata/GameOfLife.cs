using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomata
{
    class GameOfLife : IRule
    {
        public int ApplyRule(int[] oldStates)
        {
            int count = oldStates.Count((int x) => x != 0);

            if (oldStates[0] == 0) // inactive
            {
                if (count == 3)
                    return 1;
                else
                    return 0;
            }
            else // active
            {
                if (count < 3)
                    return 0;
                if (count == 3 || count == 4)
                    return 1;
                else
                    return 0;
            }
        }

        public Neighbourhoods GetNeighbourhood()
        {
            return Neighbourhoods.Moore;
        }
    }
}
