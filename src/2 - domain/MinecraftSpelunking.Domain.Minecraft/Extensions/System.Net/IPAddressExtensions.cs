namespace System.Net
{
    public static class IPAddressExtensions
    {
        public static IPAddress? IncrementIpAddress(this IPAddress ipAddress, uint increment)
        {
            byte[] addressBytes = FlipAddressBytes(ipAddress.GetAddressBytes());
            uint ipAsUint = BitConverter.ToUInt32(addressBytes, 0);
            try
            {
                uint newIpAsUint = checked(ipAsUint + increment);
                BitConverter.TryWriteBytes(addressBytes, ipAsUint + increment);

                return new IPAddress(FlipAddressBytes(addressBytes));
            }
            catch
            {
                return null;
            }
        }

        private static byte[] FlipAddressBytes(byte[] addressBytes)
        {
            byte placeholder = addressBytes[0];
            addressBytes[0] = addressBytes[3];
            addressBytes[3] = placeholder;

            placeholder = addressBytes[1];
            addressBytes[1] = addressBytes[2];
            addressBytes[2] = placeholder;

            return addressBytes;
        }
    }
}
