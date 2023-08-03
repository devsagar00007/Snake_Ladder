using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperManager : MonoBehaviour
{
    private PlayController playController;
    private IHelperController helperController;
    // Start is called before the first frame update
    void Start()
    {
        playController = FindObjectOfType<PlayController>();
        helperController = GetComponent<IHelperController>();
    }

    // Update is called once per frame
    internal void StartHelper(GameObject _playerObj)
    {
        helperController.StartHelper(_playerObj);
    }
    internal void changePlayerPos()
    {
        playController.ChangePlayerPos();
    }
}
