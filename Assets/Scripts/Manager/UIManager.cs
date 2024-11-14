using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void ClosePopUp(GameObject _popup)
    {
        Destroy(_popup);
    }

    public void OpenPopUp(string _name)
    {
        string path = "UI/";

        Instantiate(Resources.Load<GameObject>(path + _name));
    }
}
