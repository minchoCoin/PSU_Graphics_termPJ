using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class event_update : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update print!");
    }

    private void LateUpdate()
    {
        Debug.Log("LateUpdate print!");
    }

    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate print!");
    }

}
