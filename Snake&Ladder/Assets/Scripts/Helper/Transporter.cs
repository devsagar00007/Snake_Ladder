using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transporter : MonoBehaviour,IHelperController
{
    private Animator animator;
    private HelperManager helperManager;
    private GameObject player;
    private Transform child;
    [SerializeField] ParticleSystem soundboom;
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
        animator.SetTrigger("Open");
        soundboom.Play();
        StartCoroutine(End());
    }
    public void AnimStartCallBack()
    {
       
        
    }
    IEnumerator End()
    {
        yield return new WaitForSecondsRealtime(1f);
        player.SetActive(false);
        yield return new WaitForSecondsRealtime(1f);
        child.gameObject.SetActive(true);
        animator.SetTrigger("Close");
        yield return new WaitForSecondsRealtime(0.2f);
        soundboom.Play();
        yield return new WaitForSecondsRealtime(0.5f);
        player.SetActive(true);
        helperManager.changePlayerPos();
    }
}
