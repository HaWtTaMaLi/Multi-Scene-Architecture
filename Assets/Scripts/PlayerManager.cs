using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //player "movement"
    public GameObject player;
    public InputManager inputManager;

    public void OnEnable()
    {
        //??
        //inputManager.OnMoved += ???;
    }

    public void OnDisable()
    {
        //??
        //inputManager.OnMoved -= ???;
    }
}
