using UnityEngine;

public class SceneDatabase : MonoBehaviour
{
    public class Slots
    {
        //what are we calling the systems and what are their names
        public const string Menu = "Menu";
        public const string Game = "GamePlay";
        public const string Data = "SessionData";
        public const string Shop = "shop";
    }

    public class Scenes
    {
        //the scene names to assocciate with slots
        public const string MainMenu = "MainMenu";
        public const string GamePlay = "GamePlay";
        public const string SessionData = "SessionData";
        public const string Shop = "Shop";
    }
}
