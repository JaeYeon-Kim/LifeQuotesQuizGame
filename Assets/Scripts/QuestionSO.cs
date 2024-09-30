using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
질문을 모아놓는 스크립터블 오브젝트 
*/

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    // TextArea: 인스펙터 안의 텍스트 상자의 크기를 지정하고 조정할 수 있음 
    [TextArea(2, 6)]
    [SerializeField] string question = "Enter new question text here";

    public string GetQuestion()
    {
        return question;
    }

}
