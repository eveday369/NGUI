using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour {

    public GameObject resetButton;
    public GameObject invenTable;
    public GameObject backpackTable;
    private List<Transform> itemsInBackpack = new List<Transform>();
    private List<GameObject> allItems = new List<GameObject>();

	// Use this for initialization
	void Start () {
        // 이벤트리스너를 이용해 리셋 버튼의 OnClick 이벤트에 이벤트 함수를 추가한다
        UIEventListener.Get(resetButton).onClick += Button;
        // 가방에 있는 모든 아이템을 allItems 리스트에 담는다
        foreach(Transform itemTransform in backpackTable.transform)
        {
            allItems.Add(itemTransform.gameObject);
        }
        // 인벤토리에 있는 모든 아이템을 allItems 리스트에 담는다
        foreach (Transform itemTransform in invenTable.transform)
        {
            allItems.Add(itemTransform.gameObject);
        }
        // allItems에 들어있는 모든 아이템의 onDoubleClick 이벤트에 이벤트 함수를 추가한다
        foreach(GameObject item in allItems)
        {
            UIEventListener.Get(item).onDoubleClick += Button;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void Button(GameObject go)
    {
        if(go == resetButton)
        {
            // 현재 가방에 있는 모든 아이템을 itemsInBackpack 리스트에 담는다
            foreach(Transform itemTransform in backpackTable.transform)
            {
                itemsInBackpack.Add(itemTransform);
            }
            // itemsInBackpack 리스트에 있는 모든 리스트의 부모를
            foreach(Transform item in itemsInBackpack)
            {
                // 인벤토리의 테이블로 바꾼다
                item.parent = invenTable.transform;
                // MarkParentAsChanged() 함수를 호출해서 부모가 바뀌었음을 자식 위젯에 알린다
                NGUITools.MarkParentAsChanged(item.gameObject);
            }
            // 인벤토리 테이블에 있는 아이템을 정렬한다
            invenTable.GetComponent<UITable>().repositionNow = true;
            // 아이템을 모두 비웠으면 itemsInBackpack 리스트도 비운다
            itemsInBackpack.Clear();
        }
        // 이벤트를 발생한 게임오브젝트가 allItems 리스트에 들어있으면
        if(allItems.Contains(go))
        {
            // 부모가 되는 테이블을 바꾼다
            if(go.transform.parent == invenTable.transform)
            {
                go.transform.parent = backpackTable.transform;
            }
            else if(go.transform.parent == backpackTable.transform)
            {
                go.transform.parent = invenTable.transform;
            }
            // 하위 위젯에게 부모가 변경되었음을 알려주고 두 테이블을 모두 재정렬한다
            NGUITools.MarkParentAsChanged(go);
            backpackTable.GetComponent<UITable>().repositionNow = true;
            invenTable.GetComponent<UITable>().repositionNow = true;
        }
    }
}
