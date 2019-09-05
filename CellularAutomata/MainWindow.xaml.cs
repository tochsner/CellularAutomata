using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CellularAutomata
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int numStates = 8;
        int fieldSize = 256;
        double lambda = 0.1;        

        volatile Key keyPressed = Key.None;

        IRule rule;
        IAutomata automata;

        DrawingVisual visual = null;

        protected override Visual GetVisualChild(int index)
        {
            return visual;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            visual = new DrawingVisual();

            this.AddVisualChild(visual);

            rule = new RandomLangtonRule(numStates, lambda);
            automata = new BasicCellularAutomata(fieldSize, numStates, rule);

            automata.Initialize();

            Draw(automata.GetField());

            Task task = new Task(() =>
            {
                while (true)
                {                                                                        
                    Dispatcher.Invoke(() => {
                        switch (keyPressed)
                        {
                            case Key.Left:
                                RestartOld();
                                break;
                            case Key.Right:
                                RestartWithNew();
                                break;
                            case Key.Space:
                                FastForward();
                                break;
                            default:
                                Draw(automata.GetField());                                
                                break;
                        }

                        keyPressed = Key.None;                       
                    });

                    lock (this)
                    {
                        automata.Iterate();
                    }

                    System.Threading.Thread.Sleep(100);
                }
            });

            task.Start();
        }

        protected void Draw(int[,] field)
        {
            double height = this.Height;
            double width = this.Width;

            double cellHeight = height / fieldSize;
            double cellWidth = width / fieldSize;

            using (DrawingContext ctx = visual.RenderOpen())
            {                
                for (int i = 0; i < fieldSize; i++)
                {
                    for (int j = 0; j < fieldSize; j++)
                    {
                        switch (field[i, j])
                        {
                            case 1:
                                ctx.DrawRectangle(Brushes.White, null, new Rect(Math.Floor(i * cellWidth), Math.Floor(j * cellHeight), Math.Floor(cellWidth), Math.Floor(cellHeight)));
                                break;
                            case 2:
                                ctx.DrawRectangle(Brushes.Red, null, new Rect(Math.Floor(i * cellWidth), Math.Floor(j * cellHeight), Math.Floor(cellWidth), Math.Floor(cellHeight)));
                                break;
                            case 3:
                                ctx.DrawRectangle(Brushes.Blue, null, new Rect(Math.Floor(i * cellWidth), Math.Floor(j * cellHeight), Math.Floor(cellWidth), Math.Floor(cellHeight)));
                                break;
                            case 4:
                                ctx.DrawRectangle(Brushes.Yellow, null, new Rect(Math.Floor(i * cellWidth), Math.Floor(j * cellHeight), Math.Floor(cellWidth), Math.Floor(cellHeight)));
                                break;
                            case 5:
                                ctx.DrawRectangle(Brushes.Green, null, new Rect(Math.Floor(i * cellWidth), Math.Floor(j * cellHeight), Math.Floor(cellWidth), Math.Floor(cellHeight)));
                                break;
                            case 6:
                                ctx.DrawRectangle(Brushes.Pink, null, new Rect(Math.Floor(i * cellWidth), Math.Floor(j * cellHeight), Math.Floor(cellWidth), Math.Floor(cellHeight)));
                                break;
                            case 7:
                                ctx.DrawRectangle(Brushes.Orange, null, new Rect(Math.Floor(i * cellWidth), Math.Floor(j * cellHeight), Math.Floor(cellWidth), Math.Floor(cellHeight)));
                                break;
                            case 8:
                                ctx.DrawRectangle(Brushes.DarkCyan, null, new Rect(Math.Floor(i * cellWidth), Math.Floor(j * cellHeight), Math.Floor(cellWidth), Math.Floor(cellHeight)));
                                break;
                            case 9:
                                ctx.DrawRectangle(Brushes.Turquoise, null, new Rect(Math.Floor(i * cellWidth), Math.Floor(j * cellHeight), Math.Floor(cellWidth), Math.Floor(cellHeight)));
                                break;
                            case 10:
                                ctx.DrawRectangle(Brushes.SaddleBrown, null, new Rect(Math.Floor(i * cellWidth), Math.Floor(j * cellHeight), Math.Floor(cellWidth), Math.Floor(cellHeight)));
                                break;
                            case 11:
                                ctx.DrawRectangle(Brushes.Lime, null, new Rect(Math.Floor(i * cellWidth), Math.Floor(j * cellHeight), Math.Floor(cellWidth), Math.Floor(cellHeight)));
                                break;
                            case 12:
                                ctx.DrawRectangle(Brushes.Violet, null, new Rect(Math.Floor(i * cellWidth), Math.Floor(j * cellHeight), Math.Floor(cellWidth), Math.Floor(cellHeight)));
                                break;
                            case 13:
                                ctx.DrawRectangle(Brushes.Olive, null, new Rect(Math.Floor(i * cellWidth), Math.Floor(j * cellHeight), Math.Floor(cellWidth), Math.Floor(cellHeight)));
                                break;
                            case 14:
                                ctx.DrawRectangle(Brushes.Fuchsia, null, new Rect(Math.Floor(i * cellWidth), Math.Floor(j * cellHeight), Math.Floor(cellWidth), Math.Floor(cellHeight)));
                                break;
                            case 15:
                                ctx.DrawRectangle(Brushes.Silver, null, new Rect(Math.Floor(i * cellWidth), Math.Floor(j * cellHeight), Math.Floor(cellWidth), Math.Floor(cellHeight)));
                                break;
                            default:
                                break;
                        }                      
                    }
                }

            }
        }

        protected void Clear()
        {
            using (DrawingContext ctx = visual.RenderOpen())
            { }
        }

        protected void RestartOld()
        {
            Clear();
            lock (this)
            {               
                automata.Initialize();
            }
        }
        protected void RestartWithNew()
        {
            lock (this)
            {
                Clear();
                rule = new RandomLangtonRule(numStates, lambda);
                automata = new BasicCellularAutomata(fieldSize, numStates, rule);

                automata.Initialize();
            }
        }
        protected void FastForward()
        {            
            lock (this)
            {
                Clear();
                for (int i = 0; i < 100; i++)
                {
                    automata.Iterate();
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            keyPressed = e.Key;
        }
    }    
}
