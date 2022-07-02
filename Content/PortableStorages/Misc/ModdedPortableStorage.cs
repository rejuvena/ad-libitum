using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;

namespace AdLibitum.Content.PortableStorages.Misc
{
    public class ModdedPortableStorage
    {
        public int ChestId;
        public int ProjId;
        
        public SoundStyle OpenSound;
        public SoundStyle CloseSound;
        
        public Func<Player, Ref<TrackedProjectileReference>> GetTrackedProjRef;

        public ModdedPortableStorage(int portableStorageId, int projId, SoundStyle openSound, SoundStyle? closeSound, Func<Player, Ref<TrackedProjectileReference>> getTrackedProjRef) {
            ChestId = portableStorageId;
            ProjId = projId;

            OpenSound = openSound;
            CloseSound = closeSound ?? openSound;
            
            GetTrackedProjRef = getTrackedProjRef;
        }
    }
}
