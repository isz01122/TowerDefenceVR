using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySpawn : MonoBehaviour 
{
    //이부분은 원래 코루틴을 사용하면 훨씬 더 간단하다!!
    public GameObject drone;
    public Transform[] spawnPoints;
    public float spawnTime = 1.0f;
    float deltaTime = 0.0f;


	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        //시간을 누적시켜준다
        deltaTime += Time.deltaTime;

        if (deltaTime > spawnTime)
        {
            SpawnDrone();
            //다시 시간을 0으로 바꾸어준다
            deltaTime = 0.0f;
        }
    }

    void SpawnDrone()
    {
        //위치값으로 넣어주어야함, position을 넣지 않으면 transform이므로 오류가 난다!!!
        Vector3 spawnPos = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

        //게임오브젝트(프리펩) 생성 함수 Instantiate!!!
        //무엇을, 어디에, 어떤 방향으로 넣어줄지 결정한다
        Instantiate(drone, spawnPos, Quaternion.identity);
    }
}
