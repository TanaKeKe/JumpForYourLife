using System.Collections.Generic;
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
        _objectPool.Add(obj);
    }

    public int AmountObjectInPool()
    {
        return _objectPool.Count;
    }

    public void GetObjectFromPool()
    {
        float rangeTopCamera = GamePlayController.Instance.GetRangeTopCamera();
        float rangeBottomCamera = GamePlayController.Instance.GetRangeBottomCamera();
        foreach (GameObject obj in _objectPool)
        {
            if (obj != null)
            {
                if (obj.transform.position.y <= rangeTopCamera && obj.transform.position.y >= rangeBottomCamera)
                {
                    obj.SetActive(true);
                }
                else
                {
                    Messenger.Broadcast<GameObject>(EventKey.SetOriginBlock, obj);
                    obj.SetActive(false);
                }
            }
            else
            {
                 //Debug.Log("Trong Pool có phần tử null!");
            }
        }
    }

    public void CheckOutCameraToResetPositionObject(float distanceObject)
    {
        float rangeTopCamera = GamePlayController.Instance.GetRangeTopCamera(); // lấy khoảng trên của camera
        foreach (GameObject obj in _objectPool) // tìm những object ở ngoài camera về phía trên
        {
            if (obj != null)
            {
                if (obj.transform.position.y > rangeTopCamera)
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
                //Debug.Log("Trong Pool có phần tử null!");
            }
        }

        //Debug.Log(_positionLowest);
        Vector3 position = obj.transform.position;
        position.y = _positionLowest - distanceObject;
        obj.transform.position = position; // đặt lại vị trí cho object
    }

    public List<GameObject> GetObjectPool()
    {
        return _objectPool;
    }
}