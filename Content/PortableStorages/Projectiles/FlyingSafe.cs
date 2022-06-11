using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AdLibitum.Content.PortableStorages.Projectiles
{
    [UsedImplicitly]
    public class FlyingSafe : ModProjectile
    {
        public override void SetStaticDefaults() {
            Main.projFrames[Type] = 5;
        }

        public override void SetDefaults() {
            Projectile.width = 42;
            Projectile.height = 42;
            Projectile.aiStyle = 97;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 10800;
        }
    }
}
