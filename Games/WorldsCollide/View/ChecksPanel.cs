using FF.Rando.Companion.Games.WorldsCollide.Enums;
using FF.Rando.Companion.Games.WorldsCollide.Settings;
using FF.Rando.Companion.View;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FF.Rando.Companion.Games.WorldsCollide.View;

public partial class ChecksPanel : FlowPanelEx<CheckSettings>
{
    public ChecksPanel() : base()
    {
        SpacingMode = SpacingMode.Columns;
    }

    public override DockStyle DefaultDockStyle => DockStyle.Top;

    public override void InitializeDataSources(Seed game, CheckSettings settings)
    {
        game.Settings.PropertyChanged += Settings_PropertyChanged;
        base.InitializeDataSources(game, settings);
    }

    protected override Control[] GenerateControls(Seed seed)
        => (Game?.Checks ?? []).Select(ch => new CheckControl(seed, Settings!, ch)).ToArray();


    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
        {
            if (Game != null) Game.Settings.PropertyChanged -= Settings_PropertyChanged;
        }
    }

    protected override void SortControls(ControlCollection controlCollection, int columns)
    {
        foreach (var control in controlCollection.OfType<CheckControl>())
        {
            controlCollection.SetChildIndex(control, _characterBasedSortOrder.IndexOf(control.Value.Event));
        }
    }

    private readonly List<Events> _characterBasedSortOrder =
        [
            Enums.Events.DEFEATED_WHELK,
            Enums.Events.RODE_RAFT_LETE_RIVER,
            Enums.Events.BLOCK_SEALED_GATE,
            Enums.Events.GOT_ZOZO_REWARD,
            Enums.Events.RECRUITED_TERRA_MOBLIZ,
            Enums.Events.DEFEATED_TUNNEL_ARMOR,
            Enums.Events.GOT_RAGNAROK,
            Enums.Events.GOT_BOTH_REWARDS_WEAPON_SHOP,
            Enums.Events.RECRUITED_LOCKE_PHOENIX_CAVE,
            Enums.Events.NAMED_EDGAR,
            Enums.Events.DEFEATED_TENTACLES_FIGARO,
            Enums.Events.GOT_RAIDEN,
            Enums.Events.DEFEATED_VARGAS,
            Enums.Events.FINISHED_COLLAPSING_HOUSE,
            Enums.Events.NAMED_GAU,
            Enums.Events.FINISHED_IMPERIAL_CAMP,
            Enums.Events.GOT_PHANTOM_TRAIN_REWARD,
            Enums.Events.RECRUITED_SHADOW_GAU_FATHER_HOUSE,
            Enums.Events.RECRUITED_SHADOW_FLOATING_CONTINENT,
            Enums.Events.DEFEATED_ATMAWEAPON,
            Enums.Events.FINISHED_FLOATING_CONTINENT,
            Enums.Events.DEFEATED_SR_BEHEMOTH,
            Enums.Events.FINISHED_DOMA_WOB,
            Enums.Events.DEFEATED_STOOGES,
            Enums.Events.FINISHED_DOMA_WOR,
            Enums.Events.GOT_ALEXANDR,
            Enums.Events.FINISHED_MT_ZOZO,
            Enums.Events.VELDT_REWARD_OBTAINED,
            Enums.Events.GOT_SERPENT_TRENCH_REWARD,
            Enums.Events.FREED_CELES,
            Enums.Events.GOT_IFRIT_SHIVA,
            Enums.Events.DEFEATED_NUMBER_024,
            Enums.Events.DEFEATED_CRANES,
            Enums.Events.FINISHED_OPERA_DISRUPTION,
            Enums.Events.RECRUITED_SHADOW_KOHLINGEN,
            Enums.Events.DEFEATED_DULLAHAN,
            Enums.Events.CHASING_LONE_WOLF7,
            Enums.Events.GOT_BOTH_REWARDS_LONE_WOLF,
            Enums.Events.COMPLETED_MOOGLE_DEFENSE,
            Enums.Events.DEFEATED_FLAME_EATER,
            Enums.Events.DEFEATED_HIDON,
            Enums.Events.DEFEATED_MAGIMASTER,
            Enums.Events.RECRUITED_STRAGO_FANATICS_TOWER,
            Enums.Events.DEFEATED_ULTROS_ESPER_MOUNTAIN,
            Enums.Events.DEFEATED_CHADARNOOK,
            Enums.Events.RECRUITED_GOGO_WOR,
            Enums.Events.RECRUITED_UMARO_WOR,
            Enums.Events.FINISHED_NARSHE_BATTLE,
            Enums.Events.BOUGHT_ESPER_TZEN,
            Enums.Events.DEFEATED_DOOM_GAZE,
            Enums.Events.GOT_TRITOCH,
            Enums.Events.AUCTION_BOUGHT_ESPER1,
            Enums.Events.AUCTION_BOUGHT_ESPER2,
            Enums.Events.DEFEATED_ATMA,
        ];
}