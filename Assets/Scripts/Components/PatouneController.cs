using System;
using UnityEngine;

public class PatouneController : Actor
{
    public Actor Actor { get; private set; }
    public float rotationSpeed = 45.0f;
    public int scoreValue = 5;

    private void Start()
    {
        Actor = GetComponent<Actor>();
        Actor.SpecialFXController?.PopulateEnemyFXBank();

        Actor.CollisionController?.OnCollisionWithCharacter?.AddListener(OnCharacterHit);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnCharacterHit(Actor other)
    {
        StateController.ChangeState(States.dead);
        GetMesh().SetActive(false);
    }
}
