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
            Projectile.height = 50;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 10800;
        }

        public override void AI() {
            int frameTime = 4;

            if (++Projectile.frameCounter % frameTime == 0)
            {
                if (Projectile.frameCounter <= 16)
                    Projectile.frame++;

                if (Projectile.frameCounter == 20)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame = 0;
                }
            }

            base.AI();

            if (Projectile.ai[0] == 0f)
            {
                if ((double) Projectile.velocity.Length() < 0.1)
                {
                    Projectile.velocity.X = 0f;
                    Projectile.velocity.Y = 0f;
                    Projectile.ai[0] = 1f;
                    Projectile.ai[1] = 45f;
                    return;
                }
                Projectile.velocity *= 0.94f;
                if (Projectile.velocity.X < 0f)
                {
                    Projectile.direction = -1;
                }
                else
                {
                    Projectile.direction = 1;
                }
                Projectile.spriteDirection = Projectile.direction;
                return;
            }

            if (Main.player[Projectile.owner].Center.X < Projectile.Center.X)
                Projectile.direction = -1;
            else
                Projectile.direction = 1;
            Projectile.spriteDirection = Projectile.direction;

            Projectile.ai[1] += 1f;
            float num843 = 0.0065f;

            if (Projectile.ai[1] > 0f)
            {
                Projectile.velocity.Y -= num843;
            }
            else
            {
                Projectile.velocity.Y += num843;
            }
            if (Projectile.ai[1] >= 90f)
            {
                Projectile.ai[1] *= -1f;
            }
        }
    }
}
