using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coordinate : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(this.transform.position);
        Debug.Log(this.transform.rotation);
        Debug.Log(this.transform.localScale);
        
        Debug.Log(obj.transform.position);
        Debug.Log(obj.transform.rotation);
        Debug.Log(obj.transform.localScale);
    }
}
