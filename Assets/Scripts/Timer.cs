using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // 문제 풀이에 주어지는 시간과 정답을 검토하기 위해 주어진 시간 
    [SerializeField] private float timeToCompleteQuestion = 30f;
    [SerializeField] private float timeToShowCorrectAnswer = 10f;

    // 문제 풀이 시간과 정답 표시 시간 사이 전환을 위한 변수 
    public bool isAnsweringQuestion = false;

    float timerValue;       // 문제의 남은 시간 변수 

    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        // 게임이 진행되는 매 프레임마다 타이머가줄어들어야함
        timerValue -= Time.deltaTime;


        // 문제를 풀고 있는데 시간이 다 될 경우 정답 표시 상태로 넘어감 
        if (isAnsweringQuestion)
        {
            if (timerValue <= 0)
            {
                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
        }
        else
        {
            if (timerValue <= 0)
            {
                isAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion;
            }
        }


        Debug.Log(timerValue);
    }
}
