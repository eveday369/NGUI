using UnityEngine;
using System.Collections;

public class DropSurface : MonoBehaviour {

    public GameObject dragItemsContainer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnDrop(GameObject dropped)
    {
        // 드롭된 오브젝트에서 DragItem을 찾는다
        DragItem dragItem = dropped.GetComponent<DragItem>();
        // DragItem이 없다면 더 이상 진행할 필요가 없다
        if (dragItem == null)
            return;
        RecreateDragItem();
        // CreateOnDrop에 저장된 프리팹의 인스턴스를 생성한다
        GameObject newPower = NGUITools.AddChild(this.gameObject, dragItem.CreateOnDrop as GameObject);
        // 드롭한 아이템을 삭제한다
        Destroy(dropped);
        // GameManager에서 새로 선택한 아이템을 지정한다
        GameManager.SetPower(newPower.GetComponent<Power>().type);
    }

    void RecreateDragItem()
    {
        // 이미 선택된 아이템이 있으면
        if(GameManager.SelectedPower != Power.Type.None)
        {
            // 선택된 아이템의 Power.cs 스크립트를 찾는다
            Power selectedPowerScript = transform.GetChild(0).GetComponent<Power>();
            // 아이템을 본래 모습으로 아이템 상자의 Grid에서 재생성한다
            NGUITools.AddChild(dragItemsContainer, selectedPowerScript.createOnDestroy as GameObject);
            // 드롭 영역에서 교체된 아이템을 삭제한다
            Destroy(selectedPowerScript.gameObject);
        }
    }

    void OnClick()
    {
        // 아이템을 아이템 상자에서 재생성한다
        RecreateDragItem();
        // 선택한 아이템이 없는 상태로 초기화한다
        GameManager.SetPower(Power.Type.None);
        // UIGrid를 찾아서 재정렬한다
        dragItemsContainer.GetComponent<UIGrid>().Reposition();
    }
}
