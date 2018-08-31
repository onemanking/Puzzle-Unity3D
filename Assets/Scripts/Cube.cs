using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {
	public int cubeNumber;
    private Color cudeColor;
	private TextMesh cubeText;
	private GridManager gridManager;

	// Use this for initialization
	void Start () {
		cubeText = GetComponentInChildren<TextMesh> ();
		gridManager = GameObject.FindWithTag ("Grid").GetComponent<GridManager>();

		//setCubeNumber
		cubeNumber = gridManager.RandomNumber ();

		//setCubeText
		cubeText.text = "" + cubeNumber;
	}

	void OnMouseDown(){
		if (cubeNumber == gridManager.currentNumber) {
			Destroy (gameObject);
			gridManager.currentNumber += 1;
			if (gridManager.cubeInGrid < gridManager.maxNumber) {
				gridManager.cubeInGrid += 1;
				gridManager.SpawnCube (gameObject);
			}
			if(gridManager.cubeInGrid == (25 % gridManager.maxNumber)+1)
				gridManager.SetListValue();
			
		}
		Debug.Log ("Current Number "+gridManager.currentNumber);
		Debug.Log ("Cube In Grid "+gridManager.cubeInGrid);
	}
		

}
