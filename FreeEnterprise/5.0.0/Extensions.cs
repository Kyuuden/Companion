namespace FF.Rando.Companion.FreeEnterprise._5._0._0;
internal static class Extensions
{
    public static bool IsMiab(this ChestSlot chestSlot)
        => chestSlot switch
        {
            ChestSlot.Feymarch => false,
            ChestSlot.RibbonRoom1 => false,
            ChestSlot.RibbonRoom2 => false,
            _ => true
        };
}
