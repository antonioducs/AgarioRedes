#include "main.h"


void packetControl(char *packet, BYTE clientid, WORD Size)
{

    Header *p = (Header*)packet;
    printf("Pacote recebido: ID: %X, size: %d, clientid: %d\n", p->packetID, p->Size, p->clientid);

    if(p->Size == Size && p->Size < MAX_SIZE)
    {
        switch(p->packetID)
        {
        case 0x01: //pacote de login
            Login((char*)packet);
            break;

            default:
                printf("Pacote n�o cadastrado recebido: ID: %X, size: %d\n", p->packetID, p->Size);
                DC(clientid);
                break;
        }
    }
    else//pacotes inv�lidos
    {
        printf("Pacote Inv�lido Recebido! packet: %X, size: %d\n", p->packetID, p->Size);
        DC(clientid);
    }
}
