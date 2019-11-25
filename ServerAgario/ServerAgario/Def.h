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
#endif // __DEF_H__
