using UnityEngine;

public class CoreManager : MonoBehaviour
{
    void Start()
    {
        //load everything like audio managers, save systems
        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.Menu, SceneDatabase.Scenes.MainMenu)
            .WithLoadScreen()
            .Perform();
    }
}
