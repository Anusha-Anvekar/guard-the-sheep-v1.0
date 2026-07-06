using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GuardTheSheep.Application.Services;
using GuardTheSheep.Domain.Entities;
using GuardTheSheep.Domain.Enum;
using GuardTheSheep.Domain.Helper;

namespace GuardTheSheep.WPF;

public partial class MainWindow : Window
{
    private readonly GameEngine _engine = new();
    private const int Rows = 7, Cols = 7;
    private Brush? _hoverOriginal;

    public MainWindow()
    {
        InitializeComponent();
        StartNewGame();
    }

    private void NewGame_Click(object sender, RoutedEventArgs e) => StartNewGame();

    private void StartNewGame()
    {
        _engine.StartNewGame(Rows, Cols);
        DrawBoard();
    }

    private void DrawBoard()
    {
        BoardGrid.Children.Clear();
        BoardGrid.Rows = Rows;
        BoardGrid.Columns = Cols;

        var game = _engine.CurrentGame;
        var sheepPosition = game.Sheep.CurrentPosition;

        for (int x = 0; x < Rows; x++)
        {
            for (int y = 0; y < Cols; y++)
            {
                var position = new GridPosition(x, y);
                var patch = game.Meadow.GetPatch(position);

                var cell = new Border
                {
                    BorderBrush = new SolidColorBrush(Color.FromArgb(40, 0, 0, 0)),
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(6),
                    Margin = new Thickness(2),
                    Background = patch != null && patch.Type == MeadowPatchType.Escape
                        ? new SolidColorBrush(Color.FromRgb(0x4E, 0x8C, 0x37))
                        : ((x + y) % 2 == 0
                            ? new SolidColorBrush(Color.FromRgb(0x6A, 0xB0, 0x4A))
                            : new SolidColorBrush(Color.FromRgb(0x5C, 0x9E, 0x3F)))
                };

                if(patch!=null && patch.IsBlocked())
                {
                    cell.Background = Brushes.SaddleBrown;
                    cell.Child = new TextBlock
                    {
                        Background = Brushes.Transparent,
                        Text = "🚧",
                        FontSize = 32,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                    };
                }  

                // Only empty patches are interactive
                if (patch != null && patch.Type != MeadowPatchType.Blocked && !position.IsGridPositionEqual(sheepPosition))
                {
                    cell.Tag = position;
                    cell.MouseLeftButtonDown += Cell_Click;
                    cell.MouseEnter += Cell_Hover;
                    cell.MouseLeave += Cell_Unhover;
                }
                else
                {
                    // Blocked or sheep patch
                    cell.MouseLeftButtonDown += Cell_InvalidClick;
                }

                if (x == sheepPosition.X && y == sheepPosition.Y)
                {
                    cell.Child = new TextBlock
                    {
                        Background = Brushes.Transparent,
                        Text = "🐑",
                        FontSize = 30,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Effect = new System.Windows.Media.Effects.DropShadowEffect
                        {
                            Color = Colors.Black,
                            BlurRadius = 6,
                            ShadowDepth = 1,
                            Opacity = 0.4
                        }
                    };
                }
                BoardGrid.Children.Add(cell);                
            }
        }
    }

    private void Cell_Click(object sender, MouseButtonEventArgs e)
    {        
        if(_engine.GetGameState()!=GameState.InProgress)
        {
            MessageBox.Show("Game Over. Click on New Game.", "Game Over",
                MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }        
        var position = (GridPosition)((Border)sender).Tag;
        _engine.PlaceBlockade(position);

        DrawBoard();
        if (_engine.GetGameState() == GameState.Won)
        {
            MessageBox.Show("You trapped the sheep. You win!", "Game Over",
                MessageBoxButton.OK, MessageBoxImage.Information);            
        }
        else if (_engine.GetGameState() == GameState.Lost)
        {
            MessageBox.Show("The sheep escaped. You lose!", "Game Over",
                MessageBoxButton.OK, MessageBoxImage.Exclamation);            
        }
        
    }

    private void Cell_Hover(object sender, MouseEventArgs e)
    {
        var cell = (Border)sender;
        _hoverOriginal = cell.Background;
        cell.Background = new SolidColorBrush(Color.FromRgb(0x8B, 0xC3, 0x4A));
    }

    private void Cell_Unhover(object sender, MouseEventArgs e)
    {
        var cell = (Border)sender;
        if (_hoverOriginal != null)
            cell.Background = _hoverOriginal;
    }

    private async void Cell_InvalidClick(object sender, MouseButtonEventArgs e)
    {
        var cell = (Border)sender;
        var original = cell.Background;
        cell.Background = Brushes.DarkRed;
        await Task.Delay(200);
        cell.Background = original;
    }
}