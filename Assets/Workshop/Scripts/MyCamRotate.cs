using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamRotate : MonoBehaviour 
{

    public float sensitive = 500f;
    float rotateX;
    float rotateY;

	void Start () 
    {
		
	}
	
	void Update () 
    {
        //receive mouse input axis x and y
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        rotateX += x * sensitive * Time.deltaTime;
        rotateY += y * sensitive * Time.deltaTime;
        //회전이 돌아가지 않게 하기 위해서 각도를 제한해 준다
        rotateY = Mathf.Clamp(rotateY, -60, 60);
        
        //왼쪽 위가 (0,0) 기준이다
        //y축을 뒤집어 주어야 한다
        //마우스 x값 회전은 y축에 대한 회전이다
        //한번 회전 되고나서 그다음 회전은 한번 회전한 것을 기준으로 그 다음작업이니
        //누적을 시켜주어햐 한다!
        transform.eulerAngles = new Vector3(-rotateY, rotateX, 0);

    }

}
