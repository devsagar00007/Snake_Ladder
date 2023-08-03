using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    private int[] diceProbabilityNumbers = new int[] { 1,4,4,4, 2, 3,2,3,3,2, 4,1,1,2,3,6,3,3,2, 5,2,2, 6,6 };
    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        count = diceProbabilityNumbers.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal int FlipDice()
    {
        int _randomNumber = Random.Range(0, count);
        int _result = diceProbabilityNumbers[_randomNumber];
        Debug.Log("result is " + _result);
        return  _result;
    }
}
