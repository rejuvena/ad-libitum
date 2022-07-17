using AdLibitum.Content.Tweaks.BossBagSellPriceAveraging.Patches;
using System.Linq;
using TeaFramework.API.Features.Patching;
using Terraria.ModLoader;

namespace AdLibitum.Content.Tweaks.BossBagSellPriceAveraging
{
    public class BossBagAvgSystem : ModSystem
    {
        public const int Simulations = 100;

        public static bool BossBagSimulation = false;
        public static BossBagValueCalculator BossBagValueCalculator;

        public override void PostSetupContent() {
            while (true)
            {
                if (Mod.GetContent<IPatch>().FirstOrDefault(x => x.GetType() == typeof(BossBagAvgRedirectQuickSpawnItemDetour)) is not null)
                    break;
            }

            BossBagValueCalculator = new();
            BossBagSimulation = true;
            BossBagValueCalculator.CalculateAll();
            BossBagSimulation = false;
        }
    }
}
