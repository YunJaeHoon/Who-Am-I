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
    [Header("-------------메인씬 오브젝트-------------")]
    public GameObject MainSceneGroup;                               // 메인씬 그룹
    public GameObject Background;                                   // 메인화면 배경

    [Header("-------------서브씬 오브젝트-------------")]
    public GameObject SubSceneGroup;                                // 서브씬 그룹
    public GameObject[] Player = new GameObject[6];                 // 플레이어 창 배열
    public GameObject[] PlayerColorBtnArr = new GameObject[6];      // 플레이어 창의 색상 배열
    public TMP_Text HeadCountUI;                                    // 인원수 숫자

    [Space(10f)]
    public GameObject ColorSelectUI;                                // 색상창
    public GameObject[] ColorBtnArr = new GameObject[10];           // 색상창 색상 버튼 배열

    [Space(10f)]
    GameObject PlayerColor;                                         // 클릭한 플레이어 창의 색상
    Image PlayerColorImage;                                         // 클릭한 플레이어 창의 색상 이미지
    int CurrentPlayerNumber;                                        // 현재 색상 변경 중인 플레이어 번호
    int CurrentColorBtn;                                            // 현재 누른 색상창의 색상 버튼
    int[] ColorArr = { 1, 2, 0, 0, 0, 0, 0, 0, 0, 0 };              // 색상 확인 배열

    [Space(10f)]
    public GameObject[] PlayerBackground = new GameObject[10];      // 배경 프리팹 배열
    public TMP_InputField[] PlayerNickname = new TMP_InputField[6]; // 플레이어 닉네임 배열

    [Header("-----------제시어 공개씬 오브젝트-----------")]
    public GameObject ShowWordGroup;                                // 제시어씬 그룹
    public GameObject ShowWordsSlide;                               // 제시어 공개 슬라이드
    public GameObject CheckWordGroup;                               // 제시어 가림막 그룹

    [Space(10f)]
    public TMP_Text NicknameTitle;                                  // 닉네임 + "제시어"
    public TMP_Text Warning;                                        // 닉네임 + "님을 제외한 플레이어만 확인하세요"
    public TMP_Text PlayerWord;                                     // 플레이어 제시어

    [Header("-------------게임씬 오브젝트-------------")]
    public GameObject GameStartGroup;                               // 게임씬 그룹
    public GameObject GameStartSlide;                               // 게임 시작 슬라이드
    public TMP_Text Round_text;                                     // 라운드
    public TMP_Text NicknameQuestion;                               // 닉네임 + "님의 질문"

    // 힌트 오브젝트
    [Space(10f)]
    public GameObject HintBtn;                                      // 힌트 그룹
    public GameObject HintCountBtn;                                 // 글자 수 힌트 버튼
    public GameObject HintFirstBtn;                                 // 초성 힌트 버튼
    public GameObject Hint_countCover;                              // 글자 수 힌트 가리개
    public GameObject Hint_firstCover;                              // 초성 힌트 가리개
    public TMP_Text Hint_count;                                     // 글자수 힌트
    public TMP_Text Hint_first;                                     // 초성 힌트

    // 뭐더라 오브젝트
    [Space(10f)]
    public GameObject ForgetGroup;                                  // 뭐더라 그룹
    public GameObject BeforeCheckGroup;                             // 뭐더라 > 제시어 확인 전 그룹
    public GameObject AfterCheckGroup;                              // 뭐더라 > 제시어 확인 후 그룹
    public TMP_Text ForgetWarning;                                  // 뭐더라 주의사항
    public TMP_Text ForgetWord;                                     // 뭐더라 제시어

    // 메모장 오브젝트
    [Space(10f)]
    public GameObject MemoGroup;                                    // 메모장 그룹
    public TMP_InputField Memo;                                     // 메모장
    string[] PlayerMemo = new string[6];                            // 플레이어 메모 배열

    // 도전 오브젝트
    [Space(10f)]
    public GameObject CountDownGroup;                               // 카운트다운 그룹
    public GameObject FailureGroup;                                 // 실패 그룹
    public TMP_InputField Challenge;                                // 도전 입력
    public TMP_Text CountDown;                                      // 카운트다운
    public TMP_Text Failure_text;                                   // 실패 대사

    [Header("------------게임오버씬 오브젝트------------")]
    public GameObject GameOverGroup;                                // 게임종료 그룹
    public GameObject[] PlayerResults = new GameObject[6];          // 패배 플레이어 이름표 배열
    public GameObject[] PlayerResultsColor = new GameObject[6];     // 패배 플레이어 이름표 색깔
    public TMP_Text[] PlayerResultsNickname = new TMP_Text[6];      // 패배 플레이어 이름표 이름
    public TMP_Text[] PlayerResultsWord = new TMP_Text[6];          // 패배 플레이어 이름표 제시어

    [Header("-------------도움말 오브젝트-------------")]
    public GameObject[] HelpPages = new GameObject[9];              // 도움말 페이지 배열
    public TMP_Text HelpPageText;                                   // 도움말 페이지 텍스트

    [Header("-------------사운드 오브젝트-------------")]
    public AudioSource BGMPlayer;                                   // 브금 플레이어
    public AudioSource SFXPlayer;                                   // 효과음 플레이어
    public AudioSource VictoryPlayer;                               // 게임종료 플레이어
    public AudioClip[] BGMMusic = new AudioClip[3];                 // 브금 사운드
    public AudioClip[] SFXMusic = new AudioClip[6];                 // 효과음 사운드
    public AudioMixer AudioMixer;                                   // 오디오 믹서

    // 환경설정 오브젝트
    [Space(10f)]
    public Slider BGM_slider;                                       // 브금 슬라이더
    public Slider SFX_slider;                                       // 효과음 슬라이더
    public Toggle BGM_toggle;                                       // 브금 음소거 체크박스
    public Toggle SFX_toggle;                                       // 효과음 음소거 체크박스
    public TMP_Text BGM_slidertext;                                 // 브금 음량 텍스트
    public TMP_Text SFX_slidertext;                                 // 효과음 음량 텍스트

    [Header("--------------기타 오브젝트--------------")]
    public Transform BackgroundGroup;                               // 배경 축적 폴더
    public GameObject Smog;                                         // 버튼 차단 스모그

    Dictionary<string, string> WordDict = new Dictionary<string, string>()
    {
        { "유재석", "ㅇㅈㅅ" },
        { "강호동", "ㄱㅎㄷ" },
        { "아이유", "ㅇㅇㅇ" },
        { "소녀시대", "ㅅㄴㅅㄷ" },
        { "방탄소년단", "ㅂㅌㅅㄴㄷ" },
        { "이병헌", "ㅇㅂㅎ" },
        { "박명수", "ㅂㅁㅅ" },
        { "김연아", "ㄱㅇㅇ" },
        { "슈퍼주니어", "ㅅㅍㅈㄴㅇ" },
        { "잡초", "ㅈㅊ" },
        { "농약", "ㄴㅇ" },
        { "농부", "ㄴㅂ" },
        { "과수원", "ㄱㅅㅇ" },
        { "모자", "ㅁㅈ" },
        { "엄마", "ㅇㅁ" },
        { "아빠", "ㅇㅃ" },
        { "슬리퍼", "ㅅㄹㅍ" },
        { "사도세자", "ㅅㄷㅅㅈ" },
        { "송강호", "ㅅㄱㅎ" },
        { "세종대왕", "ㅅㅈㄷㅇ" },
        { "이순신", "ㅇㅅㅅ" },
        { "오바마", "ㅇㅂㅁ" },
        { "원빈", "ㅇㅂ" },
        { "윤봉길", "ㅇㅂㄱ" },
        { "김구", "ㄱㄱ" },
        { "유관순", "ㅇㄱㅅ" },
        { "주몽", "ㅈㅁ" },
        { "사과", "ㅅㄱ" },
        { "오렌지", "ㅇㄹㅈ" },
        { "바나나", "ㅂㄴㄴ" },
        { "귤", "ㄱ" },
        { "수박", "ㅅㅂ" },
        { "참외", "ㅊㅇ" },
        { "두리안", "ㄷㄹㅇ" },
        { "자두", "ㅈㄷ" },
        { "포도", "ㅍㄷ" },
        { "블루베리", "ㅂㄹㅂㄹ" },
        { "김치", "ㄱㅊ" },
        { "배추", "ㅂㅊ" },
        { "오이", "ㅇㅇ" },
        { "당근", "ㄷㄱ" },
        { "감자", "ㄱㅈ" },
        { "고구마", "ㄱㄱㅁ" },
        { "토마토", "ㅌㅁㅌ" },
        { "치즈", "ㅊㅈ" },
        { "우유", "ㅇㅇ" },
        { "소세지", "ㅅㅅㅈ" },
        { "핫도그", "ㅎㄷㄱ" },
        { "식초", "ㅅㅊ" },
        { "설탕", "ㅅㅌ" },
        { "소금", "ㅅㄱ" },
        { "마요네즈", "ㅁㅇㄴㅈ" },
        { "무", "ㅁ" },
        { "파", "ㅍ" },
        { "콜라", "ㅋㄹ" },
        { "사이다", "ㅅㅇㄷ" },
        { "옥수수", "ㅇㅅㅅ" },
        { "커피", "ㅋㅍ" },
        { "초콜렛", "ㅊㅋㄹ" },
        { "사탕", "ㅅㅌ" },
        { "뻥튀기", "ㅃㅌㄱ" },
        { "케이크", "ㅋㅇㅋ" },
        { "빵", "ㅃ" },
        { "치킨", "ㅊㅋ" },
        { "피자", "ㅍㅈ" },
        { "소", "ㅅ" },
        { "개", "ㄱ" },
        { "닭", "ㄷ" },
        { "쥐", "ㅈ" },
        { "호랑이", "ㅎㄹㅇ" },
        { "사자", "ㅅㅈ" },
        { "공작새", "ㄱㅈㅅ" },
        { "비둘기", "ㅂㄷㄱ" },
        { "참새", "ㅊㅅ" },
        { "독수리", "ㄷㅅㄹ" },
        { "토끼", "ㅌㄲ" },
        { "용", "ㅇ" },
        { "뱀", "ㅂ" },
        { "말", "ㅁ" },
        { "양", "ㅇ" },
        { "원숭이", "ㅇㅅㅇ" },
        { "고릴라", "ㄱㄹㄹ" },
        { "돼지", "ㄷㅈ" },
        { "에어컨", "ㅇㅇㅋ" },
        { "선풍기", "ㅅㅍㄱ" },
        { "전자레인지", "ㅈㅈㄹㅇㅈ" },
        { "냉장고", "ㄴㅈㄱ" },
        { "컴퓨터", "ㅋㅍㅌ" },
        { "카메라", "ㅋㅁㄹ" },
        { "지뢰", "ㅈㄹ" },
        { "복권", "ㅂㄱ" },
        { "나무", "ㄴㅁ" },
        { "꽃", "ㄲ" },
        { "연필", "ㅇㅍ" },
        { "지우개", "ㅈㅇㄱ" },
        { "장미", "ㅈㅁ" },
        { "튤립", "ㅌㄹ" },
        { "해바라기", "ㅎㅂㄹㄱ" },
        { "택시", "ㅌㅅ" },
        { "버스", "ㅂㅅ" },
        { "별", "ㅂ" },
        { "로켓", "ㄹㅋ" },
        { "자전거", "ㅈㅈㄱ" },
        { "오토바이", "ㅇㅌㅂㅇ" },
        { "사전", "ㅅㅈ" },
        { "책", "ㅊ" },
        { "불꽃놀이", "ㅂㄲㄴㅇ" },
        { "모래", "ㅁㄹ" },
        { "흙", "ㅎ" },
        { "물", "ㅁ" },
        { "불", "ㅂ" },
        { "용암", "ㅇㅇ" },
        { "고무", "ㄱㅁ" },
        { "석탄", "ㅅㅌ" },
        { "철", "ㅊ" },
        { "금", "ㄱ" },
        { "다이아몬드", "ㄷㅇㅇㅁㄷ" },
        { "똥", "ㄸ" },
        { "오줌", "ㅇㅈ" },
        { "도끼", "ㄷㄲ" },
        { "망치", "ㅁㅊ" },
        { "해머", "ㅎㅁ" },
        { "소화기", "ㅅㅎㄱ" },
        { "탱크", "ㅌㅋ" },
        { "군인", "ㄱㅇ" },
        { "경찰", "ㄱㅊ" },
        { "소방서", "ㅅㅂㅅ" },
        { "경찰서", "ㄱㅊㅅ" },
        { "지뢰찾기", "ㅈㄹㅊㄱ" },
        { "테트리스", "ㅌㅌㄹㅅ" },
        { "리그오브레전드", "ㄹㄱㅇㅂㄹㅈㄷ" },
        { "오버워치", "ㅇㅂㅇㅊ" },
        { "편의점", "ㅍㅇㅈ" },
        { "병원", "ㅂㅇ" },
        { "약국", "ㅇㄱ" },
        { "의사", "ㅇㅅ" },
        { "지하철", "ㅈㅎㅊ" },
        { "기차", "ㄱㅊ" },
        { "여의도", "ㅇㅇㄷ" },
        { "청와대", "ㅊㅇㄷ" },
        { "대통령", "ㄷㅌㄹ" },
        { "공원", "휠체어" },
        { "삼겹살", "ㅅㄱㅅ" },
        { "필통", "ㅍㅌ" },
        { "세탁기", "ㅅㅌㄱ" },
        { "책상", "ㅊㅅ" },
        { "의자", "ㅇㅈ" },
        { "서울", "ㅅㅇ" },
        { "부산", "ㅂㅅ" },
        { "제주도", "ㅈㅈㄷ" },
        { "독도", "ㄷㄷ" },
        { "대한민국", "ㄷㅎㅁㄱ" },
        { "일본", "ㅇㅂ" },
        { "중국", "ㅈㄱ" },
        { "미국", "ㅁㄱ" },
        { "지구", "ㅈㄱ" },
        { "우주", "ㅇㅈ" },
        { "산", "ㅅ" },
        { "바다", "ㅂㄷ" },
        { "화산", "ㅎㅅ" },
        { "호주", "ㅎㅈ" },
        { "유렵", "ㅇㄹ" },
        { "아프리카", "ㅇㅍㄹㅋ" },
        { "하와이", "ㅎㅇㅇ" },
        { "필리핀", "ㅍㄹㅍ" },
        { "몽골", "ㅁㄱ" },
        { "이탈리아", "ㅇㅌㄹㅇ" },
        { "프랑스", "ㅍㄹㅅ" },
        { "독일", "ㄷㅇ" },
        { "태평양", "ㅌㅍㅇ" },
        { "백두산", "ㅂㄷㅅ" },
        { "요정", "ㅇㅈ" },
        { "엘프", "ㅇㅍ" },
        { "고블린", "ㄱㅂㄹ" },
        { "마법사", "ㅁㅂㅅ" },
        { "마녀", "ㅁㄴ" },
        { "대머리", "ㄷㅁㄹ" },
        { "살인마", "ㅅㅇㅁ" },
        { "신", "ㅅ" },
        { "유령", "ㅇㄹ" },
        { "귀신", "ㄱㅅ" },
        { "천사", "ㅊㅅ" },
        { "악마", "ㅇㅁ" },
        { "좀비", "ㅈㅂ" },
        { "노예", "ㄴㅇ" },
        { "피카츄", "ㅍㅋㅊ" },
        { "유비", "ㅇㅂ" },
        { "관우", "ㄱㅇ" },
        { "장비", "ㅈㅂ" },
        { "스폰지밥", "ㅅㅍㅈㅂ" },
        { "뚱이", "ㄸㅇ" },
        { "징징이", "ㅈㅈㅇ" },
        { "코난", "ㅋㄴ" },
        { "건담", "ㄱㄷ" },
        { "짱구", "ㅉㄱ" },
        { "도라에몽", "ㄷㄹㅇㅁ" },
        { "손오공", "ㅅㅇㄱ" },
        { "저팔계", "ㅈㅍㄱ" },
        { "사오정", "ㅅㅇㅈ" },
        { "해리포터", "ㅎㄹㅍㅌ" },
        { "햄버거", "ㅎㅂㄱ" },
        { "파라오", "ㅍㄹㅇ" },
        { "스핑크스", "ㅅㅍㅋㅅ" },
        { "피라미드", "ㅍㄹㅁㄷ" },
        { "사막", "ㅅㅁ" },
        { "오아시스", "ㅇㅇㅅㅅ" },
        { "아령", "ㅇㄹ" },
        { "게임", "ㄱㅇ" },
        { "닻", "ㄷ" },
        { "전갈", "ㅈㄱ" },
        { "표창", "ㅍㅊ" },
        { "닌자", "ㄴㅈ" },
        { "뿡뿡이", "ㅃㅃㅇ" },
        { "방귀", "ㅂㄱ" },
        { "여우", "ㅇㅇ" },
        { "곰", "ㄱ" },
        { "김치찌개", "ㄱㅊㅉㄱ" },
        { "참치", "ㅊㅊ" },
        { "고등어", "ㄱㄷㅇ" },
        { "계란후라이", "ㄱㄹㅎㄹㅇ" },
        { "설렁탕", "ㅅㄹㅌ" },
        { "늑대", "ㄴㄷ" },
        { "낙지", "ㄴㅈ" },
        { "문어", "ㅁㅇ" },
        { "오징어", "ㅇㅈㅇ" },
        { "미라", "ㅁㄹ" },
        { "시체", "ㅅㅊ" },
        { "태양", "ㅌㅇ" },
        { "달", "ㄷ" },
        { "화성", "ㅎㅅ" },
        { "엘사", "ㅇㅅ" },
        { "고양이", "ㄱㅇㅇ" },
        { "천둥", "ㅊㄷ" },
        { "구름", "ㄱㄹ" },
        { "번개", "ㅂㄱ" },
        { "비", "ㅂ" },
        { "거미", "ㄱㅁ" },
        { "지네", "ㅈㄴ" },
        { "개미", "ㄱㅁ" },
        { "지렁이", "ㅈㄹㅇ" },
        { "파리", "ㅍㄹ" },
        { "조개", "ㅈㄱ" },
        { "파도", "ㅍㄷ" },
        { "스타벅스", "ㅅㅌㅂㅅ" },
        { "레드벨벳", "ㄹㄷㅂㅂ" },
        { "태연", "ㅌㅇ" },
        { "유리", "ㅇㄹ" },
        { "창문", "ㅊㅁ" },
        { "조커", "ㅈㅋ" },
        { "슈퍼맨", "ㅅㅍㅁ" },
        { "아이언맨", "ㅇㅇㅇㅁ" },
        { "박쥐", "ㅂㅈ" },
        { "배트맨", "ㅂㅌㅁ" },
        { "로마", "ㄹㅁ" },
        { "영국", "ㅇㄱ" },
        { "체스", "ㅊㅅ" },
        { "은행", "ㅇㅎ" },
        { "아침", "ㅇㅊ" },
        { "점심", "ㅈㅅ" },
        { "저녁", "ㅈㄴ" },
        { "밤", "ㅂ" },
        { "유튜브", "ㅇㅌㅂ" },
        { "작살", "ㅈㅅ" },
        { "밧줄", "ㅂㅈ" },
        { "테이프", "ㅌㅇㅍ" },
        { "풀", "ㅍ" },
        { "수영장", "ㅅㅇㅈ" },
        { "오리", "ㅇㄹ" },
        { "인형", "ㅇㅎ" },
        { "처키", "ㅊㅋ" },
        { "프랑켄슈타인", "ㅍㄹㅋㅅㅌㅇ" },
        { "빅뱅", "ㅂㅂ" },
        { "저승사자", "ㅈㅅㅅㅈ" },
        { "낫", "ㄴ" },
        { "얼음", "ㅇㅇ" },
        { "겨울", "ㄱㅇ" },
        { "봄", "ㅂ" },
        { "여름", "ㅇㄹ" },
        { "가을", "ㄱㅇ" },
        { "단풍", "ㄷㅍ" },
        { "낙엽", "ㄴㅇ" },
        { "빗자루", "ㅂㅈㄹ" },
        { "학교", "ㅎㄱ" },
        { "피클", "ㅍㅋ" },
        { "떡볶이", "ㄸㅂㅇ" },
        { "순대", "ㅅㄷ" },
        { "바둑", "ㅂㄷ" },
        { "주차장", "ㅈㅊㅈ" },
        { "바퀴", "ㅂㅋ" },
        { "하스스톤", "ㅎㅅㅅㅌ" },
        { "호두", "ㅎㄷ" },
        { "땅콩", "ㄸㅋ" },
        { "팥", "ㅍ" },
        { "붕어빵", "ㅂㅇㅃ" },
        { "돼지저금통", "ㄷㅈㅈㄱㅌ" },
        { "인쇄기", "ㅇㅅㄱ" },
        { "소닉", "ㅅㄴ" },
        { "마리오", "ㅁㄹㅇ" },
        { "복숭아", "ㅂㅅㅇ" },
        { "루이지", "ㄹㅇㅈ" },
        { "버섯", "ㅂㅅ" },
        { "곰팡이", "ㄱㅍㅇ" },
        { "세균", "ㅅㄱ" },
        { "현미경", "ㅎㅁㄱ" },
        { "돋보기", "ㄷㅂㄱ" },
        { "수학", "ㅅㅎ" },
        { "과학", "ㄱㅎ" },
        { "식당", "ㅅㄷ" },
        { "숟가락", "ㅅㄱㄹ" },
        { "백종원", "ㅂㅈㅇ" },
        { "젓가락", "ㅈㄱㄹ" },
        { "포크", "ㅍㅋ" },
        { "삼지창", "ㅅㅈㅊ" },
        { "철퇴", "ㅊㅌ" },
        { "쇠사슬", "ㅅㅅㅅ" },
        { "레고", "ㄹㄱ" },
        { "악어", "ㅇㅇ" },
        { "귀뚜라미", "ㄱㄸㄹㅁ" },
        { "메뚜기", "ㅁㄸㄱ" },
        { "보일러", "ㅂㅇㄹ" },
        { "피", "ㅍ" },
        { "영어", "ㅇㅇ" },
        { "한글", "ㅎㄱ" },
        { "마블", "ㅁㅂ" },
        { "한자", "ㅎㅈ" },
        { "깡패", "ㄲㅍ" },
        { "야쿠자", "ㅇㅋㅈ" },
        { "코끼리", "ㅋㄲㄹ" },
        { "낙타", "ㄴㅌ" },
        { "망토", "ㅁㅌ" },
        { "지니", "ㅈㄴ" },
        { "피아노", "ㅍㅇㄴ" },
        { "블랙핑크", "ㅂㄹㅍㅋ" },
        { "기타", "ㄱㅌ" },
        { "리코더", "ㄹㅋㄷ" },
        { "빛", "ㅂ" },
        { "바이올린", "ㅂㅇㅇㄹ" },
        { "소리", "ㅅㄹ" },
        { "호루라기", "ㅎㄹㄹㄱ" },
        { "짜장면", "ㅉㅈㅁ" },
        { "설거지", "ㅅㄱㅈ" },
        { "짬뽕", "ㅉㅃ" },
        { "탕수육", "ㅌㅅㅇ" },
        { "양파", "ㅇㅍ" },
        { "단무지", "ㄷㅁㅈ" },
        { "그림자", "ㄱㄹㅈ" },
        { "지구온난화", "ㅈㄱㅇㄴㅎ" },
        { "올림픽", "ㅇㄹㅍ" },
        { "메시", "ㅁㅅ" },
        { "호날두", "ㅎㄴㄷ" },
        { "월드컵", "ㅇㄷㅋ" },
        { "축구", "ㅊㄱ" },
        { "야구", "ㅇㄱ" },
        { "농구", "ㄴㄱ" },
        { "볼링", "ㅂㄹ" },
        { "썰매", "ㅆㅁ" },
        { "스케이트", "ㅅㅋㅇㅌ" },
        { "낚시", "ㄴㅅ" },
        { "배터리", "ㅂㅌㄹ" },
        { "잎", "ㅇ" },
        { "뿌리", "ㅃㄹ" },
        { "산삼", "ㅅㅅ" },
        { "심마니", "ㅅㅁㄴ" },
        { "에디슨", "ㅇㄷㅅ" },
        { "핵폭탄", "ㅎㅍㅌ" },
        { "전쟁", "ㅈㅈ" },
        { "히틀러", "ㅎㅌㄹ" },
        { "카카오톡", "ㅋㅋㅇㅌ" },
        { "네이버", "ㄴㅇㅂ" },
        { "구글", "ㄱㄱ" },
        { "시계", "ㅅㄱ" },
        { "시간", "ㅅㄱ" },
        { "아인슈타인", "ㅇㅇㅅㅌㅇ" },
    };

    int HeadCount = 2;                                              // 현재 인원 수
    int Who = 0;                                                    // 플레이어 차례
    int Round = 1;                                                  // 게임 라운드
    int[] order = { 1, 2, 3, 4, 5, 6 };                             // 순서
    int HelpPageNumber = 1;                                         // 도움말 현재 페이지

    bool isMain = true;                                             // 현재 메인 씬인가?
    bool isSub = false;                                             // 현재 서브 씬인가?
    bool isColorChanging = false;                                   // 현재 플레이어 색상 변경 중인가?
    bool isCorrect = false;                                         // 도전 성공했는가?
    bool isBGMMute = false;                                         // 브금 음소거인가?
    bool isSFXMute = false;                                         // 효과음 음소거인가?

    List<GameObject> Backgrounds = new List<GameObject>();          // 플레이어 배경 리스트
    List<String> Words = new List<String>();                        // 플레이어 제시어 리스트
    List<String> Nickname = new List<String>();                     // 플레이어 닉네임 리스트

    enum BGM { MainBGM, ShowWordBGM, GameBGM };
    enum SFX { Click, Slide, Hint, Fail, Over, Beep };

    void Awake()
    {
        // 최적화
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        // 메인씬 -> 서브씬
        if (Input.anyKeyDown && isMain && !EventSystem.current.IsPointerOverGameObject())
        {
            PlaySFX(SFX.Click);

            isMain = false;
            isSub = true;

            MainSceneGroup.SetActive(false);
            SubSceneGroup.SetActive(true);
            ColorSelectUI.SetActive(false);
        }

        // 서브 씬 -> 메인씬
        if (Input.GetKeyDown(KeyCode.Escape) && isSub && !ColorSelectUI.activeSelf)
        {
            PlaySFX(SFX.Click);

            GoMain();
        }

        // 서브 씬 색상창 닫기
        if (Input.GetKeyDown(KeyCode.Escape) && isSub && ColorSelectUI.activeSelf)
        {
            PlaySFX(SFX.Click);

            ColorSelectUI.SetActive(false);
            Smog.SetActive(false);
        }
    }

    // 메인화면으로
    public void GoMain()
    {
        PlaySFX(SFX.Click);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // 게임 종료
    public void EndGame()
    {
        PlaySFX(SFX.Click);

        Application.Quit();
    }

    // 다시하기
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

    // 창 띄우기 함수 (스모그랑)
    public void ShowWithSmog(GameObject obj)
    {
        PlaySFX(SFX.Click);

        obj.SetActive(true);
        Smog.SetActive(true);
    }

    // 창 띄우기 함수
    public void Show(GameObject obj)
    {
        PlaySFX(SFX.Click);

        obj.SetActive(true);
    }

    // 창 닫기 함수 (스모그랑)
    public void DownWithSmog(GameObject obj)
    {
        PlaySFX(SFX.Click);

        obj.SetActive(false);
        Smog.SetActive(false);
    }

    // 창 닫기 함수
    public void Down(GameObject obj)
    {
        PlaySFX(SFX.Click);

        obj.SetActive(false);
    }

    // 도움말 > 버튼
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

    // 도움말 < 버튼
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

    // 인원수 > 버튼
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

    // 인원수 < 버튼
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

    // 색상창 띄우기
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

    // 색상창 색상 선택
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

    // 게임 시작 버튼
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
            // 플레이어 배경 세팅
            GameObject background_obj;
            background_obj = Instantiate(PlayerBackground[Array.IndexOf(ColorArr, i)],BackgroundGroup);
            Backgrounds.Add(background_obj);
            Backgrounds[i-1].SetActive(false);

            // 플레이어 이름 세팅
            Nickname.Add(PlayerNickname[i-1].text);

            // 플레이어 제시어 세팅
            System.Random random = new System.Random();
            int index = random.Next(WordDict.Count);
            Words.Add(WordDict.Keys.ElementAt(index));
        }

        // 순서 섞기
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

    // 제시어 공개 슬라이드 닫기
    void DownShowWords()
    {
        PlayBGM(BGM.ShowWordBGM);
        Backgrounds[0].SetActive(true);
        ShowWordGroup.SetActive(true);

        NicknameTitle.text = Nickname[0] + "\n님의 제시어";
        Warning.text = Nickname[0] + " 님을\n 제외한 플레이어만 확인하세요";

        ShowWordsSlide.SetActive(false);
    }

    // 제시어 공개 빨간색 제시어 확인 버튼
    public void CheckWord()
    {
        PlaySFX(SFX.Click);
        CheckWordGroup.SetActive(false);
        PlayerWord.text = Words[Who];
    }

    // 제시어 공개 초록색 확인 버튼
    public void CheckNext()
    {
        if (Who + 1 < HeadCount)
        {
            PlaySFX(SFX.Click);
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

            BGMPlayer.Stop();
            PlaySFX(SFX.Slide);
            Invoke("DownGameStart", 1.5f);
        }
    }

    // 게임 시작 슬라이드 닫기
    void DownGameStart()
    {
        PlayBGM(BGM.GameBGM);

        Backgrounds[Who].SetActive(false);
        Who = 0;
        Backgrounds[Who].SetActive(true);

        NicknameQuestion.text = Nickname[Who] + "\n님의 질문";
        Hint_count.text = (Words[Who].Length).ToString() + "글자";
        Hint_first.text = WordDict[Words[Who]];

        GameStartGroup.SetActive(true);
        Round_text.gameObject.SetActive(true);
        Round_text.text = "Round " + Round;

        HintBtn.SetActive(true);
        GameStartSlide.SetActive(false);
    }

    // 글자 수 힌트 버튼
    public void ShowHintCount()
    {
        PlaySFX(SFX.Hint);
        HintCountBtn.SetActive(false);
        Hint_countCover.SetActive(false);
    }

    // 초성 힌트 버튼
    public void ShowHintFirst()
    {
        PlaySFX(SFX.Hint);
        HintFirstBtn.SetActive(false);
        Hint_firstCover.SetActive(false);
    }

    // 도전 스킵 버튼
    public void ChallengeSkip()
    {
        PlaySFX(SFX.Click);
        if (Who + 1 < HeadCount)
        {
            Who++;

            Backgrounds[Who-1].SetActive(false);
            Backgrounds[Who].SetActive(true);
            NicknameQuestion.text = Nickname[Who] + "\n님의 질문";
            Hint_count.text = (Words[Who].Length).ToString() + "글자";
            Hint_first.text = WordDict[Words[Who]];
            HintBtn.SetActive(false);
        }
        else
        {
            Who = 0;

            Backgrounds[HeadCount-1].SetActive(false);
            Backgrounds[Who].SetActive(true);
            NicknameQuestion.text = Nickname[Who] + "\n님의 질문";
            Hint_count.text = (Words[Who].Length).ToString() + "글자";
            Hint_first.text = WordDict[Words[Who]];
            HintBtn.SetActive(true);

            Round++;
            Round_text.text = "Round " + Round;
        }
    }

    // 뭐더라 버튼
    public void ShowForgetGroup()
    {
        PlaySFX(SFX.Click);
        ForgetGroup.SetActive(true);
        BeforeCheckGroup.SetActive(true);
        AfterCheckGroup.SetActive(false);
        ForgetWarning.text = Nickname[Who] + " 님을\n 제외한 플레이어만 확인하세요";

        Smog.SetActive(true);
    }

    // 뭐더라 제시어 확인 버튼
    public void ForgetBtn()
    {
        PlaySFX(SFX.Click);
        BeforeCheckGroup.SetActive(false);
        AfterCheckGroup.SetActive(true);
        ForgetWord.text = Words[Who];
    }

    // 메모장 버튼
    public void ShowMemoGroup()
    {
        PlaySFX(SFX.Click);
        MemoGroup.SetActive(true);
        Memo.text = PlayerMemo[Who];

        Smog.SetActive(true);
    }

    // 메모 저장
    public void SaveMemo()
    {
        PlayerMemo[Who] = Memo.text;
    }

    // 도전하고 카운트다운
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

            Failure_text.text = Nickname[Who] + "\n님의 제시어는\n" + Challenge.text + "이(가)\n아닙니다";
            FailureGroup.SetActive(true); ;
        }
    }

    // 도전 입력창 초기화
    public void ResetChallenge()
    {
        BGMPlayer.Stop();
        Challenge.text = "";
        isCorrect = false;
    }

    // 배경음 음소거
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
        BGM_slidertext.text = "배경음 : " + (int)((BGM_slider.value + 80f));
    }

    // 배경음 슬라이더
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

        BGM_slidertext.text = "배경음 : " + (int)((BGM_slider.value + 80f));
    }

    // 효과음 음소거
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
        BGM_slidertext.text = "배경음 : " + (int)((BGM_slider.value + 80f));
    }

    // 효과음 슬라이더
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

        SFX_slidertext.text = "효과음 : " + (int)((SFX_slider.value + 80f));
    }

    // 이미지 색깔 변화
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

    // BGM 재생
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

    // SFX 재생
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

    // 게임 음악 재생
    public void PlayGameBGM()
    {
        BGMPlayer.clip = BGMMusic[2];
        BGMPlayer.Play();
    }
}