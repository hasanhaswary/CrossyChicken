using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] public float jumpDistance = 1f;
    [SerializeField] public float jumpHeight = 0.5f;
    [SerializeField] public float jumpDuration = 0.2f;

    [Header("UI")]
    [SerializeField] public Text terrainText;
    [SerializeField] public Text scoreText;
    [SerializeField] public GameObject gameOverPanel;
    [SerializeField] public GameObject levelCompletePanel;

    [Header("Audio & VFX")]
    [SerializeField] public AudioSource audioDeath;
    [SerializeField] public AudioSource audioGameWin;
    [SerializeField] public GameObject deathEffectPrefab;
    [SerializeField] public GameObject winEffectPrefab;

    [Header("Level")]
    [SerializeField] public int scoreToComplete = 30;

    private bool isMoving = false;
    private bool isDead = false;
    private bool onLog = false;
    private int score = 0;
    private float highestZ = 0f;

    private Transform currentLog = null;

    private void Start()
    {
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);
        UpdateScoreText();
        terrainText.text = "Terrain: Grass";
    }

    private void Update()
    {
        if (isMoving || isDead) return;

        Vector3 dir = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            dir = Vector3.forward;
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            dir = Vector3.left;
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            dir = Vector3.right;
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            dir = Vector3.back;

        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(dir);
            StartCoroutine(Jump(dir));
        }
    }

    private IEnumerator Jump(Vector3 dir)
    {
        isMoving = true;
        Vector3 start = transform.position;
        Vector3 end = start + dir * jumpDistance;

        float elapsed = 0f;
        while (elapsed < jumpDuration)
        {
            float t = elapsed / jumpDuration;
            float y = Mathf.Sin(t * Mathf.PI) * jumpHeight;
            transform.position = Vector3.Lerp(start, end, t) + Vector3.up * y;
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = end;
        isMoving = false;

        if (onLog && currentLog != null)
        {
            transform.SetParent(currentLog);
        }
        else
        {
            transform.SetParent(null);
        }

        // Only increase score if moving forward to a new row
        if (end.z > highestZ)
        {
            highestZ = end.z;
            score++;
            UpdateScoreText();
            if (score == scoreToComplete)
                OnLevelComplete();
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 2f))
        {
            string tag = hit.collider.tag;
            terrainText.text = $"Terrain: {tag}";

            // Kill if on river without a log
            if (tag == "River" && !onLog)
            {
                Die();
            }
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = $"Distance/Score: {score}";
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;
        Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        audioDeath.Play();
        gameOverPanel.SetActive(true);
        Destroy(gameObject);
    }

    private void OnLevelComplete()
    {
        isDead = true;
        Instantiate(winEffectPrefab, transform.position, Quaternion.identity);
        audioGameWin.Play();
        levelCompletePanel.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isDead) return;

        string tag = collision.collider.tag;

        // Die on Car or Train collision
        if (tag == "Car" || tag == "Train")
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag("Log"))
        {
            onLog = true;
            currentLog = other.transform;
            transform.SetParent(currentLog);
            terrainText.text = "Terrain: Log";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Log") && currentLog == other.transform)
        {
            onLog = false;
            currentLog = null;
            transform.SetParent(null);
        }
    }
}
