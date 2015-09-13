using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(MyScrollRect))]
public class HorizontalScrollSnap : MonoBehaviour
{
    [Tooltip("The container the screens or pages belong to. REQUIRED")]
    public Transform ScreensContainer;


    private int _screens = 1;
    private int _startingScreen = 1;

    private bool _fastSwipeTimer = false;
    private int _fastSwipeCounter = 0;
    private int _fastSwipeTarget = 30;


    private System.Collections.Generic.List<Vector3> _positions;
    private MyScrollRect _scroll_rect;
    private Vector3 _lerp_target;
    private bool _lerp;

    private int _containerSize;

    [Tooltip("The gameobject that contains toggles which suggest pagination. THIS CAN BE MISSING")]
    public GameObject Pagination;

    [Tooltip("Button to go to the next page. THIS CAN BE MISSING")]
    public GameObject NextButton;
    [Tooltip("Button to go to the previous page. THIS CAN BE MISSING")]
    public GameObject PrevButton;

    public Boolean UseFastSwipe = true;
    public int FastSwipeThreshold = 100;

    // Use this for initialization
    void Start()
    {        
    
        DistributePages();

        _screens = ScreensContainer.childCount;
        _startingScreen = 1;

        _scroll_rect = gameObject.GetComponent<MyScrollRect>();
        _lerp = false;

        _positions = new System.Collections.Generic.List<Vector3>();

        if (_screens > 0)
        {
            for (int i = 0; i < _screens; ++i)
            {
                _scroll_rect.horizontalNormalizedPosition = (float)i / (float)(_screens - 1);
                _positions.Add(ScreensContainer.localPosition);
            }
        }

        _scroll_rect.horizontalNormalizedPosition = (float)(_startingScreen - 1) / (float)(_screens - 1);

        _containerSize = (int)ScreensContainer.gameObject.GetComponent<RectTransform>().offsetMax.x;

        ChangeBulletsInfo(CurrentScreen());

       // if (NextButton)
       //     NextButton.GetComponent<Button>().onClick.AddListener(() => { NextScreen(); });

       // if (PrevButton)
       //     PrevButton.GetComponent<Button>().onClick.AddListener(() => { PreviousScreen(); });
    }

    void Update()
    {
        if (_lerp)
        {
            ScreensContainer.localPosition = Vector3.Lerp(ScreensContainer.localPosition, _lerp_target, 7.5f * Time.deltaTime);
            if (Vector3.Distance(ScreensContainer.localPosition, _lerp_target) < 0.005f)
            {
                _lerp = false;
            }

            //change the info bullets at the bottom of the screen. Just for visual effect
            if (Vector3.Distance(ScreensContainer.localPosition, _lerp_target) < 10f)
            {
                ChangeBulletsInfo(CurrentScreen());
            }
        }

        if (_fastSwipeTimer)
        {
            _fastSwipeCounter++;
        }

    }

    private bool fastSwipe = false; //to determine if a fast swipe was performed
    public void DragEnd()
    {
        _startDrag = true;
        if (_scroll_rect.horizontal)
        {
            if (UseFastSwipe)
            {

                fastSwipe = false;
                _fastSwipeTimer = false;
                if (_fastSwipeCounter <= _fastSwipeTarget)
                {
                    if (Math.Abs(_startPosition.x - ScreensContainer.localPosition.x) > FastSwipeThreshold)
                    {
                        fastSwipe = true;
                    }
                }
                if (fastSwipe)
                {
                    if (_startPosition.x - ScreensContainer.localPosition.x > 0)
                    {
                        NextScreenCommand();
                    }
                    else
                    {
                        PrevScreenCommand();
                    }
                }
                else
                {
                    _lerp = true;
                    _lerp_target = FindClosestFrom(ScreensContainer.localPosition, _positions);
                }


            }
            else
            {
                _lerp = true;
                _lerp_target = FindClosestFrom(ScreensContainer.localPosition, _positions);
            }
            
        }
    }

