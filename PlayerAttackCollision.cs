using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClearSky;

public class PlayerAttackCollision : MonoBehaviour
{
    // Start is called before the first frame update


    float damage;
    int combo;
    void Start()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine("AutoDisable");
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        combo = (int)GetComponentInParent<DemoCollegeStudentController>().anim.GetFloat("Blend");
        
        if (other.CompareTag("Enemy"))
        {
            damage = GetComponentInParent<DemoCollegeStudentController>().Ad;
            damage = damage * ((combo *  0.5f) + 1.0f);
            
            other.GetComponent<MonsterController>().TakeDamage((int)damage, transform.position);

        }

        if (other.CompareTag("Boss"))
        {
            damage = GetComponentInParent<DemoCollegeStudentController>().Ad;
            damage = damage * ((combo * 0.5f) + 1.0f);

            other.GetComponent<BossController>().TakeDamage((int)damage, transform.position);

        }


    }
    private IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
