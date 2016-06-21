using UnityEngine;
using System.Collections;

public class LaunchValidator : MonoBehaviour {

    public UIInput nicknameInput;
    public GameObject menuContainer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick()
    {
        // 입력란이 비어있으면
        if (string.IsNullOrEmpty(nicknameInput.value))
        {
            // 2.5초동안 별명에 대한 알림 메시지를 표시한다
            NotificationManager.instance.Show(NotificationManager.Type.Nickname, 2.5f);
        }
        // 닉네임은 있으나 아이템을 선택하지 않았다면
        else if(GameManager.SelectedPower == Power.Type.None)
        {
            // 2.5초 동안 아이템에 대한 알림 메시지를 표시한다
            NotificationManager.instance.Show(NotificationManager.Type.Power, 2.5f);
        }
        // 닉네임도 있고 아이템도 선택했다면
        else
        {
            // 게임을 시작하기 전에 별명을 저장한다
            PlayerPrefs.SetString("Nickname", nicknameInput.value);
            // 게임 씬을 로딩한다
            menuContainer.SendMessage("CloseMenu");
            Invoke("LaunchNow", 0.5f);
        }
    }

    void LaunchNow()
    {
        Application.LoadLevel("Game");
    }
}
