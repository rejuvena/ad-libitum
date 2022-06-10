using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace AdLibitum.Configuration.Server
{
    [LibitumLabel("Config.Titles.StandardServerName")]
    public sealed class StandardServerConfig : AbstractServerConfig<StandardServerConfig>
    {
        [Header("Config.Headers.BuffConfiguration")]
        [LibitumLabel("Config.ItemName.MaxBuffSlotsEnabled")]
        [LibitumTooltip("Config.ItemTooltip.MaxBuffSlotsEnabled")]
        [DefaultValue(true)]
        public bool MaxBuffSlotsEnabled { get; set; } = true;

        [LibitumLabel("Config.ItemName.MaxBuffSlots")]
        [LibitumTooltip("Config.ItemTooltip.MaxBuffSlots")]
        [Slider]
        [Range(0, 100)]
        [DefaultValue(22)]
        public uint MaxBuffSlots { get; set; } = 22;
    }
}