using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringOutObject : Singleton<BringOutObject>
{
    private ThreeMatchGame threeMatchGame;

    protected override void Awake()
    {
        base.Awake();
       
    }

    public ThreeMatchGame GetThreeMatchGame()
    {
        if(threeMatchGame == null)
        {
            threeMatchGame = FindObjectOfType<ThreeMatchGame>();
        }

        return threeMatchGame;
    }



}
