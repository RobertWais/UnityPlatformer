using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour {


    public Projectile projectile;
	// Use this for initialization
	void Start () {
        projectile = GameObject.Find("Fireball").GetComponent<Projectile>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
