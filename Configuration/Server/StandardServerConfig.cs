using AdLibitum.Configuration;
using Terraria.ModLoader.Config;

namespace AdLibitum.Configuration.Server
{
    [LibitumLabel("Config.Titles.StandardServerName")]
    public sealed class StandardServerConfig : AbstractServerConfig<StandardServerConfig>
    {
        [Header("Config.Headers.BuffConfiguration")]
        [LibitumLabel("Config.Items.MaxBuffSlots.Name")]
        [LibitumTooltip("Config.Items.MaxBuffSlots.Tooltip")]
        [Slider]
        [Range(0, 100)]
        public uint MaxBuffSlots { get; set; } = 22;
    }
}