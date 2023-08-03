using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [SerializeField] private Button diceButton;
    private PlayController playController;
    [SerializeField] Sprite[] diceSprites;
    [SerializeField] Image sr;
    internal void Init(PlayController _playController)
    {
        playController = _playController;
    }

    public void OnClickDice()
    {
        int value =   playController.GetDiceValue();
        sr.sprite = diceSprites[value - 1];
        Invoke("DisableDice", 2);
    }
    private void DisableDice() => diceButton.gameObject.SetActive(false);


    internal void ActivateDiceButton() => diceButton.gameObject.SetActive(true);
   
}
