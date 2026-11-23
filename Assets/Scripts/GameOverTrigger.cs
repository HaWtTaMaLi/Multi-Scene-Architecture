using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            //Debug.Log(" Trigger Entered");
            ResultsManager.Instance.NextMatch();
        }
    }
}
