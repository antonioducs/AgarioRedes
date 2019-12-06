
using System;

namespace structDef
{
    public class constants
    {
        public const int MAX_SIZE = 4096;

        public const byte ACCEPT_LOGIN = 0x02;
        public const byte SEND_MSG = 0x03;
        public const byte MOV_CLI = 0x04;
        public const byte SEND_TO_WORLD = 0x05;
        public const byte DC_CLIENTID = 0x0A;
    }
    public struct Header
    {
        public int size;
        public byte packetID;
        public byte clientid;

    }

    unsafe public struct sLogin
    {
        public Header header;
        public fixed char name[15];
    }

    unsafe public struct sMsg
    {
        public Header header;
        public fixed char msg[150];
    }

    unsafe public struct pAcceptLogin
    {
        public Header header;
        public float posX;
        public float posY;
        public long points;
        public byte color;
        public fixed char name[15];

    }

    public static class player
    {
        public static byte clientid;

        public static String name;
        public static long points;

        public static float posX;
        public static float posY;

        public static byte color;

    }

    public struct pMov
    {
        public Header header;
        public float posX;
        public float posY;
    }

    unsafe public struct pSendToWorld
    {
        public Header header;

        public byte clientid;

        public float posX;
        public float posY;

        public long points;
        public byte color;

        public fixed char name[15];
    }
}

