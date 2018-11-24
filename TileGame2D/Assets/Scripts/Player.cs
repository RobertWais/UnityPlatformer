using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    //Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;

    //State 
    bool isAlive = true;

    //Cached Components References
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Collider2D myCollider;



	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Run();
        Jump();
        FlipSprite();
	}

    private void Run(){
        float controlFloat = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlFloat*runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHorizontalSpeed);
    }

    private void Jump(){
        if(!myCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
            return;
        }
        //SPACE BTN
        if(CrossPlatformInputManager.GetButtonDown("Jump")){
            Vector2 velocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += velocityToAdd;
        }

    }


    private void FlipSprite(){
        //If there is speed
        bool playerHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if (playerHorizontalSpeed){
            //+1 or -1
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }
}
