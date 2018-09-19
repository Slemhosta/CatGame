using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public Rigidbody2D rb2d;
	public Animator anim;

	public KeyCode Jump;

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump;

	public float speed;
	public float walkingTriggerSpeed;

	public float jumpForce = 1000f;
	private bool grounded = false;
	public Transform groundCheck;

	// Update is called once per frame
	void FixedUpdate () {
		
		float moveHorizontal = Input.GetAxis ("Horizontal");

		Vector2 movement = new Vector2 (moveHorizontal, 0.0f);

		rb2d.AddForce (movement * speed);

		//These two checks if the speed of the player is more than walkingTriggerSpeed. If it is, play the walking animation.
		//You will probably have to fine-tune this in the inspector.
		if (rb2d.velocity.x > walkingTriggerSpeed || rb2d.velocity.x < -walkingTriggerSpeed) anim.SetBool ("Walking", true);
		else anim.SetBool ("Walking", false);

		if (moveHorizontal > 0 && !facingRight) Flip();

		else if (moveHorizontal < 0 && facingRight) Flip();

		if (jump)
		{
			//jump anim
			rb2d.AddForce(new Vector2 (0f,jumpForce));
			jump = false;
		}
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.F)) 
		{
			anim.SetTrigger ("Talk");
		}	

		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

		if (Input.GetKeyDown (Jump) && grounded) 
		{
			jump = true;
		}
	}
				
			void Flip()
			{
				facingRight = !facingRight;
				Vector3 theScale = transform.localScale;
				theScale.x *= -1;
				transform.localScale = theScale;
			}
}
