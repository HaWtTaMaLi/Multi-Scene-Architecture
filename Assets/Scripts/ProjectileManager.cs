using System.Collections;
using TMPro;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    //do not assign, for visual debugging purposes
    public GameObject lastHit;
    public LayerMask targetLayer = 3;
    public float distance = 100;
    public float coolDownTime = 30; //seconds between last shot

    //to display the hit object in the UI
    public TMP_Text objectDisplayText;

    public void FixedUpdate()
    { 
        //i love Couroutines LOL
        StartCoroutine(DefaultShot());
    }

    IEnumerator DefaultShot()
    {
        while (true)
        {
            //
            var ray = new Ray(transform.position, transform.right);
            RaycastHit hit;

            //if the ray cast detects an object 
            if (Physics.Raycast(ray, out hit, distance, targetLayer))
            {
                //store the hit object in a variable
                lastHit = hit.transform.gameObject;

                //print to log
                Debug.Log(lastHit.ToString());

                //display the hit object in the UI
                objectDisplayText.text = lastHit.name.ToString();
            }

            //wait for seconds
            yield return new WaitForSeconds(coolDownTime * Time.timeScale);
        }
    }
}

