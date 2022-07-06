using AdLibitum.Configuration.Server;
using Terraria;

namespace AdLibitum.Content.PortableStorages.Items
{
    public abstract class AbstractPortableStorageItem : AdLibItem
    {
        public override bool IsEnabled => StandardServerConfig.Config.ItemToggles.PortableStorages;
        public override string ConfigOption => "Config.Option.PortableStorages.Label";

        public override bool CanUseItem(Player player) {
            return IsEnabled;
        }
    }
}
