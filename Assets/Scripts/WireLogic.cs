using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireLogic : MonoBehaviour
{
    public static WireLogic instance;
    public int maxPoints = 3;
    private int points = 0;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    void CheckCompletion()
    {
        if (points == maxPoints)
        {
            EventManager.instance.EventComplete();
        }
    }
    // Update is called once per frame
    public void AddPoint()
    {
        Debug.Log("point added");
        AddPoints(1);
    }
   public void AddPoints(int points)
    {
        instance.points += points;
        instance.CheckCompletion();
    }
   
}
