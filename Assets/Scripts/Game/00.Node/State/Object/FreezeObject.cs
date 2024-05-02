using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeObject : StateObject
{
    public enum FreezeState
    {
        Usally = 2,
        Broken = 1,
        Destory = 0,
    }


    [SerializeField]
    private GameObject freezeObject;
    [SerializeField]
    private GameObject[] brokenObjects;

    [SerializeField]
    private GameObject broken;

    private FreezeState freezeState;



    private void OnEnable()
    {
        freezeState = FreezeState.Usally;
        EffectObjectInteraction((int)freezeState);
    }

    public override void EffectObjectInteraction(int state)
    {
        freezeState = (FreezeState)state;

        if(freezeState == FreezeState.Usally)
        {
            freezeObject.SetActive(true);

            for(int i = 0; i < brokenObjects.Length; ++i)
            {
                brokenObjects[i].SetActive(false);
            }
        }
        else if(freezeState == FreezeState.Broken)
        {
            freezeObject.SetActive(false);

            for (int i = 0; i < brokenObjects.Length; ++i)
            {
                brokenObjects[i].SetActive(true);
            }
        }
        else if(freezeState == FreezeState.Destory)
        {
            //��ƼŬ ����Ʈ ����

            var ice  = ObjectPoolingManager.Instace.PopEffectObject(EffectType.Ice);

            ice.transform.localPosition = this.transform.localPosition;

        }
    }


}
