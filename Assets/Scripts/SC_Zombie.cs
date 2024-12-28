using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SC_Zombie : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    private Animator animator;
    private NavMeshAgent agent;
    public bool isDead = false;
    public GameObject zombieHand;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0)
        {
            animator.SetTrigger("DIE1");
            //GetComponent<Collider>().enabled = false;
            zombieHand.GetComponent<Collider>().enabled = false;
            isDead = true;
            //DIE2 has bug
            Destroy(gameObject, 10f);

        }
        else
        {
            animator.SetTrigger("DAMAGE");
        }

    }
    
    // Update is called once per frame
    /*
    void Update()
    {
        if (animator != null && agent!=null)
        {
            if (agent.velocity.magnitude > 0) 
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
        
    }
    */
}
