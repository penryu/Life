/* MainWindow.xaml.cs - provides GUI for Conway's Game of Life
 * Author:      Tim Hammerquist
 * Course:      CIT 134
 * Project:     Game of Life
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;      // for Dispatcher

using Life.Patterns;                 // for Life Pattern classes

namespace Life
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Fields

        private const int GRID_WIDTH = 40;
        private const int GRID_HEIGHT = 30;

        private const int CELL_WIDTH = 10;
        private const int CELL_HEIGHT = 10;

        private const int DEFAULT_CHANCE_OF_LIFE = 20;

        private readonly DispatcherTimer timer;
        private readonly GameGrid gameGrid;
        private readonly Rectangle[,] canvasGrid;

        private List<Pattern> patterns;
        private int generationCounter;
        private int probability;
        private volatile bool loopContinue;

        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            probability = DEFAULT_CHANCE_OF_LIFE;
            probabilityTextbox.Text = probability.ToString();

            loopContinue = false;
            timer = new DispatcherTimer();
            timer.Tick += (obj, e) => { if (loopContinue) NextGeneration(); };
            SetTimerInterval();
            timer.Start();

            this.gameGrid = new GameGrid(GRID_WIDTH, GRID_HEIGHT);

            this.canvasGrid = new Rectangle[GRID_WIDTH, GRID_HEIGHT];

            InitializePatterns();

            DrawGrid();
            InitializeGrid();
        } // ctor MainWindow

        #endregion

        #region Properties

        private int Probability
        {
            // initially used for control binding
            get { return probability; }
            set { probability = value; }
        } // prop Probability

        #endregion

        #region Private Methods

        private void InitializePatterns()
        {
            // populate pattern list
            patterns = new List<Pattern>
            {
                new Blinker(gameGrid),
                new Glider(gameGrid),
                new SmallExploder(gameGrid),
                new Exploder(gameGrid),
                new Acorn(gameGrid),
                new BHepto(gameGrid),
                new PiHepto(gameGrid),
                new RPento(gameGrid),
                new Rabbits(gameGrid),
                new TenCellRow(gameGrid),
                new Spaceship(gameGrid),
            };

            // populate combobox from pattern list
            foreach (var pattern in patterns)
                initialStateCombo.Items.Add(pattern.Name);
        } // InitializePatterns

        private void DrawGrid()
        {
            // creates GUI cells for each game cell
            // adds them to canvas
            for (int w = 0; w < GRID_WIDTH; ++w)
            {
                for (int h = 0; h < GRID_HEIGHT; ++h)
                {
                    canvasGrid[w, h] = MakeCellRectangle(w, h);
                    lifeCanvas.Children.Add(canvasGrid[w, h]);
                }
            }
        } // DrawGrid()

        private void InitializeGrid()
        {
            // may be called before GUI has been loaded
            bool? cbVal = (randomCheckbox == null)
                ? false
                : randomCheckbox.IsChecked;

            generationCounter = 0;

            ClearGrid();

            // randomize grid if checkbox checked
            if (cbVal.HasValue && (bool)cbVal)
                gameGrid.Randomize(probability);

            // place sprites if selected
            int patternIndex = initialStateCombo.SelectedIndex;
            if (patternIndex > 0)
            {
                int sprite_x = GRID_WIDTH / 2;
                int sprite_y = GRID_HEIGHT / 2;
                patterns[patternIndex - 1].Place(sprite_x, sprite_y);
            }

            // DRAW!
            UpdateGUI();
        } // InitializeGrid()

        private void ClearGrid()
        {
            gameGrid.Clear();
        } // ClearGrid()

        private void UpdateGUI()
        {
            UpdateGenerationCount();
            UpdateGrid();
        } // UpdateGUI()

        private void UpdateGrid()
        {
            // color each cell based on its state
            foreach (int w in Enumerable.Range(0, GRID_WIDTH))
                foreach (int h in Enumerable.Range(0, GRID_HEIGHT))
                    canvasGrid[w, h].Fill = gameGrid[w, h].Alive
                        ? Brushes.Black
                        : Brushes.White;
        } // UpdateGrid()

        private void UpdateGenerationCount()
        {
            generationLabel.Content = generationCounter.ToString();
        } // UpdateGenerationCount()

        private void SetTimerInterval()
        {
            double timerInterval;

            if (timer != null)
            {
                // not the most elegant algorithm...
                timerInterval = speedSlider.Maximum - speedSlider.Value;
                timer.Interval = TimeSpan.FromMilliseconds(timerInterval);
            }
        } // SetTimerInterval();

        private void NextGeneration()
        {
            gameGrid.Evolve();
            generationCounter++;
            UpdateGenerationCount();
            UpdateGUI();
        } // NextGeneration()

        private Rectangle MakeCellRectangle(int width, int height)
        {
            var rect = new Rectangle
            {
                Width = CELL_WIDTH - 2,
                Height = CELL_HEIGHT - 2,
                Fill = Brushes.White,
            };
            Canvas.SetLeft(rect, width * CELL_WIDTH + 1);
            Canvas.SetTop(rect, height * CELL_HEIGHT + 1);
            return rect;
        } // MakeCellRectangle(int,int)

        private int? ValidateProbability(string input)
        {
            int n;

            if (!Int32.TryParse(input, out n))
                return null;

            if (n < 0) n = 0;
            if (n > 100) n = 100;

            return (int?)n;
        } // ValidateProbability(string)

        #endregion

        #region Event Handlers

        private void stepButton_Click(object sender, RoutedEventArgs e)
        {
            NextGeneration();
        } // evt stepButton_Click

        private void initButton_Click(object sender, RoutedEventArgs e)
        {
            InitializeGrid();
        } // evt initButton_Click

        private void initialStateCombo_DropDownClosed(object sender, EventArgs e)
        {
            InitializeGrid();
        } // evt initialStateCombo

        private void probabilityTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = sender as TextBox;
            int? n;

            n = ValidateProbability(tb.Text);

            if (n == null)
            {
                tb.Background = Brushes.LightPink;
                tb.Focus();
                tb.SelectAll();
                return;
            }

            tb.Text = n.ToString();
            tb.Background = Brushes.White;
            probability = (int)n;
        } // evt probabilityTextbox_TextChanged

        private void autoButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            loopContinue = !loopContinue;
            btn.Content = loopContinue ? "Stop" : "Start";
            SetTimerInterval();
        } // evt autoButton_Click

        private void speedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetTimerInterval();
        } // evt probabilityTextbox_TextChanged

        #endregion
    }
}
