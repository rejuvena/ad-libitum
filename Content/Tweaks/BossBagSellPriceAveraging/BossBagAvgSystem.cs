using Terraria.ModLoader;

namespace AdLibitum.Content.Tweaks.BossBagSellPriceAveraging
{
    public class BossBagAvgSystem : ModSystem
    {
        public const int Simulations = 100;

        public static bool BossBagSimulation = false;
        public static BossBagValueCalculator BossBagValueCalculator;

        public override void PostSetupContent() {
            BossBagSimulation = true;
            BossBagValueCalculator = new();
            BossBagValueCalculator.CalculateAll();
            BossBagSimulation = false;
        }
    }
}
