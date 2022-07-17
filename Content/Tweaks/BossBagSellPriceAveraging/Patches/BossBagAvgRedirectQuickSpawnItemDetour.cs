using System;
using System.Reflection;
using TeaFramework.Features.Patching;
using TeaFramework.Utilities;
using Terraria;
using Terraria.DataStructures;

namespace AdLibitum.Content.Tweaks.BossBagSellPriceAveraging.Patches
{
    public class BossBagAvgRedirectQuickSpawnItemDetour : Patch<BossBagAvgRedirectQuickSpawnItemDetour.QuickSpawnItem_IEntitySource_Int_Int>
    {
        public delegate int QuickSpawnItem_IEntitySource_Int_Int(Orig orig, Player self, IEntitySource source, int item, int stack = 1);
        public delegate int Orig(Player self, IEntitySource source, int item, int stack = 1);

        public override MethodBase ModifiedMethod => typeof(Player).GetCachedMethod("QuickSpawnItem", new Type[] { typeof(IEntitySource), typeof(int), typeof(int) });

        protected override QuickSpawnItem_IEntitySource_Int_Int PatchMethod => (orig, self, source, item, stack) => {
            if (BossBagAvgSystem.BossBagSimulation)
                return BossBagAvgSystem.BossBagValueCalculator.HandleQuickSpawnItem(item, stack);
            else
                return orig(self, source, item, stack);
        };
    }
}
