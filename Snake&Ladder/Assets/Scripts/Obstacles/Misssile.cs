using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misssile : MonoBehaviour,IObstacleController
{
    private Animator animator;
    [SerializeField] private GameObject blastEffect;
    private ObstacleManager obstacleManager;
    private GameObject child;
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
        animator.SetTrigger("Start");
    }

    public void ActionafterAnimation()
    {
        blastEffect.transform.position = child.transform.position;
        blastEffect.gameObject.SetActive(true);
        child.SetActive(false);
        Invoke("MovePlayerCallback", 1f);
    }
    public void MovePlayerCallback()
    {
        obstacleManager.MovePlayerCallback();
    }
    
}
