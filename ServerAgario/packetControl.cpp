#include "main.h"


void packetControl(char *packet, BYTE clientid, WORD Size)
{

    Header *p = (Header*)packet;
    printf("Pacote recebido: ID: %X, size: %d, clientid: %d\n", p->packetID, p->Size, p->clientid);

    switch(p->packetID)
    {
        case 0x01: //pacote de login
            Login(packet, clientid);
            break;
        case MOV_CLI:
            Movimentar(clientid, packet);
            break;

            default:
                printf("Pacote não cadastrado recebido: ID: %X, size: %d\n", p->packetID, p->Size);
                DC(clientid);
                break;
    }
}
