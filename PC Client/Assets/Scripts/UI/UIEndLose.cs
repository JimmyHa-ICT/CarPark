using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEndLose : UiBase
{
    public Button btnHome;

    // Start is called before the first frame update
    void Start()
    {
        btnHome.onClick.AddListener(OnClickButtonHome);
    }

    private void OnClickButtonHome()
    {
        Hide();
        UiController.Instance.OpenUiLobby();
    }
}
