using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float minY = -2.5f;
    public float maxY = 2.5f;

    private float spawnInterval = 3f;
    private float timer = 0f;
    private float playerSpeed = 5f;

    private void Update()
    {
        if (Time.timeScale == 0f) return;
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;

            Vector3 spawnPos = transform.position;
            spawnPos.y = Random.Range(minY, maxY); 

            GameObject obs = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
        }
    }

    public void SetPlayerSpeed(float speed)
    {
        playerSpeed = speed;
        spawnInterval = Mathf.Max(1f, 15f / speed);
    }
}