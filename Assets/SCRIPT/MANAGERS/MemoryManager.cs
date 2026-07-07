using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MemoryManager : MonoBehaviour
{
    [Header("References")]
    public GridManager gridManager;
    public PathGenerator pathGenerator;
    public PlayerMovement playerMovement;

    [Header("Game Settings")]
    public int pathLength = 7;
    public float showTime = 5f;

    private List<Vector2Int> currentPath = new List<Vector2Int>();
    private HashSet<Vector2Int> usedTiles = new HashSet<Vector2Int>();

    private int currentStep = 0;
    private Vector2Int currentStartPosition;

    private int currentCheckpoint = 0;
    public int totalCheckpoints = 10;
    bool isFinalPath = false;

    void Awake()
    {
        if (gridManager == null)
            gridManager = FindAnyObjectByType<GridManager>();

        if (pathGenerator == null)
            pathGenerator = FindAnyObjectByType<PathGenerator>();

        if (playerMovement == null)
            playerMovement = FindAnyObjectByType<PlayerMovement>();
    }

    IEnumerator Start()
    {
        currentStartPosition = Vector2Int.zero;
        playerMovement.canMove = false;

        yield return new WaitForSeconds(0.2f);

        StartCoroutine(StartRound());
    }
    IEnumerator StartRound()
    {
        playerMovement.canMove = false;

        foreach (Tile tile in gridManager.grid)
        {
            tile.ResetTile();
        }

        currentPath = pathGenerator.GeneratePath(
            currentStartPosition,
            gridManager.width,
            gridManager.height,
            pathLength,
            usedTiles
        );

        currentStep = 0;

        Debug.Log("Path Generated : " + currentPath.Count);

        // Highlight path
        for (int i = 0; i < currentPath.Count; i++)
        {
            Tile tile = gridManager.grid[currentPath[i].x, currentPath[i].y];

            tile.isPath = true;

            // START TILE
            if (i == 0)
            {
                tile.StartTile();
            }
            // FINISH TILE
            else if (i == currentPath.Count - 1)
            {
                if (isFinalPath)
                    tile.FinishTile();
                else
                    tile.CheckpointTile();
            }
            // MIDDLE TILES
            else
            {
                tile.Highlight();
            }

            // ---------- SHOW ARROWS ----------
            if (i < currentPath.Count - 1)
            {
                Vector2Int dir = currentPath[i + 1] - currentPath[i];

                tile.ShowArrow();

                if (dir == Vector2Int.right)
                    tile.SetArrowRotation(new Vector3(90, 0, 0));

                else if (dir == Vector2Int.left)
                    tile.SetArrowRotation(new Vector3(90, 180, 0));

                else if (dir == Vector2Int.up)
                    tile.SetArrowRotation(new Vector3(90, 90, 0));

                else if (dir == Vector2Int.down)
                    tile.SetArrowRotation(new Vector3(90, -90, 0));
            
                    Debug.Log("Direction = " + dir);
            }
        }
        // Countdown
        if (CountdownUI.Instance != null)
        {
            for (int i = (int)showTime; i >= 1; i--)
            {
                CountdownUI.Instance.ShowNumber(i);

                yield return new WaitForSeconds(1f);
            }

            CountdownUI.Instance.Hide();
        }
        else
        {
            yield return new WaitForSeconds(showTime);
        }

        // Hide path
        foreach (Vector2Int pos in currentPath)
        {
            Tile tile = gridManager.grid[pos.x, pos.y];

            tile.HideArrow();
            tile.ResetTile();
        } 

        // Keep start & finish visible
        // Keep start & finish visible

        Tile startTile =
        gridManager.grid[currentPath[0].x, currentPath[0].y];

        Tile finishTile =
        gridManager.grid[currentPath[currentPath.Count - 1].x,
        currentPath[currentPath.Count - 1].y];

        startTile.StartTile();

        finishTile.FinishTile();


        // Spawn player exactly on start tile

        Vector3 spawnPos = startTile.transform.position;

        spawnPos.y = playerMovement.transform.position.y;

        playerMovement.transform.position = spawnPos;

        currentStep = 1;
        startTile.isVisited = true;

        playerMovement.canMove = true;
    
    }

    public void PlayerTouchedTile(Tile tile)
    {
        if (!playerMovement.canMove)
            return;

        if (currentStep >= currentPath.Count)
            return;

        if (tile.isVisited)
            return;

        Vector2Int expected = currentPath[currentStep];

        Debug.Log("Expected : " + expected);
        Debug.Log("Player : " + tile.gridPosition);

        // Ignore already completed tiles
        if (tile.isVisited)
            return;

        // Ignore touching previous path tiles again
        if (currentStep > 0 &&
            tile.gridPosition == currentPath[currentStep - 1])
            return;

        if (tile.gridPosition != expected)
        {
            Debug.Log("WRONG TILE");

            tile.WrongTile();

            if (LifeManager.Instance != null)
                LifeManager.Instance.LoseLife();

            return;
        }

        tile.isVisited = true;

        currentStep++;

        if (currentStep >= currentPath.Count)
        {
            foreach (Vector2Int pos in currentPath)
            {
                usedTiles.Add(pos);
            }

            currentCheckpoint++;

            currentStartPosition =
                currentPath[currentPath.Count - 1];

            Debug.Log("Checkpoint : " + currentCheckpoint);

            playerMovement.canMove = false;

            StartCoroutine(CheckpointRoutine());
        }
    }

    IEnumerator CheckpointRoutine()
    {
        string[] quotes =
        {
            "Every journey begins with one step.",
            "Trust what fades first.",
            "Fear remembers more than you.",
            "The wrong path teaches faster.",
            "You are closer than you think.",
            "Not every memory tells the truth.",
            "Shadows never forget.",
            "Don't chase the light.",
            "The end remembers you.",
            "You found yourself."
        };

        if (QuoteUI.Instance != null)
        {
            yield return StartCoroutine(
                QuoteUI.Instance.ShowQuote(
                    quotes[Mathf.Clamp(currentCheckpoint - 1, 0, quotes.Length - 1)]
                ));
        }

        if (currentCheckpoint >= totalCheckpoints)
        {
            isFinalPath = true;

            currentStartPosition = currentPath[currentPath.Count - 1];

            StartCoroutine(StartRound());

            yield break;
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(StartRound());
    }
}