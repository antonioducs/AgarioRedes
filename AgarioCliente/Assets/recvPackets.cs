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
            //thread responsável por ficar ouvindo o servidor
            connectData.sockStream.Read(pacote, 0, constants.MAX_SIZE);
            Header packet = transcData.BufferToStructure<Header>(pacote, 0);
            switch (packet.packetID)
            {
                case constants.SEND_TO_WORLD: //pacote recebido quando um novo cliente conecta para instancialo no game
                    pSendToWorld p = transcData.BufferToStructure<pSendToWorld>(pacote, 0);
                    pos = new Vector2(p.posX, p.posY);
                    clientid = p.clientid;
                    isStantiate = true;                    
                    break;
                case constants.MOV_CLI://pacote recebido quando um cliente ja conectado se movimenta
                    pmov = transcData.BufferToStructure<pMov>(pacote, 0);
                    move = true;
                    break;
                case constants.DC_CLIENTID: //pacote recebido quando algum cliente desconecta para destruir o objeto
                    clientid = packet.clientid;
                    dc = true;
                    break;
             }
        }
    }
}
