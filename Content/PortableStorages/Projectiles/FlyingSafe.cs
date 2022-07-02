using JetBrains.Annotations;
using Terraria;

namespace AdLibitum.Content.PortableStorages.Projectiles
{
    [UsedImplicitly]
    public class FlyingSafe : AbstractPortableStorageProjectile
    {
        public override void SetStaticDefaults() {
            Main.projFrames[Type] = 5;
        }

        public override void SetDefaults() {
            Projectile.width = 42;
            Projectile.height = 42;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 10800;
        }

        public override void AI() {
            Projectile.velocity = Microsoft.Xna.Framework.Vector2.Zero;
        }
    }
}
