using UnityEngine;

public class BlockMover : MonoBehaviour
{
    private float targetZ = -160f;
    private float speed = 1f;
    //public static bool playMusic = false;
    BeatSpawner beatSpawner;

    public void Initialize(float targetZPosition, float moveSpeed)
    {
        if (moveSpeed <= 0)
        {
            Debug.LogError("Move speed must be a positive value! Provided speed: " + moveSpeed);
            return;
        }

        targetZ = targetZPosition;
        speed = moveSpeed;

        //Debug.Log("BlockMover Initialized - Target Z: " + targetZ + ", Speed: " + speed);
    }

    private void Start()
    {
        beatSpawner = FindAnyObjectByType<BeatSpawner>();
    }

    void Update()
    {
        if (speed <= 0)
        {
            Debug.LogError("Move speed is not set or is invalid!");
            return;
        }

        //Debug.Log("Block Position: " + transform.position.z + ", Target Z: " + targetZ); // Debug log

        // Move the block toward the target Z position
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            Mathf.MoveTowards(transform.position.z, -160f, speed * Time.deltaTime)
        );

        // Check if the block has reached the target Z position
        if (transform.position.z <= targetZ)
        {
            BeatSpawner.playM = true;
            //playMusic = true;
            //Debug.Log("Block reached player position!");
        }
        if(transform.position.z <= -160f)
        {
            beatSpawner.DecreaseProgress(10f);
            Destroy(this.gameObject);
        }
    }
}