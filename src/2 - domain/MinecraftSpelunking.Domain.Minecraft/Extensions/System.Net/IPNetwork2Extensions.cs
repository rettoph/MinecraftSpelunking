namespace System.Net
{
    public static class IPNetwork2Extensions
    {
        public static IPNetwork2? CalculateNextNetwork(this IPNetwork2 network)
        {
            IPAddress? next = network.Network.IncrementIpAddress((uint)network.Total);
            if (next is null)
            {
                return null;
            }

            return new IPNetwork2(next, network.Cidr);
        }
    }
}
