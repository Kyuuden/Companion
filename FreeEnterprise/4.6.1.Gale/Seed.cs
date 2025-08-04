using FF.Rando.Companion.FreeEnterprise.RomData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.Rando.Companion.FreeEnterprise._4._6._1.Gale;
internal class Seed : SeedBase
{
    public override IEnumerable<ICharacter> Party => throw new NotImplementedException();

    public override IEnumerable<IKeyItem> KeyItems => throw new NotImplementedException();

    public override IEnumerable<IBoss> Bosses => throw new NotImplementedException();

    public override IEnumerable<IObjectiveGroup> Objectives { get => throw new NotImplementedException(); }

    public override IEnumerable<ILocation> AvailableLocations => throw new NotImplementedException();

    public override bool RequiresMemoryEvents => true;

    public Seed(string hash, Metadata metadata, Container container)
        : base(hash, metadata, container)
    {
        if (Flags.Binary != null)
        {
            //_flags = new Flags(Flags.Binary);
            XpRate = 1;
        }
    }

    public override void OnNewFrame()
    {
        base.OnNewFrame();

        if (Game.Emulation.FrameCount() % Game.RootSettings.TrackingInterval == 0)
        {

        }
    }
}
