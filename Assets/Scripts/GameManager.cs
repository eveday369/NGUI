using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public enum Difficulties
    {
        Normal,
        Hard
    }

    public static Difficulties Difficulty = Difficulties.Normal;
    public static Power.Type SelectedPower = Power.Type.None;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void ExitPressed()
    {
        // 게임을 종료하는 함수를 0.5초 뒤에 호출한다
        Invoke("QuitNow", 0.5f);
    }

    void QuitNow()
    {
        Application.Quit();
    }

    public void OnDifficultyChange()
    {
        // 난이도가 Normal로 바뀌면 Difficulty에 Difficulties.Normal을 지정한다
        if (UIPopupList.current.value == "Normal")
            Difficulty = Difficulties.Normal;
        // Normal이 아닌경우 난이도는 Hard
        else
            Difficulty = Difficulties.Hard;
    }

    public static void SetPower(Power.Type newPower)
    {
        SelectedPower = newPower;
    }
}
