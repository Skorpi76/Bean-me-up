using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Asteroid))]
public class AsteroidHelper : Editor {

    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Asteroid myScript = (Asteroid)target;
        if (GUILayout.Button("Build Object"))
        {
            foreach (Transform t in myScript.transform) {
                Debug.Log("another child");
                if (t.gameObject.GetComponent<Collider2D>() != null)
                {
                    DestroyImmediate(t.gameObject.GetComponent<Collider2D>());
                }
                Sprite sp = t.GetComponent<SpriteRenderer>().sprite;

                int i = 0;
                foreach (Sprite sCheck in myScript.textureSprites) {
                    if (sp == sCheck) {
                        t.GetComponent<SpriteRenderer>().sprite = myScript.mapSprites[i];
                        break;
                    }
                    i ++;
                }

                t.GetComponent<SpriteRenderer>().color = myScript.mapSpriteColor;

                if (t.gameObject.layer != myScript.MapLayer) {
                    t.gameObject.layer = myScript.MapLayer;
                }
            }
        }
    }
}
