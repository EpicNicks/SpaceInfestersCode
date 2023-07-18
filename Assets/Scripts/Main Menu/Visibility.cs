using UnityEngine;

public class Visibility : MonoBehaviour {
    private int MouseClicks = 0;

    private void Awake()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z  -100f);
        gameObject.SetActive(false);
    }

    public void checkIfActive()
    {
        MouseClicks++;
        if (MouseClicks%2 == 1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 100f);
            gameObject.SetActive(true);
        } else if (MouseClicks %2 == 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 100f);
            gameObject.SetActive(false);
        }

    }
}