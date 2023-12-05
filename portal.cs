using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class PlayerCollisionDetection : MonoBehaviour
{
    static private List<string> visitedScenes = new List<string>();

    private void OnTriggerEnter(Collider other)
    {
        FightManager fightManager = FindObjectOfType<FightManager>();
        if (other.gameObject.name == "Player")
        {
            fightManager.live = 1;
            string nextScene = GetRandomScene();

            // 선택된 씬이 유효하면 전환하고 visitedScenes 리스트에 추가
            if (!string.IsNullOrEmpty(nextScene))
            {
                SceneManager.LoadScene(nextScene);
                visitedScenes.Add(nextScene);
            }
            else
            {
                SceneManager.LoadScene("Boss");
            }
        }
    }
    private string GetRandomScene()
    {
        // 모든 씬 이름을 저장할 리스트
        List<string> allSceneNames = new List<string>();

        // 모든 씬 이름을 리스트에 추가
        //allSceneNames.Add("map2");
        
        allSceneNames.Add("map1");
        allSceneNames.Add("map2");
        allSceneNames.Add("map3");
        allSceneNames.Add("map4");
        allSceneNames.Add("map5");
        allSceneNames.Add("map6");
        allSceneNames.Add("map7");
        allSceneNames.Add("map8");
        allSceneNames.Add("map9");
        allSceneNames.Add("map10");
        // 이전에 방문한 씬들을 모두 제외
        List<string> remainingScenes = new List<string>(allSceneNames.Except(visitedScenes));

        // 이동할 수 있는 씬이 없으면 null 반환
        if (remainingScenes.Count == 0)
        {
            Debug.Log("이동 가능한 씬이 없습니다.");
            return null;
        }

        // 남은 씬 중에서 랜덤으로 하나 선택
        int randomIndex = Random.Range(0, remainingScenes.Count);
        string randomScene = remainingScenes[randomIndex];

        return randomScene;
    }
}