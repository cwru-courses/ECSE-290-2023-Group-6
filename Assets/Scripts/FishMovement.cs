using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public static FishMovement instance;
    public float maxLaunchForce = 100f;
    public float minY = -4.5f;
    public ParticleSystem spitParticles;
    public GameObject spitCollider;
    public bool allowInput = true;


    private Vector2? clickStart = null;
    private LineRenderer arrowLine;
    private bool canLaunch = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        arrowLine = GetComponent<LineRenderer>();
        arrowLine.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (allowInput) {
            // Handle mouse input for launching
            if (Input.GetMouseButtonDown(0) && canLaunch) // Click down
            {
                clickStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                arrowLine.enabled = true;
                UpdateArrow(transform.position, transform.position);
            } else if (Input.GetMouseButtonUp(0)) // Click up
            {
                if (!clickStart.HasValue) return;
                Vector2 clickEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = clickEnd - clickStart.Value;
                GetComponent<Rigidbody2D>().AddForce(direction * maxLaunchForce);
                canLaunch = false;
                arrowLine.enabled = false;
                clickStart = null;
            } else if (Input.GetMouseButton(0) && clickStart.HasValue) // Click held
            {
                Vector2 clickEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = clickEnd - clickStart.Value;
                if (direction.magnitude > 8f)
                    direction = direction.normalized * 8f;
                UpdateArrow(transform.position, (Vector2)transform.position + direction);
            }

            // Handle keyboard input for spitting
            if (Input.GetKeyDown(KeyCode.Space))
            {
                spitParticles.Play();
                StartCoroutine(SpitCollision());
                WaterMeter.instance.SubtractWater(0.1f);
                SoundManager.instance.Spit();
            }
        } else {
            // Clear arrow in case it was left on
            arrowLine.enabled = false;
        }

        // Die if we fall off the bottom of the screen
        if (transform.position.y < minY) {
            LevelManager.instance.Lose();
        }

        // Jump to last level when pressing F9 and alphanumeric 5
        if (Input.GetKey(KeyCode.F9) && Input.GetKey(KeyCode.Alpha5))
        {
            LevelManager.instance.GoToScene("FinalLevel");
        }
    }

    void UpdateArrow(Vector2 origin, Vector2 target, float PercentHead = 0.1f)
    {
        if (!canLaunch) return;
        arrowLine.widthCurve = new AnimationCurve(
            new Keyframe(0, 0.4f)
            , new Keyframe(0.999f - PercentHead, 0.4f)  // neck of arrow
            , new Keyframe(1 - PercentHead, 1f)  // max width of arrow head
            , new Keyframe(1, 0f));  // tip of arrow
        arrowLine.SetPositions(new Vector3[] {
            origin
            , Vector3.Lerp(origin, target, 0.999f - PercentHead)
            , Vector3.Lerp(origin, target, 1 - PercentHead)
            , target });
    }

    public IEnumerator SpitCollision() {
        spitCollider.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        spitCollider.SetActive(false);
    }

    public void AllowLaunch()
    {
        canLaunch = true;
    }
}
