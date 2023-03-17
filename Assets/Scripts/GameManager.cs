using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    [Header("-------------���ξ� ������Ʈ-------------")]
    public GameObject MainSceneGroup;                               // ���ξ� �׷�
    public GameObject Background;                                   // ����ȭ�� ���

    [Header("-------------����� ������Ʈ-------------")]
    public GameObject SubSceneGroup;                                // ����� �׷�
    public GameObject[] Player = new GameObject[6];                 // �÷��̾� â �迭
    public GameObject[] PlayerColorBtnArr = new GameObject[6];      // �÷��̾� â�� ���� �迭
    public TMP_Text HeadCountUI;                                    // �ο��� ����

    [Space(10f)]
    public GameObject ColorSelectUI;                                // ����â
    public GameObject[] ColorBtnArr = new GameObject[10];           // ����â ���� ��ư �迭

    [Space(10f)]
    GameObject PlayerColor;                                         // Ŭ���� �÷��̾� â�� ����
    Image PlayerColorImage;                                         // Ŭ���� �÷��̾� â�� ���� �̹���
    int CurrentPlayerNumber;                                        // ���� ���� ���� ���� �÷��̾� ��ȣ
    int CurrentColorBtn;                                            // ���� ���� ����â�� ���� ��ư
    int[] ColorArr = { 1, 2, 0, 0, 0, 0, 0, 0, 0, 0 };              // ���� Ȯ�� �迭

    [Space(10f)]
    public GameObject[] PlayerBackground = new GameObject[10];      // ��� ������ �迭
    public TMP_InputField[] PlayerNickname = new TMP_InputField[6]; // �÷��̾� �г��� �迭

    [Header("-----------���þ� ������ ������Ʈ-----------")]
    public GameObject ShowWordGroup;                                // ���þ�� �׷�
    public GameObject ShowWordsSlide;                               // ���þ� ���� �����̵�
    public GameObject CheckWordGroup;                               // ���þ� ������ �׷�

    [Space(10f)]
    public TMP_Text NicknameTitle;                                  // �г��� + "���þ�"
    public TMP_Text Warning;                                        // �г��� + "���� ������ �÷��̾ Ȯ���ϼ���"
    public TMP_Text PlayerWord;                                     // �÷��̾� ���þ�

    [Header("-------------���Ӿ� ������Ʈ-------------")]
    public GameObject GameStartGroup;                               // ���Ӿ� �׷�
    public GameObject GameStartSlide;                               // ���� ���� �����̵�
    public TMP_Text Round_text;                                     // ����
    public TMP_Text NicknameQuestion;                               // �г��� + "���� ����"

    // ��Ʈ ������Ʈ
    [Space(10f)]
    public GameObject HintBtn;                                      // ��Ʈ �׷�
    public GameObject HintCountBtn;                                 // ���� �� ��Ʈ ��ư
    public GameObject HintFirstBtn;                                 // �ʼ� ��Ʈ ��ư
    public GameObject Hint_countCover;                              // ���� �� ��Ʈ ������
    public GameObject Hint_firstCover;                              // �ʼ� ��Ʈ ������
    public TMP_Text Hint_count;                                     // ���ڼ� ��Ʈ
    public TMP_Text Hint_first;                                     // �ʼ� ��Ʈ

    // ������ ������Ʈ
    [Space(10f)]
    public GameObject ForgetGroup;                                  // ������ �׷�
    public GameObject BeforeCheckGroup;                             // ������ > ���þ� Ȯ�� �� �׷�
    public GameObject AfterCheckGroup;                              // ������ > ���þ� Ȯ�� �� �׷�
    public TMP_Text ForgetWarning;                                  // ������ ���ǻ���
    public TMP_Text ForgetWord;                                     // ������ ���þ�

    // �޸��� ������Ʈ
    [Space(10f)]
    public GameObject MemoGroup;                                    // �޸��� �׷�
    public TMP_InputField Memo;                                     // �޸���
    string[] PlayerMemo = new string[6];                            // �÷��̾� �޸� �迭

    // ���� ������Ʈ
    [Space(10f)]
    public GameObject CountDownGroup;                               // ī��Ʈ�ٿ� �׷�
    public GameObject FailureGroup;                                 // ���� �׷�
    public TMP_InputField Challenge;                                // ���� �Է�
    public TMP_Text CountDown;                                      // ī��Ʈ�ٿ�
    public TMP_Text Failure_text;                                   // ���� ���

    [Header("------------���ӿ����� ������Ʈ------------")]
    public GameObject GameOverGroup;                                // �������� �׷�
    public GameObject[] PlayerResults = new GameObject[6];          // �й� �÷��̾� �̸�ǥ �迭
    public GameObject[] PlayerResultsColor = new GameObject[6];     // �й� �÷��̾� �̸�ǥ ����
    public TMP_Text[] PlayerResultsNickname = new TMP_Text[6];      // �й� �÷��̾� �̸�ǥ �̸�
    public TMP_Text[] PlayerResultsWord = new TMP_Text[6];          // �й� �÷��̾� �̸�ǥ ���þ�

    [Header("-------------���� ������Ʈ-------------")]
    public GameObject[] HelpPages = new GameObject[9];              // ���� ������ �迭
    public TMP_Text HelpPageText;                                   // ���� ������ �ؽ�Ʈ

    [Header("-------------���� ������Ʈ-------------")]
    public AudioSource BGMPlayer;                                   // ��� �÷��̾�
    public AudioSource SFXPlayer;                                   // ȿ���� �÷��̾�
    public AudioSource VictoryPlayer;                               // �������� �÷��̾�
    public AudioClip[] BGMMusic = new AudioClip[3];                 // ��� ����
    public AudioClip[] SFXMusic = new AudioClip[6];                 // ȿ���� ����
    public AudioMixer AudioMixer;                                   // ����� �ͼ�

    // ȯ�漳�� ������Ʈ
    [Space(10f)]
    public Slider BGM_slider;                                       // ��� �����̴�
    public Slider SFX_slider;                                       // ȿ���� �����̴�
    public Toggle BGM_toggle;                                       // ��� ���Ұ� üũ�ڽ�
    public Toggle SFX_toggle;                                       // ȿ���� ���Ұ� üũ�ڽ�
    public TMP_Text BGM_slidertext;                                 // ��� ���� �ؽ�Ʈ
    public TMP_Text SFX_slidertext;                                 // ȿ���� ���� �ؽ�Ʈ

    [Header("--------------��Ÿ ������Ʈ--------------")]
    public Transform BackgroundGroup;                               // ��� ���� ����
    public GameObject Smog;                                         // ��ư ���� �����

    Dictionary<string, string> WordDict = new Dictionary<string, string>()
    {
        { "���缮", "������" },
        { "��ȣ��", "������" },
        { "������", "������" },
        { "�ҳ�ô�", "��������" },
        { "��ź�ҳ��", "����������" },
        { "�̺���", "������" },
        { "�ڸ��", "������" },
        { "�迬��", "������" },
        { "�����ִϾ�", "����������" },
        { "����", "����" },
        { "���", "����" },
        { "���", "����" },
        { "������", "������" },
        { "����", "����" },
        { "����", "����" },
        { "�ƺ�", "����" },
        { "������", "������" },
        { "�絵����", "��������" },
        { "�۰�ȣ", "������" },
        { "�������", "��������" },
        { "�̼���", "������" },
        { "���ٸ�", "������" },
        { "����", "����" },
        { "������", "������" },
        { "�豸", "����" },
        { "������", "������" },
        { "�ָ�", "����" },
        { "���", "����" },
        { "������", "������" },
        { "�ٳ���", "������" },
        { "��", "��" },
        { "����", "����" },
        { "����", "����" },
        { "�θ���", "������" },
        { "�ڵ�", "����" },
        { "����", "����" },
        { "��纣��", "��������" },
        { "��ġ", "����" },
        { "����", "����" },
        { "����", "����" },
        { "���", "����" },
        { "����", "����" },
        { "����", "������" },
        { "�丶��", "������" },
        { "ġ��", "����" },
        { "����", "����" },
        { "�Ҽ���", "������" },
        { "�ֵ���", "������" },
        { "����", "����" },
        { "����", "����" },
        { "�ұ�", "����" },
        { "�������", "��������" },
        { "��", "��" },
        { "��", "��" },
        { "�ݶ�", "����" },
        { "���̴�", "������" },
        { "������", "������" },
        { "Ŀ��", "����" },
        { "���ݷ�", "������" },
        { "����", "����" },
        { "��Ƣ��", "������" },
        { "����ũ", "������" },
        { "��", "��" },
        { "ġŲ", "����" },
        { "����", "����" },
        { "��", "��" },
        { "��", "��" },
        { "��", "��" },
        { "��", "��" },
        { "ȣ����", "������" },
        { "����", "����" },
        { "���ۻ�", "������" },
        { "��ѱ�", "������" },
        { "����", "����" },
        { "������", "������" },
        { "�䳢", "����" },
        { "��", "��" },
        { "��", "��" },
        { "��", "��" },
        { "��", "��" },
        { "������", "������" },
        { "����", "������" },
        { "����", "����" },
        { "������", "������" },
        { "��ǳ��", "������" },
        { "���ڷ�����", "����������" },
        { "�����", "������" },
        { "��ǻ��", "������" },
        { "ī�޶�", "������" },
        { "����", "����" },
        { "����", "����" },
        { "����", "����" },
        { "��", "��" },
        { "����", "����" },
        { "���찳", "������" },
        { "���", "����" },
        { "ƫ��", "����" },
        { "�عٶ��", "��������" },
        { "�ý�", "����" },
        { "����", "����" },
        { "��", "��" },
        { "����", "����" },
        { "������", "������" },
        { "�������", "��������" },
        { "����", "����" },
        { "å", "��" },
        { "�Ҳɳ���", "��������" },
        { "��", "����" },
        { "��", "��" },
        { "��", "��" },
        { "��", "��" },
        { "���", "����" },
        { "��", "����" },
        { "��ź", "����" },
        { "ö", "��" },
        { "��", "��" },
        { "���̾Ƹ��", "����������" },
        { "��", "��" },
        { "����", "����" },
        { "����", "����" },
        { "��ġ", "����" },
        { "�ظ�", "����" },
        { "��ȭ��", "������" },
        { "��ũ", "����" },
        { "����", "����" },
        { "����", "����" },
        { "�ҹ漭", "������" },
        { "������", "������" },
        { "����ã��", "��������" },
        { "��Ʈ����", "��������" },
        { "���׿��극����", "��������������" },
        { "������ġ", "��������" },
        { "������", "������" },
        { "����", "����" },
        { "�౹", "����" },
        { "�ǻ�", "����" },
        { "����ö", "������" },
        { "����", "����" },
        { "���ǵ�", "������" },
        { "û�ʹ�", "������" },
        { "�����", "������" },
        { "����", "��ü��" },
        { "����", "������" },
        { "����", "����" },
        { "��Ź��", "������" },
        { "å��", "����" },
        { "����", "����" },
        { "����", "����" },
        { "�λ�", "����" },
        { "���ֵ�", "������" },
        { "����", "����" },
        { "���ѹα�", "��������" },
        { "�Ϻ�", "����" },
        { "�߱�", "����" },
        { "�̱�", "����" },
        { "����", "����" },
        { "����", "����" },
        { "��", "��" },
        { "�ٴ�", "����" },
        { "ȭ��", "����" },
        { "ȣ��", "����" },
        { "����", "����" },
        { "������ī", "��������" },
        { "�Ͽ���", "������" },
        { "�ʸ���", "������" },
        { "����", "����" },
        { "��Ż����", "��������" },
        { "������", "������" },
        { "����", "����" },
        { "�����", "������" },
        { "��λ�", "������" },
        { "����", "����" },
        { "����", "����" },
        { "���", "������" },
        { "������", "������" },
        { "����", "����" },
        { "��Ӹ�", "������" },
        { "���θ�", "������" },
        { "��", "��" },
        { "����", "����" },
        { "�ͽ�", "����" },
        { "õ��", "����" },
        { "�Ǹ�", "����" },
        { "����", "����" },
        { "�뿹", "����" },
        { "��ī��", "������" },
        { "����", "����" },
        { "����", "����" },
        { "���", "����" },
        { "��������", "��������" },
        { "����", "����" },
        { "¡¡��", "������" },
        { "�ڳ�", "����" },
        { "�Ǵ�", "����" },
        { "¯��", "����" },
        { "���󿡸�", "��������" },
        { "�տ���", "������" },
        { "���Ȱ�", "������" },
        { "�����", "������" },
        { "�ظ�����", "��������" },
        { "�ܹ���", "������" },
        { "�Ķ��", "������" },
        { "����ũ��", "��������" },
        { "�Ƕ�̵�", "��������" },
        { "�縷", "����" },
        { "���ƽý�", "��������" },
        { "�Ʒ�", "����" },
        { "����", "����" },
        { "��", "��" },
        { "����", "����" },
        { "ǥâ", "����" },
        { "����", "����" },
        { "�׻���", "������" },
        { "���", "����" },
        { "����", "����" },
        { "��", "��" },
        { "��ġ�", "��������" },
        { "��ġ", "����" },
        { "����", "������" },
        { "����Ķ���", "����������" },
        { "������", "������" },
        { "����", "����" },
        { "����", "����" },
        { "����", "����" },
        { "��¡��", "������" },
        { "�̶�", "����" },
        { "��ü", "����" },
        { "�¾�", "����" },
        { "��", "��" },
        { "ȭ��", "����" },
        { "����", "����" },
        { "�����", "������" },
        { "õ��", "����" },
        { "����", "����" },
        { "����", "����" },
        { "��", "��" },
        { "�Ź�", "����" },
        { "����", "����" },
        { "����", "����" },
        { "������", "������" },
        { "�ĸ�", "����" },
        { "����", "����" },
        { "�ĵ�", "����" },
        { "��Ÿ����", "��������" },
        { "���座��", "��������" },
        { "�¿�", "����" },
        { "����", "����" },
        { "â��", "����" },
        { "��Ŀ", "����" },
        { "���۸�", "������" },
        { "���̾��", "��������" },
        { "����", "����" },
        { "��Ʈ��", "������" },
        { "�θ�", "����" },
        { "����", "����" },
        { "ü��", "����" },
        { "����", "����" },
        { "��ħ", "����" },
        { "����", "����" },
        { "����", "����" },
        { "��", "��" },
        { "��Ʃ��", "������" },
        { "�ۻ�", "����" },
        { "����", "����" },
        { "������", "������" },
        { "Ǯ", "��" },
        { "������", "������" },
        { "����", "����" },
        { "����", "����" },
        { "óŰ", "����" },
        { "�����˽�Ÿ��", "������������" },
        { "���", "����" },
        { "���»���", "��������" },
        { "��", "��" },
        { "����", "����" },
        { "�ܿ�", "����" },
        { "��", "��" },
        { "����", "����" },
        { "����", "����" },
        { "��ǳ", "����" },
        { "����", "����" },
        { "���ڷ�", "������" },
        { "�б�", "����" },
        { "��Ŭ", "����" },
        { "������", "������" },
        { "����", "����" },
        { "�ٵ�", "����" },
        { "������", "������" },
        { "����", "����" },
        { "�Ͻ�����", "��������" },
        { "ȣ��", "����" },
        { "����", "����" },
        { "��", "��" },
        { "�ؾ", "������" },
        { "����������", "����������" },
        { "�μ��", "������" },
        { "�Ҵ�", "����" },
        { "������", "������" },
        { "������", "������" },
        { "������", "������" },
        { "����", "����" },
        { "������", "������" },
        { "����", "����" },
        { "���̰�", "������" },
        { "������", "������" },
        { "����", "����" },
        { "����", "����" },
        { "�Ĵ�", "����" },
        { "������", "������" },
        { "������", "������" },
        { "������", "������" },
        { "��ũ", "����" },
        { "����â", "������" },
        { "ö��", "����" },
        { "��罽", "������" },
        { "����", "����" },
        { "�Ǿ�", "����" },
        { "�ͶѶ��", "��������" },
        { "�޶ѱ�", "������" },
        { "���Ϸ�", "������" },
        { "��", "��" },
        { "����", "����" },
        { "�ѱ�", "����" },
        { "����", "����" },
        { "����", "����" },
        { "����", "����" },
        { "������", "������" },
        { "�ڳ���", "������" },
        { "��Ÿ", "����" },
        { "����", "����" },
        { "����", "����" },
        { "�ǾƳ�", "������" },
        { "����ũ", "��������" },
        { "��Ÿ", "����" },
        { "���ڴ�", "������" },
        { "��", "��" },
        { "���̿ø�", "��������" },
        { "�Ҹ�", "����" },
        { "ȣ����", "��������" },
        { "¥���", "������" },
        { "������", "������" },
        { "«��", "����" },
        { "������", "������" },
        { "����", "����" },
        { "�ܹ���", "������" },
        { "�׸���", "������" },
        { "�����³�ȭ", "����������" },
        { "�ø���", "������" },
        { "�޽�", "����" },
        { "ȣ����", "������" },
        { "������", "������" },
        { "�౸", "����" },
        { "�߱�", "����" },
        { "��", "����" },
        { "����", "����" },
        { "���", "����" },
        { "������Ʈ", "��������" },
        { "����", "����" },
        { "���͸�", "������" },
        { "��", "��" },
        { "�Ѹ�", "����" },
        { "���", "����" },
        { "�ɸ���", "������" },
        { "����", "������" },
        { "����ź", "������" },
        { "����", "����" },
        { "��Ʋ��", "������" },
        { "īī����", "��������" },
        { "���̹�", "������" },
        { "����", "����" },
        { "�ð�", "����" },
        { "�ð�", "����" },
        { "���ν�Ÿ��", "����������" },
    };

    int HeadCount = 2;                                              // ���� �ο� ��
    int Who = 0;                                                    // �÷��̾� ����
    int Round = 1;                                                  // ���� ����
    int[] order = { 1, 2, 3, 4, 5, 6 };                             // ����
    int HelpPageNumber = 1;                                         // ���� ���� ������

    bool isMain = true;                                             // ���� ���� ���ΰ�?
    bool isSub = false;                                             // ���� ���� ���ΰ�?
    bool isColorChanging = false;                                   // ���� �÷��̾� ���� ���� ���ΰ�?
    bool isCorrect = false;                                         // ���� �����ߴ°�?
    bool isBGMMute = false;                                         // ��� ���Ұ��ΰ�?
    bool isSFXMute = false;                                         // ȿ���� ���Ұ��ΰ�?

    List<GameObject> Backgrounds = new List<GameObject>();          // �÷��̾� ��� ����Ʈ
    List<String> Words = new List<String>();                        // �÷��̾� ���þ� ����Ʈ
    List<String> Nickname = new List<String>();                     // �÷��̾� �г��� ����Ʈ

    enum BGM { MainBGM, ShowWordBGM, GameBGM };
    enum SFX { Click, Slide, Hint, Fail, Over, Beep };

    void Awake()
    {
        // ����ȭ
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        // ���ξ� -> �����
        if (Input.anyKeyDown && isMain && !EventSystem.current.IsPointerOverGameObject())
        {
            PlaySFX(SFX.Click);

            isMain = false;
            isSub = true;

            MainSceneGroup.SetActive(false);
            SubSceneGroup.SetActive(true);
            ColorSelectUI.SetActive(false);
        }

        // ���� �� -> ���ξ�
        if (Input.GetKeyDown(KeyCode.Escape) && isSub && !ColorSelectUI.activeSelf)
        {
            PlaySFX(SFX.Click);

            GoMain();
        }

        // ���� �� ����â �ݱ�
        if (Input.GetKeyDown(KeyCode.Escape) && isSub && ColorSelectUI.activeSelf)
        {
            PlaySFX(SFX.Click);

            ColorSelectUI.SetActive(false);
            Smog.SetActive(false);
        }
    }

    // ����ȭ������
    public void GoMain()
    {
        PlaySFX(SFX.Click);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ���� ����
    public void EndGame()
    {
        PlaySFX(SFX.Click);

        Application.Quit();
    }

    // �ٽ��ϱ�
    public void Regame()
    {
        for (int i = 0; i < 6; i++)
        {
            if (i < HeadCount)
            {
                PlayerMemo[i] = "";
                Backgrounds[i].SetActive(true);
            }
            order[i] = i + 1;
        }

        Transform[] childList = BackgroundGroup.GetComponentsInChildren<Transform>();
        foreach (Transform child in childList)
        {
            if (child.name != BackgroundGroup.name)
            {
                Destroy(child.gameObject);
            }
        }

        Backgrounds.Clear();
        Words.Clear();
        Nickname.Clear();
        Who = 0;
        Round = 1;

        GameOverGroup.SetActive(false);
        HintCountBtn.SetActive(true);
        HintFirstBtn.SetActive(true);
        Hint_countCover.SetActive(true);
        Hint_firstCover.SetActive(true);
        GameStart();
    }

    // â ���� �Լ� (����׶�)
    public void ShowWithSmog(GameObject obj)
    {
        PlaySFX(SFX.Click);

        obj.SetActive(true);
        Smog.SetActive(true);
    }

    // â ���� �Լ�
    public void Show(GameObject obj)
    {
        PlaySFX(SFX.Click);

        obj.SetActive(true);
    }

    // â �ݱ� �Լ� (����׶�)
    public void DownWithSmog(GameObject obj)
    {
        PlaySFX(SFX.Click);

        obj.SetActive(false);
        Smog.SetActive(false);
    }

    // â �ݱ� �Լ�
    public void Down(GameObject obj)
    {
        PlaySFX(SFX.Click);

        obj.SetActive(false);
    }

    // ���� > ��ư
    public void HelpPageUp()
    {
        PlaySFX(SFX.Click);

        if (HelpPageNumber < 9)
        {
            HelpPages[HelpPageNumber - 1].SetActive(false);
            HelpPages[HelpPageNumber].SetActive(true);
            HelpPageNumber++;
            HelpPageText.text = HelpPageNumber.ToString() + " / 9";
        }
    }

    // ���� < ��ư
    public void HelpPageDown()
    {
        PlaySFX(SFX.Click);

        if (HelpPageNumber > 1)
        {
            HelpPages[HelpPageNumber - 1].SetActive(false);
            HelpPages[HelpPageNumber - 2].SetActive(true);
            HelpPageNumber--;
            HelpPageText.text = HelpPageNumber.ToString() + " / 9";
        }
    }

    // �ο��� > ��ư
    public void HeadCountUp()
    {
        PlaySFX(SFX.Click);

        if (HeadCount < 6)
        {
            for(int i = 0; i < 10; i++)
            {
                if (ColorArr[i] == 0)
                {
                    Image AddPlayerColorImage;
                    ColorArr[i] = HeadCount + 1;
                    AddPlayerColorImage = PlayerColorBtnArr[HeadCount].GetComponent<Image>();
                    AddPlayerColorImage.color = ImageColorChange(i);

                    ColorBtnArr[i].transform.GetChild(0).gameObject.SetActive(true);

                    break;
                }
            }

            Player[HeadCount].SetActive(true);
            HeadCount += 1;
            HeadCountUI.text = HeadCount.ToString();
        }
    }

    // �ο��� < ��ư
    public void HeadCountDown()
    {
        PlaySFX(SFX.Click);

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
        PlaySFX(SFX.Click);
        Smog.SetActive(true);

        if (!isColorChanging)
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
        PlaySFX(SFX.Click);

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

            PlayerColorImage.color = ImageColorChange(CurrentColorBtn);

            for (int i = 0; i < 10; i++)
            {
                if (ColorArr[i] == CurrentPlayerNumber)
                {
                    ColorArr[i] = 0;
                    ColorBtnArr[i].transform.GetChild(0).gameObject.SetActive(false);
                }
            }

            ColorArr[CurrentColorBtn] = CurrentPlayerNumber;
            ColorBtnArr[CurrentColorBtn].transform.GetChild(0).gameObject.SetActive(true);

            isColorChanging = false;

            ColorSelectUI.SetActive(false);
            Smog.SetActive(false);
        }
    }

    // ���� ���� ��ư
    public void GameStart()
    {
        isSub = false;
        SubSceneGroup.SetActive(false);
        ShowWordsSlide.SetActive(true);

        BGMPlayer.Stop();
        PlaySFX(SFX.Slide);

        Invoke("DownShowWords", 1.5f);
        Background.SetActive(false);

        for(int i = 1; i <= HeadCount; i++)
        {
            // �÷��̾� ��� ����
            GameObject background_obj;
            background_obj = Instantiate(PlayerBackground[Array.IndexOf(ColorArr, i)],BackgroundGroup);
            Backgrounds.Add(background_obj);
            Backgrounds[i-1].SetActive(false);

            // �÷��̾� �̸� ����
            Nickname.Add(PlayerNickname[i-1].text);

            // �÷��̾� ���þ� ����
            System.Random random = new System.Random();
            int index = random.Next(WordDict.Count);
            Words.Add(WordDict.Keys.ElementAt(index));
        }

        // ���� ����
        for (int i = 0; i < 20; i++)
        {
            int r1 = UnityEngine.Random.Range(0, HeadCount);
            int r2 = UnityEngine.Random.Range(0, HeadCount);

            GameObject temp1 = Backgrounds[r1];
            Backgrounds[r1] = Backgrounds[r2];
            Backgrounds[r2] = temp1;

            String temp2 = Nickname[r1];
            Nickname[r1] = Nickname[r2];
            Nickname[r2] = temp2;

            int temp3 = order[r1];
            order[r1] = order[r2];
            order[r2] = temp3;
        }

        CheckWordGroup.SetActive(true);
    }

    // ���þ� ���� �����̵� �ݱ�
    void DownShowWords()
    {
        PlayBGM(BGM.ShowWordBGM);
        Backgrounds[0].SetActive(true);
        ShowWordGroup.SetActive(true);

        NicknameTitle.text = Nickname[0] + "\n���� ���þ�";
        Warning.text = Nickname[0] + " ����\n ������ �÷��̾ Ȯ���ϼ���";

        ShowWordsSlide.SetActive(false);
    }

    // ���þ� ���� ������ ���þ� Ȯ�� ��ư
    public void CheckWord()
    {
        PlaySFX(SFX.Click);
        CheckWordGroup.SetActive(false);
        PlayerWord.text = Words[Who];
    }

    // ���þ� ���� �ʷϻ� Ȯ�� ��ư
    public void CheckNext()
    {
        if (Who + 1 < HeadCount)
        {
            PlaySFX(SFX.Click);
            Who++;
            NicknameTitle.text = Nickname[Who] + "\n���� ���þ�";
            Warning.text = Nickname[Who] + " ����\n ������ �÷��̾ Ȯ���ϼ���";
            Backgrounds[Who - 1].SetActive(false);
            Backgrounds[Who].SetActive(true);
            CheckWordGroup.SetActive(true);
        }
        else
        {
            ShowWordGroup.SetActive(false);
            GameStartSlide.SetActive(true);

            BGMPlayer.Stop();
            PlaySFX(SFX.Slide);
            Invoke("DownGameStart", 1.5f);
        }
    }

    // ���� ���� �����̵� �ݱ�
    void DownGameStart()
    {
        PlayBGM(BGM.GameBGM);

        Backgrounds[Who].SetActive(false);
        Who = 0;
        Backgrounds[Who].SetActive(true);

        NicknameQuestion.text = Nickname[Who] + "\n���� ����";
        Hint_count.text = (Words[Who].Length).ToString() + "����";
        Hint_first.text = WordDict[Words[Who]];

        GameStartGroup.SetActive(true);
        Round_text.gameObject.SetActive(true);
        Round_text.text = "Round " + Round;

        HintBtn.SetActive(true);
        GameStartSlide.SetActive(false);
    }

    // ���� �� ��Ʈ ��ư
    public void ShowHintCount()
    {
        PlaySFX(SFX.Hint);
        HintCountBtn.SetActive(false);
        Hint_countCover.SetActive(false);
    }

    // �ʼ� ��Ʈ ��ư
    public void ShowHintFirst()
    {
        PlaySFX(SFX.Hint);
        HintFirstBtn.SetActive(false);
        Hint_firstCover.SetActive(false);
    }

    // ���� ��ŵ ��ư
    public void ChallengeSkip()
    {
        PlaySFX(SFX.Click);
        if (Who + 1 < HeadCount)
        {
            Who++;

            Backgrounds[Who-1].SetActive(false);
            Backgrounds[Who].SetActive(true);
            NicknameQuestion.text = Nickname[Who] + "\n���� ����";
            Hint_count.text = (Words[Who].Length).ToString() + "����";
            Hint_first.text = WordDict[Words[Who]];
            HintBtn.SetActive(false);
        }
        else
        {
            Who = 0;

            Backgrounds[HeadCount-1].SetActive(false);
            Backgrounds[Who].SetActive(true);
            NicknameQuestion.text = Nickname[Who] + "\n���� ����";
            Hint_count.text = (Words[Who].Length).ToString() + "����";
            Hint_first.text = WordDict[Words[Who]];
            HintBtn.SetActive(true);

            Round++;
            Round_text.text = "Round " + Round;
        }
    }

    // ������ ��ư
    public void ShowForgetGroup()
    {
        PlaySFX(SFX.Click);
        ForgetGroup.SetActive(true);
        BeforeCheckGroup.SetActive(true);
        AfterCheckGroup.SetActive(false);
        ForgetWarning.text = Nickname[Who] + " ����\n ������ �÷��̾ Ȯ���ϼ���";

        Smog.SetActive(true);
    }

    // ������ ���þ� Ȯ�� ��ư
    public void ForgetBtn()
    {
        PlaySFX(SFX.Click);
        BeforeCheckGroup.SetActive(false);
        AfterCheckGroup.SetActive(true);
        ForgetWord.text = Words[Who];
    }

    // �޸��� ��ư
    public void ShowMemoGroup()
    {
        PlaySFX(SFX.Click);
        MemoGroup.SetActive(true);
        Memo.text = PlayerMemo[Who];

        Smog.SetActive(true);
    }

    // �޸� ����
    public void SaveMemo()
    {
        PlayerMemo[Who] = Memo.text;
    }

    // �����ϰ� ī��Ʈ�ٿ�
    public void CountDownStart()
    {
        if (Challenge.text == Words[Who])
        {
            isCorrect = true;
        }
        CountDown.text = "<size=500>3</size>";
        StartCoroutine("Coroutine_CountDown");
    }

    IEnumerator Coroutine_CountDown()
    {
        PlaySFX(SFX.Beep);
        yield return new WaitForSeconds(1.0f);

        PlaySFX(SFX.Beep);
        CountDown.text = "<size=750>2</size>";
        yield return new WaitForSeconds(1.0f);

        PlaySFX(SFX.Beep);
        CountDown.text = "<size=1000>1</size>";
        yield return new WaitForSeconds(1.0f);

        CountDownGroup.SetActive(false);

        if (isCorrect == true)
        {
            VictoryPlayer.Play();
            PlaySFX(SFX.Over);

            GameStartGroup.SetActive(false);
            GameOverGroup.SetActive(true);
            Round_text.gameObject.SetActive(false);

            for (int i = HeadCount; i < 6; i++)
            {
                PlayerResults[i].SetActive(false);
            }

            int loser = 1;

            for (int i = 0; i < HeadCount; i++)
            {
                if (i == Who)
                {
                    Image WinnerColorImage;
                    WinnerColorImage = PlayerResultsColor[0].GetComponent<Image>();
                    WinnerColorImage.color = ImageColorChange(Array.IndexOf(ColorArr, order[Who]));
                    PlayerResultsNickname[0].text = Nickname[Who];
                    PlayerResultsWord[0].text = Words[Who];
                }
                else
                {
                    Image LoserColorImage;
                    LoserColorImage = PlayerResultsColor[loser].GetComponent<Image>();
                    LoserColorImage.color = ImageColorChange(Array.IndexOf(ColorArr, order[i]));
                    PlayerResultsNickname[loser].text = Nickname[i];
                    PlayerResultsWord[loser].text = Words[i];
                    loser++;
                }
            }
        }
        else
        {
            PlaySFX(SFX.Fail);

            Failure_text.text = Nickname[Who] + "\n���� ���þ��\n" + Challenge.text + "��(��)\n�ƴմϴ�";
            FailureGroup.SetActive(true); ;
        }
    }

    // ���� �Է�â �ʱ�ȭ
    public void ResetChallenge()
    {
        BGMPlayer.Stop();
        Challenge.text = "";
        isCorrect = false;
    }

    // ����� ���Ұ�
    public void BGMMute(bool isOn)
    {
        if (isOn && !isBGMMute)
        {
            BGM_slider.value = -80f;
            AudioMixer.SetFloat("bgm", -40f);
            isBGMMute = true;
        }
        else if (isOn && isBGMMute)
        {
            BGM_slider.value = -30f;
            AudioMixer.SetFloat("bgm", 0f);
            isBGMMute = false;
        }
        BGM_slidertext.text = "����� : " + (int)((BGM_slider.value + 80f));
    }

    // ����� �����̴�
    public void BGMChange()
    {
        if(BGM_slider.value == -80f && !BGM_toggle.isOn)
        {
            BGM_toggle.isOn = true;
            isBGMMute = true;
        }
        else if(BGM_slider.value != -80f && BGM_toggle.isOn)
        {
            BGM_toggle.isOn = false;
            isBGMMute = false;
        }

        AudioMixer.SetFloat("bgm", BGM_slider.value);

        BGM_slidertext.text = "����� : " + (int)((BGM_slider.value + 80f));
    }

    // ȿ���� ���Ұ�
    public void SFXMute(bool isOn)
    {
        if (isOn && !isSFXMute)
        {
            SFX_slider.value = -80f;
            AudioMixer.SetFloat("sfx", -40f);
            isSFXMute = true;
        }
        else if (isOn && isSFXMute)
        {
            SFX_slider.value = -30f;
            AudioMixer.SetFloat("sfx", 0f);
            isSFXMute = false;
        }
        BGM_slidertext.text = "����� : " + (int)((BGM_slider.value + 80f));
    }

    // ȿ���� �����̴�
    public void SFXChange()
    {
        if (SFX_slider.value == -80f && !SFX_toggle.isOn)
        {
            SFX_toggle.isOn = true;
            isSFXMute = true;
        }
        else if (SFX_slider.value != -80f && SFX_toggle.isOn)
        {
            SFX_toggle.isOn = false;
            isSFXMute = false;
        }

        AudioMixer.SetFloat("sfx", SFX_slider.value);

        SFX_slidertext.text = "ȿ���� : " + (int)((SFX_slider.value + 80f));
    }

    // �̹��� ���� ��ȭ
    Color32 ImageColorChange(int num)
    {
        Color32 color = new Color32();
        switch (num)
        {
            case 0:
                color = new Color32(255, 0, 0, 255);
                break;
            case 1:
                color = new Color32(255, 174, 0, 255);
                break;
            case 2:
                color = new Color32(255, 211, 0, 255);
                break;
            case 3:
                color = new Color32(169, 255, 0, 255);
                break;
            case 4:
                color = new Color32(7, 212, 0, 255);
                break;
            case 5:
                color = new Color32(0, 251, 255, 255);
                break;
            case 6:
                color = new Color32(0, 119, 255, 255);
                break;
            case 7:
                color = new Color32(170, 0, 255, 255);
                break;
            case 8:
                color = new Color32(0, 0, 0, 255);
                break;
            case 9:
                color = new Color32(255, 255, 255, 255);
                break;
        }

        return color;
    }

    // BGM ���
    void PlayBGM(BGM type)
    {
        switch (type)
        {
            case BGM.MainBGM:
                BGMPlayer.clip = BGMMusic[0];
                break;
            case BGM.ShowWordBGM:
                BGMPlayer.clip = BGMMusic[1];
                break;
            case BGM.GameBGM:
                BGMPlayer.clip = BGMMusic[2];
                break;
        }
        BGMPlayer.Play();
    }

    // SFX ���
    void PlaySFX(SFX type)
    {
        switch (type)
        {
            case SFX.Click:
                SFXPlayer.clip = SFXMusic[0];
                break;
            case SFX.Slide:
                SFXPlayer.clip = SFXMusic[1];
                break;
            case SFX.Hint:
                SFXPlayer.clip = SFXMusic[2];
                break;
            case SFX.Fail:
                SFXPlayer.clip = SFXMusic[3];
                break;
            case SFX.Over:
                SFXPlayer.clip = SFXMusic[4];
                break;
            case SFX.Beep:
                SFXPlayer.clip = SFXMusic[5];
                break;
        }
        SFXPlayer.Play();
    }

    // ���� ���� ���
    public void PlayGameBGM()
    {
        BGMPlayer.clip = BGMMusic[2];
        BGMPlayer.Play();
    }
}