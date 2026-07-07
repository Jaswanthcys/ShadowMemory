using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 20;
    public int height = 20;

    public float tileSpacing = 1.1f;

    public GameObject tilePrefab;

    public Tile[,] grid;

    void Awake()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        grid = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 pos =
                    new Vector3(
                        x * tileSpacing,
                        0.5f,
                        z * tileSpacing
                    );

                GameObject obj =
                    Instantiate(
                        tilePrefab,
                        pos,
                        Quaternion.identity,
                        transform
                    );

                Tile tile = obj.GetComponent<Tile>();

                tile.gridPosition =
                    new Vector2Int(x, z);

                grid[x, z] = tile;
            }
        }

        Debug.Log("Grid Ready");
    }
}