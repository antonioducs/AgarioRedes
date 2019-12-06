#include "main.h"


//envia pacote de movimentação para todos conectados
void Movimentar(BYTE clientid, char* packet){

    pMov *p = (pMov*)packet;
    printf("Movimentação recebida, posX: %f, posY: %f\n", p->posX, p->posY);

    player[clientid].posX = p->posX;
    player[clientid].posY = p->posY;

    pMov r;
    ZeroMemory(&r, sizeof(pMov));
    r.header.packetID = MOV_CLI;
    r.header.Size = sizeof(pMov);
    r.header.clientid = clientid;
    r.posX = p->posX;
    r.posY = p->posY;

    char pacote[r.header.Size];
    memcpy(&pacote, &r, r.header.Size);

    for(BYTE i = 0; i < MAX_CONNECT; i++){
        if(i != clientid && player[i].status == 1){ //verifica se não é o próprio cliente e se existe socket salvo
            send(player[i].sock, (char*)pacote, r.header.Size, 0);
        }
    }
}
