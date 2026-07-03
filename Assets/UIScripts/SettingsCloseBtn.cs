using UnityEngine;

public class CloseSettingsBtn : MonoBehaviour
{
    public GameObject settingsMenuPanel;

    public void closeSettingsMenu()
    {
        settingsMenuPanel.SetActive(false);
    }
}
