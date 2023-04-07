using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float maxLaunchForce = 100f;

    private Vector2 clickStart = Vector2.zero;
    private LineRenderer arrowLine;

    // Start is called before the first frame update
    void Start()
    {
        arrowLine = GetComponent<LineRenderer>();
        arrowLine.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Handle mouse input
        if (Input.GetMouseButtonDown(0)) // Click down
        {
            clickStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            arrowLine.enabled = true;
            UpdateArrow(transform.position, transform.position);
        } else if (Input.GetMouseButtonUp(0)) // Click up
        {
            Vector2 clickEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = clickEnd - clickStart;
            GetComponent<Rigidbody2D>().AddForce(direction * maxLaunchForce);
            arrowLine.enabled = false;
        } else if (Input.GetMouseButton(0)) // Click held
        {
            Vector2 clickEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = clickEnd - clickStart;
            UpdateArrow(transform.position, (Vector2)transform.position + direction);
        }
    }

    void UpdateArrow(Vector2 origin, Vector2 target, float PercentHead = 0.1f)
    {
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
}
