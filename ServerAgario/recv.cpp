#include "main.h"


//thread que fica escutando o cliente
void *Recv(void *arg){
    char packet[MAX_SIZE];
    int rc;
    BYTE clientid = *(BYTE*)arg;
    printf("Entrou Thread, %d, clientid: %d \n", player[clientid].sock, clientid);

    while(1)
    {
        rc = recv(player[clientid].sock, (char*)packet, MAX_SIZE, 0);

        if(rc < 0) //pacote inválido
            DC(clientid);
        else
            packetControl((char*)packet, clientid, (WORD)rc);
    }
}
