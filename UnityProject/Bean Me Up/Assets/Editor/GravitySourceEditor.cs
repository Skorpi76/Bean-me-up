using System.Collections;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(GravitySource))]
public class GravitySourceEditor : Editor
{

	private GravitySource c;
    // Use this for initialization
    public void OnSceneGUI()
    {
		c = this.target as GravitySource;

        Handles.color = Color.red;
		if (c.range == 0)
			Handles.DrawWireDisc (c.transform.position, c.transform.forward, (c.GetComponent<Collider2D> ().bounds.size.x / 2) * 3);
		else
			Handles.DrawWireDisc (c.transform.position, c.transform.forward, c.range);

    }
}