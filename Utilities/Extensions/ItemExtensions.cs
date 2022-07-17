using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace AdLibitum.Utilities.Extensions
{
    public static class ItemExtensions
    {
        public static HashSet<int> VanillaBossBags = new() {
            ItemID.KingSlimeBossBag, ItemID.EyeOfCthulhuBossBag, ItemID.DeerclopsBossBag, ItemID.EaterOfWorldsBossBag, ItemID.BrainOfCthulhuBossBag,
            ItemID.QueenBeeBossBag, ItemID.SkeletronBossBag, ItemID.WallOfFleshBossBag,

            ItemID.QueenSlimeBossBag, ItemID.DestroyerBossBag, ItemID.TwinsBossBag, ItemID.SkeletronPrimeBossBag,
            ItemID.PlanteraBossBag, ItemID.GolemBossBag, ItemID.FairyQueenBossBag, ItemID.FishronBossBag,
            ItemID.MoonLordBossBag, ItemID.BossBagBetsy,

            ItemID.CultistBossBag, ItemID.BossBagDarkMage, ItemID.BossBagOgre
        };

        public static bool IsVanilla(this Item item) => item.type < ItemID.Count;

        public static bool IsBossBag(this Item item) => VanillaBossBags.Contains(item.type) || (item.ModItem is not null && item.ModItem.BossBagNPC == 0);
    }
}
