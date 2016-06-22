using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnController : MonoBehaviour {

    public Object enemyPrefab;
    public int firstEnemyDelay = 1;
    public float minInterval = 4;
    public float maxInterval = 15;
    public float minMovementTime = 20;
    public float maxMovementTime = 50;

    public float destructCodeChance = 60;
    public string[] wordKeys;
    private List<EnemyController> enemies;
    public static EnemySpawnController instance;
    public string currentWord;

	// Use this for initialization
	void Start () {
        // 적을 생성한다
        StartCoroutine(SpawnEnemy());
	}
	
	// Update is called once per frame
	void Update () {
	    // 플레이어가 문자를 입력한다면
        if(!string.IsNullOrEmpty(Input.inputString))
        {
            // currentWord에 새로 입력된 문자를 추가한다
            currentWord += Input.inputString;
            // 입력된 코드가 최소한 하나 이상의 적과 일치하는지 알아야 한다
            bool codeMatches = false;
            // 적의 자폭 코드를 하나하나 확인한다
            foreach(EnemyController enemy in enemies)
            {
                // 적이 자폭 코드를 갖고 있고 해킹이 완료된 상태라면
                if(enemy.destructCode != "" && enemy.hacked)
                {
                    // currentWord가 자폭 코드를 포함하는가?
                    if(currentWord.Contains(enemy.destructCode))
                    {
                        // 그렇다면 적을 파괴하고 codeMatches를 참으로 한다
                        StartCoroutine(enemy.Kill());
                        codeMatches = true;
                    }
                }
            }
            // 적어도 하나 이상의 적의 자폭 코드를 입력했다면
            if(codeMatches)
            {
                // 새로움 입력을 위해 currentWord를 초기화한다
                currentWord = "";
            }
        }
	}

    IEnumerator SpawnEnemy()
    {
        // 첫 생성에 앞서 지연될 시간은 firstEnemyDelay의 값을 이용한다
        float delay = firstEnemyDelay;
        // 게임이 진행되는 동안 루프문을 실행한다
        while(true)
        {
            // 지정된 시간만큼 기다린다
            yield return new WaitForSeconds(delay);
            // 적을 생성하고 EnemyController를 저장한다
            EnemyController newEnemy = NGUITools.AddChild(gameObject, enemyPrefab as GameObject).GetComponent<EnemyController>();
            // 랜덤한 이동 속도를 지정해서 적을 초기화한다
            newEnemy.Initialize(Random.Range(minMovementTime, maxMovementTime));
            // 생성 사이에 사용할 랜덤한 시간차를 설정한다
            delay = Random.Range(minInterval, maxInterval);
            // 자폭 코드를 넣을 빈 문자열을 만든다
            string randomCode = "";
            // 랜덤 함수를 이용해서 자폭 코드를 넣을지 결정한다
            // 그 다음 자폭 코드로 사용할 문자열을 위한 Word Keys를 지정한다
            if (Random.Range(0f, 100f) < destructCodeChance)
                randomCode = GetRandomWord();
            // 생성한 적에게 자폭 코드를 부여한다
            newEnemy.SetDestructCode(randomCode);
            // 적을 리스트에 추가한다
            enemies.Add(newEnemy);
        }
    }

    void Awake()
    {
        // 정적 변수인 instance에 현재 스크립트의 인스턴스를 저장한다
        instance = this;
        // 리스트를 초기화한다
        enemies = new List<EnemyController>();
    }

    private string GetRandomWord()
    {
        // 무작위로 선택한 Word Key를 반환한다
        return wordKeys[Random.Range(0, wordKeys.Length)];
    }

    public void EnemyDestroyed(EnemyController destroyEnemy)
    {
        // 리스트에서 파괴된 적을 제거한다
        enemies.Remove(destroyEnemy);
    }
}
