using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private List<GameObject> _objectPool;
    private float _positionLowest;
    private void Awake()
    {
        _objectPool = new List<GameObject>();
        _positionLowest = 5.1f;

    }

    public void AddObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        _objectPool.Add(obj);

    }

    public int AmountObjectInPool()
    {
        return _objectPool.Count;
    }

    public void GetObjectFromPool(float lengthObject)
    {
        float rangeTopCamera = GameController.Instance.GetRangeTopCamera();
        float rangeBottomCamera = GameController.Instance.GetRangeBottomCamera();
        foreach (GameObject obj in _objectPool)
        {
            if (obj != null)
            {
                if (obj.transform.position.y - lengthObject <= rangeTopCamera && obj.transform.position.y + lengthObject >= rangeBottomCamera)
                {
                    obj.SetActive(true);
                }
            }
            else
            {
                Debug.Log("Trong Pool có phần tử null!");
            }
        }
    }

    public void CheckOutCameraToResetPositionObject(float lengthObject, float distanceObject)
    {
        float rangeTopCamera = GameController.Instance.GetRangeTopCamera(); // lấy khoảng trên của camera
        foreach (GameObject obj in _objectPool) // tìm những object ở ngoài camera về phía trên
        {
            if (obj != null)
            {
                if (obj.transform.position.y - lengthObject > rangeTopCamera)
                {
                    obj.SetActive(false); // ẩn obj đi
                    ResetPositionObject(obj, distanceObject);
                }
            }
        }


    }

    private void ResetPositionObject(GameObject obj, float distanceObject)
    {
        foreach (GameObject objIndex in _objectPool) // tìm vị trí thấp nhất của các object
        {
            if (objIndex != null)
            {
                if (_positionLowest > objIndex.transform.position.y) _positionLowest = objIndex.transform.position.y;
            }
            else
            {
                Debug.Log("Trong Pool có phần tử null!");
            }
        }
        Debug.Log(_positionLowest);
        Vector3 position = obj.transform.position;
        position.y = _positionLowest - distanceObject;
        obj.transform.position = position; // đặt lại vị trí cho object
    }

    public List<GameObject> GetObjectPool()
    {
        return _objectPool;
    }
}
