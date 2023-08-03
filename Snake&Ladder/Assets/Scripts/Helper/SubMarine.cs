using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMarine : MonoBehaviour,IHelperController
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
        Invoke("Carry", 2);

    }
    private void Carry()
    {
        player.SetActive(false);
        animator.SetTrigger("Carry");
    }

    public void AnimEndCallBack()
    {
        Invoke("End", 1);
    }

    private void End()
    {
        player.SetActive(true);
        helperManager.changePlayerPos();
        animator.SetTrigger("End");
    }

}
