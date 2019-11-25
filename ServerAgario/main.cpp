#include "main.h"
#include <locale.h>



BYTE conectados;
playerLog player[MAX_CONNECT]; //players conectados

int main()
{
    setlocale(LC_ALL, "");

    conectados = 0;
    ZeroMemory(player, sizeof(player));


    startSocket();

    return 0;
}
