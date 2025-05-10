using Kuroneko.UIDelivery;
using UnityEngine.SceneManagement;

public class MainMenuPopup : Popup
{
    protected override void InitPopup()
    {
        
    }

    public void NewGame()
    {
        // NOTE(Alex): Hardcoded because I'm fucking lazy
        SceneManager.LoadScene(1);
    }

    public void Continue()
    {
        // TODO(Alex): Add logic here to determine to show cutscene 1, or not
    }

    public void Load()
    {
        
    }
}
