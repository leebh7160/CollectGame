using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera
{
    private Camera mainCamera;
    private Vector2 CameraCurrent;
    private Vector2 CameraReset;
    //private Transform character;

    public CharacterCamera()
    {
        //CameraCurrent = character.position;
        mainCamera = Camera.main;
    }

    private void Camera_Init()//카메라 값 초기화
    { 
    }

    internal void Camera_MoveCoroutine(Transform tr_charac)//카메라 이동 코루틴 실행
    {
        float t = Time.deltaTime / 1.5f;
        Transform character = tr_charac;
        CameraCurrent       = character.position;
        mainCamera.transform.position = Vector3.Lerp(CameraCurrent, character.position, t);
        Camera_WideOutCheck(character.position);
    }

    private void Camera_WideOutCheck(Vector2 characterPose) //카메라 화면 밖으로 나가는지 확인
    {
        Vector2 wideOutCheck = characterPose;

        if (wideOutCheck.y < -5f)
            wideOutCheck.y = -5f;

        if(wideOutCheck.x > 10f)
        {
            wideOutCheck.x = 10f;
            mainCamera.transform.position = wideOutCheck;
        }
        if (wideOutCheck.x < -10f)
        {
            wideOutCheck.x = -10f;
            mainCamera.transform.position = wideOutCheck;
        }
    }
}
