using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidbody;
    CapsuleCollider2D capsuleCollider;



	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Shot();
        if (IfIsFacingRight()){
            myRigidbody.velocity = new Vector2(moveSpeed, 0f);
        }else{
            myRigidbody.velocity = new Vector2(-moveSpeed, 0f);
        }
	}

    private bool IfIsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
    }

    private void Shot(){
        if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Fireball")))
        {
            Debug.Log("Hit");
            Destroy(gameObject);
        }
    }

}
