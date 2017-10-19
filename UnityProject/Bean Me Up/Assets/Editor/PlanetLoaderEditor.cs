using System.Collections;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(PlanetLoader))]
public class PlanetLoaderEditor : Editor
{

    private PlanetLoader c;
    // Use this for initialization
    public void OnSceneGUI()
    {
        c = this.target as PlanetLoader;

        Handles.color = Color.red;
        Handles.DrawWireDisc(c.transform.position, c.transform.forward, c.planetRadius);                            

    }
}