using UnityEngine;
using System.Collections;

public class NotificationManager : MonoBehaviour {

    public enum Type
    {
        Nickname,
        Power,
        BarrierAvailable
    }

    public UILocalize loc;
    public Type type;

    public static NotificationManager instance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Awake()
    {
        // 정적 변수인 instance에 현재 NotificationManager의 인스턴스를 저장한다
        instance = this;
        // Notification 게임오브젝트를 비활성화한다
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        // TweenScale로 0.5초동안 1, 1, 1크기로 커진다
        TweenScale tween = TweenScale.Begin(this.gameObject, 0.5f, new Vector3(1, 1, 1));
        // 트윈의 시작과 끝에 이징 효과를 추가한다
        tween.method = UITweener.Method.EaseInOut;
        // 알림메시지 유형 + "Notification"을 지역화 key로 사용한다
        loc.key = type.ToString() + "Notification";
    }

    public void Show(Type notificationType, float duration)
    {
        // 현재 알림 메시지가 없다면
        if(!gameObject.activeInHierarchy)
        {
            // 전달 받은 알림 메시지 유형을 지정한다
            type = notificationType;
            // 알림 메시지를 화면에 띄운다
            gameObject.SetActive(true);
            // 전달 받은 표시 시간이 경과하면 알림 메시지를 제거한다
            StartCoroutine(Remove(duration));
        }
    }

    public IEnumerator Remove(float duration)
    {
        // 알림 메시지의 표시 기간이 끝날 때까지 기다린다
        yield return new WaitForSeconds(duration);
        // TweenScale로 메시지가 사라지게 만든다
        TweenScale.Begin(gameObject, 0.5f, new Vector3(0, 0, 1));
        // TweenScale이 이뤄지는 0.5초동안 기다린다
        yield return new WaitForSeconds(0.5f);
        // Notification 게임 오브젝트를 비활성화한다
        gameObject.SetActive(false);
    }
}
