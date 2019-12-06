#ifndef __DEF_H__
#define __DEF_H__

#include <windows.h>

typedef struct{

    SOCKET sock;
    BYTE status;
    char name[15];

    float posX;
    float posY;

    long points;
    BYTE color;

}playerLog;



typedef struct{
    int Size;

    BYTE packetID;
    BYTE clientid;

}Header;

typedef struct{
    Header header;
    char msg[150];
}pSendMsg;

typedef struct{
    Header header;
    char name[15];
}pLogin;

typedef struct{
    Header header;
    float posX;
    float posY;
}pMov;

typedef struct{
    Header header;
    float posX;
    float posY;

    long points;
    BYTE color;

    char name[15];
}pAcceptLogin;

typedef struct{
    Header header;

    byte clientid;

    float posX;
    float posY;

    long points;
    BYTE color;

    char name[15];
}pSendToWorld;

#endif // __DEF_H__
