using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("< Main Scene Group >")]
    public GameObject MainSceneGroup;                       // 메인씬 그룹
    bool isMain = true;                                     // 현재 메인 씬인가?
    bool isSub = false;                                     // 현재 서브 씬인가?

    [Header("< Sub Scene Group >")]
    public GameObject SubSceneGroup;                                // 서브 씬 그룹
    public TMP_Text HeadCountUI;                                    // 인원 수 숫자

    public GameObject[] Player = new GameObject[6];                 // 플레이어 창
    public GameObject[] PlayerColorArr = new GameObject[6];         // 플레이어 창 색상 버튼

    [Header("< Color Select Group >")]
    public GameObject ColorSelectUI;                        // 색상 선택 창
    public GameObject[] ColorButton = new GameObject[10];   // 색상 선택 버튼

    int HeadCount = 2;                                      // 현재 인원 수

    GameObject PlayerColor;                                 // 클릭한 플레이어 색상
    Image PlayerColorImage;                                 // 클릭한 플레이어 색상 이미지
                                                            // 
    int CurrentColor;                                       // 현재 색상 변경 중인 플레이어
    int CurrentColorBtn;                                    // 현재 누른 색상 선택 창의 색상 버튼
    int[] ColorArr = { 1, 2, 0, 0, 0, 0, 0, 0, 0, 0 };      // 색상 확인 배열

    void Awake()
    {
        // 최적화
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (Input.anyKeyDown && isMain)
        {
            // 게임 시작
            isMain = false;
            isSub = true;

            // 메인 씬 그룹 비활성화, 서브 씬 그룹 활성화(색상창 UI 제외)
            MainSceneGroup.SetActive(false);
            SubSceneGroup.SetActive(true);
            ColorSelectUI.SetActive(false);
        }

        // 서브 씬 : 색상 선택 창 없을 때 뒤로가기
        if (Input.GetKeyDown(KeyCode.Escape) && isSub && !ColorSelectUI.activeSelf)
        {
            isMain = true;
            isSub = false;

            MainSceneGroup.SetActive(true);
            SubSceneGroup.SetActive(false);
        }

        // 서브 씬 : 색상 선택 창 있을 때 뒤로가기
        if (Input.GetKeyDown(KeyCode.Escape) && isSub && ColorSelectUI.activeSelf)
        {
            ColorSelectUI.SetActive(false);
        }
    }

    // > 버튼
    public void HeadCountUp()
    {
        if(HeadCount < 6)
        {
            for(int i = 0; i < 10; i++)
            {
                if (ColorArr[i] == 0)
                {
                    Image AddPlayerColorImage;
                    ColorArr[i] = HeadCount + 1;
                    AddPlayerColorImage = PlayerColorArr[HeadCount].GetComponent<Image>();
                    switch (i)
                    {
                        case 0:
                            AddPlayerColorImage.color = new Color32(255, 0, 0, 255);
                            break;
                        case 1:
                            AddPlayerColorImage.color = new Color32(255, 174, 0, 255);
                            break;
                        case 2:
                            AddPlayerColorImage.color = new Color32(255, 211, 0, 255);
                            break;
                        case 3:
                            AddPlayerColorImage.color = new Color32(169, 255, 0, 255);
                            break;
                        case 4:
                            AddPlayerColorImage.color = new Color32(7, 212, 0, 255);
                            break;
                        case 5:
                            AddPlayerColorImage.color = new Color32(0, 251, 255, 255);
                            break;
                        case 6:
                            AddPlayerColorImage.color = new Color32(0, 119, 255, 255);
                            break;
                        case 7:
                            AddPlayerColorImage.color = new Color32(170, 0, 255, 255);
                            break;
                        case 8:
                            AddPlayerColorImage.color = new Color32(0, 0, 0, 255);
                            break;
                        case 9:
                            AddPlayerColorImage.color = new Color32(255, 255, 255, 255);
                            break;
                    }
                    break;
                }
            }

            for (int i = 0; i < 10; i++)
            {
                if (ColorArr[i] != 0)
                {
                    ColorButton[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                else if (ColorArr[i] == 0)
                {
                    ColorButton[i].transform.GetChild(0).gameObject.SetActive(false);
                }
            }

            Player[HeadCount].SetActive(true);
            HeadCount += 1;
            HeadCountUI.text = HeadCount.ToString();
        }
    }

    public void HeadCountDown()
    {
        if (HeadCount > 2)
        {
            for(int i = 0; i < 10; i++)
            {
                if (ColorArr[i] == HeadCount)
                {
                    ColorArr[i] = 0;
                    break;
                }
            }

            for (int i = 0; i < 10; i++)
            {
                if (ColorArr[i] != 0)
                {
                    ColorButton[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                else if (ColorArr[i] == 0)
                {
                    ColorButton[i].transform.GetChild(0).gameObject.SetActive(false);
                }
            }

            HeadCount -= 1;
            Player[HeadCount].SetActive(false);
            HeadCountUI.text = HeadCount.ToString();
        }
    }

    public void ColorSelectOn()
    {
        ColorSelectUI.SetActive(true);
        PlayerColor = EventSystem.current.currentSelectedGameObject;
        CurrentColor = Array.IndexOf(PlayerColorArr, PlayerColor) + 1;
    }

    public void ColorSelect()
    {
        GameObject SelectColor = EventSystem.current.currentSelectedGameObject;
        CurrentColorBtn = Array.IndexOf(ColorButton, SelectColor);
        PlayerColorImage = PlayerColor.GetComponent<Image>();

        if (ColorArr[CurrentColorBtn] == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                if (ColorArr[i] == CurrentColor)
                {
                    ColorArr[i] = 0;
                    break;
                }
            }
            switch (CurrentColorBtn)
            {
                case 0:
                    PlayerColorImage.color = new Color32(255, 0, 0, 255);
                    break;
                case 1:
                    PlayerColorImage.color = new Color32(255, 174, 0, 255);
                    break;
                case 2:
                    PlayerColorImage.color = new Color32(255, 211, 0, 255);
                    break;
                case 3:
                    PlayerColorImage.color = new Color32(169, 255, 0, 255);
                    break;
                case 4:
                    PlayerColorImage.color = new Color32(7, 212, 0, 255);
                    break;
                case 5:
                    PlayerColorImage.color = new Color32(0, 251, 255, 255);
                    break;
                case 6:
                    PlayerColorImage.color = new Color32(0, 119, 255, 255);
                    break;
                case 7:
                    PlayerColorImage.color = new Color32(170, 0, 255, 255);
                    break;
                case 8:
                    PlayerColorImage.color = new Color32(0, 0, 0, 255);
                    break;
                case 9:
                    PlayerColorImage.color = new Color32(255, 255, 255, 255);
                    break;
            }
            ColorArr[CurrentColorBtn] = CurrentColor;

            for (int i = 0; i < 10; i++)
            {
                if (ColorArr[i] != 0)
                {
                    ColorButton[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                else if (ColorArr[i] == 0)
                {
                    ColorButton[i].transform.GetChild(0).gameObject.SetActive(false);
                }
            }

            ColorSelectUI.SetActive(false);
        }
    }

    public void GameStart()
    {
        isSub = false;
        SubSceneGroup.SetActive(false);
    }
}