using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomata
{
    class BasicCellularAutomata : IAutomata
    {
        private int[,] field;

        private int fieldSize;
        private int numStates;
        private IRule rule;

        public BasicCellularAutomata(int fieldSize, int numStates, IRule rule)
        {
            this.fieldSize = fieldSize;
            this.numStates = numStates;
            this.rule = rule;
        }

        public int[,] GetField()
        {
            return field;
        }

        public void Initialize()
        {
            Random rand = new Random();

            field = new int[fieldSize, fieldSize];

            for (int x = 0; x < fieldSize; x++)
            {
                for (int y = 0; y < fieldSize; y++)
                {
                    field[x, y] = rand.Next(numStates);
                }
            }
        }

        public void Iterate()
        {
            int[,] new_field = new int[fieldSize, fieldSize];

            Parallel.For(0, fieldSize, (int x) =>
            {
                for (int y = 0; y < fieldSize; y++)
                {
                    switch (rule.GetNeighbourhood())
                    {
                        case Neighbourhoods.VonNeumann:
                            int[] neighbours = new int[5];

                            neighbours[0] = field[x, y];
                            neighbours[1] = field[mod(x - 1, fieldSize), y];
                            neighbours[2] = field[x, mod(y + 1, fieldSize)];
                            neighbours[3] = field[mod(x + 1, fieldSize), y];
                            neighbours[4] = field[x, mod(y - 1, fieldSize)];

                            new_field[x, y] = rule.ApplyRule(neighbours);

                            break;
                        case Neighbourhoods.Moore:
                            neighbours = new int[9];

                            neighbours[0] = field[mod(x, fieldSize), mod(y, fieldSize)];
                            neighbours[1] = field[mod(x+1, fieldSize), mod(y-1, fieldSize)];
                            neighbours[2] = field[mod(x+1, fieldSize), mod(y, fieldSize)];
                            neighbours[3] = field[mod(x+1, fieldSize), mod(y+1, fieldSize)];
                            neighbours[4] = field[mod(x, fieldSize), mod(y-1, fieldSize)];
                            neighbours[5] = field[mod(x, fieldSize), mod(y+1, fieldSize)];
                            neighbours[6] = field[mod(x-1, fieldSize), mod(y-1, fieldSize)];
                            neighbours[7] = field[mod(x-1, fieldSize), mod(y, fieldSize)];
                            neighbours[8] = field[mod(x-1, fieldSize), mod(y+1, fieldSize)];

                            new_field[x, y] = rule.ApplyRule(neighbours);

                            break;
                    }

                }
            });

            field = new_field;
        }

        private int mod(int x, int m)
        {
            int r = x % m;
            return r < 0 ? r + m : r;
        }
    }
}
