using UnityEngine;
using System.Collections;
using UnityEngine.AI;

//NavMeshAgent컴파일 에러가 나는경우 : NavMeshAgent가 없을때
//AI에 있는것이므로 using을 해주어야 한다!!!
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class Drone : MonoBehaviour {
	UnityEngine.AI.NavMeshAgent agent;
	Transform tower;
	public float ATTACK_TIME = 2;
	float attackTime = 0;
	public int MAX_HP = 3;
	[System.NonSerialized]
	public int hp = 0;

	public float ATTACK_DISTANCE = 1;
	// Use this for initialization
	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		tower = GameObject.Find("Tower").transform;
		agent.destination = tower.position;

		hp = MAX_HP;
		attackTime = ATTACK_TIME;
	}


	void Update()
	{
		
		if(agent.remainingDistance <= ATTACK_DISTANCE)
		{
			attackTime += Time.deltaTime;
			if(attackTime > ATTACK_TIME)
			{
				attackTime = 0;
				Tower.Instance.Damage();
			}
		}
	}
}
