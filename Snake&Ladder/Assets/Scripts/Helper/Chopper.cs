using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chopper : MonoBehaviour,IHelperController
{
    private Animator animator;
    private HelperManager helperManager;
    private GameObject player;
    private Transform child;
    [SerializeField] AudioClip HelicopterStart;
    // Start is called before the first frame update

    private void Start()
    {
        child = transform.GetChild(0);
        animator = GetComponent<Animator>();
        helperManager = GetComponent<HelperManager>();
    }
    public void StartHelper(GameObject _playerObj)
    {
        player = _playerObj;
        Invoke("Enableobj", 1);
    }
    private void Enableobj()
    {
        child.gameObject.SetActive(true);
        SoundController.instance.PlaySoundOnce(HelicopterStart);
        Invoke("StartAnim", 1);
    }
    private void StartAnim()
    {
        player.SetActive(false);
        animator.SetTrigger("Start");
    }
    public void AnimCallback()
    {
        StartCoroutine(End());
    }
    IEnumerator End()
    {
       
        yield return new WaitForSecondsRealtime(2);
        animator.SetTrigger("End");
        player.SetActive(true);
        helperManager.changePlayerPos();
    }
}
