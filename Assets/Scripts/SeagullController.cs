using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SeagullController : MonoBehaviour
{
    public List<Transform> path;
    public float searchRadius = 5;
    public float idleSpeed = 1;
    public float attackSpeed = 1;
    public float returnSpeed = 1;
    public GameObject Seagull;
    public Sprite flying;
    public Sprite attacking;


    private List<Transform> reversePath;
    private PolygonCollider2D detector;
    private SeagullTalons talons;
    private Sprite sprite;
    private Transform home;
    public Vector3 curTarget;

    public enum State
    {
        searching,
        attacking,
        carrying,
        returning
    }
    public State state;
    private int pathIndex = 1;


    void Start()
    {
        state = State.searching;
        home = path[0];
        reversePath = Enumerable.Reverse(path).ToList();
        detector = GetComponent<PolygonCollider2D>();
        talons = GetComponentInChildren<SeagullTalons>();
        sprite = Seagull.GetComponent<SpriteRenderer>().sprite;
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && state == State.searching)
        {
            state = State.attacking;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && state == State.attacking)
        {
            curTarget = other.transform.position - talons.transform.localPosition;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            state = State.returning;
        }
    }

    // Flies back and forth around home. If fish enters detector, set state to attack
    void SearchingAction()
    {
        Seagull.GetComponent<SpriteRenderer>().sprite = flying;
        if (Vector3.Distance(transform.position, curTarget) < 0.1f || curTarget == Vector3.zero)
        {
            curTarget = AroundHome();
        }

        MoveToPoint(curTarget, idleSpeed);
    }

    // Flies toward fish position. If fish enters talons, set state to carry
    void AttackingAction()
    {
        Seagull.GetComponent<SpriteRenderer>().sprite = attacking;
        MoveToPoint(curTarget, attackSpeed);
        if (Vector3.Distance(transform.position, curTarget) < 0.1f)
        {
            state = State.carrying;
            pathIndex = 1;
        }
    }

    // Flies to the left. If fish escapes or the level beginning is reached, set state to return
    void CarryingAction()
    {
        FollowPath(path, attackSpeed);
        if (Vector3.Distance(transform.position, path.Last().position) < 0.1f)
        {
            state = State.returning;
            pathIndex = 0;
        }
    }

    // Disables talons, flies back to home. On reaching home, set state to searching and re-enable talons
    void ReturningAction()
    {
        Seagull.GetComponent<SpriteRenderer>().sprite = flying;
        FollowPath(reversePath, returnSpeed);
        talons.Disengage();
        if (Vector3.Distance(transform.position, home.position) < 0.1f)
        {
            state = State.searching;
            talons.Engage();
            pathIndex = 1;
        }
    }

    void MoveToPoint(Vector3 target, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    void FollowPath(List<Transform> path, float speed)
    {
        MoveToPoint(path[pathIndex].position, speed);
        if (Vector3.Distance(transform.position, path[pathIndex].position) < 0.1f)
        {
            pathIndex++;
        }
    }

    public Vector3 AroundHome()
    {
        return home.position + new Vector3(Random.Range(-searchRadius, searchRadius), 0, 0);
    }
}
