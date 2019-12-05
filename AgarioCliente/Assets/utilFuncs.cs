using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace utilFuncs
{
    public class transcData
    {
        /*   public byte[] toByteArray()
            {
                int size = Marshal.SizeOf(this);
                byte[] arr = new byte[size];
                IntPtr pt = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(this, pt, true);
                Marshal.Copy(pt, arr, 0, Marshal.SizeOf(this));

                Marshal.FreeHGlobal(pt);

                return arr;
            }
            */


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
    }


}