using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    public int totalCheckpoints = 10;

    private int currentCheckpoint = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void ReachCheckpoint()
    {
        currentCheckpoint++;

        Debug.Log("Checkpoint : " + currentCheckpoint);

        if (currentCheckpoint >= totalCheckpoints)
        {
            Debug.Log("Spawn Finish Tile");
        }
        else
        {
            Debug.Log("Generate Next Path");
        }
    }

    public int GetCheckpointCount()
    {
        return currentCheckpoint;
    }
    void Start()
    {
        ReachCheckpoint();
    } 
}
