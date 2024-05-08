using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;

public class QuizManager : MonoBehaviour
{
    private int time; // present time
    private int start_Time = 5;
    private int level = 1; // present level
    private int sum_Number; // answer from the quiz
    private int answer_Interval = 5; // interval number that make the wrong answer
    private int max_Level = 7;
    private float start_Delay = 2f; // before start the next quiz time
    private float hide_Delay = 6f;
    private float gameover_Delay = 2f;
    private Vector3 road_SpawnPos = new Vector3(60, 0, 0);
    private GameObject instance_Road;
    private SoundManager soundManager;
    private DataTable dt;
    private string math_Str = "";

    public GameObject roads;
    public GameObject quiz_UI;
    public GameObject road_Number_Mark;
    public Text[] road_Quiz_Number_Text;
    public Text quiz_Text;
    public Text quiz_Time_Text;
    public GameObject gameover_Object;

    private void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        quiz_UI.SetActive(true);
        time = start_Time;
        quiz_Time_Text.text = "Time: " + time;
        dt = new DataTable();
        StartCoroutine(TimeDescrease());
    }

    IEnumerator TimeDescrease() // time UI and quiz UI appear on screen, and show the time flowing
    {
        Setting_Quiz();

        while(time >0)
        {
            yield return new WaitForSeconds(1f);
            time -= 1;
            quiz_Time_Text.text = "Time: " + time;

            if(time !=0)
            {
                soundManager.time_Flow();
            }
        }

        quiz_UI.SetActive(false);
        instance_Road=Instantiate(roads, road_SpawnPos, roads.transform.rotation);
        StartCoroutine(AnswerHide());
        Random_Answer();
    }
    
    IEnumerator AnswerHide()
    {
        road_Number_Mark.SetActive(true);

        yield return new WaitForSeconds(hide_Delay);

        road_Number_Mark.SetActive(false);
    }

    IEnumerator Quiz_Restart() // quiz get resetting
    {
        yield return new WaitForSeconds(start_Delay);

        quiz_UI.SetActive(true);
        time = start_Time;
        quiz_Time_Text.text = "Time: " + time;
        quiz_Text.text = " ";
        math_Str = "";
        if(level < max_Level)
        {
            level += 1;
        }
        StartCoroutine(TimeDescrease());

    }

    public IEnumerator Gameover_Delay()
    {
        yield return new WaitForSeconds(gameover_Delay);

        gameover_Object.SetActive(true);
    }

    public void quiz_Correct() // if quiz correct, that func played
    {
        StartCoroutine(Quiz_Restart());
    }

    private void Setting_Quiz() // quiz get setting by level
    {
        Random_Formula(level);
    }

    private void Random_Formula(int level) // make the formula
    {
        List<int> number = new List<int>();

        for(int i=0; i< level+1; i++)
        {
            number.Add(Random.Range(1, 10));
            quiz_Text.text += number[i];
            math_Str += number[i];

            if(i==0)
            {
                sum_Number = number[i];
            }

            if(i != level)
            {
                int random_Sign = Random.Range(0, 3);

                switch(random_Sign)
                {
                    case 0:
                        quiz_Text.text += "+";
                        math_Str += "+";
                        break;
                    case 1:
                        quiz_Text.text += "-";
                        math_Str += "-";
                        break;
                    case 2:
                        quiz_Text.text += "x";
                        math_Str += "*";
                        break;
                }
            }
        }

        sum_Number = (int)dt.Compute(math_Str, "");
    }

    private void Random_Answer() // three roads's number have the random number that two number are wrond answer and one nubmer is correct answer, and correct answer with road is open to pass
    {
        int correct_Answer_number = Random.Range(0, 3);

        GameObject[] wall = GameObject.FindGameObjectsWithTag("Wall");

        for(int i=0; i<3;i++)
        {
            if(i == correct_Answer_number)
            {
                road_Quiz_Number_Text[i].text = sum_Number.ToString();
                wall[correct_Answer_number].SetActive(false);
            }

            else
            {
                int random_Number = Random.Range(sum_Number - answer_Interval, sum_Number + answer_Interval);

                while(random_Number == sum_Number)
                {
                    random_Number = Random.Range(sum_Number - answer_Interval, sum_Number + answer_Interval);
                }

                road_Quiz_Number_Text[i].text = random_Number.ToString();
            }
        }
    }

}
