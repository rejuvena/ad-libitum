using AdLibitum.Configuration.Server;
using AdLibitum.Utilities.Extensions;
using Terraria;
using Terraria.ModLoader;

namespace AdLibitum.Content.Tweaks.BossBagSellPriceAveraging
{
    public class BossBagAvgGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public int DefaultValue;

        public override void SetDefaults(Item item) {
            DefaultValue = item.value;
            AssignBossBagValueIfIAmABossBag(item);
        }

        public override void UpdateInventory(Item item, Player player) {
            if (Main.GameUpdateCount % 120 == 0) // Only do this check every two seconds because eh I need picoseconds.
                AssignBossBagValueIfIAmABossBag(item);
        }

        private void AssignBossBagValueIfIAmABossBag(Item item) {
            if (StandardServerConfig.Config.Tweaks.BossBagSellPriceAveraging)
            {
                if (BossBagAvgSystem.BossBagValueCalculator == null) // We're probably in the middle of loading
                    return;

                if (BossBagAvgSystem.BossBagValueCalculator.AveragedValues.TryGetValue(item.type, out int value))
                    item.value = value;
            }
            else if (item.IsBossBag()) // Reset to default if the config option is disabled
                item.value = DefaultValue;
        }
    }
}
