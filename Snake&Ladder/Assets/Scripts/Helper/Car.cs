using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour,IHelperController
{
    private Animator animator;
    private HelperManager helperManager;
    private GameObject player;
    private Transform child;

    [SerializeField] AudioClip carSound;
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
        StartCoroutine(PlayAnim());
       
    }
    IEnumerator PlayAnim()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        player.SetActive(false);
        yield return new WaitForSecondsRealtime(0.5f);
        SoundController.instance.PlaySoundOnce(carSound);
        animator.SetTrigger("Start");
    }
    public void AnimStartCallback()
    {
       
        player.SetActive(true);
        helperManager.changePlayerPos();
        animator.SetTrigger("Close");
    }
}
