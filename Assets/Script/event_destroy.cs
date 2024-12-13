using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class event_destroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Debug.Log("OnDestroy print!");
    }

    private void OnApplicationQuit()
    {
        Debug.Log("OnApplicationQuit print!");
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable print!");
    }
}
