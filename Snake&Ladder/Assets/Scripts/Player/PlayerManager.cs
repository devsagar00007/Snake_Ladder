using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private IPlayerController playerController;
    private Animator animator;

    private bool doubleMov = false;

    #region AnimationId
    int runAnimation;
    int idleAnimation;
    int dieAnimation;
    int attackAnimation;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
       
    }
    internal void Init(List<Transform> posPoints)
    {
        AccessComponents();
        playerController.Init(posPoints,this);
    }
    private void AccessComponents()
    {
        playerController = GetComponent<IPlayerController>();
         animator = GetComponent<Animator>();
        SetAnimationId();
    }
   
    private void SetAnimationId()
    {
        runAnimation = Animator.StringToHash("Move");
        idleAnimation = Animator.StringToHash("Idle");
        attackAnimation = Animator.StringToHash("Attack");
        dieAnimation = Animator.StringToHash("Die");
    }

    // Update is called once per frame
    internal void MovePlayer(int placesToMove,bool isDoubleMov)
    {
        doubleMov = isDoubleMov;
        animator.SetTrigger(runAnimation);
        playerController.Move(placesToMove);
    }
   
    internal void SetToIdle()
    {
        animator.SetTrigger(idleAnimation);
        if (doubleMov) PlayController.instance.AttackandKillSequence();
        else PlayController.instance.PlayerMovedAcknowledgement();
    }
    internal void Attack()
    {
        animator.SetTrigger(attackAnimation);
        playerController.Attack();
    }
    internal void Changeposwithobstacle()
    {
        playerController.ChangePosForObstacle();
    }
    internal void DeactivePlayerCallBack()
    {
        PlayController.instance.DeactivatePlayerCallback();
    }
    #region killOpp
    public void AttackAnimationEvent()
    {
        PlayController.instance.OppDieSequence();
    }
    internal void Die()
    {
        animator.SetTrigger(dieAnimation);
    }
    #endregion
    #region notKillopp

    internal void SlideToSide(Vector2 offset)
    {
        playerController.SlideToSide(offset);
    }
    #endregion
    public void ObstaclePlayerMov(int pos)
    {
        playerController.ObstaclePlayerMove(pos);
    }
    internal void HelperPlayerMov(int pos)
    {     
        playerController.HelperPlayerMove(pos);
    }
}
