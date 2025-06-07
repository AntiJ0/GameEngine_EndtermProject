using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;

    public float baseInterval = 3f;         // �⺻ ����
    public float referenceSpeed = 5f;       // ���� �ӵ�
    public float minInterval = 0.8f;        // �ʹ� ������ �����Ǵ� �� ����

    private float currentPlayerSpeed = 5f;  // Player���� ���޹��� ���� �ӵ�

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
            interval = Mathf.Max(interval, minInterval); // �ּ� ���� ����

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

        // �ӵ� ����
        float moveSpeed = currentPlayerSpeed * 2f; // �÷��̾� �ӵ��� 2��� ���� ����
        newObstacle.GetComponent<ObstacleMover>()?.SetSpeed(moveSpeed);
    }
}