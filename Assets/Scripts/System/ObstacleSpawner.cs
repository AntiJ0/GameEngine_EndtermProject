using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;

    public float baseInterval = 3f;         // 기본 간격
    public float referenceSpeed = 5f;       // 기준 속도
    public float minInterval = 0.8f;        // 너무 빠르게 생성되는 것 방지

    private float currentPlayerSpeed = 5f;  // Player에서 전달받을 실제 속도

    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnObstacle();

            float interval = baseInterval * (referenceSpeed / currentPlayerSpeed);
            interval = Mathf.Max(interval, minInterval); // 최소 간격 보장

            yield return new WaitForSeconds(interval);
        }
    }

    public void SetPlayerSpeed(float speed)
    {
        currentPlayerSpeed = speed;
    }

    void SpawnObstacle()
    {
        Vector3 camRight = mainCam.ViewportToWorldPoint(new Vector3(1, 0.5f, 0));
        float spawnX = camRight.x + 1f;
        float spawnY = Random.Range(-2.7f, 3.0f);

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);
        GameObject newObstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);

        // 속도 전달
        float moveSpeed = currentPlayerSpeed * 2f; // 플레이어 속도의 2배로 예시 설정
        newObstacle.GetComponent<ObstacleMover>()?.SetSpeed(moveSpeed);
    }
}