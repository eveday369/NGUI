using UnityEngine;
using System.Collections;

public class ActiveBarrierController : MonoBehaviour {

    private UISlider slider;
    private UILocalize loc;
    private bool built = false;

	// Use this for initialization
	void Start () {
        // 뷰포트를 UIForwardEvents의 target으로 지정한다
        GetComponent<UIForwardEvents>().target = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Awake()
    {
        // Awake()에서 필요한 컴포넌트를 찾는다
        slider = GetComponentInChildren<UISlider>();
        loc = GetComponentInChildren<UILocalize>();
    }

    public IEnumerator Build(float buildTime)
    {
        while(slider.value < 1)
        {
            slider.value += (Time.deltaTime / buildTime);
            yield return null;
        }
        // 슬라이더의 value가 1보다 크면
        BuildFinished();
    }

    private void BuildFinished()
    {
        // Value가 1인이 확인한다
        slider.value = 1;
        // key를 완성된 상태인 Barrier로 바꾸고 UILocalize 컴포넌트를 갱신한다
        loc.key = "Barrier";
        //GetComponentInChildren<UILabel>().text = loc.key;
        NGUITools.Broadcast("OnLocalize", this);
        // 장애물이 완성되면 built 변수를 참으로 하고 충돌체를 활성화한다
        built = true;
        GetComponent<Collider>().enabled = true;
    }

    public void HitByEnemy(EnemyController enemy)
    {
        // 장애물이 완성되지 않았다면 더 이상 함수를 진행하지 않는다
        if (!built)
            return;

        // 완성된 상태에서 충돌했다면 적을 파괴한다
        StartCoroutine(enemy.Kill());
        // 장애물도 파괴한다
        StartCoroutine(RemoveBarrier());
    }

    IEnumerator RemoveBarrier()
    {
        // 트윈을 이용해서 부드럽게 사라지는 효과를 연출한다
        TweenScale.Begin(gameObject, 0.2f, Vector3.zero);
        // 장애물이 파괴되었음을 알린다
        transform.parent.SendMessage("BarrierRemoved");
        // 트윈이 끝나길 기다려 장애물을 제거한다
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
