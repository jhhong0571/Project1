using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardInteraction : MonoBehaviour
{
    public Sprite[] cardSprites;
    public GameObject[] cardSlots; 
    public float slideSpeed = 2.0f; 
    public Sprite card;

    private bool isScreenUp = true; 
    private bool isCardShowing = false; 

    private void Start()
    {
        HideCardSlots();
    }

    private void Update()
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
            }
            isCardShowing = !isCardShowing;
        }
    }

    private void ShowRandomCards()
    {
        foreach (GameObject slot in cardSlots)
        {
            Image slotImage = slot.GetComponent<Image>();
            if (slotImage != null)
            {
                int selectedIndex = Random.Range(0, cardSprites.Length);
                slotImage.sprite = cardSprites[selectedIndex];
            }
        }

        float targetY = isScreenUp ? -Screen.height : 0;
        StartCoroutine(SlideScreen(targetY));
        isScreenUp = !isScreenUp;
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
}

