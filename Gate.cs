using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public GameObject targetObj;
    public GameObject toObj;
    [SerializeField] private AudioClip[] clip;
    private AudioSource bgm;
    private bool sound = false;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        bgm = GetComponent<AudioSource>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Player"))
        {
            anim.SetBool("gate", true);
            targetObj = collision.gameObject;
            if(!sound)
            {
                bgm.Stop();
                StartCoroutine("BgmStart");
                
            }
            
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&Input.GetKeyDown(KeyCode.UpArrow))
        {
            sound = false;
            bgm.Stop();
            StartCoroutine(TeleportRoutine());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            anim.SetBool("gate", false);
    }

    IEnumerator TeleportRoutine()
    {
        yield return null;
        targetObj.transform.position = toObj.transform.position;
    }

    IEnumerator BgmStart()
    {
        yield return new WaitForSeconds(2f);
        sound = true;
        bgm.clip = clip[0];
        bgm.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
