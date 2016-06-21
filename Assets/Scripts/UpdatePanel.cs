using UnityEngine;
using System.Collections;

public class UpdatePanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateNow()
    {
        // 패널을 갱신하기 위해 0, 0, 0만큼 패널을 드래그한다
        GetComponent<UIScrollView>().MoveRelative(Vector3.zero);
    }
}
