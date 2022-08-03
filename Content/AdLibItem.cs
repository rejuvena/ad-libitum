using AdLibitum.Utilities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace AdLibitum.Content
{
    public abstract class AdLibItem : ModItem
    {
        public abstract bool IsEnabled { get; }
        public abstract string ConfigOption { get; }

        public override void ModifyTooltips(List<TooltipLine> tooltips) {
            if (IsEnabled)
                return;

            // Make the name gray...
            int name = tooltips.FindIndex(x => x.Mod == "Terraria" && x.Name == "ItemName");
            if (name == -1)
                return;

            tooltips[name].OverrideColor = Colors.RarityTrash;

            // ...and add a notice.
            TooltipLine itemDisabledNotice = new(Mod, "AdLibitum:ItemDisabledNotice", L10nUtils.GetModTextValue("MiscTooltipFragments.ItemDisabledNotice", L10nUtils.GetModTextValue(ConfigOption)));
            itemDisabledNotice.OverrideColor = Color.Red;
            tooltips.Insert(name + 1, itemDisabledNotice);
        }
    }
}
