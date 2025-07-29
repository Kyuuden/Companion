using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using System.Windows.Forms;

namespace FF.Rando.Companion.Settings;
public partial class SettingsDialog : FormBase
{
    private ISettings? _settings;

    public SettingsDialog()
    {
        InitializeComponent();
    }

    public SettingsDialog(IInputApi inputApi)
    {
        InitializeComponent();
        InputApi = inputApi;
    }

    protected override string WindowTitleStatic => "Settings";

    public override bool BlocksInputWhenFocused => false;

    public SettingsDialog(ISettings settings, IInputApi inputApi) :this(inputApi)
    {
        propertyGrid1.SelectedObject = _settings = settings;
        propertyGrid1.LargeButtons = true;

        foreach (var game in settings.GameSettings)
        {
            var gameTab = new TabPage();
            gameTab.Name = game.Value.Name;
            gameTab.Text = game.Value.DisplayName;

            var gamePropertyGrid = new PropertyGrid
            {
                SelectedObject = game.Value,
                Dock = DockStyle.Fill,
                PropertySort = PropertySort.Alphabetical,
                ToolbarVisible = false
            };

            gamePropertyGrid.ExpandAllGridItems();

            gameTab.Controls.Add(gamePropertyGrid);

            tabControl1.TabPages.Add(gameTab);
        }
    }

    public IInputApi InputApi { get; }

    private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
    {
        _settings?.SaveToFile();
    }
}
