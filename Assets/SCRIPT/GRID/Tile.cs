using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject arrow;
    [HideInInspector] public Vector2Int gridPosition;

    [HideInInspector] public bool isVisited;
    [HideInInspector] public bool isPath;

    Renderer rend;

    Color defaultColor;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        defaultColor = rend.material.color;

        Transform arrowTransform = transform.Find("Arrow");

        if (arrowTransform != null)
        {
            arrow = arrowTransform.gameObject;
            arrow.SetActive(false);
        }
    }

    public void Highlight()
    {
        rend.material.color = Color.cyan;
    }

    public void StartTile()
    {
        rend.material.color = new Color(1f, 0.5f, 0f); // Orange
    }
    public void CheckpointTile()
    {
        rend.material.color = Color.yellow;
    }

    public void FinishTile()
    {
        rend.material.color = Color.green;
    }

    public void WrongTile()
    {
        Debug.Log("RED CALLED");

        rend.material.color = Color.red;
    }

    public void ResetTile()
    {
        rend.material.color = defaultColor;

        isVisited = false;
        isPath = false;
    }
    public void ShowArrow()
    {
        if (arrow != null)
            arrow.SetActive(true);
    }
    public void HideArrow()
    {
        if (arrow != null)
            arrow.SetActive(false);
    }
    public void SetArrowRotation(Vector3 rotation)
    {
        if (arrow != null)
            arrow.transform.localEulerAngles = rotation;
    } 
}