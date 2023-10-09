using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loby : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {

        Invoke("StartBGM", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickStartBtn()
    {
        StartCoroutine("StartBtn");
        
    }

    public void OnClickExitBtn()
    {
        StartCoroutine("ExitBtn");
    }

    private IEnumerator StartBtn()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("TopLevel1");
    }

    private IEnumerator ExitBtn()
    {
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }
}
