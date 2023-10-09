using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClearSky;

public class SkillScript : MonoBehaviour
{
    // Start is called before the first frame update


    float damage;
    int combo;
    public float Dmg;
    public float Dmg_time;
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
            damage = damage * Dmg;

            other.GetComponent<MonsterController>().TakeDamage((int)damage, transform.position);

        }

        if (other.CompareTag("Boss"))
        {
            damage = GetComponentInParent<DemoCollegeStudentController>().Ad;
            damage = damage * Dmg;

            other.GetComponent<BossController>().TakeDamage((int)damage, transform.position);

        }


    }
    private IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(Dmg_time);
        gameObject.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {

    }
}
