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

    public IEffect GetEffectOfType<T>()
    {
        IEffect effect = _specialEffects[typeof(T)];

        if (effect != null)
            return effect;
        else
            return null;
    }
}
