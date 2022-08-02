using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

	public float upForce;							// Upward force of the "Fly" motion

	private bool isDead = false;					// Has the player collided with a wall?
	private Animator anim;							// Reference to the Animator component
	private Rigidbody2D rb2d;						// Holds a reference to the Rigidbody2D component of the Hero



	void Start () 
	{
		// Get reference to the Animator component attached to this GameObject
		anim = GetComponent<Animator> ();
		// Get and store a reference to the Rigidbody2D attached to this GameObject
		rb2d = GetComponent<Rigidbody2D> ();
	}
	

	void Update () 
	{
		// Don't allow control if the Hero has died
		if (isDead == false) 
		{
			//  Look for input to trigger a "Fly" motion
			if (Input.GetMouseButtonDown (0))
			{
				//...tell the animator about it and then...
				anim.SetTrigger ("Fly");
				// ...zero out the Heros current y velocity before...
				rb2d.velocity = Vector2.zero;
				// new vector2(rb2D.velocity.x,0);
				// ...giving the Hero some upward force
				rb2d.AddForce (new Vector2(0, upForce));
			}
		}
		
	}

	void OnCollisionEnter2D ()
	{
		// Zero out the Hero's velocity
		rb2d.velocity = Vector2.zero;
		// If the Hero collides with something set it to dead...
		isDead = true;
		//...tell the Animator about it...
		anim.SetTrigger ("Die");
		//...and tell the game control about it
		GameControl.instance.HeroDied ();
	}
}