using JetBrains.Annotations;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.ModLoader;

namespace AdLibitum.Content.PortableStorages
{
    [UsedImplicitly]
    public class PortableStoragePlayer : ModPlayer
    {
        public Ref<TrackedProjectileReference> SafeTracker;
        public Ref<TrackedProjectileReference> DefendersForgeTracker;

        public override void Initialize() {
            SafeTracker = new();
            DefendersForgeTracker = new();

            // Just to be safe because vanilla does the same thing
            SafeTracker.Value.Clear();
            DefendersForgeTracker.Value.Clear();
        }

        public override void PreUpdate() {
            NPC.downedMechBossAny = true;

            if (Main.gameMenu || Main.dayTime)
                return;

            NPC.NewNPC(Player.GetSource_Accessory(new Item()), (int)Player.position.X, (int)Player.position.Y, Terraria.ID.NPCID.DD2EterniaCrystal);
            DD2Event.DropMedals(20);
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
