using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    //Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 2f;

    //State 
    bool isAlive = true;
    float gravityAtFirst;

    //Cached Components References
    Rigidbody2D myRigidBody;
    Animator myAnimator;

    CapsuleCollider2D myCollider;
    BoxCollider2D myBoxCollider;



	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        gravityAtFirst = myRigidBody.gravityScale;
	}
	
	// Update is called once per frame
	void Update () {
        Run();
        Jump();
        FlipSprite(); 
        Climb();
    }

    private void Run(){
        float controlFloat = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlFloat*runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHorizontalSpeed);
    }

    private void Jump(){
        if(!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
            return;
        }
        //SPACE BTN
        if(CrossPlatformInputManager.GetButtonDown("Jump")){
            Vector2 velocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += velocityToAdd;
        }

    }

    private void Climb(){
        
        if(!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))){
            myAnimator.SetBool("Climbing", false);

            //For stalling on ladder
            myRigidBody.gravityScale = gravityAtFirst;
            return;
        }
        myRigidBody.gravityScale = 0f;
        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Debug.Log("Control: " + controlThrow);
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow*climbSpeed);
        myRigidBody.velocity = climbVelocity;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
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
