using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class CardInteraction : MonoBehaviour
{

    public TextMeshProUGUI UItext;
    public GameObject End;
    public Sprite[] cardSprites;
    public GameObject[] cardSlots; 
    public float slideSpeed = 2.0f; 
    public Sprite card;
    int count = 3;

    public GameManager gameManager;

    private bool isScreenUp = true; 
    private bool isCardShowing = false;

    /*
    //PS 코드에 변수
    float CC = PS.instance.InitialCriticalChance;
    float CD = PS.instance.InitialCriticalDamage;
    int Damage = PS.instance.InitialDamage;
    int maxHealth = PS.instance.maxHealth;
    float Defense = PS.instance.InitialDefense;
    */

    private void Start()
    {
        gameManager.LoadPlayerData();
        HideCardSlots();
        
    }

    private void Update()
    {
        if (count != 0)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (isCardShowing)
                {
                    HideCardSlots();
                }
                else
                {
                    ShowRandomCards();
                    count--;
                }
                isCardShowing = !isCardShowing;
            }
        }
        else
        {
            SetEnd(true);
        }

        CCUI();
    }

    void CCUI()
    {
        UItext.text = "남은 횟수:" + count.ToString();
    }

    public void SetEnd(bool isActive)
    {
        End.SetActive(isActive);
    }

    private void ShowRandomCards()
    {
        for (int i = 0; i < cardSlots.Length; i++)
        {
            GameObject slot = cardSlots[i];
            Image slotImage = slot.GetComponent<Image>();
            if (slotImage != null)
            {
                int selectedIndex = Random.Range(0, cardSprites.Length);
                slotImage.sprite = cardSprites[selectedIndex];

                Button button = slot.GetComponent<Button>();
                button.onClick.RemoveAllListeners();

                switch (i)
                {
                    case 1:
                        button.onClick.AddListener(() => OnButtonClick(selectedIndex));
                        break;
                    case 2:
                        button.onClick.AddListener(() => OnButtonClick2(selectedIndex));
                        break;
                    case 3:
                        button.onClick.AddListener(() => OnButtonClick3(selectedIndex));
                        break;
                    default:
                        button.onClick.AddListener(() => OnButtonClick(selectedIndex));
                        break;
                }
            }
        }

        float targetY = isScreenUp ? -Screen.height : 0;
        StartCoroutine(SlideScreen(targetY));
        isScreenUp = !isScreenUp;
    }

    public void OnButtonClick(int cardIndex)
    {
        Debug.Log("버튼 1 클릭하여 index값: " + cardIndex);
        ApplyCardEffect(cardIndex);
    }

    public void OnButtonClick2(int cardIndex)
    {
        Debug.Log("버튼 2 클릭하여 index값: " + cardIndex);
        ApplyCardEffect(cardIndex);
    }

    public void OnButtonClick3(int cardIndex)
    {
        Debug.Log("버튼 3 클릭하여 index값: " + cardIndex);
        ApplyCardEffect(cardIndex);
    }


    private void HideCardSlots()
    {
        foreach (GameObject slot in cardSlots)
        {
            Image slotImage = slot.GetComponent<Image>();
            if (slotImage != null)
            {
                slotImage.sprite = card;
            }
        }

       
        float targetY = isScreenUp ? -Screen.height : 0;
        StartCoroutine(SlideScreen(targetY));
        isScreenUp = !isScreenUp;
    }

    private IEnumerator SlideScreen(float targetY)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = cardSlots[0].transform.parent.localPosition;
        Vector3 targetPosition = new Vector3(startPosition.x, targetY, startPosition.z);

        while (elapsedTime < slideSpeed)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsedTime / slideSpeed);
            cardSlots[0].transform.parent.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }
    }

    private void ApplyCardEffect(int cardIndex)
    {
        // 각 카드에 따른 효과를 부여
        switch (cardIndex)
        {
            case 0:
                // 카드 0 - 
                Debug.Log("0번 카드");
                Card0();
                break;
            case 1:
                // 카드 1
                Debug.Log("1번 카드");
                Card1();
                break;
            // 다른 카드에 대한 효과를 추가할 수 있습니다.
            case 2:
                Debug.Log("2번 카드");
                Card2();
                break;
            case 3:
                Debug.Log("3번 카드");
                Card3();
                break;
            case 4:
                Debug.Log("4번 카드");
                Card4();
                break;
            case 5:
                Debug.Log("5번 카드");
                Card5();
                break;
            case 6:
                Debug.Log("6번 카드");
                Card6();
                break;
            case 7:
                Debug.Log("7번 카드");
                Card7();
                break;
            case 8:
                Debug.Log("8번 카드");
                Card8();
                break;
            case 9:
                Debug.Log("9번 카드");
                Card9();
                break;
            case 10:
                Debug.Log("10번 카드");
                Card10();
                break;
            case 11:
                Debug.Log("11번 카드");
                Card11();
                break;
            case 12:
                Debug.Log("12번 카드");
                Card12();
                break;
            case 13:
                Debug.Log("13번 카드");
                Card13();
                break;
        }
    }

    private void Card0()
    {
        gameManager.initialDamage += 5;
        gameManager.initialDefense += 5;
        gameManager.initialCriticalDamage -= 0.05f;
        gameManager.initialCriticalChance += 0.05f;
        gameManager.maxHealth += 10;
        gameManager.SavePlayerData();
    }

    private void Card1()
    {
        gameManager.initialDamage += 8;
        gameManager.initialDefense -= 10;
        gameManager.initialCriticalDamage += 0.15f;
        gameManager.initialCriticalChance += 0.05f;
        gameManager.maxHealth += 20;
        gameManager.SavePlayerData();
    }

    private void Card2()
    {
        gameManager.initialDamage += 10;
        gameManager.initialDefense -= 5;
        gameManager.initialCriticalDamage += 0.2f;
        gameManager.initialCriticalChance += 0.2f;
        gameManager.maxHealth -= 20;
        gameManager.SavePlayerData();

    }

    private void Card3()
    {
        gameManager.initialDamage -= 7;
        gameManager.initialDefense += 5;
        gameManager.initialCriticalDamage -= 0.1f;
        gameManager.maxHealth += 10;
        gameManager.SavePlayerData();

    }

    private void Card4()
    {
        gameManager.initialDamage += 2;
        gameManager.initialDefense += 2;
        gameManager.SavePlayerData();
    }

    private void Card5()
    {
        gameManager.initialDamage += 10;
        gameManager.initialDefense -= 5;
        gameManager.initialCriticalDamage += 0.1f;
        gameManager.SavePlayerData();
    }
    private void Card6()
    {
        gameManager.initialDefense -= 3;
        gameManager.initialCriticalDamage += 0.05f;
        gameManager.initialCriticalChance += 0.08f;
        gameManager.maxHealth -= 2;
        gameManager.SavePlayerData();
    }
    private void Card7()
    {
        gameManager.initialDefense += 3;
        gameManager.maxHealth += 2;
        gameManager.SavePlayerData();
    }
    private void Card8()
    {
        gameManager.initialDefense += 2;
        gameManager.maxHealth += 2;
        gameManager.SavePlayerData();
    }
    private void Card9()
    {
        gameManager.initialDefense -= 2;
        gameManager.maxHealth += 5;
        gameManager.SavePlayerData();
    }
    private void Card10()
    {
        gameManager.initialDamage -= 2;
        gameManager.initialCriticalDamage += 0.05f;
        gameManager.initialCriticalChance += 0.1f;
        gameManager.SavePlayerData();
    }
    private void Card11()
    {
        gameManager.initialDamage += 4;
        gameManager.SavePlayerData();
    }
    private void Card12()
    {
        gameManager.initialDamage += 10;
        gameManager.initialCriticalDamage += 0.2f;
        gameManager.maxHealth -= 15;
        gameManager.SavePlayerData();
    }
    private void Card13()
    {
        gameManager.initialCriticalChance += 0.12f;
        gameManager.maxHealth -= 5;
        gameManager.SavePlayerData();

    }

}


