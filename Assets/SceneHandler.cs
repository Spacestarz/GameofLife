using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    public Slider slider; 
    public void PlayGame()
    {
        float sliderValue = slider.value;
        Slider_Value_Holder.SetSliderValue(sliderValue); //talk with another script
        Debug.Log("Slider Value Set: " + sliderValue); // check the value

        SceneManager.LoadScene(1);
    }

    public void backtomainmenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
