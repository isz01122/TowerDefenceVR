using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MyDrone : MonoBehaviour 
{
    NavMeshAgent agent;
    public int MAX_HP = 3;
    public int hp = 0;
    public float ATTACK_TIME = 2;
    float attackTime = 0;

    public AudioSource hitSound;

    public float ATTACK_DISTANCE = 1;

    void Start () 
    {
        // NavMeshAgent를 가져온다
        agent = GetComponent<NavMeshAgent>();

        // 타워 게임오브젝트를 가져온다 이때 오브젝트인지 오브젝츠인지 단/복수형 구분 주의
        GameObject tower = GameObject.FindGameObjectWithTag("Player");

        // 목적지를 타워의 위치로 지정해준다!!!
        agent.destination = tower.transform.position;

        hp = MAX_HP;
        attackTime = ATTACK_TIME;
	}
	

	void Update () 
    {
        if (agent.remainingDistance <= ATTACK_DISTANCE)
        {
            attackTime += Time.deltaTime;
            if (attackTime > ATTACK_TIME)
            {
                if (Tower.Instance.gameOver)
                {
                    return;
                }
                attackTime = 0;
                Tower.Instance.Damage();
                hitSound.Play();
            }
        }
    }
}
