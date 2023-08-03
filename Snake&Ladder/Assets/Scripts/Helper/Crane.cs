using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crane : MonoBehaviour,IHelperController
{
    private Animator animator;
    private HelperManager helperManager;
    private GameObject player;
    private Transform child;
    [SerializeField] Transform point;

    [SerializeField] AudioClip craneStart, craneCarry;
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
        SoundController.instance.PlaySoundOnce(craneStart);
        animator.SetTrigger("Start");
        Invoke("Carry", 2);
        
    }
    public void AnimStartCallback()
    {
        player.transform.parent = point;
        player.transform.localPosition = Vector3.zero;
    }
    private void Carry()
    {
        animator.SetTrigger("Carry");
        SoundController.instance.PlaySoundOnce(craneCarry);
    }

    public void AnimEndCallBack()
    {
        Invoke("End", 2f);
    }
    private void End()
    {
        player.transform.parent = null;
        helperManager.changePlayerPos();
        animator.SetTrigger("End");
        SoundController.instance.PlaySoundOnce(craneStart);
    }


}
