#include "main.h"

void DC(BYTE clientid)
{
    SendClientSignal(clientid, DC_CLIENT); //envia sinal para o cliente da desconexão
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
