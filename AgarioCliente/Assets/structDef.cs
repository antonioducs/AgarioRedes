

using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

namespace structDef
{
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
}

