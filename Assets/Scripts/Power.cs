using UnityEngine;
using System.Collections;

public class Power : MonoBehaviour {

    public enum Type
    {
        None,
        Time,
        Bomb
    }

    public Type type;
    public Object createOnDestroy;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
