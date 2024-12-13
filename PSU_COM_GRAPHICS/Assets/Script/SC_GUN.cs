using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SC_GUN : MonoBehaviour
{
    [SerializeField]
    float bulletSpeed = 5f;
    [SerializeField]
    float bulletLifetime = 5f;
    [SerializeField]
    public Vector3 bulletScale = new Vector3(0.2f, 0.2f, 0.2f);



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBullet();
        }
    }


    void SpawnBullet()
    {
        GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        bullet.transform.position = this.transform.position;
 
        bullet.transform.localScale = bulletScale;

        bullet.AddComponent<Bullet>().SetSpeed(bulletSpeed);

        SphereCollider sc = bullet.GetComponent<SphereCollider>();
        sc.radius = 0.2f;
        sc.isTrigger = true;
        Destroy(bullet, bulletLifetime);
    }
}

public class Bullet : MonoBehaviour
{
    private float speed;

    public void SetSpeed(float bulletSpeed)
    {
        speed = bulletSpeed;

    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject hitObject = collision.gameObject;

        Debug.Log("object hit!!!");
        Destroy(hitObject);
        Destroy(this.gameObject);
    }

}
