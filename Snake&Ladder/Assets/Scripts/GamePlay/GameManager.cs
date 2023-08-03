using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform posPointsParent;  
    [SerializeField] private Transform[] players;

    [SerializeField] private bool killOpp;

    #region Scripts
    [SerializeField]
    private DiceController diceController;
    [SerializeField]
    private PlayController playController;
    [SerializeField]
    private CameraFollower cameraFollower;
    [SerializeField]
    private UiController uiController;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        Initialize();
    }

    private void Initialize()
    {
        playController.Init(posPointsParent,diceController,players,cameraFollower,uiController,killOpp);
        uiController.Init(playController);
    }

    // Update is called once per frame

}
