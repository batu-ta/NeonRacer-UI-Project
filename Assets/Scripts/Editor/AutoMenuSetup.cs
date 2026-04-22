using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AutoMenuSetup : EditorWindow
{
    [MenuItem("Tools/Ödevi Otomatik Kur (Auto-Setup)")]
    public static void SetupScenes()
    {
        // 1. Scenes klasörü oluştur
        if (!AssetDatabase.IsValidFolder("Assets/Scenes"))
        {
            AssetDatabase.CreateFolder("Assets", "Scenes");
        }

        // FONT HATASI İÇİN GÜNCELLENEN KISIM
        Font defaultFont = null;
        try { defaultFont = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf"); } catch { }
        if (defaultFont == null) { try { defaultFont = Resources.GetBuiltinResource<Font>("Arial.ttf"); } catch { } }

        // --- 2. GAME SCENE (OYUN SAHNESİ) KURULUMU ---
        Scene gameScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        GameObject gameCam = new GameObject("Main Camera");
        Camera camComp = gameCam.AddComponent<Camera>();
        camComp.backgroundColor = new Color(0.1f, 0.1f, 0.15f);
        camComp.clearFlags = CameraClearFlags.SolidColor;
        gameCam.tag = "MainCamera";

        GameObject gameEventSystem = new GameObject("EventSystem");
        gameEventSystem.AddComponent<EventSystem>();
        gameEventSystem.AddComponent<StandaloneInputModule>();

        GameObject gameCanvas = new GameObject("Canvas");
        Canvas canvasComp2 = gameCanvas.AddComponent<Canvas>();
        canvasComp2.renderMode = RenderMode.ScreenSpaceOverlay;
        gameCanvas.AddComponent<CanvasScaler>();
        gameCanvas.AddComponent<GraphicRaycaster>();

        GameManager gm = gameCanvas.AddComponent<GameManager>();

        GameObject gameText = new GameObject("TitleText");
        gameText.transform.SetParent(gameCanvas.transform, false);
        Text gt = gameText.AddComponent<Text>();
        gt.text = "game scene";
        gt.font = defaultFont;
        gt.fontSize = 50;
        gt.alignment = TextAnchor.MiddleCenter;
        gt.color = Color.white;
        RectTransform gtRect = gameText.GetComponent<RectTransform>();
        gtRect.sizeDelta = new Vector2(400, 100);
        gtRect.anchoredPosition = new Vector2(0, 100);

        GameObject backBtnObj = new GameObject("BackButton");
        backBtnObj.transform.SetParent(gameCanvas.transform, false);
        backBtnObj.AddComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f);
        Button backBtn = backBtnObj.AddComponent<Button>();
        RectTransform backRect = backBtnObj.GetComponent<RectTransform>();
        backRect.sizeDelta = new Vector2(200, 60);
        backRect.anchoredPosition = new Vector2(0, -50);

        GameObject backBtnText = new GameObject("Text");
        backBtnText.transform.SetParent(backBtnObj.transform, false);
        Text bbt = backBtnText.AddComponent<Text>();
        bbt.text = "Return to Menu";
        bbt.font = defaultFont;
        bbt.fontSize = 20;
        bbt.alignment = TextAnchor.MiddleCenter;
        bbt.color = Color.white;
        bbt.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 60);

        UnityEditor.Events.UnityEventTools.AddVoidPersistentListener(backBtn.onClick, new UnityEngine.Events.UnityAction(gm.ReturnToMenu));

        string gameScenePath = "Assets/Scenes/GameScene.unity";
        EditorSceneManager.SaveScene(gameScene, gameScenePath);


        // --- 3. MAIN MENU (ANA MENÜ) KURULUMU ---
        Scene menuScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        GameObject menuCam = new GameObject("Main Camera");
        Camera mCamComp = menuCam.AddComponent<Camera>();
        mCamComp.backgroundColor = new Color(0.05f, 0.02f, 0.1f); // Cyberpunk koyu arka plan
        mCamComp.clearFlags = CameraClearFlags.SolidColor;
        menuCam.tag = "MainCamera";

        GameObject eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();

        GameObject canvas = new GameObject("Canvas");
        Canvas canvasComp = canvas.AddComponent<Canvas>();
        canvasComp.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.AddComponent<CanvasScaler>();
        canvas.AddComponent<GraphicRaycaster>();

        MainMenu mm = canvas.AddComponent<MainMenu>();

        GameObject themeText = new GameObject("ThemeText");
        themeText.transform.SetParent(canvas.transform, false);
        Text tt = themeText.AddComponent<Text>();
        tt.text = "CYBERPUNK THEME"; // Ödevde istenen tema yazısı
        tt.font = defaultFont;
        tt.fontSize = 24;
        tt.fontStyle = FontStyle.Bold;
        tt.alignment = TextAnchor.MiddleCenter;
        tt.color = Color.cyan;
        RectTransform ttRect = themeText.GetComponent<RectTransform>();
        ttRect.sizeDelta = new Vector2(400, 50);
        ttRect.anchoredPosition = new Vector2(0, 200);

        GameObject titleText = new GameObject("TitleText");
        titleText.transform.SetParent(canvas.transform, false);
        Text titlet = titleText.AddComponent<Text>();
        titlet.text = "NEON RACER";
        titlet.font = defaultFont;
        titlet.fontSize = 70;
        titlet.fontStyle = FontStyle.Bold;
        titlet.alignment = TextAnchor.MiddleCenter;
        titlet.color = Color.magenta;
        RectTransform titletRect = titleText.GetComponent<RectTransform>();
        titletRect.sizeDelta = new Vector2(600, 100);
        titletRect.anchoredPosition = new Vector2(0, 100);

        GameObject playBtnObj = new GameObject("PlayButton");
        playBtnObj.transform.SetParent(canvas.transform, false);
        playBtnObj.AddComponent<Image>().color = new Color(0.2f, 0.8f, 0.8f); // Cyan buton
        Button playBtn = playBtnObj.AddComponent<Button>();
        RectTransform playRect = playBtnObj.GetComponent<RectTransform>();
        playRect.sizeDelta = new Vector2(200, 60);
        playRect.anchoredPosition = new Vector2(0, -50);

        GameObject playBtnText = new GameObject("Text");
        playBtnText.transform.SetParent(playBtnObj.transform, false);
        Text pbt = playBtnText.AddComponent<Text>();
        pbt.text = "PLAY";
        pbt.font = defaultFont;
        pbt.fontSize = 24;
        pbt.fontStyle = FontStyle.Bold;
        pbt.alignment = TextAnchor.MiddleCenter;
        pbt.color = Color.black;
        pbt.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 60);

        UnityEditor.Events.UnityEventTools.AddVoidPersistentListener(playBtn.onClick, new UnityEngine.Events.UnityAction(mm.PlayGame));

        GameObject quitBtnObj = new GameObject("QuitButton");
        quitBtnObj.transform.SetParent(canvas.transform, false);
        quitBtnObj.AddComponent<Image>().color = new Color(0.8f, 0.2f, 0.5f); // Magenta buton
        Button quitBtn = quitBtnObj.AddComponent<Button>();
        RectTransform quitRect = quitBtnObj.GetComponent<RectTransform>();
        quitRect.sizeDelta = new Vector2(200, 60);
        quitRect.anchoredPosition = new Vector2(0, -130);

        GameObject quitBtnText = new GameObject("Text");
        quitBtnText.transform.SetParent(quitBtnObj.transform, false);
        Text qbt = quitBtnText.AddComponent<Text>();
        qbt.text = "QUIT";
        qbt.font = defaultFont;
        qbt.fontSize = 24;
        qbt.fontStyle = FontStyle.Bold;
        qbt.alignment = TextAnchor.MiddleCenter;
        qbt.color = Color.black;
        qbt.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 60);

        UnityEditor.Events.UnityEventTools.AddVoidPersistentListener(quitBtn.onClick, new UnityEngine.Events.UnityAction(mm.QuitGame));

        string menuScenePath = "Assets/Scenes/MainMenu.unity";
        EditorSceneManager.SaveScene(menuScene, menuScenePath);

        // --- 4. BUILD SETTINGS (SAHNELERİ LİSTEYE EKLEME) ---
        EditorBuildSettingsScene[] buildScenes = new EditorBuildSettingsScene[2];
        buildScenes[0] = new EditorBuildSettingsScene(menuScenePath, true);
        buildScenes[1] = new EditorBuildSettingsScene(gameScenePath, true);
        EditorBuildSettings.scenes = buildScenes;

        Debug.Log("Sahneler, UI elemanları ve Build Ayarları başarıyla oluşturuldu!");
    }
}
