using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour {


    public int direction;
    float dir;
	// Use this for initialization
	void Start () {

        if (direction == 1)
        {
            dir = 0.10f;
        }
        else
        {
            dir = -0.10f;
        }

    }

    // Update is called once per frame
    void Update () {
       
        //Decide which way to shoot fireball
       
        Vector2 vector = new Vector2(0f, dir);
        transform.Translate(vector);
	}
}
