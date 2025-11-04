using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float minX, maxX;
    [SerializeField] float minY, maxY;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void FixedUpdate()
    {
    if (player.position.x >= transform.position.x || player.position.y >= transform.position.y)
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);

    transform.position = new Vector3(
        Mathf.Clamp(transform.position.x, minX, maxX),
        Mathf.Clamp(player.position.y, minY, maxY),
        transform.position.z
    );
    }

}
