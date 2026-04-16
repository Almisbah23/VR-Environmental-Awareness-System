using UnityEngine;

public class FarmerWander : MonoBehaviour
{
    public float walkRadius = 3f;     // how far farmer can move
    public float walkSpeed = 0.6f;    // slow = sad
    public float waitTime = 2f;       // pause time

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float waitTimer;

    void Start()
    {
        startPosition = transform.position;
        SetNewTarget();
    }

    void Update()
    {
        // move towards target
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            walkSpeed * Time.deltaTime
        );

        // reached target → wait
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= waitTime)
            {
                SetNewTarget();
                waitTimer = 0f;
            }
        }
    }

    void SetNewTarget()
    {
        Vector2 randomPoint = Random.insideUnitCircle * walkRadius;
        targetPosition = startPosition + new Vector3(randomPoint.x, 0, randomPoint.y);

        // face direction of movement
        transform.LookAt(new Vector3(
            targetPosition.x,
            transform.position.y,
            targetPosition.z
        ));
    }
}
