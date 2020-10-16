using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void GotoMainScene()
    {
        SceneManager.LoadScene("StageSelect");
    }

    public void OpenFridge()
    {
        SceneManager.LoadScene("MyFridge");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void GoToKitchen()
    {
        SceneManager.LoadScene("Kitchen");
    }

    public void GoToCollections()
    {
        SceneManager.LoadScene("Collections");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

	public void GoToStore()
	{
		SceneManager.LoadScene("Store");
	}

    public void GoToDish2()
    {
        SceneManager.LoadScene("Dish2");
    }

    public void GoToDish3()
    {
        SceneManager.LoadScene("Dish3");
    }

    public void GoToDish4()
    {
        SceneManager.LoadScene("Dish4");
    }

    public void GoToDish5()
    {
        SceneManager.LoadScene("Dish5");
    }


}