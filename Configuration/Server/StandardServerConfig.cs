using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace AdLibitum.Configuration.Server
{
    [LibitumLabel("Config.Title.StandardServerName")]
    public sealed class StandardServerConfig : AbstractServerConfig<StandardServerConfig>
    {
        [LibitumLabel("Config.Page.Tweaks")]
        [SeparatePage]
        public TweaksConf Tweaks = new();
        public class TweaksConf
        {
            //[Header("Config.Headers.BuffConfiguration")]
            [LibitumLabel("Config.Option.MaxBuffSlotsEnabled.Label")]
            [LibitumTooltip("Config.Option.MaxBuffSlotsEnabled.Tooltip")]
            [DefaultValue(true)]
            public bool MaxBuffSlotsEnabled { get; set; } = true;

            [LibitumLabel("Config.Option.MaxBuffSlots.Label")]
            [LibitumTooltip("Config.Option.MaxBuffSlots.Tooltip")]
            [Slider]
            [Range(0, 100)]
            [DefaultValue(22)]
            public uint MaxBuffSlots { get; set; } = 22;

            [LibitumLabel("Config.Option.BossBagSellPriceAveraging.Label")]
            [LibitumTooltip("Config.Option.BossBagSellPriceAveraging.Tooltip")]
            [DefaultValue(true)]
            public bool BossBagSellPriceAveraging = true;
        }

        [LibitumLabel("Config.Page.ItemToggles")]
        [SeparatePage]
        public ItemTogglesConf ItemToggles = new();
        public class ItemTogglesConf
        {
            [LibitumLabel("Config.Option.PortableStorages.Label")]
            [LibitumTooltip("Config.Option.PortableStorages.Tooltip")]
            [DefaultValue(true)]
            public bool PortableStorages = true;
        }
    }
}