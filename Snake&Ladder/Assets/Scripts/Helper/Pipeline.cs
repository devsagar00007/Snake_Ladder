using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipeline : MonoBehaviour,IHelperController
{
    private Animator animator;
    private HelperManager helperManager;
    private GameObject player;
    private Transform child;
    // Start is called before the first frame update

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
    public void AnimCallBack()
    {
        Invoke("ChangePlayerPosAsk", 2);
    }
    private void ChangePlayerPosAsk()
    {
        helperManager.changePlayerPos();
        animator.SetTrigger("Close");
    }

}
