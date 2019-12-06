#ifndef __MAIN_H__
#define __MAIN_H__



#include <stdio.h>
#include <stdlib.h>
#include <WinSock2.h>
#include <pthread.h>
#include <time.h>
#include <math.h>

#include "Def.h"

#define MAX_CONNECT 100
#define PORT 8281
#define MAX_SIZE 4096

#define DC_CLIENT 0x0A
#define SEND_MSG 0x03
#define MOV_CLI 0x04
#define ACCEPT_LOGIN 0x02
#define SEND_TO_WORD 0x05 //

//FUNCOES
void startSocket();
void packetControl(char *packet, BYTE clientid, WORD Size);
void DC(BYTE clientid);
void SendClientSignal(BYTE clientid, BYTE signal);
void sendMsg(BYTE clientid, char* msg);
void *Recv(void *arg);
void Movimentar(BYTE clientid, char* packet);

//Variaveis globais
extern BYTE conectados;
extern pthread_t thread[MAX_CONNECT];
extern playerLog player[MAX_CONNECT];

//pacotes
void Login(char *packet, BYTE clientid);

#endif // __MAIN_H__
