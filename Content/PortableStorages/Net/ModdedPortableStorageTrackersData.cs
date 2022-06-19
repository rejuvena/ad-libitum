using AdLibitum.Content.PortableStorages.Misc;
using JetBrains.Annotations;
using System.IO;
using System.Linq;
using TeaFramework.API.Features.Packets;
using Terraria;
using Terraria.DataStructures;

namespace AdLibitum.Content.PortableStorages.Net
{
    [UsedImplicitly]
    public struct ModdedPortableStorageTrackersData : IPacketData
    {
        public short OwnerWhoAmI;
        public TrackedProjectileReference[] PortableStoragesData;

        public ModdedPortableStorageTrackersData(Player player, bool fillPortableStorageProjRefs = true) {
            OwnerWhoAmI = (short) player.whoAmI;

            int count = PortableStorageSystem.ModdedPortableStorages.Count;
            PortableStoragesData = new TrackedProjectileReference[count];

            if (!fillPortableStorageProjRefs)
                return;

            for (int i = 0; i < count; i++)
            {
                ModdedPortableStorage mps = PortableStorageSystem.ModdedPortableStorages[i];

                PortableStoragesData[i] = mps.GetTrackedProjRef(player).Value;
            }
        }

        public void SerializePacket(BinaryWriter writer) {
            writer.Write(OwnerWhoAmI);

            for (int i = 0; i < PortableStoragesData.Length; i++)
                PortableStoragesData[i].Write(writer);
        }

        public static ModdedPortableStorageTrackersData DeserializePacket(BinaryReader reader) {
            ModdedPortableStorageTrackersData data = new(Main.player[reader.ReadInt16()], false);

            // The ModdedPortableStorages amount and order should always be shared between all clients.
            for (int i = 0; i < PortableStorageSystem.ModdedPortableStorages.Count; i++)
                data.PortableStoragesData[i].TryReading(reader);

            return data;
        }
    }
}
