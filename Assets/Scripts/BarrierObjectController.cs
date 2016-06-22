using UnityEngine;
using System.Collections;

public class BarrierObjectController : MonoBehaviour {

    private UIButton button;
    private UILabel label;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnPress(bool pressed)
    {
        // 충돌체의 상태를 바꾼다
        GetComponent<Collider>().enabled = !pressed;
        
        // 지금 막 드롭됐다면
        if(!pressed)
        {
            // 타겟의 충돌체를 찾는다
            Collider col = UICamera.lastHit.collider;
            // 타겟에 충돌체가 없거나 타겟이 Viewport 게임오브젝트가 아니면
            if(col == null || col.GetComponent<ViewportHolder>() == null)
            {
                // 로컬 좌표상의 위치를 0,0,0으로 재배치한다
                transform.localPosition = Vector3.zero;
            }
        }
    }

    void Awake()
    {
        // Awake() 함수에서 필요한 컴포넌트를 찾는다
        button = GetComponentInChildren<UIButton>();
        label = GetComponentInChildren<UILabel>();
    }

    public IEnumerator Cooldown(int cooldown)
    {
        // 장애물 버튼을 비활성화하고 그에 맞게 버튼 색상을 갱신한다
        button.isEnabled = false;
        button.UpdateColor(false);
        while(cooldown > 0)
        {
            // 매 초, 지역화된 문자열로 레이블을 갱신한다
            label.text = Localization.Get("Wait") + " " + cooldown.ToString() + "s";
            cooldown -= 1;
            // 1초동안 기다렸다가 while문의 첫 부분으로 돌아간다
            yield return new WaitForSeconds(1);
        }
        // 대기 시간이 0보다 작으면
        CooldownFinished();
    }

    void CooldownFinished()
    {
        // 레이블의 텍스트를 기본 상태로 되돌린다
        label.text = Localization.Get("Barrier");
        // 버튼을 다시 활성화하고 색상을 Normal 상태로 되돌린다
        button.isEnabled = true;
        button.UpdateColor(true);
        // 크기를 0, 0, 0으로 설정한다
        transform.localScale = Vector3.zero;
        // 트윈을 이용해서 부드럽게 나타나는 효과를 연출한다
        TweenScale.Begin(gameObject, 0.3f, new Vector3(1, 1, 1));
        // 플레이어에게 알림 메시지를 띄운다
        NotificationManager.instance.Show(NotificationManager.Type.BarrierAvailable, 1.5f);
    }
}
