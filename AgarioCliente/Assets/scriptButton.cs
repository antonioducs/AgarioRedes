﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets; //lib sockets C#
using System.Net; //lib sockets C#
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using structDef;
using utilFuncs;

public class scriptButton : MonoBehaviour
{
    public Text msgServer;
    public Text nickName;
    public GameObject pmsg;
    public NetworkStream sockStream;

    public void Start()
    {
        msgServer.enabled = false;
        pmsg.SetActive(false);
    }


    public void connectionServerAndStartGame()
    {
        msgServer.text = "Conectando...";
        msgServer.enabled = true;
        pmsg.SetActive(true);

        try
        {
            TcpClient cliente = new TcpClient();
            cliente.Connect("localhost", 8281);

            sockStream = cliente.GetStream();

            sLogin p;
            p.header.clientid = 0;
            p.header.packetID = 0x01;
            p.header.timeStamp = (ulong)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            p.name = nickName.text.ToCharArray();
            p.header.size = (ushort)Marshal.SizeOf(typeof(sLogin));

            byte[] pacote = new byte[p.header.size];
            transcData tData = new transcData();
            pacote = tData.StructureToByteArray(p);
            sockStream.Write(pacote, 0, pacote.Length);

            sockStream.Close();
            cliente.Close();
        }
        catch (Exception error)
        {
            msgServer.text = error.Message;//"Falha ao tentar se conectar!";
            Invoke("invMsg", 3);
        }
    }

    private void invMsg()
    {
        msgServer.enabled = false;
        pmsg.SetActive(false);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}