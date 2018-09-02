using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridManager : MonoBehaviour {

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

    public Color startCubeColor;
    public Color spawnCubeColor;

    private List<Cube> startCubeList = new List<Cube>();
    private bool _isInitComplete;

    //private int maxDiv;

    // Use this for initialization
    void Start() {
        //InitGrid();
    }

    public void InitGridWithMaxNunber(int _maxNumber) {
        maxNumber = _maxNumber;
        StartCoroutine(InitGridCoroutine());
    }

    IEnumerator InitGridCoroutine() {
        _isInitComplete = false;
        startCubeList.Clear();
        //Set Number 
        currentNumber = 1;
        cubeInGrid = width * height;
        currentMax = maxNumber;
        //maxDiv = currentMax / cubeInGrid;

        //SetListValue
        for (int i = 1; i <= cubeInGrid; i++)
        {
            randomNumber.Add(i);
        }

        //SetGrid
        transform.localScale = new Vector2(1.8f, 1.8f);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject startCube = Instantiate(cube, new Vector2(transform.position.x + (offSet * x), transform.position.y + (offSet * y)), Quaternion.identity) as GameObject;
                startCube.transform.SetParent(transform, true);
                startCube.transform.localScale = new Vector2(1, 1);

                Cube _cube = startCube.GetComponent<Cube>();
                _cube.SetColor(startCubeColor);

                startCubeList.Add(_cube);
            }
        }

        yield return new WaitForEndOfFrame();

        ToggleStartCubeNumber(false);

        yield return new WaitForEndOfFrame();

        _isInitComplete = true;
    }

    public bool IsInitComplete() {
        return _isInitComplete;
    }

    public int RandomNumber() {
        int index = Random.Range(0, randomNumber.Count);
        int value = randomNumber[index];
        randomNumber.RemoveAt(index);
        return value;
    }

    public void SetListValue() { //Set New Value In List
        //currentMax = currentMax - (25*(maxDiv/2)); // Need more math!!!
        Debug.Log("Current Max " + currentMax);
        for (int i = cubeInGrid; i <= currentMax; i++)
        {
            randomNumber.Add(i);
        }

    }

    public void SpawnCube(Transform oldCube) {
        //SpawnCubeAtSamePoint
        GameObject newCube = Instantiate(cube, oldCube.transform.position, Quaternion.identity) as GameObject;
        newCube.transform.SetParent(transform, true);
        newCube.transform.localScale = new Vector2(1, 1);
        Cube _cube = newCube.GetComponent<Cube>();
        _cube.SetColor(spawnCubeColor);
    }

    public void ToggleStartCubeNumber(bool enabled) {
        foreach (Cube cube in startCubeList)
        {
            if(cube.cubeNumber != 1)
                cube.ToggleText(enabled);
        }
    }
}
