using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public bool canMove = false;

    void Update()
    {
        if (!canMove)
            return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(h, 0, v).normalized;

        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    public void LockPlayer()
    {
        canMove = false;
    }

    public void UnlockPlayer()
    {
        canMove = true;
    }
}