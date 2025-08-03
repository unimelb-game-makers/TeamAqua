using System;
using System.Collections;
using Kuroneko.UtilityDelivery;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class DayManager : MonoBehaviour, ISaveable
{
    public static DayManager instance;
    private const float LIGHT_DURATION = 1.0f;

    [SerializeField]
    private DayDatabase dayDatabase;

    [Header("Night Settings")]
    [SerializeField]
    private Color targetColor = Color.black;

    [SerializeField]
    private Material skyboxMaterial;

    [SerializeField]
    private float dayExposure = 1f;

    [SerializeField]
    private float nightExposure = 0.6f;

    // Exposed Actions
    public static Action<int> OnDayChanged;

    public ParticleSystem rainParticle;
    public const float RAINCHANCE = 0.3f;

    private Coroutine rainRoutine;

    // Current Day and Night
    private int _currentDay = 1;

    private bool isNight = false;

    // Serialized Variables
    private PlayerController _playerController;
    private bool shouldChangeColor = false;
    private bool firstTime = true;
    private float lerpTime = 0f;
    private Color initialColor;
    private Light directionalLight;
    private static readonly int Exposure = Shader.PropertyToID("_Exposure");
    private Material _runtimeSkyboxMaterial;
    
    // World Data
    private WorldData _worldData = new();

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        // TODO(Alex): We naively assume that there is only one light source for now.
        directionalLight = FindFirstObjectByType<Light>();
        _playerController = FindFirstObjectByType<PlayerController>();
        initialColor = directionalLight.color;
        EnergyManager.OnEnergyChanged += CheckEnergy;

        _runtimeSkyboxMaterial = new Material(skyboxMaterial);
        RenderSettings.skybox = _runtimeSkyboxMaterial;
    }

    public void RegisterNpc(NPC npc)
    {
        if (!npc.id)
        {
            Debug.LogWarning($"NPC {npc.name} does not have an ID!");
            return;
        }
        // If there is another NPC, then we want to log a warning
        if (_worldData.npcs.ContainsKey(npc.id))
        {
            Debug.LogError($"Conflicting NPC ID: {npc.id} from {_worldData.npcs[npc.id]} and {npc.name}");
            return;
        }
        
        Day currentDay = dayDatabase.GetDay(_currentDay);
        npc.gameObject.SetActiveFast(currentDay.worldDatabase.CanEnable(npc.id));
        _worldData.npcs.Add(npc.id, npc);

        // If the NPC is Amelia, then we want to store it differently and set their position
        if (npc.GetType() == typeof(Amelia) && _worldData.ameliaSavePosition != Vector3.zero)
        {
            _worldData.amelia = (Amelia)npc;
            npc.transform.position = _worldData.ameliaSavePosition;
        }
    }

    public void Load(SaveSlot saveSlot)
    {
        // Reset the world data each time
        _worldData = new WorldData();
        
        // Init the day database
        dayDatabase.Init();
        
        _currentDay = saveSlot.worldSaveData.currentDay;
        _worldData.ameliaSavePosition = saveSlot.worldSaveData.ameliaPosition;

        // Enter the current day
        Day currentDay = dayDatabase.GetDay(_currentDay);
        currentDay.Enter(_worldData);
    }

    public SaveSlot Save(SaveSlot saveSlot)
    {
        SaveSlot save = saveSlot;
        save.worldSaveData.currentDay = _currentDay;
        save.worldSaveData.ameliaPosition = _worldData.amelia.transform.position;
        return save;
    }

    private void CheckEnergy(float energyAmount)
    {
        if (energyAmount < 50f && !isNight)
        {
            SetNight(true);
        }
    }

    private void FixedUpdate()
    {
        if (shouldChangeColor && firstTime)
        {
            ChangeLight();
        }
    }

    public bool CanChangeDay()
    {
        return isNight;
    }

    public void StartNewDay()
    {
        // Uninitialise the current day
        Day previousDay = dayDatabase.GetDay(_currentDay);
        previousDay.Exit(_worldData);
        
        // Change days
        OnDayChanged?.Invoke(_currentDay);
        _currentDay += 1;
        
        // Initialise the next day
        Day nextDay = dayDatabase.GetDay(_currentDay);
        nextDay.Enter(_worldData);
        
        // Notify other managers
        DialogueManager.instance.SetDay(_currentDay); // _currentDay starts index at 1
        SetNight(false);
        _playerController.handleNextDay();
        RainWithDelay(false, 2.0f);
        float randomValue = Random.value;
        if (randomValue < RAINCHANCE)
        {
            RainWithDelay(true, 2.0f); //wait 2s, so rain starts after screen blacked out
            Debug.Log("Rain starts!");
        }
        else
        {
            Debug.Log("No rain");
        }
        EnergyManager.instance.OnNextDay();

        // Trigger Save whenever a new day is started
        SaveManager.instance.Save();
    }

    private void SetNight(bool value)
    {
        isNight = value;
        if (isNight)
        {
            shouldChangeColor = true;
            lerpTime = 0f;
        }
        else
        {
            firstTime = false;
            _runtimeSkyboxMaterial.SetFloat(Exposure, dayExposure);
            directionalLight.color = initialColor;
        }
    }

    private void ChangeLight()
    {
        lerpTime += Time.deltaTime / LIGHT_DURATION;
        directionalLight.color = Color.Lerp(initialColor, targetColor, lerpTime);
        float exposure = Mathf.Lerp(dayExposure, nightExposure, lerpTime);
        _runtimeSkyboxMaterial.SetFloat(Exposure, exposure);
        if (lerpTime >= 1f)
        {
            directionalLight.color = targetColor;
            _runtimeSkyboxMaterial.SetFloat(Exposure, nightExposure);
            firstTime = false;
        }
    }

    private void OnDestroy()
    {
        EnergyManager.OnEnergyChanged -= CheckEnergy;
    }

    [Button]
    private void DebugNight()
    {
        SetNight(true);
    }

    [Button]
    public void StartRain()
    {
        if (rainParticle != null && !rainParticle.isPlaying)
        {
            rainParticle.Play();
        }
    }

    public void StopRain()
    {
        if (rainParticle != null && rainParticle.isPlaying)
        {
            rainParticle.Stop();
        }
    }

    public void RainWithDelay(bool startRain, float delay)
    {
        if (rainRoutine != null)
        {
            StopCoroutine(rainRoutine);
        }

        rainRoutine = StartCoroutine(RainAfterDelay(startRain, delay));
    }

    private IEnumerator RainAfterDelay(bool startRain, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (startRain)
        {
            StartRain();
        }
        else
        {
            StopRain();
        }
    }
}