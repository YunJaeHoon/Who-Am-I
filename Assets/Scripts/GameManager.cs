using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("< Main Scene Group >")]
    public GameObject MainSceneGroup;                       // ���ξ� �׷�
    bool isMain = true;                                     // ���� ���� ���ΰ�?
    bool isSub = false;                                     // ���� ���� ���ΰ�?

    [Header("< Sub Scene Group >")]
    public GameObject SubSceneGroup;                                // ���� �� �׷�
    public TMP_Text HeadCountUI;                                    // �ο� �� ����

    public GameObject[] Player = new GameObject[6];                 // �÷��̾� â
    public GameObject[] PlayerColorArr = new GameObject[6];         // �÷��̾� â ���� ��ư

    [Header("< Color Select Group >")]
    public GameObject ColorSelectUI;                        // ���� ���� â
    public GameObject[] ColorButton = new GameObject[10];   // ���� ���� ��ư

    int HeadCount = 2;                                      // ���� �ο� ��

    GameObject PlayerColor;                                 // Ŭ���� �÷��̾� ����
    Image PlayerColorImage;                                 // Ŭ���� �÷��̾� ���� �̹���
                                                            // 
    int CurrentColor;                                       // ���� ���� ���� ���� �÷��̾�
    int CurrentColorBtn;                                    // ���� ���� ���� ���� â�� ���� ��ư
    int[] ColorArr = { 1, 2, 0, 0, 0, 0, 0, 0, 0, 0 };      // ���� Ȯ�� �迭

    void Awake()
    {
        // ����ȭ
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (Input.anyKeyDown && isMain)
        {
            // ���� ����
            isMain = false;
            isSub = true;

            // ���� �� �׷� ��Ȱ��ȭ, ���� �� �׷� Ȱ��ȭ(����â UI ����)
            MainSceneGroup.SetActive(false);
            SubSceneGroup.SetActive(true);
            ColorSelectUI.SetActive(false);
        }

        // ���� �� : ���� ���� â ���� �� �ڷΰ���
        if (Input.GetKeyDown(KeyCode.Escape) && isSub && !ColorSelectUI.activeSelf)
        {
            isMain = true;
            isSub = false;

            MainSceneGroup.SetActive(true);
            SubSceneGroup.SetActive(false);
        }

        // ���� �� : ���� ���� â ���� �� �ڷΰ���
        if (Input.GetKeyDown(KeyCode.Escape) && isSub && ColorSelectUI.activeSelf)
        {
            ColorSelectUI.SetActive(false);
        }
    }

    // > ��ư
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