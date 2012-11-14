/* GameCell.cs - provides GameCell object for use in GameGrid
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
    public class GameCell
    {
        #region Private Fields
        private const string ERR_CANT_CHANGE_NEIGHBORS = "Cell neighbors cannot be changed!";
        private readonly Func<int,bool> SURVIVES = (n => (n == 2 || n == 3));
        private readonly Func<int,bool> SPAWNS   = (n => (n == 3));

        private bool aliveNow;
        private bool aliveNext;
        private GameCell[] neighbors;
        #endregion

        #region Constructors

        public GameCell()
        {
            neighbors = null;
        } // ctor GameCell()

        #endregion

        #region Properties

        public bool Alive
        {
            get { return aliveNow; }
            set { aliveNow = value; }
        } // prop Alive

        #endregion

        #region Public Methods

        public void SetNeighbors(GameCell[] newNeighbors)
        {
            // can only set neighbors once
            // just because they must be bound late doesn't mean they're mutable!
            if (neighbors == null)
                neighbors = newNeighbors;
            else
                throw new InvalidOperationException(ERR_CANT_CHANGE_NEIGHBORS);
        } // SetNeighbors(GameCell[])

        public void Evaluate()
        {
            // number of live neighbors
            int neighborCount = neighbors.Where(x => x.Alive).Count();

            aliveNext = (aliveNow)
                ? SURVIVES(neighborCount)
                : SPAWNS(neighborCount);
        } // Evaluate()

        public void Update()
        {
            // such a deceptively simple yet crucial operation...
            aliveNow = aliveNext;
        } // Update()

        #endregion
    }
}
