using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour {


    public int direction;
    public Transform trans;

    float dir;
	// Use this for initialization
	void Start () {
        dir = 0.10f;
        if (direction == -1)
        {
            transform.Rotate(0, 0, -180);
        }
        //transform.position = transform.position;
    }

    // Update is called once per frame
    void Update () {
       
        //Decide which way to shoot fireball
       
        Vector2 vector = new Vector2(0f, dir);
        transform.Translate(vector);
	}
}
