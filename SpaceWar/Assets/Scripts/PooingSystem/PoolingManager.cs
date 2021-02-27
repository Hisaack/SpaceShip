using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    private static PoolingManager _instance;
    public static PoolingManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public GameObject UseObject(GameObject obj, Vector3 pos, Quaternion rot)
    {
        return Instantiate(obj, pos, rot);
    }

    public void ReturnObject(GameObject obj)
    {
        Destroy(obj);
    }
  
}
