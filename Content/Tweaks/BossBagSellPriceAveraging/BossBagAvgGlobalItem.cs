using AdLibitum.Configuration.Server;
using Terraria;
using Terraria.ID;
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
            if (Main.GameUpdateCount % 30 == 0)
                AssignBossBagValueIfIAmABossBag(item);
        }

        private void AssignBossBagValueIfIAmABossBag(Item item) {
            if (!ItemID.Sets.BossBag[item.type])
                return;

            if (StandardServerConfig.Config.Tweaks.BossBagSellPriceAveraging)
            {
                if (BossBagAvgSystem.BossBagValueCalculator == null) // We're probably in the middle of loading
                    return;

                if (BossBagAvgSystem.BossBagValueCalculator.AveragedValues.TryGetValue(item.type, out int value))
                    item.value = value;
            }
            else // Reset to default if the config option is disabled
                item.value = DefaultValue;
        }
    }
}
