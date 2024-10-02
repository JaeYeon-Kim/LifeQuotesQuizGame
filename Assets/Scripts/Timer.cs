using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // 문제 풀이에 주어지는 시간과 정답을 검토하기 위해 주어진 시간 
    [SerializeField] private float timeToCompleteQuestion = 30f;
    [SerializeField] private float timeToShowCorrectAnswer = 10f;

    // 다음 문제를 언제 보여줘야할지 연결해주는 변수 
    public bool loadNextQuestion;


    // 문제 풀이 시간과 정답 표시 시간 사이 전환을 위한 변수 
    public bool isAnsweringQuestion = false;
    public float fillFraction;

    float timerValue;       // 문제의 남은 시간 변수 

    void Update()
    {
        UpdateTimer();
    }


    // 정답일 경우 타이머를 기다리지 않고 즉시 종료하는 메소드 
    public void CancelTimer()
    {
        timerValue = 0;
    }

    void UpdateTimer()
    {
        // 게임이 진행되는 매 프레임마다 타이머가줄어들어야함
        timerValue -= Time.deltaTime;


        // 문제를 풀고 있는데 시간이 다 될 경우 정답 표시 상태로 넘어감 
        if (isAnsweringQuestion)
        {

            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToCompleteQuestion;
            }
            else
            {
                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }

        }
        else
        {

            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            else
            {
                isAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
        }

        Debug.Log(isAnsweringQuestion + ": " + timerValue + "=" + fillFraction);
    }
}
