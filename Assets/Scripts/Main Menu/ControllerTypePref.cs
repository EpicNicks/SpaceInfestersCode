using UnityEngine;

public class ControllerTypePref : MonoBehaviour {

    void Start()
    {
        if (PlayerPrefs.GetString("HasChosen") == null)
        {
            PlayerPrefs.SetInt("ControlType", 1);
            Debug.Log("Control Type NOT Selected Yet");
        }

    }

    public void ControlTypeMouse()
    {
        Debug.Log("Control Type Set to \"MOUSE\"");
        PlayerPrefs.SetInt("ControlType", 1);
        PlayerPrefs.SetString("HasChosen", "YES");
    }
    public void ControlTypeController()
    {
        Debug.Log("Control Type Set to \"USB CONTROLLER\"");
        PlayerPrefs.SetInt("ControlType", 2);
        PlayerPrefs.SetString("HasChosen", "YES");
    }
}