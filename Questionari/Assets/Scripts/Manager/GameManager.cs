using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class Questions
    {
        public string question;
        public answers[] answerA;
        [System.Serializable]
        public class answers
        {
            public string answer;
            public bool Correct;
        }
    }
    public GameObject fatherGrid, answerPref, questionObj;
    public Questions[] questionsA;

    private List<GameObject> questionsInst = new List<GameObject>();
    private int countQuestions = 0;
    void Start()
    {
        ActiveQuestion();
    }
    public void ActiveQuestion()
    {
        if (countQuestions != questionsA.Length)
        {
            for (int i = 0; i < questionsA.Length; i++)
            {
                if (i == countQuestions)
                {
                    for (int i2 = 0; i2 < questionsA[i].answerA.Length; i2++)
                    {
                        GameObject NewAnswer = Instantiate(answerPref, fatherGrid.transform.position, Quaternion.identity); //Instantiate Button


                        NewAnswer.transform.parent = fatherGrid.transform; //Put as son of the grid
                        NewAnswer.transform.localScale = fatherGrid.transform.localScale; //Match the father scale
                        questionsInst.Add(NewAnswer);  //Add to list to have them all saved
                        NewAnswer.transform.GetChild(0).transform.GetComponent<Text>().text = questionsA[i].answerA[i2].answer;//Match the text of the button to the text of the response already created
                        NewAnswer.name = questionsA[i].answerA[i2].answer;//I equal the name of the button to the answer
                        bool ItsCorrect = questionsA[i].answerA[i2].Correct;
                        NewAnswer.transform.GetComponent<Button>().onClick.AddListener(delegate { Try(ItsCorrect); });// assign the function of the button

                        questionObj.transform.GetChild(0).GetComponent<Text>().text = questionsA[i].question;//Assign name Question
                    }
                }
            }
            countQuestions++;
        }
        else
        {
            questionObj.transform.GetChild(0).GetComponent<Text>().text = " ";
            print("Finish");//Finish Questions
        }
    }
    public void Try(bool Correct) // Check if the selected question is correct
    {
        if (Correct) // If the question is correct
        {
            print("You're right");
            RemoveQuestions();// Delete the questions on the screen
            ActiveQuestion();// Put the following questions
        }
        else //If the question is incorrect
        {
            print("You have failed");
            RemoveQuestions();
            ActiveQuestion();
        }
    }
    public void RemoveQuestions()
    {
        for (int i = 0; i < questionsInst.Count; i++)
        {
            Destroy(questionsInst[i]); //Destroy the questions
        }
        questionsInst.Clear(); // Clear the list
    }

}
