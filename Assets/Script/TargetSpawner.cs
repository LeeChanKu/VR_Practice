using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner: MonoBehaviour
{
    public GameObject prefabToSpawn; // ������ ������
    public List<GameObject> spawnLocationObjects = new List<GameObject>(); //���� ����Ʈ ����Ʈ
    public int initialSpawnCount = 5; //������ ������Ʈ ����
    

    private void Start()
    {
        // �ʱ⿡ ������ ������ŭ ������Ʈ�� ����
        for (int i = 0; i < initialSpawnCount; i++)
        {
            SpawnObject();
        }
    }

    public void Spwan()
    {
        for (int i = 0; i < initialSpawnCount; i++)
        {
            SpawnObject();
        }
    }




    // ������Ʈ�� �����ϰ� ��ġ�� �����ϴ� �Լ�
    public void SpawnObject()
    {
        // �����ϰ� ��ġ�� �����ϰų� ���ϴ� ������� ��ġ�� ����
        GameObject spawnLocationObject = GetRandomSpawnLocationObject();
        Vector3 spawnPosition = spawnLocationObject.transform.position;

        // �������� �����ϰ�, ������ ������Ʈ�� ������ ����
        GameObject clonedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

    }

    // �����ϰ� ��ġ�� �����ϴ� �Լ�
    GameObject GetRandomSpawnLocationObject()
    {
        // ���� ��� �� ���ϴ� ������� ��ġ ������Ʈ�� �����ϰų� ������ �� �ֽ��ϴ�.
        int randomIndex = Random.Range(0, spawnLocationObjects.Count);
        return spawnLocationObjects[randomIndex];
    }

    // ������Ʈ�� �ı��Ǿ��� �� ȣ��Ǵ� �ݹ� �Լ�
    private void OnDestroy()
    {
        initialSpawnCount--;
    }
}
