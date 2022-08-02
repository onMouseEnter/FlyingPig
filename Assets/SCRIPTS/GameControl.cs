using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

	public static GameControl instance;					// A reference to our game control script so we can access it statically							
	public GameObject gameOverText;						// A reference to the object that appears when the player dies
	public Text scoreText;								// A reference to the UI text component that displays the player's score
	public bool gameOver = false;						// Is the game over?
	public float scrollSpeed = -1.5f;					// Speed for background movement
	public AudioClip dieSound; 
	public AudioClip flySound; 
	public AudioClip scoreSound; 

	private int score = 0;								// The player's score
	private AudioSource source;

	void Awake () 
	{
		// If we don't currently have a game control...
		if (instance == null) 
		{
			// ...set this one to be it...
			instance = this;
		} 
		//...otherwise...
		else if (instance != this) 
			{
			//...destroy this one because it's a duplicate
			Destroy (gameObject);
			}

		source = GetComponent<AudioSource> ();
	}
	

	void Update () 
	{
		// If the game is over and the player has pressed some input...
		if (gameOver == true && Input.GetMouseButtonDown (0)) 
		{
			//...reload the current scene
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
	}
	public void HeroScored()
	{
		// The Hero can't score if the game is over
		if (gameOver) 
		{
			return;
		}
		// If the game is not over play sound...
		source.PlayOneShot (scoreSound, 1f);
		//...increase the score
		score++;
		//...adjust the sccore text 
		scoreText.text = "Score: " + score.ToString ();

	}

	public void HeroDied()
	{
		// Play die sound...
		source.PlayOneShot (dieSound, 1f);
		//Activate game over text
		gameOverText.SetActive (true);
		// Set the game to be over
		gameOver = true;
	}
}
