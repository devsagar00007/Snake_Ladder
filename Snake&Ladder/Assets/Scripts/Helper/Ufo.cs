using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : MonoBehaviour,IHelperController
{
    private Animator animator;
    private HelperManager helperManager;
    private GameObject player;
    private Transform child;
    [SerializeField] GameObject lightBeam;
    bool isDataSent = false;

    private void Start()
    {
        child = transform.GetChild(0);
        animator = GetComponent<Animator>();
        helperManager = GetComponent<HelperManager>();
    }
    public void StartHelper(GameObject _playerObj)
    {
        child.gameObject.SetActive(true);
        player = _playerObj;
        animator.SetTrigger("Start");
     
    }

    public void AnimStartCallback()
    {
        SetLightBeam(true);
        StartCoroutine(Visibility("Close",false));
    }
 
    public void AnimEndCallback()
    {
        SetLightBeam(true);
        StartCoroutine(Visibility("End", true));
    }
    IEnumerator Visibility(string _anim,bool flag)
    {
        yield return new WaitForSecondsRealtime(1.5f);
        if (isDataSent) helperManager.changePlayerPos();
        isDataSent = !isDataSent;
        player.SetActive(flag);
        yield return new WaitForSecondsRealtime(1);
        SetLightBeam(false);
        animator.SetTrigger(_anim);
       
        
    }
    private void SetLightBeam(bool flag) => lightBeam.SetActive(flag);

}
