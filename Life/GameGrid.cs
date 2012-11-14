/* GameGrid.cs - provides grid for creating and managing GameCells
 * Author:      Tim Hammerquist
 * Course:      CIT 134
 * Project:     Game of Life
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Life
{
    public class GameGrid
    {
        #region Private Fields

        private readonly int FIELD_WIDTH;
        private readonly int FIELD_HEIGHT;
        private readonly GameCell[,] field;

        #endregion

        #region Constructors

        public GameGrid(int width, int height)
        {
            FIELD_WIDTH = width;
            FIELD_HEIGHT = height;

            field = new GameCell[FIELD_WIDTH, FIELD_HEIGHT];

            InitializeCells();
        } // ctor GameGrid

        #endregion

        #region Properties

        public GameCell this[int x, int y]
        {
            get { return GetCell(x, y); }
        } // prop []

        #endregion

        #region Public Methods

        public void Evolve()
        {
            // each cell stores its own neighbors
            // this iteration need not be indexed

            // evaluate next state of each cell
            foreach (var cell in field)
                cell.Evaluate();

            // with all evaluation complete, update can take place
            foreach (var cell in field)
                cell.Update();
        } // Evolve()

        public void Clear()
        {
            foreach (var cell in field)
                cell.Alive = false;
        } // Clear()

        public void Randomize(int probability)
        {
            var rng = new Random();

            foreach (var cell in field)
                cell.Alive = (rng.Next(100) < probability);
        } // Randomize(int)

        #endregion

        #region Private Methods

        private void InitializeCells()
        {
            // here's the meat
            // messy, but only done once

            int w, h;

            // populate each grid point with a cell object
            for (w = 0; w < FIELD_WIDTH; ++w)
                for (h = 0; h < FIELD_HEIGHT; ++h)
                    field[w, h] = new GameCell();

            // populate the neighbors array for each cell
            for (w = 0; w < FIELD_WIDTH; ++w)
                for (h = 0; h < FIELD_HEIGHT; ++h)
                    field[w, h].SetNeighbors(CollectNeighbors(w,h));
        } // InitializeCells()

        private GameCell[] CollectNeighbors(int x, int y)
        {
            // collect all neighboring Cell objects
            // GetCell() performs coord normalization
            var neighbors = new GameCell[8];
            neighbors[0] = this[x - 1, y - 1];  // left-top
            neighbors[1] = this[x - 1, y];      // left
            neighbors[2] = this[x - 1, y + 1];  // left-bottom
            neighbors[3] = this[x, y - 1];      // top
            neighbors[4] = this[x, y + 1];      // bottom
            neighbors[5] = this[x + 1, y - 1];  // right-top
            neighbors[6] = this[x + 1, y];      // right
            neighbors[7] = this[x + 1, y + 1];  // right-bottom
            return neighbors;
        } // CollectNeighbors(int,int)

        private GameCell GetCell(int x, int y)
        {
            // auto-normalizes coordinates and returns cell
            return field[
                (FIELD_WIDTH + x) % FIELD_WIDTH,
                (FIELD_HEIGHT + y) % FIELD_HEIGHT
                ];
        } // GetCell(int,int)

        #endregion

    }
}
