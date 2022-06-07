using Terraria;
using Terraria.ID;

namespace AdLibitum.Utilities
{
    public static class NetworkUtilities
    {
        public static bool IsClientLocalHost()
        {
            return Main.netMode == NetmodeID.MultiplayerClient && Netplay.Connection.Socket.GetRemoteAddress().IsLocalHost();
        }

        public static bool IsPlayerLocalHost(int whoAmI)
        {
            if (whoAmI is < 0 or > Main.maxPlayers) return false;

            RemoteClient client = Netplay.Clients[whoAmI];
            return client.State == 10 && client.Socket.GetRemoteAddress().IsLocalHost();
        }

        public static bool IsLocalHost(int whoAmI)
        {
            return Main.netMode == NetmodeID.SinglePlayer || IsClientLocalHost() || IsPlayerLocalHost(whoAmI);
        }
    }
}