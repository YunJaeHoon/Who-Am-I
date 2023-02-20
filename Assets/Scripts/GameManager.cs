using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    string[] WordArr = {"유재석","강호동","아이유","소녀시대","방탄소년단","이병헌","박명수","김연아",
                        "아이유","슈퍼주니어","잡초","농약","농부","과수원","모자","엄마","아빠","슬리퍼",
                        "사도세자","송강호","세종대왕","이순신","오바마",
                        "원빈","윤봉길","김구","유관순","주몽",
                        "사과","오렌지","바나나","귤",
                        "수박","참외","두리안","자두","포도","블루베리","김치","배추","오이",
                        "당근","감자","고구마","토마토","치즈","우유","소세지","핫도그","식초","설탕","소금",
                        "마요네즈","무","파","콜라","사이다","옥수수","커피","초콜렛",
                        "사탕","뻥튀기","케이크","빵","치킨","피자","소","개","닭","쥐","호랑이",
                        "사자","공작새","비둘기","참새","독수리","토끼","용","뱀","말",
                        "양","원숭이","고릴라","돼지","에어컨","선풍기","전자레인지","냉장고","컴퓨터",
                        "카메라","지뢰","복권","나무","꽃","연필","지우개","장미","튤립","해바라기","택시",
                        "버스","별","로켓","자전거","오토바이","사전","책","불꽃놀이","모래","흙","물",
                        "불","용암","고무","석탄","철","금","다이아몬드","똥","오줌","도끼","망치","해머",
                        "소화기","탱크","군인","경찰","소방서","경찰서","지뢰찾기","테트리스","리그오브레전드",
                        "오버워치","편의점","병원","약국","의사","지하철","기차","여의도","청와대","대통령",
                        "공원","휠체어","삼겹살","필통","세탁기","책상","의자","서울","부산","제주도","독도",
                        "대한민국","일본","중국","미국","지구","우주","산","바다","화산","호주",
                        "유럽","아프리카","하와이","필리핀","몽골","이탈리아","프랑스","독일","태평양","백두산",
                        "요정","엘프","고블린","마법사","마녀","대머리","살인마","신","유령","귀신","천사",
                        "악마","좀비","노예","피카츄","유비","관우","장비",
                        "스폰지밥","뚱이","징징이","코난","건담","짱구","도라에몽",
                        "손오공","저팔계","사오정","해리포터","햄버거",
                        "파라오","스핑크스","피라미드","사막","오아시스","아령",
                        "게임","닻",
                        "전갈","표창","닌자","뿡뿡이","방귀","아리","여우",
                        "곰","김치찌개","참치","고등어","계란후라이","설렁탕","늑대","낙지","문어","오징어",
                        "미라","시체","태양","달",
                        "화성",
                        "엘사",
                        "고양이","천둥","구름","번개","비",
                        "거미","지네","개미","지렁이","파리","조개","파도",
                        "스타벅스","레드벨벳","태연",
                        "유리","창문","조커","슈퍼맨","아이언맨",
                        "박쥐","배트맨",
                        "로마","영국",
                        "체스","은행","아침","점심","저녁","밤",
                        "유튜브","작살","밧줄","테이프","풀","수영장","오리",
                        "인형","처키","프랑켄슈타인",
                        "빅뱅",
                        "저승사자","낫",
                        "얼음","겨울","봄","여름","가을","단풍","낙엽","빗자루","학교","피클",
                        "떡볶이","순대","바둑",
                        "주차장","바퀴","하스스톤",
                        "호두","땅콩","팥","붕어빵","돼지저금통","인쇄기","소닉","마리오","복숭아","루이지",
                        "버섯","곰팡이","세균","현미경","돋보기","수학","과학","식당","숟가락","젓가락","포크",
                        "삼지창","철퇴","쇠사슬","레고","악어","귀뚜라미","메뚜기","피","영어","한글","한자",
                        "마블","깡패","야쿠자","코끼리","낙타","망토","지니","피아노","기타","리코더","빛",
                        "바이올린","짜장면","설거지","짬뽕","탕수육","양파","단무지","그림자",
                        "지구온난화","올림픽","월드컵","축구","야구","농구","볼링","썰매","스케이트","낚시"};

    public Transform BackgounrdGroup;                               // 배경 축적 폴더

    public GameObject Background;                                   // 메인화면 배경
    public GameObject MainSceneGroup;                               // 메인씬 그룹
    public GameObject SubSceneGroup;                                // 서브씬 그룹
    public GameObject GameStartGroup;                               // 게임씬 그룹

    public GameObject ShowWordsSlide;                               // 제시어 공개 슬라이드
    public GameObject GameStartSlide;                               // 제시어 공개 슬라이드
    public GameObject ShowWordGroup;                                // 제시어씬 그룹
    public GameObject CheckWordGroup;                               // 제시어 가리는 그룹
    public GameObject HintBtn;                                      // 힌트 버튼
    public GameObject Hint_countCover;                              // 글자 수 힌트 가리개
    public GameObject Hint_firstCover;                              // 초성 힌트 가리개

    public TMP_Text HeadCountUI;                                    // 인원수 숫자
    public TMP_Text NicknameTitle;                                  // 닉네임 + "제시어"
    public TMP_Text Warning;                                        // 닉네임 + "님을 제외한 플레이어만 확인하세요"
    public TMP_Text PlayerWord;                                     // 플레이어 제시어
    public TMP_Text Round_text;                                     // 라운드

    public TMP_Text NicknameQuestion;                               // 닉네임 + "님의 질문"
    public TMP_Text Hint_count;                                     // 글자수 힌트
    public TMP_Text Hint_first;                                     // 초성 힌트

    public GameObject[] Player = new GameObject[6];                 // 플레이어 창 배열
    public GameObject[] PlayerColorBtnArr = new GameObject[6];      // 플레이어 창의 색상 배열
    public GameObject[] PlayerBackground = new GameObject[10];      // 배경 프리팹 배열
    public TMP_InputField[] PlayerNickname = new TMP_InputField[6]; // 플레이어 닉네임 배열

    public GameObject ColorSelectUI;                                // 색상창
    public GameObject[] ColorBtnArr = new GameObject[10];           // 색상창 색상 버튼 배열

    GameObject PlayerColor;                                 // 클릭한 플레이어 창의 색상
    Image PlayerColorImage;                                 // 클릭한 플레이어 창의 색상 이미지

    int HeadCount = 2;                                      // 현재 인원 수

    int CurrentPlayerNumber;                                // 현재 색상 변경 중인 플레이어 번호
    int CurrentColorBtn;                                    // 현재 누른 색상창의 색상 버튼
    int[] ColorArr = { 1, 2, 0, 0, 0, 0, 0, 0, 0, 0 };      // 색상 확인 배열
    int Who = 0;                                            // 플레이어 차례
    int Round = 1;                                          // 게임 라운드

    bool isMain = true;                                     // 현재 메인 씬인가?
    bool isSub = false;                                     // 현재 서브 씬인가?
    bool isColorChanging = false;                           // 현재 플레이어 색상 변경 중인가?

    List<GameObject> Backgrounds = new List<GameObject>();        // 플레이어 배경 리스트
    List<String> Words = new List<String>();                      // 플레이어 제시어 리스트
    List<String> Nickname = new List<String>();                   // 플레이어 닉네임 리스트

    void Awake()
    {
        // 최적화
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        // 메인씬 -> 서브씬
        if (Input.anyKeyDown && isMain)
        {
            isMain = false;
            isSub = true;

            MainSceneGroup.SetActive(false);
            SubSceneGroup.SetActive(true);
            ColorSelectUI.SetActive(false);
        }

        // 서브 씬 -> 메인씬
        if (Input.GetKeyDown(KeyCode.Escape) && isSub && !ColorSelectUI.activeSelf)
        {
            isMain = true;
            isSub = false;

            MainSceneGroup.SetActive(true);
            SubSceneGroup.SetActive(false);
        }

        // 서브 씬 색상창 닫기
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

    // 색상창 띄우기
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

    // 색상창 색상 선택
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

    // 게임 시작 버튼
    public void GameStart()
    {
        isSub = false;
        SubSceneGroup.SetActive(false);
        ShowWordsSlide.SetActive(true);
        Invoke("DownShowWords", 1.5f);
        Background.SetActive(false);

        for(int i = 1; i <= HeadCount; i++)
        {
            // 플레이어 배경 세팅
            GameObject background_obj;
            background_obj = Instantiate(PlayerBackground[Array.IndexOf(ColorArr, i)],BackgounrdGroup);
            Backgrounds.Add(background_obj);
            Backgrounds[i-1].SetActive(false);

            // 플레이어 이름 세팅
            Nickname.Add(PlayerNickname[i-1].text);

            // 플레이어 제시어 세팅
            Words.Add(WordArr[UnityEngine.Random.Range(0,WordArr.Length)]);
        }
    }

    // 빨간색 제시어 확인 버튼
    public void CheckWord()
    {
        CheckWordGroup.SetActive(false);
        PlayerWord.text = Words[Who];
    }

    // 초록색 확인 버튼
    public void CheckNext()
    {
        if(Who + 1 < HeadCount)
        {
            Who++;
            NicknameTitle.text = Nickname[Who] + "\n님의 제시어";
            Warning.text = Nickname[Who] + " 님을\n 제외한 플레이어만 확인하세요";
            Backgrounds[Who - 1].SetActive(false);
            Backgrounds[Who].SetActive(true);
            CheckWordGroup.SetActive(true);
        }
        else
        {
            ShowWordGroup.SetActive(false);
            GameStartSlide.SetActive(true);

            Invoke("DownGameStart", 1.5f);
        }
    }

    // 글자 수 힌트 버튼
    public void ShowHintCount()
    {
        Hint_countCover.SetActive(false);
        HintBtn.transform.GetChild(0).gameObject.SetActive(false);
    }

    // 초성 힌트 버튼
    public void ShowHintFirst()
    {
        Hint_firstCover.SetActive(false);
        HintBtn.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void ChallengeSkip()
    {
        if (Who + 1 < HeadCount)
        {
            Who++;

            Backgrounds[Who-1].SetActive(false);
            Backgrounds[Who].SetActive(true);
            NicknameQuestion.text = Nickname[Who] + "\n님의 질문";
            Hint_count.text = Words[Who];
            Hint_first.text = Words[Who];
            HintBtn.SetActive(false);
        }
        else
        {
            Who = 0;

            Backgrounds[HeadCount-1].SetActive(false);
            Backgrounds[Who].SetActive(true);
            NicknameQuestion.text = Nickname[Who] + "\n님의 질문";
            Hint_count.text = Words[Who];
            Hint_first.text = Words[Who];
            HintBtn.SetActive(true);

            Round++;
            Round_text.text = "Round " + Round;
        }
    }

    // 제시어 공개 슬라이드 닫기
    void DownShowWords()
    {
        Backgrounds[0].SetActive(true);
        ShowWordGroup.SetActive(true);

        NicknameTitle.text = Nickname[0] + "\n님의 제시어";
        Warning.text = Nickname[0] + " 님을\n 제외한 플레이어만 확인하세요";

        ShowWordsSlide.SetActive(false);
    }

    // 게임 시작 슬라이드 닫기
    void DownGameStart()
    {
        Backgrounds[Who].SetActive(false);
        Who = 0;
        Backgrounds[Who].SetActive(true);

        NicknameQuestion.text = Nickname[Who] + "\n님의 질문";
        Hint_count.text = Words[Who];
        Hint_first.text = Words[Who];

        GameStartGroup.SetActive(true);
        Round_text.text = "Round " + Round;

        GameStartSlide.SetActive(false);
    }
}