using UnityEngine;

public class DeletePrefs : MonoBehaviour {

    public void DeleteAllPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Prefs Killed");
        PlayerPrefs.SetFloat("PlayerTime_1", 1000f);
    }
}
