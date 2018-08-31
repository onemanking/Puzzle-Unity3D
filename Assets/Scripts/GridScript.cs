using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridScript : MonoBehaviour {
	//GridSetUp
	public int width = 5;
	public int height = 5;
	public GameObject cube;
	public float offSet = 1.5f;

	//NumberSetUp
	private List<int> randomNumber = new List<int>();
	public int maxNumber = 50;
	[HideInInspector]
	public int cubeInGrid; // = cube in grid
	[HideInInspector]
	public int currentNumber = 1;
	private int currentMax;
	//private int maxDiv;

	// Use this for initialization
	void Start () {
		//Set Number 
		cubeInGrid = width * height;
		currentMax = maxNumber;
		//maxDiv = currentMax / cubeInGrid;

		//SetListValue
		for (int i = 1; i <= cubeInGrid; i++) {
			randomNumber.Add (i);
		}

		//SetGrid
		transform.localScale = new Vector2 (1.8f, 1.8f);
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++) 
			{
				GameObject gridCude = Instantiate(cube, new Vector2 (transform.position.x+ (offSet* x), transform.position.y + (offSet* y)),Quaternion.identity) as GameObject ;
				gridCude.transform.SetParent (transform,true);
				gridCude.transform.localScale = new Vector2(1,1);
			}
		}
	}
		
	public int RandomNumber(){
		int index = Random.Range (0, randomNumber.Count);
		int value = randomNumber [index];
		randomNumber.RemoveAt (index);
		return value;
	}

	public void SetListValue(){ //Set New Value In List
		//currentMax = currentMax - (25*(maxDiv/2)); // Need more math!!!
		Debug.Log("Current Max "+currentMax);
		for (int i = cubeInGrid; i <= currentMax; i++) 
		{
			randomNumber.Add (i);
		}
	
	}


	public void SpawnCube(GameObject newCube){
		//SpawnCubeAtSamePoint
		GameObject gridCude2 = Instantiate(cube, newCube.transform.position,Quaternion.identity) as GameObject ;
		gridCude2.transform.SetParent (transform,true);
		gridCude2.transform.localScale = new Vector2(1,1);
	}

}
