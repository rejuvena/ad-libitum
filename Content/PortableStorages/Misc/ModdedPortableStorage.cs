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
        public int CursorItem;
        
        public SoundStyle OpenSound;
        public SoundStyle CloseSound;
        
        public Func<Player, Ref<TrackedProjectileReference>> GetTrackedProjRef;

        public ModdedPortableStorage(int portableStorageId, int projId, int cursorItem, SoundStyle openSound, SoundStyle? closeSound, Func<Player, Ref<TrackedProjectileReference>> getTrackedProjRef) {
            ChestId = portableStorageId;
            ProjId = projId;
            CursorItem = cursorItem;

            OpenSound = openSound;
            CloseSound = closeSound ?? openSound;
            
            GetTrackedProjRef = getTrackedProjRef;
        }
    }
}
