using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Ana menüye dönmek için kullanılacak fonksiyon (İsteğe bağlı)
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
