using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlippingCard : MonoBehaviour
{
    [SerializeField]
    public float x, y, z;

    [SerializeField]
    public GameObject cardBack;

    [SerializeField]
    public bool cardBackIsActive;

    [SerializeField]
    public int timer;

    [SerializeField]
    public Button ShoreUp;
    public FloodManager floodManager;

    public int flag = 3; // Initial value of the flag (2 for card front, 1 for card back)


    // Start is called before the first frame update
    void Start()
    {
        cardBackIsActive = false;
        UpdateCardState(); // Update the card state based on the initial flag value

        floodManager = GetComponentInParent<FloodManager>(); // Get the FloodManager component in the parent GameObject
    }

    public void StartFlip()
    {
        StartCoroutine(CalculateFlip());
    }

    public void Flip()
    {
        flag = (flag == 2) ? 1 : 2; // Toggle between 2 and 1
        UpdateCardState(); // Update the card state based on the new flag value
    }

    IEnumerator CalculateFlip()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(x, y, z) * startRotation;
        float elapsedTime = 0f;
        float flipDuration = 0.5f; // Adjust the duration as needed

        while (elapsedTime < flipDuration)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / flipDuration);
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            timer++;

            if (timer == 90 || timer == -90)
            {
                Flip();
            }
        }

        transform.rotation = endRotation;
        timer = 0;
    }


    private void UpdateCardState()
    {
        if (flag == 2)
        {
            cardBack.SetActive(false);
            cardBackIsActive = false;
            ShoreUp.interactable = true;
        }
        else if (flag == 1)
        {
            cardBack.SetActive(true);
            cardBackIsActive = true;
            ShoreUp.interactable = false;
            Debug.Log("Island tile has been flipped");

            // Check if the corresponding island tile should sink
            int slotIndex = transform.GetSiblingIndex();
            if (!floodManager.availableCardSlots[slotIndex])
            {
                floodManager.SinkIslandTile(gameObject);
            }

        }
    }
    public void OnDisable()
    {
        if (flag == 1)
        {
            ShoreUp.interactable = false;
        }
    }
}