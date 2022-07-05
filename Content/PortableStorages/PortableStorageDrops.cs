using AdLibitum.Configuration.Server;
using AdLibitum.Content.PortableStorages.Items;
using JetBrains.Annotations;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AdLibitum.Content.PortableStorages
{
    [UsedImplicitly]
    public class PortableStorageDrops : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) {
            if (!StandardServerConfig.Config.ItemToggles.PortableStorages)
                return;

            if (NPC.downedBoss3)
            {
                int grotesqueStatuette = ModContent.ItemType<GrotesqueStatuette>();

                if (npc.type is NPCID.Demon or NPCID.VoodooDemon)
                    npcLoot.Add(ItemDropRule.NormalvsExpert(grotesqueStatuette, 80, 40));

                if (npc.type == NPCID.RedDevil)
                    npcLoot.Add(ItemDropRule.NormalvsExpert(grotesqueStatuette, 10, 5));
            }
        }
    }
}
