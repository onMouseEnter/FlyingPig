using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : MonoBehaviour {
	
 									
	public GameObject[] obstaclePrefabs;								// The obstacle game object
	public int obstaclePoolSize = 5; 									// How many columns to keep on standby
	public float spawnRate1 = 5f; 										// How quickly columns spawn
	public float spawnRate2 = 15f; 	
	public float obstacleMin = -1f;										// Minimum y value of the column position
	public float obstacleMax = 3.5f;									// Minimum x value of the column position

	private GameObject[] obstacles;										// Collection of pooled obstacles
	private GameObject[] MyObstacles;	
	private int currentObstacle = 0;									// Index of the current obstacle in the collection
	private Vector2 objectPoolPosition = new Vector2 (-15f, -25f);		// A holding position for unused obstacles offscreen
	private float timeSinceLastSpawned;
	private float spawnXPosition = 10f;


	void Start () 
	{
		timeSinceLastSpawned = 0f;

		// Initialize the obstacles collection
		obstacles = new GameObject[obstaclePoolSize];


		// Loop through the collection...
		for (int i = 0; i < obstaclePoolSize; i++) 
		{
			//...pick a random obstacle element from the defined prefabs...
			GameObject MyObstacles = obstaclePrefabs[Random.Range (0, obstaclePrefabs.Length)];
			//...and create the individual obstacles
			obstacles [i] = (GameObject)Instantiate (MyObstacles, objectPoolPosition, Quaternion.identity);
		}
	}
	
	// This spawns obstacles as long as the game is not over
	void Update () 
	{
		timeSinceLastSpawned += Time.deltaTime;
		var randomRange = Random.Range(spawnRate1, spawnRate2);
		
		if (GameControl.instance.gameOver == false && timeSinceLastSpawned >= randomRange) 
		{
			timeSinceLastSpawned = 0;

			// Set random y position for obstacle...
			float spawnYPosition = Random.Range (obstacleMin, obstacleMax);

			// ... then set the current column to that position
			obstacles [currentObstacle].transform.position = new Vector2 (spawnXPosition, spawnYPosition);

			// Increaser the value of currentObstacle. If the new siez is too big, set i back to zero
			currentObstacle++;
			if (currentObstacle >= obstaclePoolSize) 
			{
				currentObstacle = 0;
			}
		}
	}
}
