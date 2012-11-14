/* GameGrid.cs - provides grid for creating and managing GameCells
 * Author:      Tim Hammerquist
 * Course:      CIT 134
 * Project:     Game of Life
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Life.Patterns
{
    abstract public class Pattern
    {
        protected GameGrid grid;

        public Pattern(GameGrid targetGrid)
        {
            grid = targetGrid;
        }

        abstract public string Name { get; }

        abstract public void Place(int x, int y);
    }

    public class Blinker : Pattern
    {
        public Blinker(GameGrid grid) : base(grid) { }

        public override string Name {
            get { return "Blinker"; }
        }

        public override void Place(int x, int y)
        {
            grid[x - 1, y].Alive = true;
            grid[x, y].Alive = true;
            grid[x + 1, y].Alive = true;
        }
    }

    public class Glider : Pattern
    {
        public Glider(GameGrid grid) : base(grid) { }

        public override string Name {
            get { return "Glider"; }
        }

        public override void Place(int x, int y)
        {
            grid[x, y - 1].Alive = true;
            grid[x + 1, y].Alive = true;
            grid[x - 1, y + 1].Alive = true;
            grid[x, y + 1].Alive = true;
            grid[x + 1, y + 1].Alive = true;
        }
    }

    public class SmallExploder : Pattern
    {
        public SmallExploder(GameGrid grid) : base(grid) { }

        public override string Name {
            get { return "Small Exploder"; }
        }

        public override void Place(int x, int y)
        {
            grid[x, y - 1].Alive = true;
            grid[x - 1, y].Alive = true;
            grid[x, y].Alive = true;
            grid[x + 1, y].Alive = true;
            grid[x - 1, y + 1].Alive = true;
            grid[x + 1, y + 1].Alive = true;
            grid[x, y + 2].Alive = true;
        }
    }

    public class Acorn : Pattern
    {
        public Acorn(GameGrid grid) : base(grid) { }

        public override string Name {
            get { return "Acorn"; }
        }

        public override void Place(int x, int y)
        {
            grid[x - 2, y - 1].Alive = true;
            grid[x, y].Alive = true;
            grid[x - 3, y + 1].Alive = true;
            grid[x - 2, y + 1].Alive = true;
            grid[x + 1, y + 1].Alive = true;
            grid[x + 2, y + 1].Alive = true;
            grid[x + 3, y + 1].Alive = true;
        }
    }

    public class BHepto : Pattern
    {
        public BHepto(GameGrid grid) : base(grid) { }

        public override string Name {
            get { return "B-Heptomino"; }
        }

        public override void Place(int x, int y)
        {
            grid[x, y - 1].Alive = true;
            grid[x - 1, y].Alive = true;
            grid[x, y].Alive = true;
            grid[x + 1, y].Alive = true;
            grid[x - 1, y + 1].Alive = true;
            grid[x + 1, y + 1].Alive = true;
            grid[x + 2, y + 1].Alive = true;
        }
    }

    public class PiHepto : Pattern
    {
        public PiHepto(GameGrid grid) : base(grid) { }

        public override string Name {
            get { return "Pi-Heptomino"; }
        }

        public override void Place(int x, int y)
        {
            grid[x - 1, y - 1].Alive = true;
            grid[x, y - 1].Alive = true;
            grid[x + 1, y - 1].Alive = true;
            grid[x - 1, y].Alive = true;
            grid[x + 1, y].Alive = true;
            grid[x - 1, y + 1].Alive = true;
            grid[x + 1, y + 1].Alive = true;
        }
    }

    public class RPento : Pattern
    {
        public RPento(GameGrid grid) : base(grid) { }

        public override string Name {
            get { return "R-Pentomino"; }
        }

        public override void Place(int x, int y)
        {
            grid[x, y - 1].Alive = true;
            grid[x + 1, y - 1].Alive = true;
            grid[x - 1, y].Alive = true;
            grid[x, y].Alive = true;
            grid[x, y + 1].Alive = true;
        }
    }

    public class Rabbits : Pattern
    {
        public Rabbits(GameGrid grid) : base(grid) { }

        public override string Name
        {
            get { return "Rabbits"; }
        }

        public override void Place(int x, int y)
        {
            grid[x - 3, y - 1].Alive = true;
            grid[x + 1, y - 1].Alive = true;
            grid[x + 2, y - 1].Alive = true;
            grid[x + 3, y - 1].Alive = true;
            grid[x - 3, y].Alive = true;
            grid[x - 2, y].Alive = true;
            grid[x - 1, y].Alive = true;
            grid[x + 2, y].Alive = true;
            grid[x - 2, y + 1].Alive = true;
        }
    }

    public class Exploder : Pattern
    {
        public Exploder(GameGrid grid) : base(grid) { }

        public override string Name
        {
            get { return "Exploder"; }
        }

        public override void Place(int x, int y)
        {
            grid[x - 2, y - 2].Alive = true;
            grid[x, y - 2].Alive = true;
            grid[x + 2, y - 2].Alive = true;
            grid[x - 2, y - 1].Alive = true;
            grid[x + 2, y - 1].Alive = true;
            grid[x - 2, y].Alive = true;
            grid[x + 2, y].Alive = true;
            grid[x - 2, y + 1].Alive = true;
            grid[x + 2, y + 1].Alive = true;
            grid[x - 2, y + 2].Alive = true;
            grid[x, y + 2].Alive = true;
            grid[x + 2, y + 2].Alive = true;
        }
    }

    public class TenCellRow : Pattern
    {
        public TenCellRow(GameGrid grid) : base(grid) { }

        public override string Name
        {
            get { return "Ten Cell Row"; }
        }

        public override void Place(int x, int y)
        {
            for (int i = 0; i < 10; ++i)
                grid[x + i - 4, y].Alive = true;
        }
    }

    public class Spaceship : Pattern
    {
        public Spaceship(GameGrid grid) : base(grid) { }

        public override string Name
        {
            get { return "Spaceship"; }
        }

        public override void Place(int x, int y)
        {
            grid[x-1,y-1].Alive = true;
            grid[x,y-1].Alive = true;
            grid[x+1,y-1].Alive = true;
            grid[x+2,y-1].Alive = true;
            grid[x - 2, y].Alive = true;
            grid[x + 2, y].Alive = true;
            grid[x + 2, y + 1].Alive = true;
            grid[x - 2, y + 2].Alive = true;
            grid[x + 1, y + 2].Alive = true;
        }
    }

}
