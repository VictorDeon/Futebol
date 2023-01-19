using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Constants;

// Windows -> Service -> Ads ON -> Game ID -> Android/IOS
public class AdsManager: MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener {

    public static AdsManager instance;
    string platformId;
    bool testMode = true;

    // Disparar anuncio
    string adBasicId = null;
    bool adBasicLoaded = false;
    int loseGame = 0;

    // Disparar anuncio por recompensa
    string adRewardsId = null;
    Button btnAds;
    bool adRewardLoaded = false;
    public bool adsCompleted = false;

    void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        platformId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? PLATFORM_ADS_ID.IOS
            : PLATFORM_ADS_ID.ANDROID;

        adBasicId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? UNITY_MONITIZATION.BASIC_AD_IOS
            : UNITY_MONITIZATION.BASIC_AD_ANDROID;

        adRewardsId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? UNITY_MONITIZATION.REWARD_AD_IOS
            : UNITY_MONITIZATION.REWARD_AD_ANDROID;

        Advertisement.Initialize(platformId, testMode, this);

        SceneManager.sceneLoaded += GetButton;
    }

    void GetButton(Scene scene, LoadSceneMode mode) {
        if (WhereAmI.instance.isStageScene()) {
            btnAds = GameObject.Find("Ads Button").GetComponent<Button>();
            btnAds.onClick.AddListener(ShowRewardsAds);
        }
    }

    public void ShowRewardsAds() {
        if(adRewardLoaded) {
            Debug.Log("Mostrando anuncio rewards: " + adRewardsId);
            Advertisement.Show(adRewardsId, this);
        }
    }

    public void showBasicAds() {
        if(adBasicLoaded && loseGame == 2) {
            Debug.Log("Mostrando anuncio basico: " + adBasicId);
            Advertisement.Show(adBasicId, this);
            loseGame = 0;
        } else {
            loseGame++;
        }
    }

    public void OnInitializationComplete() {
        Debug.Log("Inicialização do Unity Ads completada.");
        Debug.Log("Carregando anuncio basico: " + adBasicId);
        Advertisement.Load(adBasicId, this);
        Debug.Log("Carregando anuncio rewards: " + adRewardsId);
        Advertisement.Load(adRewardsId, this);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message) {
        Debug.Log($"Inicialização do Unity Ads falhou: {error} - {message}");
    }

    public void OnUnityAdsAdLoaded(string adUnitId) {
        Debug.Log($"Anuncio {adUnitId} carregado com sucesso!");
        if (adUnitId.Equals(adBasicId)) {
            adBasicLoaded = true;
        }
        if (adUnitId.Equals(adRewardsId)) {
            adRewardLoaded = true;
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message) {
        // Opcionalmente, execute o código se o anúncio falhar ao carregar, como tentar novamente.
        Debug.Log($"Error ao carregar o Ad: {adUnitId} - {error} - {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message) {
        // Opcionalmente, execute o código se a unidade de anúncio não for exibida, como ao carregar outro anúncio.
        Debug.Log($"Error ao mostrar o anuncio {adUnitId}: {error} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId) {
        Debug.Log($"Anuncio {adUnitId} iniciado com sucesso");
    }

    public void OnUnityAdsShowClick(string adUnitId) {
        Debug.Log($"Anuncio {adUnitId} clicado");
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) {
        if (adUnitId.Equals(adRewardsId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED)) {
            Debug.Log($"Anuncio rewards {adUnitId} completado com sucesso!");
            adsCompleted = true;
        }
        this.OnUnityAdsAdLoaded(adUnitId);
    }
}

