using UnityEngine;

public class LaserRemover : MonoBehaviour {
    public float width, height;

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(col.gameObject);
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }
}
