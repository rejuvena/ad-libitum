using AdLibitum.Content.PortableStorages.Misc;
using AdLibitum.Content.PortableStorages.Net;
using AdLibitum.Content.PortableStorages.Projectiles;
using JetBrains.Annotations;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AdLibitum.Content.PortableStorages
{
    [UsedImplicitly]
    public class PortableStorageSystem : ModSystem
    {
        public static List<ModdedPortableStorage> ModdedPortableStorages;

        public override void OnModLoad() {
            ModdedPortableStorages = new();

            ModdedPortableStorages.Add(new(-3, ModContent.ProjectileType<FlyingSafe>(), (player) => player.GetModPlayer<PortableStoragePlayer>().SafeTracker));
            ModdedPortableStorages.Add(new(-4, 0, (player) => player.GetModPlayer<PortableStoragePlayer>().DefendersForgeTracker));
        }

        public override void Unload() {
            ModdedPortableStorages.Clear();
            ModdedPortableStorages = null;
        }

        public override bool HijackSendData(int whoAmI, int msgType, int remoteClient, int ignoreClient, NetworkText text, int number, float number2, float number3, float number4, int number5, int number6, int number7) {
            if (msgType == MessageID.SyncProjectileTrackers)
            {
                Player player = Main.player[number];

                ModPacket packet = Mod.GetPacket();
                SyncModdedPortableStorageTrackers mPacket = new();
                mPacket.WritePacket(packet, new(player));
                packet.Send();
            }
            
            return false;
        }

        public static void ClearModPortableStorages(Player player) {
            foreach (ModdedPortableStorage mps in ModdedPortableStorages)
                mps.GetTrackedProjRef(player).Value.Clear();
        }
    }
}
