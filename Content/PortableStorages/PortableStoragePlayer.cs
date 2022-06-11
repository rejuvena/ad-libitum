using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria.ModLoader;
// TileInteractionsUse

namespace AdLibitum.Content.PortableStorages
{
    [UsedImplicitly]
    public class PortableStoragePlayer : ModPlayer
    {
        public TrackedProjectileReference SafeTracker;
        public TrackedProjectileReference DefendersForgeTracker;

        public override void Initialize() {
            SafeTracker = new();
            DefendersForgeTracker = new();

            // Just to be safe because vanilla does the same thing
            SafeTracker.Clear();
            DefendersForgeTracker.Clear();
        }

        // These shouldn't be needed because I'm hijacking Terraria's netmessage
        /*public override void SendClientChanges(ModPlayer clientPlayer) {
            base.SendClientChanges(clientPlayer);
        }

        public override void clientClone(ModPlayer clientClone) {
            PortableStoragePlayer psp = clientClone as PortableStoragePlayer;
            psp.SafeTracker = SafeTracker;
            psp.DefendersForgeTracker = DefendersForgeTracker;
        }*/
    }
}