    private bool _startDrag = true;

    public void OnDrag()
    {
        _lerp = false;
        if (_startDrag)
        {
            OnDragStart();
            _startDrag = false;
        }
    }

    private Vector3 _startPosition = new Vector3();
    private int _currentScreen;

    public void OnDragStart()
    {
        _startPosition = ScreensContainer.localPosition;
        _fastSwipeCounter = 0;
        _fastSwipeTimer = true;
        _currentScreen = CurrentScreen();
    }

    //Function for switching screens with buttons
    public void NextScreen()
    {
        if (CurrentScreen() < _screens - 1)
        {
            _lerp = true;
            _lerp_target = _positions[CurrentScreen() + 1];

            ChangeBulletsInfo(CurrentScreen() + 1);
        }
    }

    //Function for switching screens with buttons
    public void PreviousScreen()
    {
        if (CurrentScreen() > 0)
        {
            _lerp = true;
            _lerp_target = _positions[CurrentScreen() - 1];

            ChangeBulletsInfo(CurrentScreen() - 1);
        }
    }

    //Because the CurrentScreen function is not so reliable, these are the functions used for swipes
    private void NextScreenCommand()
    {
        if (_currentScreen < _screens - 1)
        {
            _lerp = true;
            _lerp_target = _positions[_currentScreen + 1];

            ChangeBulletsInfo(_currentScreen + 1);
        }
    }

    //Because the CurrentScreen function is not so reliable, these are the functions used for swipes
    private void PrevScreenCommand()
    {
        if (_currentScreen > 0)
        {
            _lerp = true;
            _lerp_target = _positions[_currentScreen - 1];

            ChangeBulletsInfo(_currentScreen - 1);
        }
    }


    //find the closest registered point to the releasing point
    private Vector3 FindClosestFrom(Vector3 start, System.Collections.Generic.List<Vector3> positions)
    {
        Vector3 closest = Vector3.zero;
        float distance = Mathf.Infinity;

        foreach (Vector3 position in _positions)
        {
            if (Vector3.Distance(start, position) < distance)
            {
                distance = Vector3.Distance(start, position);
                closest = position;
            }
        }

        return closest;
    }


    //returns the current screen that the is seeing
    public int CurrentScreen()
    {
        float absPoz = Math.Abs(ScreensContainer.gameObject.GetComponent<RectTransform>().offsetMin.x);

        absPoz = Mathf.Clamp(absPoz, 1, _containerSize - 1);

        float calc = ( absPoz / _containerSize) * _screens;

        return (int) calc;
    }

    //changes the bullets on the bottom of the page - pagination
    private void ChangeBulletsInfo(int currentScreen)
    {
        if (Pagination)
            for (int i = 0; i < Pagination.transform.childCount; i++)
            {
                    Pagination.transform.GetChild(i).GetComponent<Toggle>().isOn = (currentScreen == i)
                        ? true
                        : false;
            }
    }
    
    //used for changing between screen resolutions
    private void DistributePages()
    {
        int _offset = 0;
        int _step = Screen.width;
        int _dimension = 0;
        
        int currentXPosition = 0;
        
        for (int i = 0; i < ScreensContainer.transform.childCount; i++)
        {
            RectTransform child = ScreensContainer.transform.GetChild(i).gameObject.GetComponent<RectTransform>();
            currentXPosition = _offset + i * _step;          
            child.anchoredPosition = new Vector2(currentXPosition, 0f);
			child.sizeDelta = new Vector2( gameObject.GetComponent<RectTransform>().sizeDelta.x, gameObject.GetComponent<RectTransform>().sizeDelta.y );
        }
        
        _dimension = currentXPosition + _offset * -1;
        
        ScreensContainer.GetComponent<RectTransform>().offsetMax = new Vector2(_dimension, 0f);
    }

}