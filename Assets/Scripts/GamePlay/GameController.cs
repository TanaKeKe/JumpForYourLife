using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController : Singleton<GameController>
{
    [SerializeField] private Camera myCamera;
    [SerializeField] private GameObject bars;
    [SerializeField] private float rangeCamera;
    [SerializeField] private float countChangeCamera;
    [Space(10)]
    private Vector3 _targetPosition;
    public void ChangePositionCamera(float distanceJump)
    {
        Debug.Log("Thay đổi camera" + distanceJump);
        _targetPosition = myCamera.transform.position + Vector3.down * (distanceJump);
        StartCoroutine(CoroutineSmooth());
        
    }

    private IEnumerator CoroutineSmooth()
    {
        while (myCamera.transform.position.y > _targetPosition.y)
        {
            myCamera.transform.position += Vector3.down * countChangeCamera;
            if (myCamera.transform.position.y == _targetPosition.y) break;
            yield return null;
        }
    }

    
    public void ChangePositionBars(float distanceJump)
    {
        Debug.Log("Thay đổi vị trí của 2 thanh tắt trigger" + bars.transform.position);
        bars.transform.position += (Vector3.down * distanceJump);
    }

    public float GetRangeTopCamera()
    {
        return myCamera.transform.position.y + rangeCamera;
    }

    public float GetRangeBottomCamera()
    {
        return myCamera.transform.position.y - rangeCamera;
    }
}
