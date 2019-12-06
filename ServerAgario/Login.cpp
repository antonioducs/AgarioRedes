#include "main.h"


void Login(char *packet, BYTE clientid)
{
    pLogin *p = (pLogin*)packet;
    printf("Novo player conectado, name: %s\n", p->name);


    //pacote que sera enviado para o cliente com as informações para construção do objeto player
    pAcceptLogin s;
    pSendToWorld stw;
    ZeroMemory(&s, sizeof(pAcceptLogin));
    ZeroMemory(&stw, sizeof(pSendToWorld));


    printf(player[clientid].name, p->name);
    printf(s.name, p->name);
    printf(stw.name, p->name);

    //muda as configurações na struct global do player
    player[clientid].status = 1; //status = 1, logado no jogo

    stw.color = s.color = player[clientid].color = 0x000000;
    stw.posX = s.posX = player[clientid].posX = 0;
    stw.posY = s.posY = player[clientid].posY = 0;
    stw.points = s.points = player[clientid].points = 0;

    s.header.packetID = ACCEPT_LOGIN;
    s.header.Size = sizeof(pAcceptLogin);
    s.header.clientid = clientid;

    char pacote[s.header.Size];
    memcpy(&pacote, &s, s.header.Size);

    send(player[clientid].sock, (char*)pacote, p->header.Size, 0);


    stw.header.packetID = SEND_TO_WORD;
    stw.header.Size = sizeof(pSendToWorld);
    stw.clientid = clientid;
    char pacote2[stw.header.Size];


    pSendToWorld stw2;
    stw2.header.clientid = clientid;
    stw2.header.packetID = SEND_TO_WORD;
    stw2.header.Size = sizeof(pSendToWorld);


    for(byte i = 0; i < MAX_CONNECT; i++){
        if(i != clientid && player[i].status == 1){
            stw.header.clientid = i;
            memcpy(&pacote2, &stw, stw.header.Size);
            send(player[i].sock, (char*)pacote2, stw.header.Size, 0);

            stw2.clientid = i;
            stw2.posX = player[i].posX;
            stw2.posY = player[i].posY;
            stw2.color = player[i].color;
            sprintf(stw2.name, player[i].name);
            stw2.points = player[i].points;

            memcpy(&pacote2, &stw2, stw2.header.Size);
            send(player[clientid].sock, (char*)pacote2, stw2.header.Size, 0);
            Sleep(1000);
        }
    }
}
