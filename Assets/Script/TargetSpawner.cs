using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner: MonoBehaviour
{
    public GameObject prefabToSpawn; // 복제할 프리팹
    public List<GameObject> spawnLocationObjects = new List<GameObject>(); //스폰 포인트 리스트
    public int initialSpawnCount = 5; //복제할 오브젝트 개수
    

    private void Start()
    {
        // 초기에 지정된 개수만큼 오브젝트를 복제
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




    // 오브젝트를 복제하고 위치를 갱신하는 함수
    public void SpawnObject()
    {
        // 랜덤하게 위치를 선택하거나 원하는 방식으로 위치를 설정
        GameObject spawnLocationObject = GetRandomSpawnLocationObject();
        Vector3 spawnPosition = spawnLocationObject.transform.position;

        // 프리팹을 복제하고, 복제된 오브젝트를 변수에 저장
        GameObject clonedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

    }

    // 랜덤하게 위치를 선택하는 함수
    GameObject GetRandomSpawnLocationObject()
    {
        // 여러 방법 중 원하는 방법으로 위치 오브젝트를 선택하거나 수정할 수 있습니다.
        int randomIndex = Random.Range(0, spawnLocationObjects.Count);
        return spawnLocationObjects[randomIndex];
    }

    // 오브젝트가 파괴되었을 때 호출되는 콜백 함수
    private void OnDestroy()
    {
        initialSpawnCount--;
    }
}
