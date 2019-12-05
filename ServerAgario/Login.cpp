#include "main.h"


void Login(char *packet)
{
    printf("sLogin: %d\n", sizeof(pLogin));
    pLogin *p = (pLogin*)packet;
    printf("Name: %s\n", p->name);
}
