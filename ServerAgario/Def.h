#ifndef __DEF_H__
#define __DEF_H__

#include <windows.h>

typedef struct{

    SOCKET sock;
    char name[15];

    WORD posX;
    WORD posy;

    DWORD points;
    BYTE color;

}playerLog;


typedef struct{
    WORD Size;

    BYTE packetID;
    BYTE clientid;

    DWORD TimeStamp;

}Header;

typedef struct{
    Header header;
    char msg[150];
}pSendMsg;

typedef struct{
    Header header;
    char name[15];
}pLogin;

#endif // __DEF_H__
