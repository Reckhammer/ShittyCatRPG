using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEntryPoint : MonoBehaviour
{
    public string sceneName = "TEMP";
    public string niceName = "TEMP";
    public bool isRandomEncounter = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isRandomEncounter)
        {
            int chance = Random.Range(1,10);

            if (chance == 5)
                MapEntryMenu.instance.OpenMenu(sceneName, isRandomEncounter);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (!isRandomEncounter)
        {
            if (Input.GetKey("Space"))
            {
                MapEntryMenu.instance.OpenMenu(sceneName, isRandomEncounter, niceName);
            }
        }
    }
}
