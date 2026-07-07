using UnityEngine;
using System.Collections.Generic;

public class PathGenerator : MonoBehaviour
{
    public List<Vector2Int> GeneratePath(
     Vector2Int startPos,
     int width,
     int height,
     int pathLength,
     HashSet<Vector2Int> usedTiles)
    {
        for (int attempt = 0; attempt < 100; attempt++)
        {
            List<Vector2Int> path = new List<Vector2Int>();

            Vector2Int current = startPos;
            path.Add(current);

            while (path.Count < pathLength)
            {
                List<Vector2Int> possibleMoves = new List<Vector2Int>();

                Vector2Int[] directions =
                {
                Vector2Int.up,
                Vector2Int.down,
                Vector2Int.left,
                Vector2Int.right
            };

                foreach (Vector2Int dir in directions)
                {
                    Vector2Int next = current + dir;

                    if (IsValid(next, width, height, path, usedTiles))
                        possibleMoves.Add(next);
                }

                if (possibleMoves.Count == 0)
                    break;

                current = possibleMoves[Random.Range(0, possibleMoves.Count)];
                path.Add(current);
            }

            if (path.Count == pathLength)
                return path;
        }

        Debug.LogWarning("Couldn't generate full path!");

        return new List<Vector2Int>() { startPos };
    }

    bool IsValid(
    Vector2Int pos,
    int width,
    int height,
    List<Vector2Int> path,
    HashSet<Vector2Int> usedTiles)
    {
        if (pos.x < 0 || pos.x >= width)
            return false;

        if (pos.y < 0 || pos.y >= height)
            return false;

        if (path.Contains(pos))
            return false;

        if (usedTiles.Contains(pos))
            return false;

        return true;
    } 
}