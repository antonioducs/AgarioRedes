

namespace structDef
{

    public struct Header
    {
        public ushort size;

        public byte packetID;
        public byte clientid;

        public ulong timeStamp;
    }
    public struct sLogin
    {
        public Header header;
        public byte[] name;
    }

    public struct teste
    {
        public byte[] name;
    }
}
