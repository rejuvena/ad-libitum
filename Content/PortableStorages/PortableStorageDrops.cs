using AdLibitum.Content.PortableStorages.Items;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
