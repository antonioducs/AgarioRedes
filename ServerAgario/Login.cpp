#include "main.h"

void Login(char *packet)
{
    pLogin *p = (pLogin*)packet;
    printf("Name: %s\n", p->name);
}
