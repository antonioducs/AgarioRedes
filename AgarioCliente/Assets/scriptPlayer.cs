using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using structDef;
using utilFuncs;

public class scriptPlayer : MonoBehaviour
{
    private float speed = 5;
    private float cresc;
    private Vector2 mousePosition;
    private Rigidbody2D rbd;
    private SpriteRenderer rend;
    public GameObject cam;
    void Start()
    {
        cresc = player.points;
        rend = GetComponent<SpriteRenderer>();
        rbd = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3((0.2f*player.points)+0.2f,
            (0.2f * player.points) + 0.2f, 
            (0.2f * player.points) + 0.2f);
        rend.color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
       
         mover(x,y);
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        if (Input.GetKeyDown(KeyCode.Space))
        { transform.localScale = new Vector3(transform.localScale.x + cresc ,
            transform.localScale.y + cresc, transform.localScale.z + cresc) ;
        Camera.main.orthographicSize = Camera.main.orthographicSize + cresc * 2;

        }
    }

    private void mover(float x, float y)
    {
        rbd.velocity = new Vector2(x, y) * speed;
        if (x != 0.0f || y != 0.0f)
        {
            player.posX = transform.position.x;
            player.posY = transform.position.y;
            transcData.sendMov(); //envia movimentação para o servidor
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //Add funcao enviar server
    }

    
}
