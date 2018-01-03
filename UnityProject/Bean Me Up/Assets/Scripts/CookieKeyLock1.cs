using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieKeyLock1 : MonoBehaviour {

    public keyHole[] keyHoles;
    // Update is called once per frame
    void Update()
    {
        bool anyInactive = false;
        foreach (keyHole kh in keyHoles)
        {
            if (!kh.Open)
            {
                anyInactive = true;
                break;
            }
        }

        if (!anyInactive)
        {
            Destroy(gameObject);
        }
    }
}
