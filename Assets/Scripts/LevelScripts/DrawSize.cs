using UnityEngine;

public class DrawSize : MonoBehaviour {
    public float width, height;

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }
}
