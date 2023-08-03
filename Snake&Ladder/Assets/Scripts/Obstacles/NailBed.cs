using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailBed :MonoBehaviour, IObstacleController
{
    private Animator animator;
    private ObstacleManager obstacleManager;
    private GameObject child;

    [SerializeField] AudioClip nailBedSound;
    // Start is called before the first frame update
    private void Start()
    {
        child = transform.GetChild(0).gameObject;
        animator = GetComponent<Animator>();
        obstacleManager = GetComponent<ObstacleManager>();
    }
    public void StartObstacle()
    {
        child.SetActive(true);
        SoundController.instance.PlaySoundOnce(nailBedSound);
        animator.SetTrigger("Start");
    }

    public void ActionafterAnimation()
    {
        obstacleManager.MovePlayerCallback();
        Invoke("MovePlayerCallback", 0.4f);
    }
    private void MovePlayerCallback()
    {
        animator.SetTrigger("Close");     
        Invoke("DeactivateObject", 0.5f);
    }
    private void DeactivateObject()
    {
        child.gameObject.SetActive(false);
    }

}
