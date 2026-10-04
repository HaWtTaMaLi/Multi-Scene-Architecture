using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    void Start()
    {
        //load everything like audio managers, save systems
        SceneController.Instance
            .NewTransition()
            .Load(SceneDatabase.Slots.Menu, SceneDatabase.Scenes.MainMenu)
            .Perform();
    }
}
