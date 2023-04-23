using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public static FishMovement instance;
    public float maxLaunchForce = 100f;
    public float minY = -4.5f;
    public ParticleSystem spitParticles;
    public bool allowInput = true;


    private Vector2? clickStart = null;
    private LineRenderer arrowLine;
    private bool canLaunch = true;
    private float particleDefaultRotation;

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
        particleDefaultRotation = spitParticles.shape.rotation.z;
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
                // Rotate the particle system to face the pointer
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 direction = mousePos - transform.position;
                spitParticles.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                spitParticles.Play();
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

    public void AllowLaunch()
    {
        canLaunch = true;
    }
}
