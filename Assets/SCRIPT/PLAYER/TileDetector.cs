using UnityEngine;

public class TileDetector : MonoBehaviour
{
    private MemoryManager memoryManager;

    void Start()
    {
        memoryManager = FindAnyObjectByType<MemoryManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENTER : " + other.name);

        if (memoryManager == null)
        {
            Debug.Log("MemoryManager NULL");
            return;
        }

        Debug.Log("canMove = " + memoryManager.playerMovement.canMove);

        Tile tile = other.GetComponent<Tile>();

        if (tile == null)
        {
            Debug.Log("Tile NULL");
            return;
        }

        Debug.Log("Calling PlayerTouchedTile");

        memoryManager.PlayerTouchedTile(tile);
    }
}