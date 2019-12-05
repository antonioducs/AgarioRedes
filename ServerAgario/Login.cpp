#include "main.h"

typedef struct  {
    char name[15];
}teste;

void Login(char *packet)
{
    teste *p = (teste*)packet;
    printf("Name: %s\n", p->name);
}
