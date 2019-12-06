using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets; //lib sockets C#
using System;
using System.Runtime.InteropServices;
using structDef;
using utilFuncs;
using UnityEngine.SceneManagement;

public class connectData : MonoBehaviour
{
    public Text msgServer;
    public Text nickName;
    public GameObject pmsg;
    public static NetworkStream sockStream;
    public static TcpClient cliente;

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
            cliente = new TcpClient();
            cliente.Connect("localhost", 8281);

            sockStream = cliente.GetStream();

            sLogin p;
            p.header.clientid = 0;
            p.header.packetID = 0x01;
            unsafe
            {
                for(byte i = 0; i < nickName.text.Length; i++)
                {
                    p.name[i] = nickName.text.ToCharArray()[i];
                }
            }
            
            p.header.size = Marshal.SizeOf(typeof(sLogin));

            byte[] pacote = new byte[p.header.size];

            transcData td = new transcData();
            pacote = transcData.StructureToBuffer<sLogin>(p);

            sockStream.Write(pacote, 0, pacote.Length);
            byte[] recv = new byte[constants.MAX_SIZE];
 
                sockStream.Read(recv, 0, constants.MAX_SIZE);
                Header header = transcData.BufferToStructure<Header>(recv, 0);
                switch (header.packetID)
                {
                    case constants.SEND_MSG: //caso chegue algum pacote de msg, exibe a msg e a conexão não sucedida
                        sMsg r = transcData.BufferToStructure<sMsg>(recv, 0);
                        unsafe { 
                            msgServer.text = new string(r.msg);
                        }
                        Invoke("invMsg", 3);
                        sockStream.Close();
                        cliente.Close();
                        break;
                case constants.ACCEPT_LOGIN: //caso chegue o pacote de loguin, envia o player para a cena do game
                    pAcceptLogin al = transcData.BufferToStructure<pAcceptLogin>(recv, 0);
                    //seta dados do player na classe statica
                    player.clientid = al.header.clientid;
                    unsafe
                    {
                        player.name = new string(al.name);
                    }
                    player.points = al.points;
                    player.posX = al.posX;
                    player.posY = al.posY;
                    player.color = al.color;
                    SceneManager.LoadScene("Game");
                    break;

                default:
                    Debug.Log(header.packetID);
                    msgServer.text = "Falha ao tentar se conectar!";
                    Invoke("invMsg", 3);
                    sockStream.Close();
                    cliente.Close();
                    break;
            }
        }
        catch (Exception error)
        {
            msgServer.text = error.Message;// "Falha ao tentar se conectar!";
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
