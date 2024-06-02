using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        Actor?.StateController?.OnStateChange?.AddListener(OnStateChange);
        Actor?.CollisionController?.OnCollisionWithEnemy?.AddListener(OnCharacterAttack);
    }

    private void OnCharacterAttack(Actor other)
    {
        if (Actor.StateController.CurrentState == Actor.States.kyubi)
            TriggerFX(bank.GetEffectOfType<AttackFX>());
    }

    private void OnStateChange(Actor.States state)
    {
        if (state == Actor.States.kyubi)
        {
            TriggerFX(bank.GetEffectOfType<KyubiFX>());
        }
        else if (state == Actor.States.dead)
        {
            TriggerFX(bank.GetEffectOfType<DeathFX>());
        }
        else if (state == Actor.States.run)
        {
            TriggerFX(bank.GetEffectOfType<RunFX>());

            if (Actor.GetType() == typeof(EnemySleeping))
            {
                foreach (Transform fx in Actor.GetMesh().transform)
                    fx.gameObject.SetActive(false);
            }
        }
        else
        {
            DisableFX(bank.GetEffectOfType<KyubiFX>());
        }
    }

    public void PopulateEnemyFXBank()
    {
        Transform actorFX = Actor.transform.Find("FX");
        DeathFX deathFX = new DeathFX(actorFX.Find("Death").gameObject);
        RunFX runFX = new RunFX(actorFX.Find("WakeUp").gameObject);

        bank.AddEffect(deathFX);
        bank.AddEffect(runFX);
    }

    public void PopulateCharacterFXBank()
    {
        Transform actorFX = Actor.transform.Find("FX");
        KyubiFX kyubiFX = new KyubiFX(actorFX.Find("Kyubi").gameObject);
        AttackFX attackFX = new AttackFX(actorFX.Find("Attack").gameObject);
        DeathFX deathFX = new DeathFX(actorFX.Find("Death").gameObject);

        bank.AddEffect(kyubiFX);
        bank.AddEffect(attackFX);
        bank.AddEffect(deathFX);
    }

    public void TriggerFX(IEffect fx)
    {
        if (fx != null)
            fx.Trigger();
    }

    public void DisableFX(IEffect fx)
    {
        if (fx != null)
            fx.Disable();
    }
}
