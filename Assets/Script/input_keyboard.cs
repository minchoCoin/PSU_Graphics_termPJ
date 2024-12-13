using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class input_keyboard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("GetKeyDown : A");
        }
        else if(Input.GetKeyUp(KeyCode.S))
        {
            Debug.Log("GetKeyUp : S");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("GetKey : D");
        }
    }
}
