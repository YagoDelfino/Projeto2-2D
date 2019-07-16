using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    public float destroyTimer = 3;
    void Start()
    {
        Destroy(gameObject, destroyTimer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
