using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Play butonuna tıklandığında çalışacak fonksiyon
    public void PlayGame()
    {
        // "GameScene" adlı sahneyi yükler. 
        // Build Settings'te sahnelerin ekli olduğundan emin olun.
        SceneManager.LoadScene("GameScene");
    }

    // Çıkış butonuna tıklandığında çalışacak fonksiyon
    public void QuitGame()
    {
        // Editörde çalışırken çıkış yapıldığını görmek için log yazdırıyoruz
        Debug.Log("Oyundan Çıkıldı!");
        Application.Quit();
    }
}
