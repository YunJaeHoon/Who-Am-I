using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    string[] WordArr = {"���缮","��ȣ��","������","�ҳ�ô�","��ź�ҳ��(BTS)","�̺���","Ȳ����","�ڸ��",
                        "������","������","������","�ֹν�","��ȫö","����(�ϵ���)","��(����)","�̼���",
                        "�絵����","������","�̽±�","�۰�ȣ","�������","�̼���","���ٸ�","������","���ϴ�",
                        "����(���)","�躴��","�ڼ���","������","������","�۰�","���߱�","���޼�","������",
                        "����","����","������","�豸","������","�ָ�","�̱���","������","��","������","�ں���",
                        "������","������","��(���)","����","�̰��","�豹��","�豸��","����","���̳��͵��",
                        "�峪��","������","õ����","��ȣ��","�̸���(ħ����)","���","������","�ٳ���","��",
                        "����","����","�θ���","���(�޷�)","�ڵ�","����","��纣��","��ġ","����","����",
                        "���","����","����","�丶��","ġ��","����","�Ҽ���","�ֵ���","����","����","�ұ�",
                        "�������","��(��ä)","��(��ä)","�ݶ�","���̴�","������","Ŀ��","���ݸ�(���ݷ�)",
                        "����","��Ƣ��","����ũ","��","ġŲ","����","��(����)","��(����)","��","��","ȣ����",
                        "����","����(���ۻ�)","��(����)","��ѱ�","����","������","�䳢","��","��","��",
                        "��(����)","������","����","����","������","��ǳ��","���ڷ�����","�����","��ǻ��",
                        "ī�޶�","����","����","����","��","����","���찳","���","ƫ��","�عٶ��","�ý�",
                        "����","��","���ּ�(����)","������","�������","����","å","�Ҳɳ���","��","��","��",
                        "��","���","��","��ź","ö","��(Ȳ��)","���̾Ƹ��","��","����","����","��ġ","�ظ�",
                        "��ȭ��","��ũ","����","����","�ҹ漭","������","����ã��","��Ʈ����","���׿��극����",
                        "������ġ","������","����","�౹","�ǻ�","����ö","����","���ǵ�","û�ʹ�","�����",
                        "����","��ü��","����","����","��Ź��","å��","����","����","�λ�","���ֵ�","����",
                        "���ѹα�(�ѱ�)","�Ϻ�","�߱�","�̱�","����","����","��(mountain)","�ٴ�","ȭ��","ȣ��",
                        "����","������ī","�Ͽ���","�ʸ���","����","��Ż����","������","����","�����","��λ�",
                        "����","����","���","������","����","��Ӹ�","���θ�","��(God)","����","�ͽ�","õ��",
                        "�Ǹ�","����","�뿹(���)","����(���ϸ�)","��ī��","����","����","���(�ﱹ��)",
                        "��������","����","¡¡��","�ڳ�","�Ǵ�","��¯��(¯��)","���󿡸�","����(���󿡸�)",
                        "�̽���(���󿡸�)","������(���󿡸�)","�����(���󿡸�)","ö��(¯��)","����(¯��)",
                        "����(¯��)","�ͱ�(¯��)","���̼�(¯��)","¯��(¯��)","������(¯��)","�����(¯��)",
                        "�տ���","���Ȱ�","�����","�ظ�����","������","����Ż","����","������","���÷�ũ",
                        "�׶󰡽�","�׷��̺���","����","����","����","������","��ƿ����","����","�ܹ���",
                        "����(������������)","�ϴ޸�","����","�Ҷ�","�ٸ��콺","���ֳ̾�","�巹�̺�","������",
                        "��ĭ","���ӽ�","�Ķ��","����ũ��","�Ƕ�̵�","����Ʈ","�縷","���ƽý�","����","����",
                        "����Ÿ(����Ÿ�۶�ũ)","������","������","������","��","����","��þ�","���","�Ʒ�",
                        "�����","����","����","������","������","��������","����ī��","������","������Ʈ",
                        "��ī����","�𸣰���","����(�����ڻ�)","�̽�����","�ٵ�","�ٷ罺","����","���̰�",
                        "����","����","������","������","��������","����","�귣��","����̸�","����","��",
                        "����ũ��ũ","�񿡰�","���丣","�ǻ�","��̶�","���̿�","���Ϸ���","����","����",
                        "������","���־ƴ�","��Ʈ(��)","�ҳ�","�Ҷ�ī","��","���ٳ�","������","��ī��","�ú�",
                        "����","ǥâ","����","��¥��","�ŵ��","������","�׻���","������","���","�Ƹ�","����",
                        "��","��ġ�","��ġ","����","����Ķ���","������","����","����","����","��¡��",
                        "�ƹ���","�̶�","��ü","�ƿ췼���¼�","���̹�","������","��Į��","�¾�","��",
                        "��쳪(������)","����","�ݼ�","ȭ��","��","�伺","õ�ռ�","�ؿռ�","��ռ�","��ũ��",
                        "��Ʈ�Ͻ�","���縮����","�˸���Ÿ","�ִ�(��)","Ƽ��","�ִϺ��","�ֽ�","�߽���",
                        "�߽���","����","������(��)","����","�����Ƴ�","�ö���(��)","����","�ȳ�(�ܿ�ձ�)",
                        "�ö���(�ܿ�ձ�)","����(��)","���","�丯","���","�츣��","����","����(��)",
                        "�̷�����","�̺�","�����","�϶����","�ڸ���4��","�ھ�","���̶�","��ũ","�ܳ�",
                        "�轺","����","����","����(��)","�����","����(��)","õ��","����","����","��(����)",
                        "�Ź�","����","����","������","�ĸ�(����)","���̽�","����(��)","���̸�","����","�ĵ�",
                        "����(���座��)","����(���座��)","����(���座��)","����(���座��)","���座��","�¿�",
                        "����","â��","����","��(��)","������","¡ũ��(��)","��Ŀ","���۸�","���̾��",
                        "ĸƾ�Ƹ޸�ī","��Ʈ��","�Ҹ���","�ʰ���","ī����","ī��","ī���","ī����",
                        "ī�ÿ����","ī�̻�","ī����","īŸ����","Į����Ÿ","�θ�","����","�ɳ�","����Ʋ��",
                        "����","����","�ڱ׸�","�ڸ�Ű","��(��)","ü��","����","��ħ","����","����","��",
                        "ũ����","Ŭ����","Ű�Ƴ�","Ų�巹��","Ʈ����","�ڱ���","������","������","Ÿ��","Ż��",
                        "Ż����","Ž��ġ","Ʈ����","Ʈ����Ÿ��","Ʈ���ٹ̾�","Ʈ����Ƽ������Ʈ","Ʈ��ġ",
                        "��Ʃ��","Ƽ��","�ۻ�","�ľ�ũ","����","�������÷�","������","Ǯ","������","����",
                        "���׿�","����","�ǵ齺ƽ","óŰ","�����˽�Ÿ��","�ǿ���","����","���̸ӵ���","��ī��",
                        "���ǽ�Ʈ","��Ʈ��","�����ϸ�Ʈ","��ŷ��","�ε�ȣ��","�ñ׸�(������ġ)","���",
                        "���(d.va)","������","������","�ڸ���","��Ŀ��","����","����","���»���","��","����",
                        "����","�ܿ�","��","����","����","��ǳ","����","���ڷ�","�ٽ�Ƽ��","����","�б�","��Ŭ",
                        "�غ��","�ø�Ʈ��","���������Ŀ","��ũ��","ĳ����(��ũ��)","������","����","�ٵ�",
                        "�丣���","Ʈ���̼�","�Ķ�","����","ȭ��","Ȱ","��ÿ�","�޸���","���̶�","��Ƽ��Ʈ",
                        "�긮����","�Ƴ�","����Ÿ","Ű����","������","����","�Ͻ�����","����̾�","ȣ��",
                        "ȣ��","����","��","�ؾ","����������","�μ��","�Ҵ�","������","������","������",
                        "����","������","����","���̰�","������","����","����","�Ĵ�","������","������","��ũ",
                        "����â","ö��","��罽","����","�Ǿ�","�ͶѶ��","�޶ѱ�","��","����","�ѱ�","����",
                        "����","����","������","�ڳ���","��Ÿ","����","����","�ǾƳ�","��Ÿ","���ڴ�","��",
                        "���̿ø�","¥���(�����)","������(������)","«��","������","����","�ܹ���","�׸���",
                        "�����³�ȭ","�ø���","������","�౸","�߱�","��","����","���","������Ʈ","����"};

    public Transform BackgounrdGroup;                               // ��� ���� ����

    public GameObject Background;                                   // ����ȭ�� ���
    public GameObject MainSceneGroup;                               // ���ξ� �׷�
    public GameObject SubSceneGroup;                                // ����� �׷�
    public GameObject ShowWords;                                    // ���þ� ����
    public GameObject ShowWordGroup;                                // ���þ�� �׷�

    public TMP_Text HeadCountUI;                                    // �ο��� ����
    public TMP_Text NicknameTitle;                                       // �г��� + "���þ�"
    public TMP_Text Warning;                                        // �г��� + "���� ������ �÷��̾ Ȯ���ϼ���"

    public GameObject[] Player = new GameObject[6];                 // �÷��̾� â �迭
    public GameObject[] PlayerColorBtnArr = new GameObject[6];      // �÷��̾� â�� ���� �迭
    public GameObject[] PlayerBackground = new GameObject[10];      // ��� ������ �迭
    public TMP_InputField[] PlayerNickname = new TMP_InputField[6]; // �÷��̾� �г��� �迭

    public GameObject ColorSelectUI;                                // ����â
    public GameObject[] ColorBtnArr = new GameObject[10];           // ����â ���� ��ư �迭

    GameObject PlayerColor;                                 // Ŭ���� �÷��̾� â�� ����
    Image PlayerColorImage;                                 // Ŭ���� �÷��̾� â�� ���� �̹���

    int HeadCount = 2;                                      // ���� �ο� ��

    int CurrentPlayerNumber;                                // ���� ���� ���� ���� �÷��̾� ��ȣ
    int CurrentColorBtn;                                    // ���� ���� ����â�� ���� ��ư
    int[] ColorArr = { 1, 2, 0, 0, 0, 0, 0, 0, 0, 0 };      // ���� Ȯ�� �迭

    bool isMain = true;                                     // ���� ���� ���ΰ�?
    bool isSub = false;                                     // ���� ���� ���ΰ�?
    bool isColorChanging = false;                           // ���� �÷��̾� ���� ���� ���ΰ�?

    List<GameObject> Backgrounds = new List<GameObject>();        // �÷��̾� ��� ����Ʈ
    List<String> Words = new List<String>();                      // �÷��̾� ���þ� ����Ʈ
    List<String> Nickname = new List<String>();                   // �÷��̾� �г��� ����Ʈ

    void Awake()
    {
        // ����ȭ
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        // ���ξ� -> �����
        if (Input.anyKeyDown && isMain)
        {
            isMain = false;
            isSub = true;

            MainSceneGroup.SetActive(false);
            SubSceneGroup.SetActive(true);
            ColorSelectUI.SetActive(false);
        }

        // ���� �� -> ���ξ�
        if (Input.GetKeyDown(KeyCode.Escape) && isSub && !ColorSelectUI.activeSelf)
        {
            isMain = true;
            isSub = false;

            MainSceneGroup.SetActive(true);
            SubSceneGroup.SetActive(false);
        }

        // ���� �� ����â �ݱ�
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
                    AddPlayerColorImage = PlayerColorBtnArr[HeadCount].GetComponent<Image>();
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

                    ColorBtnArr[i].transform.GetChild(0).gameObject.SetActive(true);

                    break;
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
                    ColorBtnArr[i].transform.GetChild(0).gameObject.SetActive(false);
                    break;
                }
            }

            HeadCount -= 1;
            Player[HeadCount].SetActive(false);
            HeadCountUI.text = HeadCount.ToString();
        }
    }

    // ����â ����
    public void ColorSelectOn()
    {
        if(!isColorChanging)
        {
            ColorSelectUI.SetActive(true);
            PlayerColor = EventSystem.current.currentSelectedGameObject;
            CurrentPlayerNumber = Array.IndexOf(PlayerColorBtnArr, PlayerColor) + 1;

            isColorChanging = true;
        }
        else
        {
            ColorSelectUI.SetActive(false);
            isColorChanging = false;
        }
    }

    // ����â ���� ����
    public void ColorSelect()
    {
        GameObject SelectColor = EventSystem.current.currentSelectedGameObject;
        CurrentColorBtn = Array.IndexOf(ColorBtnArr, SelectColor);
        PlayerColorImage = PlayerColor.GetComponent<Image>();

        if (ColorArr[CurrentColorBtn] == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                if (ColorArr[i] == CurrentPlayerNumber)
                {
                    ColorArr[i] = 0;
                    ColorBtnArr[i].transform.GetChild(0).gameObject.SetActive(false);
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

            for(int i = 0; i < 10; i++)
            {
                if (ColorArr[i] == CurrentPlayerNumber)
                {
                    ColorArr[i] = 0;
                    ColorBtnArr[i].transform.GetChild(0).gameObject.SetActive(false);
                    Debug.Log(i);
                }
            }

            ColorArr[CurrentColorBtn] = CurrentPlayerNumber;
            ColorBtnArr[CurrentColorBtn].transform.GetChild(0).gameObject.SetActive(true);

            isColorChanging = false;

            ColorSelectUI.SetActive(false);
        }
    }

    // ���� ���� ��ư
    public void GameStart()
    {
        isSub = false;
        SubSceneGroup.SetActive(false);
        ShowWords.SetActive(true);
        Invoke("DownShowWords", 1.5f);
        Background.SetActive(false);

        for(int i = 1; i <= HeadCount; i++)
        {
            // �÷��̾� ��� ����
            GameObject background_obj;
            background_obj = Instantiate(PlayerBackground[Array.IndexOf(ColorArr, i)],BackgounrdGroup);
            Backgrounds.Add(background_obj);
            Backgrounds[i-1].SetActive(false);

            // �÷��̾� �̸� ����
            Nickname.Add(PlayerNickname[i-1].text);

            // �÷��̾� ���þ� ����
            Words.Add(WordArr[UnityEngine.Random.Range(0,WordArr.Length)]);
        }
    }

    // ���þ� ���� �ݱ�
    void DownShowWords()
    {
        ShowWords.SetActive(false);
        Backgrounds[0].SetActive(true);
        ShowWordGroup.SetActive(true);
        NicknameTitle.text = Nickname[0] + "\n���� ���þ�";
        Warning.text = Nickname[0] + " ����\n ������ �÷��̾ Ȯ���ϼ���";
    }
}