using System.Runtime.InteropServices;

namespace MinecraftSpelunking.Common.Minecraft.Structs
{
    [StructLayout(LayoutKind.Explicit)]
    public struct IPRange
    {
        private const uint MaxId = 256 * 256 * 256;

        [FieldOffset(0)]
        private uint _id;

        [FieldOffset(1)]
        public byte First;

        [FieldOffset(2)]
        public byte Second;

        [FieldOffset(3)]
        public byte Third;

        public IPRange(uint id)
        {
            while (id > MaxId)
            {
                id -= MaxId;
            }

            _id = id;
        }

        public IPRange Next()
        {
            return new IPRange(_id + 1);
        }

        public override string ToString()
        {
            return $"{this.First}.{this.Second}.{this.Third}.0/24";
        }
    }
}
