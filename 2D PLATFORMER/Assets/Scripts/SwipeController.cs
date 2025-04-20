using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour, IEndDragHandler
{
    [SerializeField] int maxPage;
    private int currentPage;
    private Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPageRect;

    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;
    private float dragThreshould;

    [SerializeField] Image[] barImage;
    [SerializeField] Sprite barClosed, barOpen;

    [SerializeField] Button prevBtn, nextBtn;

    private void Awake() {
        currentPage = 1;
        targetPos = levelPageRect.localPosition;
        dragThreshould = Screen.width / 15;
        UpdateBar();
        UpdateArrowButton();
    }

    public void Next() {
        if (currentPage < maxPage) {
            currentPage++;
            targetPos += pageStep;
            MovePage();
        }
    }
    public void Previous() {
        if (currentPage > 1) {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
        }
    }

    private void MovePage() {
        levelPageRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
        UpdateBar();
        UpdateArrowButton();
    }

    public void OnEndDrag(PointerEventData evenData) {
        if(Mathf.Abs(evenData.position.x - evenData.pressPosition.x) > dragThreshould) {
            if(evenData.position.x > evenData.pressPosition.x) {
                Previous();
            }
            else {
                MovePage();
            }
        }
    }

    private void UpdateBar() {
        foreach(var item in barImage) {
            item.sprite = barClosed;
        }
        barImage[currentPage - 1].sprite = barOpen;
    }

    private void UpdateArrowButton() {
        nextBtn.interactable = true;
        prevBtn.interactable = true;
        if(currentPage == 1) {
            prevBtn.interactable = false;
        }
        else if(currentPage == maxPage) {
            nextBtn.interactable = false;
        }
    }
}
