using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public List<Menu> allMenus = new List<Menu>();

    void Update()
    {

    }

    void OpenMenu(string target)
    {

    }

    [System.Serializable]
    public class Menu
    {
        public string name = "";
        public GameObject gameObject;
    }
}
