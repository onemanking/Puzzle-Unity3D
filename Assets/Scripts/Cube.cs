using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Cube : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField] private TextMesh cubeText;
    public int cubeNumber;
    private GridManager gridManager;

    // Use this for initialization
    void Start()
    {
        gridManager = GameObject.FindWithTag("Grid").GetComponent<GridManager>();

        SetNumber(gridManager.RandomNumber());
    }

    void SetNumber(int number)
    {
        //setCubeNumber
        cubeNumber = number;
  
        //setCubeText
        cubeText.text = "" + cubeNumber;
    }

    public void SetColor(Color cubeColor)
    {
        GetComponent<SpriteRenderer>().color = cubeColor;
    }

    public void ToggleText(bool enabled)
    {
        cubeText.gameObject.SetActive(enabled);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (cubeNumber <= 1)
        {
            GameManager.Instance.StartGame();
        }

        if (cubeNumber == gridManager.currentNumber)
        {
            Destroy(gameObject);
            gridManager.currentNumber += 1;
            if (gridManager.cubeInGrid < gridManager.maxNumber)
            {
                gridManager.cubeInGrid += 1;
                gridManager.SpawnCube(transform);
            }

            if (gridManager.cubeInGrid == (25 % gridManager.maxNumber) + 1)
            {
                gridManager.SetListValue();
            }

        }
        Debug.Log("Current Number " + gridManager.currentNumber);
        Debug.Log("Cube In Grid " + gridManager.cubeInGrid);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       
    }
}
