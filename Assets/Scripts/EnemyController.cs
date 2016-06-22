using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public bool hacked = false;
    private UILabel codeLabel;
    private UISlider hackSlider;
    public string destructCode = "";
    float hackSpeed = 0.2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Initialize(float _movementDuration)
    {
        // Viewport의 Background 사이즈를 구한다
        Vector2 bgSize = transform.parent.parent.FindChild("Background").GetComponent<UISprite>().localSize;
        // 적 스프라이트의 크기를 구한다
        Vector2 spriteSize = transform.FindChild("Sprite").GetComponent<UISprite>().localSize;
        // 위치x는 화면 넓이 안에서 랜덤하게 정한다. 위치 값 y는 공통적으로
        // 적 높이의 절반에 -1을 곱한 값을 사용한다
        transform.localPosition = new Vector3(Random.Range(spriteSize.x * 0.5f, bgSize.x - (spriteSize.x * 0.5f)), -(spriteSize.y * 0.5f), 0);
        // 트윈을 이용해서 화면 하단으로 이동한다
        TweenPosition.Begin(gameObject, _movementDuration, new Vector3(transform.localPosition.x, -bgSize.y + (spriteSize.y * 0.5f), 0));
        // UIForwardEvents의 Target으로 Viewport를 지정한다
        GetComponent<UIForwardEvents>().target = transform.parent.parent.gameObject;
        // 해킹 슬라이더를 찾는다
        hackSlider = transform.FindChild("DestructCode").GetComponent<UISlider>();
        // 해킹 상태를 표시할 레이블을 찾는다
        codeLabel = hackSlider.transform.FindChild("Label").GetComponent<UILabel>();
    }

    public IEnumerator Kill()
    {
        // 트윈을 이용해서 부드럽게 사라지는 효과를 연출한다
        TweenScale.Begin(gameObject, 0.2f, Vector3.zero);
        // 충돌체를 비활성화한다
        GetComponent<Collider>().enabled = false;
        // 트윈이 끝나길 기다려 적을 제거한다
        yield return new WaitForSeconds(0.2f);
        // 리스트에서 적을 제거한다
        EnemySpawnController.instance.EnemyDestroyed(this);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트에 DamageZone 태그가 있는가?
        if(other.CompareTag("DamageZone"))
        {
            // 그렇다면 플레이어의 HP를 감소시킨다
            HealthController.Instance.Damage(30f);
            // 적을 제거해서 뷰포트 밖으로 나가지 못하게 한다
            StartCoroutine(Kill());
            return;
        }
        // 충돌한 게임오브젝트에 있는 ActiveBarrierController 컴포넌트를 저장한다
        ActiveBarrierController barrierController = other.GetComponent<ActiveBarrierController>();
        // 충돌한 오브젝트가 장애물이면 HitByEnemy 함수를 호출하다
        if (barrierController != null)
            barrierController.HitByEnemy(this);
    }

    public void SetDestructCode(string randomWordKey)
    {
        // randomWordKey가 비어있지 않다면
        if (!string.IsNullOrEmpty(randomWordKey))
        {
            // 상응하는 지역화된 문자열을 찾는다
            destructCode = Localization.Get(randomWordKey);
            // 레이블에 Code Encrypted라고 표시한다
            codeLabel.text = Localization.Get("CodeEncrypted");
        }
        // randomWordKey가 비어있으며, 해킹 슬라이더를 비활성화한다
        else
            hackSlider.gameObject.SetActive(false);
    }

    IEnumerator Hack()
    {
        // 레이블에 "Hacking..."이라고 표시한다
        codeLabel.text = Localization.Get("Hacking");
        // 해킹 슬라이더가 아직 완전히 차지 않았다면
        while(hackSlider.value < 1)
        {
            // 프레임 레이트의 영향을 받지 않게 슬라이더 value 값을 증가시킨다
            hackSlider.value += Time.deltaTime * hackSpeed;
            // 다음 프레임을 기다린다
            yield return null;
        }
        // 슬라이더의 값을 1로 확실하게 설정한다
        hackSlider.value = 1;
        // 해킹 여부를 확인하는 변수인 hacked를 참으로 한다
        hacked = true;
        // 자폭 코드를 표시한다
        codeLabel.text = "[99FF99]" + destructCode;
    }

    void OnClick()
    {
        // 적이 자폭 코드를 갖고 있다면 해킹을 시작한다
        if (!string.IsNullOrEmpty(destructCode))
            StartCoroutine(Hack());
    }
}
