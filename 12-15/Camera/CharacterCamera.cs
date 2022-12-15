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

    private void Camera_Init()//ī�޶� �� �ʱ�ȭ
    { 
    }

    internal void Camera_MoveCoroutine(Transform tr_charac)//ī�޶� �̵� �ڷ�ƾ ����
    {
        float t = Time.deltaTime / 1.5f;
        Transform character = tr_charac;
        CameraCurrent       = character.position;
        mainCamera.transform.position = Vector3.Lerp(CameraCurrent, character.position, t);
        Camera_WideOutCheck(character.position);
    }

    private void Camera_WideOutCheck(Vector2 characterPose) //ī�޶� ȭ�� ������ �������� Ȯ��
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
