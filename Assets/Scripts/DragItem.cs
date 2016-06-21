using UnityEngine;
using System.Collections;

public class DragItem : MonoBehaviour {

    public Object CreateOnDrop;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnPress(bool pressed)
    {
        GetComponent<Collider>().enabled = !pressed;

        // 아이템을 드롭할 때
        if(!pressed)
        {
            // 레이캐스트가 찾은 마지막 충돌체를 확인한다
            Collider col = UICamera.lastHit.collider;
            // 충돌체가 없거나, 드롭 영역이 아니라면
            if(col == null || col.GetComponent<DropSurface>() == null)
            {
                // 부모에 있는 UIGrid를 찾는다
                UIGrid grid = NGUITools.FindInParents<UIGrid>(gameObject);
                // UIGrid를 찾았으면 재정렬을 실행한다
                if (grid != null) grid.Reposition();
            }
        }
    }
}
