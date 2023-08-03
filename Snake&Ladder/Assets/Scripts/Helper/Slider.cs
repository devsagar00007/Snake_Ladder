using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour,IHelperController
{
    private Animator animator;
    private HelperManager helperManager;
    private GameObject player;
    private Transform child;

    [SerializeField] AudioClip sliderSound;
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
        SoundController.instance.PlaySoundOnce(sliderSound);
        player = _playerObj;
        player.transform.parent = child.transform;
        StartCoroutine(PlayAnim());
    }
    IEnumerator PlayAnim()
    {
        yield return new WaitForSeconds(2);
        animator.SetTrigger("Open");
        
        yield return new WaitForSeconds(3);
        player.transform.parent = null;
        helperManager.changePlayerPos();
        animator.SetTrigger("Close");

    }
}
