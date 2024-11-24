using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bonePrefab;
    public Transform boneSpawnPoint;
    public int boneCount = 10;

    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ThrowBone();
        }
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector2 moveDir = new Vector2(moveX, moveY).normalized;
        transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);

        if (moveDir != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void ThrowBone()
    {
        if (boneCount > 0)
        {
            Instantiate(bonePrefab, boneSpawnPoint.position, transform.rotation);
            boneCount--;
            // Update UI Counter Here
        }
    }
}
