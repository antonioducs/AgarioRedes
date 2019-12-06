using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptAd : MonoBehaviour
{
   private float speed = 5;
    private Vector2 mousePosition;
    private Rigidbody2D rbd;
    private SpriteRenderer rend;
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rbd = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(0.2f,0.2f,0.2f);
        rend.color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    public void mover(float x, float y)
    {
        rbd.velocity = new Vector2(x, y) * speed;
    }

   

}
