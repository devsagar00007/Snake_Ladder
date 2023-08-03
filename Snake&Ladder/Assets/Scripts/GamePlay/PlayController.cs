using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayController : MonoBehaviour
{
    public static PlayController instance;

    private List<Transform> players = new List<Transform>();
    private List<Transform> posPoints = new List<Transform>();

    private int[] helperArray = new int[] { 2,7,22,28,30,44,54,70,80,87};
    private int[] obstacleArray = new int[] {27,35,39,50,59, 66,73,76,89,97,99 };//27,35,39,50,59,66,73,76,89,97,99};
    private int[] obstaclefallArray = new int[] {6,5,3,34,46,23,12,63,67,86,26 };
    private int[] helperfallArray = new int[] {23,29,41,77,32,58,69,90,83,93 };
    
    private bool killOpp = false;
    private bool isHelperPos = false;
    private bool isObstaclePos = false;
    private bool isResultDeclared = false;

    private Vector2[] posOffset = new[] { new Vector2(0.4f, 0.4f), new Vector2(0.4f, -0.4f), new Vector2(-0.4f, 0.4f), new Vector2(-0.4f, -0.4f), new Vector2(0,0) };

    private int[] currentPos;
    private int result = 0;
    private int placesToMove = 0;
    private int playerTurn = 0;
    private int doubleMovement = 0;
    private int oppPosIndex = -1;
    private int sixCounter = 0;
    private int slideToSide; // when more than one player is on same pos,  this is used to adjust there pos
    private int indexForSlidingPlayer = 0; // use when there is one player at the pos,to know it's index
    private int helperIndex = 0;
    private int obstacleIndex = 0;

    private DiceController diceController;
    private CameraFollower cameraFollower;
    private UiController uiController;
    private List<PlayerManager> playerManager = new List<PlayerManager>();
    [SerializeField]private List<HelperManager> helperManager = new List<HelperManager>();
    [SerializeField] private List<ObstacleManager> obstacleManager = new List<ObstacleManager>();


    // There is still some issue with player sliding, it sometimes slides even when there is no player at the pos
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(this);
    }
    internal void Init(Transform posPointsParent,DiceController _diceController, Transform[] _players,CameraFollower _cameraFollower,UiController _uiController,bool _killopp)
    {      
        diceController = _diceController;
        cameraFollower = _cameraFollower;
        uiController = _uiController;
        AccessPosPoints(posPointsParent);
        AccessPlayers(_players);
        AccessPlayerManager();
        killOpp = _killopp;
        currentPos = new int[players.Count];
        SendTargetToCamera();
        
    }
    #region AccessRegion
    private void AccessPosPoints(Transform posPointsParent)
    {
        int len = posPointsParent.childCount;
        for (int i = 0; i < len; i++)
        {
            posPoints.Add(posPointsParent.GetChild(i));
        }
    }
    private void AccessPlayers(Transform[] _players)
    {
        for(int i = 0;i<_players.Length;i++)
        {
            players.Add(_players[i]);
        }
    }
    private void AccessPlayerManager()
    {
        foreach(var player in players)
        {
            playerManager.Add(player.GetComponent<PlayerManager>());
        }
        for(int i = 0;i<playerManager.Count;i++)
        {
            playerManager[i].Init(posPoints);
        }
    }
    #endregion
    private void DiceThrowForBot()
    {
        Invoke("GetDiceValue", 2f);
    }
    internal int GetDiceValue()
    {
        result = 27;// diceController.FlipDice();
        if(result == 6)
        {
            placesToMove += result;
            sixCounter++;
            if (sixCounter == 3) ChangePlayerTurn();
            else ReDice();
        }
        else
        {
            placesToMove += result;
            Debug.Log("Places to move " + placesToMove);
            CheckThePos();
        }
        return result;
    }
    private void ReDice()
    {

        if (playerTurn == 0) uiController.ActivateDiceButton();
        else Invoke("GetDiceValue", 2f);
    }
    private void SendTargetToCamera()
    {
        Debug.Log("PlayerTurn is "+playerTurn);
        cameraFollower.AssignTarget(players[playerTurn]);//send index instead of whole transform
    }
    private void CheckThePos()
    {
        int index_ = 0;
        helperIndex = FindInArray(helperArray, currentPos[playerTurn] + placesToMove);
        Debug.Log("helperinde is " + helperIndex);
        obstacleIndex = FindInArray(obstacleArray, currentPos[playerTurn] + placesToMove);
        if (helperIndex >  -1)
        {
            Debug.Log("found a helper");
            isHelperPos = true;
            MovePlayer(placesToMove, false);
        }
        else if (obstacleIndex > -1)
        {
            Debug.Log("found obstacle");
            isObstaclePos = true;
            MovePlayer(placesToMove, false);
        }
        else if (currentPos[playerTurn] + placesToMove > 100)
        {
            ChangePlayerTurn();
        }
        else if (currentPos[playerTurn] + placesToMove == 100)
        {
            isResultDeclared = true;
            MovePlayer(placesToMove, false);
            ChangePlayerAtPrevPos();
            Debug.Log("Game Won");
        }
        else
        {
            if (killOpp) index_ = CheckIfAnotherPlayerIsAtPos(currentPos[playerTurn] + result);
            if (killOpp && index_ != -1)
            {
                doubleMovement = 1;
                AttackMoveSequence(index_);
            }
            if (!killOpp)
            {

                ChangePlayerPosatCurrentPos(currentPos[playerTurn] + result);
                MovePlayer(placesToMove, false);
                ChangePlayerAtPrevPos();

            }
        }
       

      
    }
    #region findObstacle


    private int CheckIfAnotherPlayerIsAtPos(int target)
    {
        for(int i = 0;i<currentPos.Length;i++)
        {
            if (i == playerTurn) continue;
            if (currentPos[i] == target) return i;
        }
        return -1;
    }
    private int FindInArray(int[] array,int target)
    {
        int s = 0;
        int e = array.Length-1;
        while(s<=e)
        {
            int mid = (s + e) / 2;
            Debug.Log("mid is " + mid);
            if (array[mid] == target) return mid;
            else if (array[mid] > target) e = mid - 1;
            else s = mid + 1;
        }
        return -1;
    }
    private int findCountofPlayers(int target)
    {
        int count = 0;
        for (int i = 0; i < currentPos.Length; i++)
        {
            if (i == playerTurn) continue;
            if (currentPos[i] == target)
            {
                indexForSlidingPlayer = i;
                count++;
            }
        }
        if (count != 1) indexForSlidingPlayer = 0;
        return count;
    }
    #endregion

    #region AdjustPlayersPos

    private void ChangePlayerPosatCurrentPos(int pos)
    {
        int _count = findCountofPlayers(pos);

        if (_count == 1)
        {
            Debug.Log("index is " + indexForSlidingPlayer);
            playerManager[indexForSlidingPlayer].SlideToSide(posOffset[0]);
            slideToSide = 1;
        }
        else if (_count > 1)
            slideToSide = _count;
         
    }

    private void ChangePlayerAtPrevPos()
    {
        int prePos = currentPos[playerTurn] - result;
        Debug.Log("prepos is " + prePos);
        if (prePos == 0)
            return;
        int _count = findCountofPlayers(prePos);
        if (_count == 1) playerManager[indexForSlidingPlayer].SlideToSide(posOffset[4]);
        else if (_count > 1)
        {
            int indictor = 0;
            for (int i = 0; i < players.Count; i++)
            {
                if (currentPos[i] == prePos)
                {

                    playerManager[i].SlideToSide(posOffset[indictor]);
                    indictor++;
                }

            }
        }
    }
    #endregion
    private void ChangePlayerTurn()
    {
        playerTurn+=1;
        if (playerTurn >= players.Count) playerTurn = 0;
        SendTargetToCamera();
        placesToMove = 0;
        sixCounter = 0;
        slideToSide = 0;
        isObstaclePos = isHelperPos = false;
        if (playerTurn == 0) uiController.ActivateDiceButton();
        else DiceThrowForBot();
    }
    private void MovePlayer(int pointToMove,bool flag)
    {
        currentPos[playerTurn] += pointToMove;
        playerManager[playerTurn].MovePlayer(pointToMove,flag);
    }
    internal void PlayerMovedAcknowledgement()
    {
        if (isHelperPos) HelperSequence();
        else if (isObstaclePos) ObstacleSequence();
        else if (isResultDeclared) return;
        else
        {
            SlideCurrentPlayer();
            ChangePlayerTurn();
        }
     //   ChangePlayerAtPrevPos();
       
    }
    private void SlideCurrentPlayer()
    {
        if (slideToSide > 0) playerManager[playerTurn].SlideToSide(posOffset[slideToSide]);// it is only for sliding player on current pos and not prev pos
    }
    #region OppOnPos
    private void AttackMoveSequence(int oppPlayerIndex)
    {
        MovePlayer(placesToMove - 1, true);
        oppPosIndex = oppPlayerIndex;
    }
    internal void AttackandKillSequence()
    {
        playerManager[playerTurn].Attack();
    }
    internal void OppDieSequence()
    {
        playerManager[oppPosIndex].Die();
    }

    #endregion
    #region obstacleregion
    private void ObstacleSequence()
    {
        Debug.Log("obstacleCAllback");
        obstacleManager[obstacleIndex].StartObstacle();
    }
    internal void ObstacleCallback()
    {      
        playerManager[playerTurn].Changeposwithobstacle();      
    }
    internal void DeactivatePlayerCallback()
    {
       
        playerManager[playerTurn].gameObject.SetActive(false);
        Invoke("ActivatePlayer", 2f);
    }
    private void ActivatePlayer()
    {
        currentPos[playerTurn] = obstaclefallArray[obstacleIndex];
        ChangePlayerPosatCurrentPos(obstaclefallArray[obstacleIndex]);
        playerManager[playerTurn].gameObject.SetActive(true);     
        playerManager[playerTurn].ObstaclePlayerMov(obstaclefallArray[obstacleIndex]);
        Invoke("ObstacleSequenceEnd", 2f);
    }
    private void ObstacleSequenceEnd()
    {
        SlideCurrentPlayer();
        ChangePlayerTurn();
    }
    #endregion
    #region HelperRegion
    private void HelperSequence()
    {
        ChangePlayerPosatCurrentPos(helperfallArray[helperIndex]);
        helperManager[helperIndex].StartHelper(playerManager[playerTurn].gameObject);
    }
    internal void ChangePlayerPos()
    {
        currentPos[playerTurn] = helperfallArray[helperIndex];
        playerManager[playerTurn].HelperPlayerMov(helperfallArray[helperIndex]);
        Invoke("ObstacleSequenceEnd", 1f);
    }
    #endregion


}

