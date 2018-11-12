using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour {

	public static Tower Instance;
    public GameObject Spawn;
    public GameObject mainCamera;

    //HP를 표시할 슬라이더를 받아옴
    public Slider hpSlider;

    public AudioSource dieSound;

    public int MAX_HP = 10;
	int hp = 0;
    public bool gameOver = false;
    public GameObject die;
    public bool restart = false;


	void Awake()
	{
        
		if(Instance == null)
			Instance = this;
	}

	void Start()
	{
        
		hp = MAX_HP;
	}

    private void Update()
    {
        if ((gameOver && restart) || (gameOver && Input.GetMouseButtonDown(0)))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void Damage()
	{
        //데미지를 입으면 피가 깍임
		hp--;

        //슬라이더에 HP를 세팅한다
        hpSlider.value = hp;


        //피가 0이하이면 사망
		if(hp <= 0)
		{
            gameOver = true;
            dieSound.Play();

            if(die)
            {
                die.SetActive(true);
                Spawn.SetActive(false);
			}
		}
	}
}
