using BizHawk.Client.EmuHawk;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.Settings;
public partial class SettingsDialog : FormBase
{
    private readonly ISettings? _settings;

    public SettingsDialog()
    {
        InitializeComponent();
    }

    protected override string WindowTitleStatic => "Settings";

    public override bool BlocksInputWhenFocused => false;

    public SettingsDialog(ISettings settings) :this()
    {
        propertyGrid1.SelectedObject = _settings = settings;
        propertyGrid1.LargeButtons = true;

        var buttons = propertyGrid1.SelectedGridItem?.Parent?.Parent?.GridItems?.Cast<GridItem>().FirstOrDefault(i => i.Label == "Buttons");

        if (buttons != null)
            buttons.Expanded = true;

        foreach (var game in settings.GameSettings)
        {
            var gameTab = new TabPage
            {
                Name = game.Value.Name,
                Text = game.Value.DisplayName
            };

            var gamePropertyGrid = new PropertyGrid
            {
                SelectedObject = game.Value,
                Dock = DockStyle.Fill,
                PropertySort = PropertySort.Alphabetical,
                ToolbarVisible = false
            };

            gamePropertyGrid.ExpandAllGridItems();
            gamePropertyGrid.PropertyValueChanged += PropertyValueChanged;

            gameTab.Controls.Add(gamePropertyGrid);

            tabControl1.TabPages.Add(gameTab);
        }
    }

    private void PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
    {
        _settings?.SaveToFile();
    }
}
