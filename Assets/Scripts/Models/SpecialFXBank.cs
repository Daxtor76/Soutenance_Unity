using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialFXBank
{
    private Dictionary<Type, IEffect> _specialEffects = new Dictionary<Type, IEffect>();

    public void AddEffect(IEffect fx)
    {
        _specialEffects.Add(fx.GetType(), fx);
    }

    public Dictionary<Type, IEffect> GetEffects()
    {
        return _specialEffects;
    }

    public IEffect GetEffectOfType<T>()
    {
        if (_specialEffects.ContainsKey(typeof(T)))
            return _specialEffects[typeof(T)];

        return null;
    }
}
