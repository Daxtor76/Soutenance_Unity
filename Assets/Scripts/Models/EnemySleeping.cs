using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemySleeping : Actor
{
    public Actor target;
    public List<GameObject> _meshes = new List<GameObject>();
    private GameObject _currentMesh;

    private void Start()
    {
        SpecialFXController?.PopulateEnemyFXBank();

        target = GameObject.Find("Character").GetComponent<Actor>();

        _meshes = GetChildrenMeshes(GetMesh().transform);
        _currentMesh = SetCurrentMesh(0);

        StateController?.ChangeState(States.sleep);

        MovementController.runMover = new ToTargetMover(Const.ENEMY_FORWARD_SPEED, Const.ENEMY_SIDE_SPEED, target);

        CollisionController?.OnCollisionWithCharacter?.AddListener(OnCharacterHit);
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState == GameManager.GameStates.Playing &&
            StateController.CurrentState == States.sleep &&
            !IsCharacterSneaky() &&
            IsCharacterTooClose(target.transform.position, 5.0f))
        {
            _currentMesh = SetCurrentMesh(1);
            StateController?.ChangeState(States.run);
        }
    }

    bool IsCharacterSneaky()
    {
        return target.StateController.CurrentState == States.sneak;
    }

    bool IsCharacterTooClose(Vector3 targetPos, float distance)
    {
        return Vector3.Distance(transform.position, targetPos) < distance;
    }

    public void OnCharacterHit(Actor other)
    {
        if (other.StateController.CurrentState == States.kyubi)
        {
            StateController.ChangeState(States.dead);
            GetMesh().SetActive(false);
        }
    }

    public GameObject SetCurrentMesh(int meshId)
    {
        for (int i = 0; i < _meshes.Count; i++)
            _meshes[i].SetActive(i == meshId);

        AnimationController?.GetAnimator();

        return _meshes[meshId];
    }

    public List<GameObject> GetChildrenMeshes(Transform parent)
    {
        List<GameObject> list = new List<GameObject>();

        for (int i = 0; i < parent.childCount; i++)
            list.Add(parent.GetChild(i).gameObject);

        return list;
    }
}
