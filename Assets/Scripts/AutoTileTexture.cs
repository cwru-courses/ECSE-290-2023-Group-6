using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTileTexture : MonoBehaviour
{
    public bool isEnabled = false;
    public float scale = 1;

    void OnDrawGizmos()
    {
        if (isEnabled)
        {
            Vector2 scaleVec = new Vector2(this.gameObject.transform.lossyScale.x / scale, this.gameObject.transform.lossyScale.y / scale);
            this.gameObject.GetComponent<Renderer>().material.SetTextureScale("_MainTex", scaleVec);
        }
    } 
}
