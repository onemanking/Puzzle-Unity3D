using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Cube : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private TextMesh cubeText;
    public int cubeNumber;
    private GridManager gridManager;
    private Animator anim;
    private bool isHover;

    // Use this for initialization
    void Start()
    {
        gridManager = GameObject.FindWithTag("Grid").GetComponent<GridManager>();
        anim = GetComponent<Animator>();
        anim.enabled = false;
        SetNumber(gridManager.RandomNumber());
    }

    void Update() {
        if (isHover)
        {
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, 1.1f, Time.deltaTime*10), Mathf.Lerp(transform.localScale.y, 1.1f, Time.deltaTime*10), 1);
        }
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
            isHover = false;
            
            StartCoroutine(PlayClickAndWait());
            
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
        isHover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHover = false;
        transform.localScale = Vector2.one;
    }

    void PlayAnimation(string animationString)
    {
        anim.SetBool(animationString, true);
    }

    IEnumerator PlayClickAndWait()
    {
        anim.enabled = true;
        anim.SetTrigger("clicked");

        cubeText.GetComponent<Renderer>().sortingOrder = 1;

        while (anim.GetCurrentAnimatorStateInfo(0).length > anim.GetCurrentAnimatorStateInfo(0).normalizedTime)
        {
            yield return null;
        }

        cubeText.GetComponent<Renderer>().sortingOrder = -10;

        yield return new WaitForEndOfFrame();

        Destroy(gameObject);
    }

}
