using AdLibitum.Content.PortableStorages.Misc;
using JetBrains.Annotations;
using System;
using System.IO;
using TeaFramework.API.Features.Packets;
using Terraria;
using Terraria.ModLoader;

namespace AdLibitum.Content.PortableStorages.Net
{
    [UsedImplicitly]
    public class SyncModdedPortableStorageTrackers : IPacketHandler<ModdedPortableStorageTrackersData>
    {
        public void WritePacket(BinaryWriter writer, ModdedPortableStorageTrackersData packetData) {
            packetData.SerializePacket(writer);
        }

        public void ReadPacket(BinaryReader reader, int whoAmI) {
            ModdedPortableStorageTrackersData data = ModdedPortableStorageTrackersData.DeserializePacket(reader);

            Player p = Main.player[data.OwnerWhoAmI];
            for (int i = 0; i < data.PortableStoragesData.Length; i++)
            {
                ModdedPortableStorage mps = PortableStorageSystem.ModdedPortableStorages[i];
                mps.GetTrackedProjRef(p).Value = data.PortableStoragesData[i];
            }
        }

        public void Load(Mod mod) {
        }

        public void Unload() {
        }
    }
}
