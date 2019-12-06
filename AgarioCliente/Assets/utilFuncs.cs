using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using structDef;

namespace utilFuncs
{
    public class transcData
    {
        public static Byte[] StructureToBuffer<T>(T structure)
        {
            Byte[] buffer = new Byte[Marshal.SizeOf(typeof(T))];

            unsafe
            {
                fixed (byte* pBuffer = buffer)
                {
                    Marshal.StructureToPtr(structure, new IntPtr((void*)pBuffer), true);
                }
            }

            return buffer;
        }

        public static T BufferToStructure<T>(Byte[] buffer, Int32 offset)
        {
            unsafe
            {
                fixed (Byte* pBuffer = buffer)
                {
                    return (T)Marshal.PtrToStructure(new IntPtr((void*)&pBuffer[offset]), typeof(T));
                }
            }
        }

        public static void sendMov()
        {
            pMov p;
            p.header.packetID = constants.MOV_CLI;
            p.header.clientid = player.clientid;
            p.header.size = Marshal.SizeOf(typeof(pMov));
            p.posX = player.posX;
            p.posY = player.posY;

            byte[] pacote = new byte[p.header.size];
            pacote = transcData.StructureToBuffer<pMov>(p);

            connectData.sockStream.Write(pacote, 0, pacote.Length);
        }
    }


}