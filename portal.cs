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

            // ���õ� ���� ��ȿ�ϸ� ��ȯ�ϰ� visitedScenes ����Ʈ�� �߰�
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
        // ��� �� �̸��� ������ ����Ʈ
        List<string> allSceneNames = new List<string>();

        // ��� �� �̸��� ����Ʈ�� �߰�
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
        // ������ �湮�� ������ ��� ����
        List<string> remainingScenes = new List<string>(allSceneNames.Except(visitedScenes));

        // �̵��� �� �ִ� ���� ������ null ��ȯ
        if (remainingScenes.Count == 0)
        {
            Debug.Log("�̵� ������ ���� �����ϴ�.");
            return null;
        }

        // ���� �� �߿��� �������� �ϳ� ����
        int randomIndex = Random.Range(0, remainingScenes.Count);
        string randomScene = remainingScenes[randomIndex];

        return randomScene;
    }
}