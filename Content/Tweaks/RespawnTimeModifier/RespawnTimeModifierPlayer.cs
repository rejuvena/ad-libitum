using AdLibitum.Configuration.Server;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AdLibitum.Content.Tweaks.RespawnTimeModifier
{
    public class RespawnTimeModifierPlayer : ModPlayer
    {
        public override void UpdateDead() {
            if (AmICloseToRespawnTimeAffectingBoss())
                return;

            int targetRespawnTime = MaxRespawnTime() / StandardServerConfig.Config.Tweaks.RespawnTimeModifier;
            if (Player.respawnTimer > targetRespawnTime)
                Player.respawnTimer = targetRespawnTime;
        }

        private int MaxRespawnTime() => (int) (600f * (Main.expertMode ? 1.5f : 1f));

        private bool AmICloseToRespawnTimeAffectingBoss() {
            // From Player.KillMe
            for (int k = 0; k < 200; k++)
            {
                if (Main.npc[k].active &&
                    (Main.npc[k].boss || Main.npc[k].type == NPCID.EaterofWorldsHead || Main.npc[k].type == NPCID.EaterofWorldsBody || Main.npc[k].type == NPCID.EaterofWorldsTail)
                    && Math.Abs(Player.Center.X - Main.npc[k].Center.X) + Math.Abs(Player.Center.Y - Main.npc[k].Center.Y) < 4000f)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
