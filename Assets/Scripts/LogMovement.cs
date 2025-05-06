using UnityEngine;

public class LogMovement : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 2f;
    [SerializeField, Tooltip("World‑space X at which this log should be destroyed (positive side).")]
    private float destroyBoundaryX = 24f;

    [HideInInspector] public Vector3 moveDirection = Vector3.forward;

    private void Update()
    {
        transform.Translate(moveDirection * baseSpeed * DifficultyManager.SpeedMultiplier * Time.deltaTime,
                            Space.World);

        if (moveDirection.x > 0 && transform.position.x > destroyBoundaryX)
            Destroy(gameObject);
        if (moveDirection.x < 0 && transform.position.x < -destroyBoundaryX)
            Destroy(gameObject);
    }
}
