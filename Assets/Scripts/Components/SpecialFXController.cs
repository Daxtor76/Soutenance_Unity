using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialFXController : MonoBehaviour
{
    public Actor Actor { get; private set; }
    public SpecialFXBank bank { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        Actor = GetComponent<Actor>();
        bank = new SpecialFXBank();
        Actor.StateController.OnStateChange.AddListener(OnStateChange);
    }

    private void OnStateChange(Actor.States state)
    {
        if (state == Actor.States.kyubi)
        {
            TriggerFX(bank.GetEffectOfType<KyubiFX>());
        }
        else
        {
            DisableFX(bank.GetEffectOfType<KyubiFX>());
        }
    }

    public void PopulateEnemyFXBank()
    {
        
    }

    public void PopulateCharacterFXBank()
    {
        Transform characterFX = Actor.transform.Find("FX");
        KyubiFX kyubiFX = new KyubiFX(characterFX.Find("Kyubi").gameObject);

        bank.AddEffect(kyubiFX);
    }

    public void TriggerFX(IEffect fx)
    {
        fx.Trigger();
    }

    public void DisableFX(IEffect fx)
    {
        fx.Disable();
    }
}
