using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;

    // 문제 리스트들 (질문 스크립터블 오브젝트를 리스트로 생성)
    [SerializeField] List<QuestionSO> questions;
    QuestionSO currentQuestion;


    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;    // 버튼에 대한 배열 
    int correctAnswerIndex;
    bool hasAnsweredEarly;  // 타이머가 다되어 정답을 표시해야 하는것인지 버튼을 클릭했으니 정답을 표시해야하는 것인지

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    // 타이머 이미지 연동을 위한 변수들
    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;


    void Start()
    {
        timer = FindObjectOfType<Timer>();
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;     // timer이미지의 양을 타이머 분수의 값으로 프레임마다 적용 

        if (timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        // 답을 일찍 선택하지 않았을 경우 && 질문에 답변중이지 않을 경우 => 두 조건 충족시 정답을 표시 
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);      // 정답과 다른 인덱스를 전달해야하므로 보통 -1 대입 
            SetButtonState(false);
        }
    }

    // 버튼을 눌렀을때 호출할 메소드 (정답 체크 시)
    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;        // 답변 버튼을 클릭했으면 질문에 일찍 답변한 것
        DisplayAnswer(index);

        SetButtonState(false);
        // 답을 선택한 후 타이머를 끊고 다음 상태로 전환 
        timer.CancelTimer();
    }

    void DisplayAnswer(int index)
    {
        Image buttonImage;

        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            // 정답일때 
            questionText.text = "Correct!";
            // 선택한 버튼의 이미지를 가져오고 정답 임을 표시하는 강조 이미지로 변경해줌 
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
        // 정답이 아닐 경우 
        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();  // 정답 인덱스 가져오기 
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry, the correct answer was;\n" + correctAnswer;

            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
        }

    }


    // 문제를 랜덤으로 생성 
    private void GetRandomQuestion()
    {
        // 목록의 0번 ~ 리스트 마지막 숫자 사이의 임의의 숫자가 반환 
        int index = Random.Range(0, questions.Count);

        // 현재 문제를 지정 
        currentQuestion = questions[index];


        // 문제 리스트에 뽑은 문제가 존재할 경우에만 제거 
        if (questions.Contains(currentQuestion))
        {
            // 뽑은 문제를 전체 리스트에서 제거 
            questions.Remove(currentQuestion);
        }
    }

    private void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {

            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();

            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    // 버튼의 상태를 관리하는 메소드
    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }
    private void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
}
