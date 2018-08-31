using UnityEngine;
using System.Collections;

public class CubeScript : MonoBehaviour {
	public int cubeNumber;
	private TextMesh cubeText;
	private GridScript gridScript;
	// Use this for initialization
	void Start () {
		cubeText = GetComponentInChildren<TextMesh> ();
		gridScript = GameObject.FindWithTag ("Grid").GetComponent<GridScript>();

		//setCubeNumber
		cubeNumber = gridScript.RandomNumber ();

		//setCubeText
		cubeText.text = "" + cubeNumber;
	}

	void OnMouseDown(){
		if (cubeNumber == gridScript.currentNumber) {
			Destroy (gameObject);
			gridScript.currentNumber += 1;
			if (gridScript.cubeInGrid < gridScript.maxNumber) {
				gridScript.cubeInGrid += 1;
				gridScript.SpawnCube (gameObject);
			}
			if(gridScript.cubeInGrid == (25 % gridScript.maxNumber)+1)
				gridScript.SetListValue();
			
		}
		Debug.Log ("Current Number "+gridScript.currentNumber);
		Debug.Log ("Cube In Grid "+gridScript.cubeInGrid);
	}
		

}
