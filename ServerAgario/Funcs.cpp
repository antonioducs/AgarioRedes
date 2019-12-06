#include "main.h"

void DC(BYTE clientid)
{
    for(byte i = 0; i < MAX_CONNECT; i++) {
        if(i != clientid && player[i].status == 1){
            Header p;
            p.Size = sizeof(Header);
            p.packetID = DC_CLIENT;
            p.clientid = clientid;

            char pacote[p.Size];
            memcpy(&pacote, &p, p.Size);

            send(player[i].sock, (char*)pacote, p.Size, 0); //envia
        }
    }
    closesocket(player[clientid].sock);//fecha socket
    ZeroMemory(&player[clientid], sizeof(playerLog)); //limpa struct
    conectados--; //decrementa quantidade de conectados
    pthread_exit(&thread[clientid]); //encerra thread
}

//eviar um pacote somente com header cujo ID já é sufuciente para saber a ação que deve ser execultada
void SendClientSignal(BYTE clientid, BYTE signal)
{
    Header p;
    p.Size = sizeof(Header);
    p.packetID = signal;
    p.clientid = clientid;

    char pacote[p.Size];
    memcpy(&pacote, &p, p.Size);

    send(player[clientid].sock, (char*)pacote, p.Size, 0); //envia
}

void sendMsg(BYTE clientid, char* msg)
{
    pSendMsg p;
    ZeroMemory(&p, sizeof(pSendMsg));

    p.header.clientid = clientid;
    p.header.packetID = SEND_MSG;
    p.header.Size = sizeof(pSendMsg);

    sprintf(p.msg, msg);

    char pacote[p.header.Size];
    memcpy(&pacote, &p, p.header.Size);

    send(player[clientid].sock, (char*)pacote, p.header.Size, 0); //envia
}
