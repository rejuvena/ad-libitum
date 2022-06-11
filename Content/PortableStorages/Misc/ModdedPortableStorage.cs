using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;

namespace AdLibitum.Content.PortableStorages.Misc
{
    public class ModdedPortableStorage
    {
        public int ChestId;
        public Func<Player, TrackedProjectileReference> GetTrackedProjRef;

        public ModdedPortableStorage(int portableStorageId, Func<Player, TrackedProjectileReference> getTrackedProjRef) {
            ChestId = portableStorageId;
            GetTrackedProjRef = getTrackedProjRef;
        }
    }
}
