#include "main.h"

pthread_t thread[MAX_CONNECT];

BYTE set_null_thread(); //função que retorna o numero da posição de uma struct vazia do vetor

void startSocket()
{
    int server, rc, novosock, tamanho;
	struct sockaddr_in meusock, conexao;
    WSADATA wsaData;
    WSAStartup(MAKEWORD(2,1),&wsaData);

    server = socket(AF_INET, SOCK_STREAM, IPPROTO_IP);
	if(server<0)
	{
		printf("Erro Socket\n\n");
		system("pause");
		exit(1);
	}

	meusock.sin_family = AF_INET;
	meusock.sin_port = htons(PORT);
	meusock.sin_addr.s_addr = htonl(INADDR_ANY);

	rc = bind(server,(struct sockaddr *)&meusock,sizeof(meusock));
	if(rc < 0)
	{
		printf("Bind\n");
		system("pause");
		exit(1);
	}
	rc = listen(server, MAX_CONNECT);
	if(rc < 0)
	{
		printf("listen\n");
		system("pause");
		exit(1);
	}
	printf("Socket Iniciado!\n");
	while(1)
	{
        tamanho = sizeof(struct sockaddr);
		novosock = accept(server,(struct sockaddr *)&conexao, &tamanho);
		if(novosock > 0)
        {
            if(conectados < MAX_CONNECT)
            {
                BYTE pos = set_null_thread();
                player[pos].sock = (SOCKET)novosock;
                conectados++;
                pthread_create(&thread[pos], NULL, Recv, (void*)&pos); // cria o thread do recv
            }
            else
            {
                pSendMsg p;
                p.header.packetID = SEND_MSG;
                p.header.Size = sizeof(p);
                sprintf(&p.msg[0], "Servidor cheio!");

                char Pacote[p.header.Size];
                memcpy(&Pacote, &p, sizeof(p));
                send(novosock, (char*)Pacote, p.header.Size, 0);
            }
        }
	}
}

BYTE set_null_thread(){
    BYTE res;
    for(BYTE i = 1; i < MAX_CONNECT; i++)
    {
        if(player[i].sock == 0)//verifica se não está em uso
        {
            res = i;
            break;
        }
    }
    return res;
}
