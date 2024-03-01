using DungeonFloorLib;
using DungeonFloorLib.Enum;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DungeonMaster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DungeonFloor dungeonFloor;
        private RoomTileUC selectedTile;
        private double zoomValue = 2.0;
        private double minZoomValue = 0.9;
        private double maxZoomValue = 4.0;
        private const string windowName = "DungeonMaster";
        private string fileName = "No file";

        public MainWindow()
        {
            InitializeComponent();
            SetVersion();
            InitGrid();
        }

        
        private void SetVersion()
        {
            lbl_Version.Content = $"v{Util.GetAppVersion()}";
        }


        private void InitGrid()
        {
            for (int i = 0; i < 31; i++)
            {
                RowDefinition rowDef = new RowDefinition();
                ColumnDefinition colDef = new ColumnDefinition();
                grid_RoomLayout.RowDefinitions.Add(rowDef);
                grid_RoomLayout.ColumnDefinitions.Add(colDef);
            }

            //Start zoomed in and (badly) centered
            ScaleTransform scale = new ScaleTransform(zoomValue, zoomValue);
            grid_RoomLayout.LayoutTransform = scale;
            scrollviewer_LayoutGrid.ScrollToHorizontalOffset(650);
            scrollviewer_LayoutGrid.ScrollToVerticalOffset(680);
        }


        private void DrawLayout(DungeonFloor dfloor)
        {
            grid_RoomLayout.Children.Clear();

            for(int i = 0; i < dfloor.Rooms.Count; i++)
            {
                RoomTileUC tile = new RoomTileUC(dfloor.Rooms[i]);
                tile.PreviewMouseLeftButtonUp += RoomTileUC_Click;
                grid_RoomLayout.Children.Add(tile);

                Grid.SetRow(tile, 31 - i / 31);
                Grid.SetColumn(tile, (i % 31));
            }
        }


        private void UpdateEditorConnectionButtons()
        {
            if (selectedTile == null) return;

            string basePath = "Resources/Image/Editor/";

            string imageNorth = selectedTile.Room.IsConnectNorth ? "ArrowNorthHighlight" : "ArrowNorth";
            string imageEast = selectedTile.Room.IsConnectEast ? "ArrowEastHighlight" : "ArrowEast";
            string imageWest = selectedTile.Room.IsConnectWest ? "ArrowWestHighlight" : "ArrowWest";
            string imageSouth = selectedTile.Room.IsConnectSouth ? "ArrowSouthHighlight" : "ArrowSouth";


            img_EditorConnectionNorth.Source = new BitmapImage(new Uri($"{basePath}{imageNorth}.png", UriKind.Relative));
            img_EditorConnectionEast.Source = new BitmapImage(new Uri($"{basePath}{imageEast}.png", UriKind.Relative));
            img_EditorConnectionWest.Source = new BitmapImage(new Uri($"{basePath}{imageWest}.png", UriKind.Relative));
            img_EditorConnectionSouth.Source = new BitmapImage(new Uri($"{basePath}{imageSouth}.png", UriKind.Relative));
        }


        private void RoomTileUC_Click(object sender, RoutedEventArgs e)
        {
            if (sender is UIElement clickedElement)
            {
                RoomTileUC roomTile = clickedElement as RoomTileUC;
                if (selectedTile != null)
                {
                    selectedTile.Deselect();
                }
                selectedTile = roomTile;
                selectedTile.Select();

                UpdateEditorConnectionButtons();
            }
        }


        private void SetRoomType(RoomType roomType)
        {
            if (selectedTile != null)
            {
                selectedTile.Room.Type = roomType;
                selectedTile.RefreshDraw();
                UpdateRoomAmountPanel();
            }
        }


        private void UpdateWindowTitle()
        {
            this.Title = $"{windowName} [{fileName}]";
        }


        private void UpdateTileConnection(RoomConnection connection)
        {
            if (selectedTile == null) return;
            selectedTile.ToggleConnection(connection);
            UpdateEditorConnectionButtons();
        }


        private void UpdateRoomAmountPanel()
        {
            string labelBase = "{0}/{1}";

            int randomAmount = dungeonFloor.GetRoomTypeAmount(RoomType.Random);
            int startAmount = dungeonFloor.GetRoomTypeAmount(RoomType.Start);
            int exitAmount = dungeonFloor.GetRoomTypeAmount(RoomType.Exit);
            int encounterAmount = dungeonFloor.GetRoomTypeAmount(RoomType.Encounter);
            int lootAmount = dungeonFloor.GetRoomTypeAmount(RoomType.Loot);

            int randomMax = Settings.GetMaxRoomAmount(RoomType.Random);
            int startMax = Settings.GetMaxRoomAmount(RoomType.Start);
            int exitMax = Settings.GetMaxRoomAmount(RoomType.Exit);
            int encounterMax = Settings.GetMaxRoomAmount(RoomType.Encounter);
            int lootMax = Settings.GetMaxRoomAmount(RoomType.Loot);

            //Check for format limitations
            if (randomAmount > randomMax)
            {
                //Game will crash if there's any encounter or loot rooms once the random maximum has been exceeded.
                encounterMax = 0;
                lootMax = 0;
            }

            lbl_AmountRandom.Content = String.Format(labelBase, randomAmount, randomMax);
            lbl_AmountStart.Content = String.Format(labelBase, startAmount, startMax);
            lbl_AmountExit.Content = String.Format(labelBase, exitAmount, exitMax);
            lbl_AmountEncounter.Content = String.Format(labelBase, encounterAmount, encounterMax);
            lbl_AmountLoot.Content = String.Format(labelBase, lootAmount, lootMax);

            lbl_AmountRandom.Foreground = randomAmount > randomMax ? Brushes.Red : Brushes.Black;
            lbl_AmountStart.Foreground = startAmount > startMax ? Brushes.Red : Brushes.Black;
            lbl_AmountExit.Foreground = exitAmount > exitMax ? Brushes.Red : Brushes.Black;
            lbl_AmountEncounter.Foreground = encounterAmount > encounterMax ? Brushes.Red : Brushes.Black;
            lbl_AmountLoot.Foreground = lootAmount > lootMax ? Brushes.Red : Brushes.Black;
        }


        private void grid_RoomLayout_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (zoomValue >= maxZoomValue)
                {
                    e.Handled = true;
                    return;
                }
                zoomValue += 0.1;
            }
            else
            {
                if (zoomValue <= minZoomValue)
                {
                    e.Handled = true;
                    return;
                }
                zoomValue -= 0.1;
            }

            ScaleTransform scale = new ScaleTransform(zoomValue, zoomValue);
            grid_RoomLayout.LayoutTransform = scale;
            e.Handled = true;
        }


        Point scrollMousePoint = new Point();
        double hOffset = 1;
        double vOffset = 1;
        private void scrollViewer_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            scrollMousePoint = e.GetPosition(scrollviewer_LayoutGrid);
            hOffset = scrollviewer_LayoutGrid.HorizontalOffset;
            vOffset = scrollviewer_LayoutGrid.VerticalOffset;
            scrollviewer_LayoutGrid.CaptureMouse();
        }


        private void scrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (scrollviewer_LayoutGrid.IsMouseCaptured)
            {
                scrollviewer_LayoutGrid.ScrollToHorizontalOffset(hOffset + (scrollMousePoint.X - e.GetPosition(scrollviewer_LayoutGrid).X));
                scrollviewer_LayoutGrid.ScrollToVerticalOffset(vOffset + (scrollMousePoint.Y - e.GetPosition(scrollviewer_LayoutGrid).Y));
            }
        }


        private void scrollViewer_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            scrollviewer_LayoutGrid.ReleaseMouseCapture();
        }


        private void menuitem_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "DGRF file (*.bin)|*.bin|" + "All types (*.*)|*.*";
            saveFileDialog.FileName = this.fileName;
            if (saveFileDialog.ShowDialog() == true)
            {
                DungeonFloorWriter.WriteDGRFToFile(dungeonFloor, saveFileDialog.FileName);
            }
        }


        private void menuitem_Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "DGRF file (*.bin)|*.bin|" + "All types (*.*)|*.*";
            Nullable<bool> result = openFileDialog.ShowDialog();
            if (result == true)
            {
                try
                {
                    // Open file 
                    string filePath = openFileDialog.FileName;
                    dungeonFloor = DungeonFloorReader.ReadDGRF(filePath);
                    this.fileName = Path.GetFileName(openFileDialog.FileName);
                    DrawLayout(dungeonFloor);
                    UpdateRoomAmountPanel();
                    UpdateWindowTitle();
                } catch (Exception ex)
                {
                    MessageBox.Show($"An error has occurred when opening the file.\nThe exception message is:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void menuitem_Controls_Click(object sender, RoutedEventArgs e)
        {
            ControlsWindow window = new ControlsWindow();
            window.Show();
        }


        private void menuitem_GitHub_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer", "https://github.com/Ret-HZ/DungeonMaster");
        }


        private void menuitem_Donate_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer", "https://ko-fi.com/ret_hz");
        }



        /// EDITOR COMMANDS ///

        private void cmd_SetRoomTypeNone(object sender, ExecutedRoutedEventArgs e)
        {
            SetRoomType(RoomType.None);
        }

        private void cmd_SetRoomTypeRandom(object sender, ExecutedRoutedEventArgs e)
        {
            SetRoomType(RoomType.Random);
        }

        private void cmd_SetRoomTypeHallway(object sender, ExecutedRoutedEventArgs e)
        {
            SetRoomType(RoomType.Hallway);
        }

        private void cmd_SetRoomTypeStart(object sender, ExecutedRoutedEventArgs e)
        {
            SetRoomType(RoomType.Start);
        }

        private void cmd_SetRoomTypeExit(object sender, ExecutedRoutedEventArgs e)
        {
            SetRoomType(RoomType.Exit);
        }

        private void cmd_SetRoomTypeEncounter(object sender, ExecutedRoutedEventArgs e)
        {
            SetRoomType(RoomType.Encounter);
        }

        private void cmd_SetRoomTypeLoot(object sender, ExecutedRoutedEventArgs e)
        {
            SetRoomType(RoomType.Loot);
        }

        private void cmd_SetRoomTypeExtension(object sender, ExecutedRoutedEventArgs e)
        {
            SetRoomType(RoomType.Extension);
        }


        private void cmd_SetRoomConnectionNorth(object sender, ExecutedRoutedEventArgs e)
        {
            UpdateTileConnection(RoomConnection.North);
        }

        private void cmd_SetRoomConnectionWest(object sender, ExecutedRoutedEventArgs e)
        {
            UpdateTileConnection(RoomConnection.West);
        }

        private void cmd_SetRoomConnectionEast(object sender, ExecutedRoutedEventArgs e)
        {
            UpdateTileConnection(RoomConnection.East);
        }

        private void cmd_SetRoomConnectionSouth(object sender, ExecutedRoutedEventArgs e)
        {
            UpdateTileConnection(RoomConnection.South);
        }
    }
}