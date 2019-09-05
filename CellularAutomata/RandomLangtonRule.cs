using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomata
{
    class RandomLangtonRule : IRule
    {
        readonly Dictionary<int[], int> stateTable;

        const int neighbourhoodSize = 5;
        readonly int numStates;
        readonly double lambda;

        public RandomLangtonRule(int numStates, double lambda)
        {
            this.numStates = numStates;
            this.lambda = lambda;

            stateTable = new Dictionary<int[], int>(new ArrayEqualityComparer());

            GenerateStateTable();
        }

        void GenerateStateTable()
        {
            Random rand = new Random();
            
            int numActiveRules = (int) (lambda * Math.Pow(numStates, neighbourhoodSize)) ;            

            for (int i = 0; i < numActiveRules; i++)
            {
                int[] newRule = new int[neighbourhoodSize];

                do
                {
                    for (int j = 0; j < neighbourhoodSize; j++)
                    {
                        newRule[j] = rand.Next(numStates);
                    }
                } while (stateTable.ContainsKey(newRule));

                stateTable.Add(newRule, rand.Next(numStates));
            }
        }

        public int ApplyRule(int[] oldStates)
        {
            stateTable.TryGetValue(oldStates, out int nextState);

            return nextState;
        }

        public Neighbourhoods GetNeighbourhood() => Neighbourhoods.VonNeumann;
    }

    class ArrayEqualityComparer : IEqualityComparer<int[]>
    {
        public bool Equals(int[] x, int[] y)
        {
            if (x.Length != y.Length)
            {
                return false;
            }
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != y[i])
                {
                    return false;
                }
            }
            return true;
        }

        public int GetHashCode(int[] obj)
        {
            int result = 17;
            for (int i = 0; i < obj.Length; i++)
            {
                unchecked
                {
                    result = result * 23 + obj[i];
                }
            }
            return result;
        }
    }
}

