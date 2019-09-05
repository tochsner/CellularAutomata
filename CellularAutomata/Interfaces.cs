using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomata
{
    enum Neighbourhoods
    {
        VonNeumann = 5,
        Moore = 9
    }

    interface IRule
    {
        Neighbourhoods GetNeighbourhood();

        int ApplyRule(int[] oldStates);
    }

    interface IAutomata
    {
        int[,] GetField();

        void Initialize();
        void Iterate();
    }
}
