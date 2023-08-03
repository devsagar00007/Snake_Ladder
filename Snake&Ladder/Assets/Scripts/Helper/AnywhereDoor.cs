using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnywhereDoor : MonoBehaviour,IHelperController
{
    private Animator animator;
    private HelperManager helperManager;
    private GameObject player;
    private Transform child;
    [SerializeField] AudioClip doorOpen, doorClose;
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
        SoundController.instance.PlaySoundOnce(doorOpen);
        Invoke("CloseHelper", 2);
    }
    private void CloseHelper()
    {
        animator.SetTrigger("End");
        SoundController.instance.PlaySoundOnce(doorClose);
    }

    internal void StartAnimCallback()
    {
        player.SetActive(false);
    }
    internal void EndAnimCallback()
    {
        player.SetActive(true);
        ChangePlayerPosAsk();
    }
    private void ChangePlayerPosAsk()
    {
        helperManager.changePlayerPos();
    }
   

    
}
