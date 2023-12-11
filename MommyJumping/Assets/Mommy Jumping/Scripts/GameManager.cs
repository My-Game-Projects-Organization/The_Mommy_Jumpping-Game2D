using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState state;
    public Player player;
    public Player player_special;
    public int startingPlatform;
    public float xSpawnOffset;
    public float minYSpawnPos;
    public float maxYSpawnPos;
    public Platform[] platformPrefabs;
    public CollectableItem[] collectableItems;


    private Platform m_lastPlatformSpawned;
    private List<int> m_platformLandedIds;
    private float m_halfCamSizeX;
    private int m_score;
    private int numberOFCoins;
     

    public Platform LastPlatformSpawned { get => m_lastPlatformSpawned; set => m_lastPlatformSpawned = value; }
    public List<int> PlatformLandedIds { get => m_platformLandedIds; set => m_platformLandedIds = value; }
    public int Score { get => m_score; }
    public int NumberOFCoins { get => numberOFCoins;}

    public override void Awake()
    {
        MakeSingleton(false);

        m_platformLandedIds = new List<int>();
        m_halfCamSizeX = Helper.Get2DCamSize().x / 2; 
    }

    public override void Start()
    {
        base.Start();
        state = GameState.Starting;
        

        if (AudioController.Ins)
            AudioController.Ins.PlayBackgroundMusic();
    }

    public void PlayGame()
    {
        if (GUIManager.Ins)
        {
                if (Pref.SelectedCharacterId == 0)
                {
                    if (!player) return;
                    player = Instantiate(player, Vector3.zero, Quaternion.identity);
                    player.transform.localScale = Vector3.one;
                    player.transform.localPosition = Vector3.zero;
                }
                else
                {
                    if (!player_special) return;
                    player_special = Instantiate(player_special, Vector3.zero, Quaternion.identity);
                player_special.transform.localScale = new Vector3(0.2f,0.2f,0.2f);
                player_special.transform.localPosition = Vector3.zero;
                player_special.GetComponent<SpriteRenderer>().sprite = ShopManager.Ins.items[Pref.SelectedCharacterId].hud;
                }

            GUIManager.Ins.ShowGamePlay(true);
                
        }
        Invoke("PlatformInit", 0.5f);
        Invoke("PlayGameInk", 1f);
    }

    private void PlayGameInk()
    {
        state = GameState.Playing;
        if (player || player_special)
        {
            player.Jump();
            player_special.Jump();
        }
    }

    private void PlatformInit()
    {
        m_lastPlatformSpawned = Pref.SelectedCharacterId == 0 ? player.PlatformLanded : player_special.PlatformLanded;
        //m_lastPlatformSpawned = player_special.PlatformLanded;
        if (m_lastPlatformSpawned == null) Debug.Log("Null"); 
        for (int i = 0; i < startingPlatform; i++)
        {
            SpawnPlatform();
        }
    }

    public bool IsPlatformLanded(int id)
    {
        // check nếu id plat này nếu nằm trong số những plat player đã nhảy lên rồi thì return true
        if (m_platformLandedIds == null || m_platformLandedIds.Count <= 0) return false;

        return m_platformLandedIds.Contains(id);
    }

    public void SpawnPlatform()
    {
        if (!player || !player_special || platformPrefabs == null || platformPrefabs.Length <= 0)
        {
            return;
        } 
        float spawnPosX = Random.Range(
            -(m_halfCamSizeX - xSpawnOffset), (m_halfCamSizeX - xSpawnOffset));
        float distBetweenPlat = Random.Range(minYSpawnPos,maxYSpawnPos);
        float spawnPosY = m_lastPlatformSpawned.transform.position.y + distBetweenPlat;
        Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, 0f);
        
        int randUIdx = Random.Range(0, platformPrefabs.Length);
        Debug.Log(randUIdx + "");
        var platformPrefab = platformPrefabs[randUIdx];

        if (!platformPrefab)
        {
            return;
        }
        var platformClone = Instantiate(platformPrefab,spawnPos, Quaternion.identity);
        platformClone.Id = m_lastPlatformSpawned.Id + 1;

        m_lastPlatformSpawned = platformClone;
    }

    public void SpawnCollectable(Transform spawnPoint)
    {
        if(collectableItems == null || collectableItems.Length <= 0 || state != GameState.Playing) return;

        int randIdx = Random.Range(0, collectableItems.Length);
        var collectableItem = collectableItems[randIdx];

        if(collectableItem == null) return;

        float randCheck = Random.Range(0f, 1f);
        
        if(randCheck < collectableItem.spawnRate && collectableItem.collectablePrefabs)
        {
            var cClone = Instantiate(collectableItem.collectablePrefabs,spawnPoint.position, Quaternion.identity);
            // set obj thanh con của spawnPoint để lấy vị trí 
            cClone.transform.SetParent(spawnPoint);
        }
    }
    public void AddCoin()
    {
        if (state != GameState.Playing) return;

        numberOFCoins++;
        //Pref.numberOfCoins = numberOFCoins;
        if (GUIManager.Ins)
        {
            GUIManager.Ins.UpdateCoin(numberOFCoins);
        }
    }

    public void AddScore(int scoreAdd)
    {
        if(state != GameState.Playing) return;

        m_score += scoreAdd;
        Pref.bestScore = m_score;
        if (GUIManager.Ins)
        {
            GUIManager.Ins.UpdateScore(m_score);
        }
    }
}
