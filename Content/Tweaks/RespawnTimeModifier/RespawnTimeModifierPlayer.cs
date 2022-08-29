using AdLibitum.Configuration.Server;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;

namespace AdLibitum.Content.Tweaks.RespawnTimeModifier
{
    public class RespawnTimeModifierPlayer : ModPlayer
    {
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource) {
            if (AmICloseToRespawnTimeAffectingBoss())
                return;

            float respawnTimeMod = StandardServerConfig.Config.Tweaks.RespawnTimeModifier;

            // 0 / 1 == 1
            if (respawnTimeMod == 0)
                Player.respawnTimer = 0;

            int targetRespawnTime = (int) (Player.respawnTimer * (respawnTimeMod));

            Player.respawnTimer = targetRespawnTime;
        }

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
