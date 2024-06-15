using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObject : MonoBehaviour
{
    [SerializeField]
    protected EffectType effectType;
    
    public EffectType GetEffectType() => effectType;
    
    public void Delete()
    {

        ObjectPoolingManager.Instace.PushEffectObject(this);
    }

}
