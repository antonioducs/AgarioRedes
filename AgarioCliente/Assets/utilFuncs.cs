using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace utilFuncs
{
    public class transcData
    {
        public byte[] StructureToByteArray(object obj)
        {

            int len = Marshal.SizeOf(obj);

            byte[] arr = new byte[len];

            IntPtr ptr = Marshal.AllocHGlobal(len);

            Marshal.StructureToPtr(obj, ptr, true);

            Marshal.Copy(ptr, arr, 0, len);

            Marshal.FreeHGlobal(ptr);

            return arr;

        }
    }


}