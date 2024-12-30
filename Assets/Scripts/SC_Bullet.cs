using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Bullet : MonoBehaviour
{
    public GameObject HitSpark;

    public SC_PlayerControl PlayerControl;

    public int bulletDamage;

    void Start()
    {
        Destroy(gameObject,10);
    }

    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.tag == "Bullet")
        {
            return;
        }
        if (collision.gameObject.CompareTag("Zombie"))
        {
            bool isDead = collision.gameObject.GetComponent<SC_Zombie>().TakeDamage(bulletDamage);
            if (isDead)
            {
                PlayerControl.Score += 1;
            }
        }

        //Instantiate(HitSpark, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
