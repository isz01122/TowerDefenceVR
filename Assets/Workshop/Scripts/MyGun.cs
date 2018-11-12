using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyGun : MonoBehaviour 
{
    public GameObject bulletEffect;
    public GameObject explosionEffect;
    public GameObject gameoverText;
    public float shootTime = 1.0f;

    public Text scoreText;
    float score;
    float deltaTime;
    float reloadTime;
    bool lookAtGameOver = false;

    void Start () 
    {

	}
	
	void Update () 
    {
        //시간을 누적시켜준다!!
        deltaTime += Time.deltaTime;

        //총이 자동사격으로 변
        if (deltaTime > shootTime)
        {
            Shoot();
            deltaTime = 0f;
        }

        Restart();

        if (lookAtGameOver)
        {
            reloadTime += Time.deltaTime;
            if (0.5f <= reloadTime && reloadTime < 1.0f)
                gameoverText.GetComponent<Text>().text = "Restarting.";
            if (1.0f <= reloadTime && reloadTime < 1.5f) 
                gameoverText.GetComponent<Text>().text = "Restarting..";
            if (1.5f <= reloadTime)
            {
                gameoverText.GetComponent<Text>().text = "Restarting...";
                if (2.0f <= reloadTime) 
                    Tower.Instance.restart = true;
            }
        }
    }

    void Restart()
    {
        //시작지점과 방향이 필요함! 카메라 위치로 부터 z방향 forward로 반직선을 쏜다
        Ray ray = new Ray(Camera.main.transform.position,
                          Camera.main.transform.forward);

        RaycastHit hitInfo;

        //만약 맞았다면
        if (Physics.Raycast(ray, out hitInfo, 1000f))
        {
            //1.맞은놈이 GameOver이면 3초동안 시간 누적 후에 신을 리로드한다
            if (hitInfo.transform.tag == "GameOver")
            {
                lookAtGameOver = true;
                gameoverText.GetComponent<Text>().text = "Restarting";
            }
            else
            {
                lookAtGameOver = false;
                gameoverText.GetComponent<Text>().text = "GAME OVER";
                reloadTime = 0.0f;
            }
        }
    }

    void Shoot()
    {
        //시작지점과 방향이 필요함! 카메라 위치로 부터 z방향 forward로 반직선을 쏜다
        Ray ray = new Ray(Camera.main.transform.position,
                          Camera.main.transform.forward);

        RaycastHit hitInfo;

        //만약 맞았다면
        if (Physics.Raycast(ray, out hitInfo, 1000f))
        {

            //게임오버이면 아무작업을 하지 않기위해 방어코드 작성
            if (Tower.Instance.gameOver || hitInfo.transform.tag == "EmptyGround")
            {
                return;
            }

            //1.파티(이펙트) 위치를 맞은 위치로클 옮긴다
            bulletEffect.transform.position = hitInfo.point;
            //2.파티클 재생
            bulletEffect.GetComponent<ParticleSystem>().Stop();
            bulletEffect.GetComponent<ParticleSystem>().Play();
            //3.오디오 재생
            bulletEffect.GetComponent<AudioSource>().Stop();
            bulletEffect.GetComponent<AudioSource>().Play();

            //4.맞은놈이 드론이면 파괴한다
            if (hitInfo.transform.tag == "Drone")
            {
               
                //(히트임포 정보, 트랜스폼 = 맞은놈)의 게임오브젝트를 
                Destroy(hitInfo.transform.gameObject);

                //1.파티(이펙트) 위치를 맞은 위치로클 옮긴다
                explosionEffect.transform.position = hitInfo.point;

                //2.파티클 재생
                explosionEffect.GetComponent<ParticleSystem>().Stop();
                explosionEffect.GetComponent<ParticleSystem>().Play();
                //3.오디오 재생
                explosionEffect.GetComponent<AudioSource>().Stop();
                explosionEffect.GetComponent<AudioSource>().Play();

                //적이 죽을때 마다 스코어가 하나씩 증가한다
                ++score;
                scoreText.text = "Score : " + score;

            }

        }

    }
}
