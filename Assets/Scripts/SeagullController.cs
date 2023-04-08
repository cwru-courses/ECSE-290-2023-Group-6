using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullController : MonoBehaviour
{
    public Transform home;
    public float searchRadius = 5;
    public float idleSpeed = 1;
    public float attackSpeed = 1;
    public float returnSpeed = 1;

    private PolygonCollider2D detector;
    private CircleCollider2D talons;

    private enum State
    {
        searching,
        attacking,
        carrying,
        returning
    }
    private State state;


    void Start()
    {
        state = State.searching;
        detector = GetComponent<PolygonCollider2D>();
        talons = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        switch (this.state)
        {
            case State.searching:
                SearchingAction();
                break;

            case State.attacking:
                AttackingAction();
                break;

            case State.carrying:
                CarryingAction();
                break;

            case State.returning:
                ReturningAction();
                break;
        }
    }

    // Flies back and forth around home. If fish enters detector, set state to attack
    void SearchingAction()
    {
        
    }

    // Flies toward fish position. If fish enters talons, set state to carry
    void AttackingAction()
    {

    }

    // Flies to the left. If fish escapes or the level beginning is reached, set state to return
    void CarryingAction()
    {

    }

    // Disables talons, flies back to home. On reaching home, set state to searching and re-enable talons
    void ReturningAction()
    {

    }

    void MoveToPoint(Vector3 target, float speed)
    {

    }
}
