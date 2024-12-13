using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class event_initialize : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Log("Awake print!");
    }

    void Start()
    {
        Debug.Log("Start print!");
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable print!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
