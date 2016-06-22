using UnityEngine;
using System.Collections;

public class ViewportHolder : MonoBehaviour {

    public Object barrierObjectPrefab;
    public Object activeBarrierPrefab;

    public GameObject barrierContainer;

    public int barrierCount = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrop(GameObject droppedObj)
    {
        // 드롭된 게임오브젝트에서 BarrierObjectController를 찾는다
        BarrierObjectController barrierObj = droppedObj.GetComponent<BarrierObjectController>();

        // 게임 오브젝트가 BarrierObjectController 컴포넌트를 갖고 있다면
        // 그 게임 오브젝트를 제거한다
        if(barrierObj != null)
        {
            Destroy(droppedObj);
            RecreateBarrierObject();
            CreateActiveBarrier(droppedObj.transform);
        }
    }

    void RecreateBarrierObject()
    {
        // BarrierObject의 인스턴스를 컨테이너 역할을 하는 Barrier 게임오브젝트의 자식으로 생성
        Transform newBarrierTrans = NGUITools.AddChild(barrierContainer, barrierObjectPrefab as GameObject).transform;
        // 상대 좌표상의 위치 값을 0, 0, 0으로 한다
        newBarrierTrans.localPosition = Vector3.zero;
        // 새로운 장애물과 함꼐 대기 시스템을 시작하는 코루틴을 호출한다
        StartCoroutine(newBarrierTrans.GetComponent<BarrierObjectController>().Cooldown((barrierCount + 1) * 3));
    }

    void CreateActiveBarrier(Transform barrierObjectTrans)
    {
        // ActiveBarrier의 인스턴스를 Viewport의 자식으로 생성한다
        Transform newActiveBarrierTrans = NGUITools.AddChild(gameObject, activeBarrierPrefab as GameObject).transform;
        // 드롭된 오브젝트의 위치를 장애물 인스턴스의 위치로 사용한다
        newActiveBarrierTrans.position = barrierObjectTrans.position;
        // barrierCount를 갱신한다
        barrierCount++;
        // buildTime을 계산해서 Coroutine을 시작한다
        StartCoroutine(newActiveBarrierTrans.GetComponent<ActiveBarrierController>().Build(barrierCount * 2));
    }

    void BarrierRemoved()
    {
        // barrierCount의 값을 감소시킨다
        barrierCount--;
    }
}
