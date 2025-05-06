using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField, Tooltip("World‑space X at which this train should be destroyed (positive side).")]
    private float destroyBoundaryX = 28f;

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
