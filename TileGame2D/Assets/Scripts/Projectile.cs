using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour {

    
    public int direction;
    public Transform trans;
    CapsuleCollider2D myCollider;

    float dir;
	// Use this for initialization
	void Start () {
        myCollider = GetComponent<CapsuleCollider2D>();
        dir = 0.10f;
        if (direction == -1)
        {
            transform.Rotate(0, 0, -180);
        }
        //transform.position = transform.position;
    }

    // Update is called once per frame
    void Update () {
        HitBackground();
        //Decide which way to shoot fireball
       
        Vector2 vector = new Vector2(0f, dir);
        transform.Translate(vector);
	}

    void HitBackground(){


        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(LayerMask.LayerToName(other.gameObject.layer).Equals("Enemy")){
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

    }
}
