using System;
using Terraria;
using Terraria.DataStructures;

namespace AdLibitum.Content.PortableStorages.Misc
{
    public class ModdedPortableStorage
    {
        public int ChestId;
        public int ProjId;
        public Func<Player, Ref<TrackedProjectileReference>> GetTrackedProjRef;

        public ModdedPortableStorage(int portableStorageId, int projId, Func<Player, Ref<TrackedProjectileReference>> getTrackedProjRef) {
            ChestId = portableStorageId;
            GetTrackedProjRef = getTrackedProjRef;
            ProjId = projId;
        }
    }
}
