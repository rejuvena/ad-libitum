using AdLibitum.Utilities;
using Terraria.ModLoader.Config;

namespace AdLibitum.Configuration
{
    public abstract class AbstractServerConfig<T> : AbstractModConfig<T>
        where T : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message)
        {
            if (NetworkUtilities.IsLocalHost(whoAmI)) return true;
            message = AdLibitum.GetTextValue("Config.InsufficientPermissions");
            return false;
        }
    }
}