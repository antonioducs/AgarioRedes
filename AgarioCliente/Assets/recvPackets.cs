using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using structDef;
using utilFuncs;
using System.Threading;

public class recvPackets : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject adversario;
    private bool isStantiate;
    private Vector2 pos;
    public GameObject[] allAdversarios;
    private byte clientid;
    private pMov pmov;
    private bool move;
    private bool dc;
    private void Start()
    {
        dc = false;
        isStantiate = false;
        move = false;
        allAdversarios = new GameObject[100];
        new Thread(recvP).Start();
    }
    // Update is called once per frame
    private void Update()
    {
        if (isStantiate)
        {
            allAdversarios[clientid] = Instantiate(adversario, pos, Quaternion.identity);
            isStantiate = false;
        }

        if(move)
        {
            allAdversarios[pmov.header.clientid].transform.position =
                new Vector3(pmov.posX, pmov.posY, 1);
            move = false;
        }

        if(dc)
        {
            Object.Destroy(allAdversarios[clientid]);
            dc = false;
        }
    }
    public void recvP()
    {
        byte[] pacote = new byte[constants.MAX_SIZE];
        while (true) 
        { 
            
            connectData.sockStream.Read(pacote, 0, constants.MAX_SIZE);
            Header packet = transcData.BufferToStructure<Header>(pacote, 0);
            Debug.Log("Packet id: " + packet.packetID + " Clientid: " + packet.clientid);
            switch (packet.packetID)
            {
                case constants.SEND_TO_WORLD:
                    pSendToWorld p = transcData.BufferToStructure<pSendToWorld>(pacote, 0);
                    pos = new Vector2(p.posX, p.posY);
                    clientid = p.clientid;
                    isStantiate = true;
                    break;
                case constants.MOV_CLI:
                    pmov = transcData.BufferToStructure<pMov>(pacote, 0);
                    move = true;
                    break;
                case constants.DC_CLIENTID:
                    clientid = packet.clientid;
                    dc = true;
                    break;
             }
        }
    }
}
