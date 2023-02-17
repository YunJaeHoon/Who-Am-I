using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    string[] WordArr = {"유재석","강호동","아이유","소녀시대","방탄소년단(BTS)","이병헌","황정민","박명수",
                        "이정재","정준하","정형돈","최민식","노홍철","하하(하동훈)","길(가수)","이수근",
                        "사도세자","은지원","이승기","송강호","세종대왕","이순신","오바마","강동원","강하늘",
                        "공유(배우)","김병만","박성웅","서강준","소지섭","송강","송중기","오달수","유해진",
                        "원빈","현빈","윤봉길","김구","유관순","주몽","이광수","지석진","츄","나문희","박보영",
                        "송혜교","김태희","비(배우)","설현","이경규","김국진","김구라","개리","다이나믹듀오",
                        "장나라","조보아","천우희","주호민","이말년(침착맨)","사과","오렌지","바나나","귤",
                        "수박","참외","두리안","멜론(메론)","자두","포도","블루베리","김치","배추","오이",
                        "당근","감자","고구마","토마토","치즈","우유","소세지","핫도그","식초","설탕","소금",
                        "마요네즈","무(야채)","파(야채)","콜라","사이다","옥수수","커피","초콜릿(초콜렛)",
                        "사탕","뻥튀기","케이크","빵","치킨","피자","소(동물)","개(동물)","닭","쥐","호랑이",
                        "사자","공작(공작새)","새(동물)","비둘기","참새","독수리","토끼","용","뱀","말",
                        "양(동물)","원숭이","고릴라","돼지","에어컨","선풍기","전자레인지","냉장고","컴퓨터",
                        "카메라","지뢰","복권","나무","꽃","연필","지우개","장미","튤립","해바라기","택시",
                        "버스","별","우주선(로켓)","자전거","오토바이","사전","책","불꽃놀이","모래","흙","물",
                        "불","용암","고무","석탄","철","금(황금)","다이아몬드","똥","오줌","도끼","망치","해머",
                        "소화기","탱크","군인","경찰","소방서","경찰서","지뢰찾기","테트리스","리그오브레전드",
                        "오버워치","편의점","병원","약국","의사","지하철","기차","여의도","청와대","대통령",
                        "공원","휠체어","삼겹살","필통","세탁기","책상","의자","서울","부산","제주도","독도",
                        "대한민국(한국)","일본","중국","미국","지구","우주","산(mountain)","바다","화산","호주",
                        "유럽","아프리카","하와이","필리핀","몽골","이탈리아","프랑스","독일","태평양","백두산",
                        "요정","엘프","고블린","마법사","마녀","대머리","살인마","신(God)","유령","귀신","천사",
                        "악마","좀비","노예(노비)","지우(포켓몬)","피카츄","유비","관우","장비(삼국지)",
                        "스폰지밥","뚱이","징징이","코난","건담","신짱구(짱구)","도라에몽","진구(도라에몽)",
                        "이슬이(도라에몽)","퉁퉁이(도라에몽)","비실이(도라에몽)","철수(짱구)","유리(짱구)",
                        "훈이(짱구)","맹구(짱구)","봉미선(짱구)","짱아(짱구)","신형만(짱구)","흰둥이(짱구)",
                        "손오공","저팔계","사오정","해리포터","덤블도어","각시탈","가렌","갈리오","갱플랭크",
                        "그라가스","그레이브즈","그웬","나르","나미","나서스","노틸러스","녹턴","햄버거",
                        "누누(누누와윌럼프)","니달리","니코","닐라","다리우스","다이애나","드레이븐","라이즈",
                        "라칸","람머스","파라오","스핑크스","피라미드","이집트","사막","오아시스","럭스","럼블",
                        "레나타(레나타글라스크)","레넥톤","레오나","렉사이","렐","렝가","루시안","룰루","아령",
                        "르블랑","리신","리븐","리산드라","릴리아","마스터이","마오카이","말자하","말파이트",
                        "모데카이저","모르가나","문도(문도박사)","미스포츈","바드","바루스","바이","베이가",
                        "베인","벡스","벨베스","벨코즈","볼리베어","브라움","브랜드","블라디미르","게임","닻",
                        "블리츠크랭크","비에고","빅토르","뽀삐","사미라","사이온","사일러스","샤코","세나",
                        "세라핀","세주아니","세트(롤)","소나","소라카","쉔","쉬바나","스웨인","스카너","시비르",
                        "전갈","표창","닌자","신짜오","신드라","신지드","뿡뿡이","쓰레쉬","방귀","아리","여우",
                        "곰","김치찌개","참치","고등어","계란후라이","설렁탕","늑대","낙지","문어","오징어",
                        "아무무","미라","시체","아우렐리온솔","아이번","아지르","아칼리","태양","달",
                        "사우나(찜질방)","수성","금성","화성","목성","토성","천왕성","해왕성","명왕성","아크샨",
                        "아트록스","아펠리오스","알리스타","애니(롤)","티버","애니비아","애쉬","야스오",
                        "야스오","에코","엘리스(롤)","오른","오리아나","올라프(롤)","엘사","안나(겨울왕국)",
                        "올라프(겨울왕국)","오공(롤)","요네","요릭","우디르","우르곳","워윅","유미(롤)",
                        "이렐리아","이블린","이즈리얼","일라오이","자르반4세","자야","자이라","자크","잔나",
                        "잭스","제드","제라스","제리(롤)","고양이","제리(롤)","천둥","구름","번개","비(날씨)",
                        "거미","지네","개미","지렁이","파리(벌레)","제이스","조이(롤)","아이린","조개","파도",
                        "조이(레드벨벳)","웬디(레드벨벳)","슬기(레드벨벳)","예리(레드벨벳)","레드벨벳","태연",
                        "유리","창문","직스","진(롤)","질리언","징크스(롤)","조커","슈퍼맨","아이언맨",
                        "캡틴아메리카","배트맨","할리퀸","초가스","카르마","카밀","카사딘","카서스",
                        "카시오페아","카이사","카직스","카타리나","칼리스타","로마","영국","케넨","케이틀린",
                        "케인","케일","코그모","코르키","퀸(롤)","체스","은행","아침","점심","저녁","밤",
                        "크산테","클레드","키아나","킨드레드","트럼프","박근혜","문재인","윤석열","타릭","탈론",
                        "탈리야","탐켄치","트런들","트리스타나","트린다미어","트위스티드페이트","트위치",
                        "유튜브","티모","작살","파아크","밧줄","스테이플러","테이프","풀","수영장","오리",
                        "판테온","인형","피들스틱","처키","프랑켄슈타인","피오라","피즈","하이머딩거","헤카림",
                        "둠피스트","라마트라","라인하르트","레킹볼","로드호그","시그마(오버워치)","빅뱅",
                        "디바(d.va)","오리사","윈스턴","자리야","정커퀸","겐지","리퍼","저승사자","낫","메이",
                        "얼음","겨울","봄","여름","가을","단풍","낙엽","빗자루","바스티온","소전","학교","피클",
                        "솜브라","시메트라","위도우메이커","정크랫","캐서디(맥크리)","떡볶이","순대","바둑",
                        "토르비욘","트레이서","파라","한조","화살","활","루시우","메르시","모이라","바티스트",
                        "브리기테","아나","젠야타","키리코","주차장","바퀴","하스스톤","얼라이언스","호드",
                        "호두","땅콩","팥","붕어빵","돼지저금통","인쇄기","소닉","마리오","복숭아","루이지",
                        "버섯","곰팡이","세균","현미경","돋보기","수학","과학","식당","숟가락","젓가락","포크",
                        "삼지창","철퇴","쇠사슬","레고","악어","귀뚜라미","메뚜기","피","영어","한글","한자",
                        "마블","깡패","야쿠자","코끼리","낙타","망토","지니","피아노","기타","리코더","빛",
                        "바이올린","짜장면(자장면)","설거지(설겆이)","짬뽕","탕수육","양파","단무지","그림자",
                        "지구온난화","올림픽","월드컵","축구","야구","농구","볼링","썰매","스케이트","낚시"};

    public Transform BackgounrdGroup;                               // 배경 축적 폴더

    public GameObject Background;                                   // 메인화면 배경
    public GameObject MainSceneGroup;                               // 메인씬 그룹
    public GameObject SubSceneGroup;                                // 서브씬 그룹
    public GameObject ShowWords;                                    // 제시어 공개
    public GameObject ShowWordGroup;                                // 제시어씬 그룹

    public TMP_Text HeadCountUI;                                    // 인원수 숫자
    public TMP_Text NicknameTitle;                                       // 닉네임 + "제시어"
    public TMP_Text Warning;                                        // 닉네임 + "님을 제외한 플레이어만 확인하세요"

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
        ShowWords.SetActive(true);
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

    // 제시어 공개 닫기
    void DownShowWords()
    {
        ShowWords.SetActive(false);
        Backgrounds[0].SetActive(true);
        ShowWordGroup.SetActive(true);
        NicknameTitle.text = Nickname[0] + "\n님의 제시어";
        Warning.text = Nickname[0] + " 님을\n 제외한 플레이어만 확인하세요";
    }
}