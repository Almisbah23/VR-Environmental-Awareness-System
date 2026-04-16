using UnityEngine;

public class TreeWindMotion : MonoBehaviour
{
    [Header("Wind Settings")]
    public float swayAmount = 5f;       // how much it sways (degrees)
    public float swaySpeed = 1.0f;      // how fast it sways
    public float randomOffset = 0f;     // to make each tree unique

    private Quaternion startRotation;

    void Start()
    {
        startRotation = transform.localRotation;
        randomOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    void Update()
    {
        float angle = Mathf.Sin(Time.time * swaySpeed + randomOffset) * swayAmount;
        transform.localRotation = startRotation * Quaternion.Euler(angle, 0, 0);
    }
}
