using DungeonFloorLib;
using System.Windows.Controls;

namespace DungeonMaster
{
    /// <summary>
    /// Interaction logic for RoomTileEditPanelUC.xaml
    /// </summary>
    public partial class RoomTileEditPanelUC : UserControl
    {
        DungeonRoom droom;


        public RoomTileEditPanelUC(DungeonRoom dungeonRoom)
        {
            InitializeComponent();
            this.droom = dungeonRoom;
        }
    }
}
