using Terraria.ModLoader.Config;

namespace AdLibitum.Common.Configuration
{
    public abstract class AbstractClientConfig<T> : AbstractModConfig<T>
        where T : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
    }
}