using DungeonFloorLib;
using DungeonFloorLib.Enum;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DungeonMaster
{
    /// <summary>
    /// Interaction logic for RoomTileUC.xaml
    /// </summary>
    public partial class RoomTileUC : UserControl
    {
        public DungeonRoom Room;
        public bool IsSelected { get; private set; }


        public RoomTileUC(DungeonRoom room)
        {
            InitializeComponent();
            Room = room;

            image_RoomConnectionNorth.Source = new BitmapImage(new Uri("Resources/Image/Tiles/RoomConnection/North.png", UriKind.Relative));
            image_RoomConnectionSouth.Source = new BitmapImage(new Uri("Resources/Image/Tiles/RoomConnection/South.png", UriKind.Relative));
            image_RoomConnectionEast.Source = new BitmapImage(new Uri("Resources/Image/Tiles/RoomConnection/East.png", UriKind.Relative));
            image_RoomConnectionWest.Source = new BitmapImage(new Uri("Resources/Image/Tiles/RoomConnection/West.png", UriKind.Relative));

            DrawTile();
        }


        private void DrawTile()
        {
            switch (Room.Type)
            {
                case RoomType.None:
                    image_RoomType.Source = new BitmapImage(new Uri("Resources/Image/Tiles/RoomType/None.png", UriKind.Relative));
                    break;
                case RoomType.Start:
                    image_RoomType.Source = new BitmapImage(new Uri("Resources/Image/Tiles/RoomType/Start.png", UriKind.Relative));
                    break;
                case RoomType.Hallway:
                    image_RoomType.Source = new BitmapImage(new Uri("Resources/Image/Tiles/RoomType/Hallway.png", UriKind.Relative));
                    break;
                case RoomType.Random:
                    image_RoomType.Source = new BitmapImage(new Uri("Resources/Image/Tiles/RoomType/Room.png", UriKind.Relative));
                    break;
                case RoomType.Exit:
                    image_RoomType.Source = new BitmapImage(new Uri("Resources/Image/Tiles/RoomType/Exit.png", UriKind.Relative));
                    break;
                case RoomType.Encounter:
                    image_RoomType.Source = new BitmapImage(new Uri("Resources/Image/Tiles/RoomType/Encounter.png", UriKind.Relative));
                    break;
                case RoomType.Loot:
                    image_RoomType.Source = new BitmapImage(new Uri("Resources/Image/Tiles/RoomType/Loot.png", UriKind.Relative));
                    break;
                case RoomType.Extension:
                    image_RoomType.Source = new BitmapImage(new Uri("Resources/Image/Tiles/RoomType/Extension.png", UriKind.Relative));
                    break;
                default:
                    image_RoomType.Source = new BitmapImage(new Uri("Resources/Image/Tiles/RoomType/Unknown.png", UriKind.Relative));
                    break;
            }

            image_RoomConnectionNorth.Visibility = Room.IsConnectNorth ? Visibility.Visible : Visibility.Hidden;
            image_RoomConnectionSouth.Visibility = Room.IsConnectSouth ? Visibility.Visible : Visibility.Hidden;
            image_RoomConnectionEast.Visibility = Room.IsConnectEast ? Visibility.Visible : Visibility.Hidden;
            image_RoomConnectionWest.Visibility = Room.IsConnectWest ? Visibility.Visible : Visibility.Hidden;
        }


        private void DrawBorder()
        {
            if (IsSelected)
            {
                border_TileBorder.BorderThickness = new Thickness(2f);
                border_TileBorder.BorderBrush = Brushes.Red;
            }
            else
            {
                border_TileBorder.BorderThickness = new Thickness(0f);
            }
        }

        public void Select()
        {
            IsSelected = true;
            DrawBorder();
        }


        public void Deselect()
        {
            IsSelected = false;
            DrawBorder();
        }


        public void RefreshDraw()
        {
            DrawTile();
        }


        public void ToggleConnection(RoomConnection roomConnection)
        {
            Room.SetConnection(roomConnection, !Room.GetConnection(roomConnection));
            RefreshDraw();
        }
    }
}
