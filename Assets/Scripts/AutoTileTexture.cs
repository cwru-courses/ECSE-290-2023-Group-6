using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTileTexture : MonoBehaviour
{
    public float scale = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        Vector2 scaleVec = new Vector2(this.gameObject.transform.lossyScale.x / scale, this.gameObject.transform.lossyScale.y / scale);
        this.gameObject.GetComponent<Renderer>().material.SetTextureScale("_MainTex", scaleVec);
    } 
}
