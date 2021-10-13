using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;
using DG.Tweening;

public class Bank : MonoBehaviour
{
    public int money = 0;

    [SerializeField] TMP_Text moneyText;
    [SerializeField] TMP_Text appleText;
    [SerializeField] TMP_Text mushroomText;
    [SerializeField] TMP_Text workersText;
    [SerializeField] TMP_Text SeedText;

    public ResourceStorage appleStorage;
    public ResourceStorage mushroomStorage;
    public ResourceStorage seedStorage;
    public GameObject antAI;
    public Mover mover;
    public Expirience expirience;
    public HealthPlayer healthPlayer;
    public Attack playerAttack;
    public EnemyHealth enemyHealth;
    public ProcessingRoom processingRoom;
    public AntMother antMother;

    #region TextValues
    [SerializeField] TMP_Text appleCurrentLevelText;
    [SerializeField] TMP_Text appleCostText;

    [SerializeField] TMP_Text seedCurrentLevelText;
    [SerializeField] TMP_Text seedCostText;

    [SerializeField] TMP_Text processingCurrentLevelText;
    [SerializeField] TMP_Text processingCostText;

    [SerializeField] TMP_Text mushroomCurrentLevelText;
    [SerializeField] TMP_Text mushroomCostText;

    [SerializeField] TMP_Text capacityOfWorkersText;
    [SerializeField] TMP_Text capacityOfWorkersCostText;

    [SerializeField] TMP_Text currentSpeedAIText;
    [SerializeField] TMP_Text currentSpeedAICostText;

    [SerializeField] TMP_Text currentSpeedPlayerText;
    [SerializeField] TMP_Text currentSpeedPlayerCostText;

    [SerializeField] TMP_Text currentHealthPlayerText;
    [SerializeField] TMP_Text currentHealthlayerCostText;

    [SerializeField] TMP_Text currentDamagePlayerText;
    [SerializeField] TMP_Text currentDamagePlayerCostText;

    #endregion

    private void Start()
    {

        money = PlayerPrefs.GetInt("money", 10);
        appleStorage.currentAmount = PlayerPrefs.GetInt("appleStorage.currentAmount", 2);
        appleStorage.roomLevel = PlayerPrefs.GetInt("appleStorage.roomLevel", 1);
        appleStorage.roomCapacity = PlayerPrefs.GetInt("appleStorage.roomCapacity", 4);

        seedStorage.currentAmount = PlayerPrefs.GetInt("seedStorage.currentAmount", 2);
        seedStorage.roomLevel = PlayerPrefs.GetInt("seedStorage.roomLevel", 1);
        seedStorage.roomCapacity = PlayerPrefs.GetInt("seedStorage.roomCapacity", 3);

        mushroomStorage.currentAmount = PlayerPrefs.GetInt("mushroomStorage.currentAmount", 2);
        mushroomStorage.roomLevel = PlayerPrefs.GetInt("mushroomStorage.roomLevel", 1);
        mushroomStorage.roomCapacity = PlayerPrefs.GetInt("mushroomStorage.roomCapacity", 2);

        processingRoom.roomLvl = PlayerPrefs.GetInt("processingRoom.roomLvl", 0);

        antMother.currentAmount = PlayerPrefs.GetInt("antMother.currentAmount", 0);
        antMother.roomCapacity = PlayerPrefs.GetInt("antMother.roomCapacity", 1);
        antMother.speedAntAI = PlayerPrefs.GetFloat("antMother.speedAntAI", 2);
        antMother.roomLevel = PlayerPrefs.GetInt("antMother.roomLevel", 1);

        expirience.exp = PlayerPrefs.GetInt("expirience.exp", 0);
        expirience.lvl = PlayerPrefs.GetInt("expirience.lvl", 1);

        playerAttack.attackPoint = PlayerPrefs.GetInt("playerAttack.attackPoint", 3);
        healthPlayer.fullHealth = PlayerPrefs.GetFloat("healthPlayer.fullHealth", 3);

        enemyHealth.expAward = PlayerPrefs.GetInt("enemyHealth.expAward");
        enemyHealth.fullHealth = PlayerPrefs.GetFloat("enemyHealth.fullHealth", 10);

    }


    private void OnApplicationPause(bool pause)
    {
        if (pause) SaveData();

    }

    private void OnApplicationQuit()
    {
        SaveData();
    }


    private void SaveData()
    {
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetInt("appleStorage.currentAmount", appleStorage.currentAmount);
        PlayerPrefs.SetInt("appleStorage.roomLevel", appleStorage.roomLevel);
        PlayerPrefs.SetInt("appleStorage.roomCapacity", appleStorage.roomCapacity);

        PlayerPrefs.SetInt("seedStorage.currentAmount", seedStorage.currentAmount);
        PlayerPrefs.SetInt("seedStorage.roomLevel", seedStorage.roomLevel);
        PlayerPrefs.SetInt("seedStorage.roomCapacity", seedStorage.roomCapacity);

        PlayerPrefs.SetInt("mushroomStorage.currentAmount", mushroomStorage.currentAmount);
        PlayerPrefs.SetInt("mushroomStorage.roomLevel", mushroomStorage.roomLevel);
        PlayerPrefs.SetInt("mushroomStorage.roomCapacity", mushroomStorage.roomCapacity);

        PlayerPrefs.SetInt("processingRoom.roomLvl", processingRoom.roomLvl);

        PlayerPrefs.SetInt("antMother.currentAmount", antMother.currentAmount);
        PlayerPrefs.SetInt("antMother.roomCapacity", antMother.roomCapacity);
        PlayerPrefs.SetFloat("antMother.speedAntAI", antMother.speedAntAI);
        PlayerPrefs.SetInt("antMother.roomLevel", antMother.roomLevel);

        PlayerPrefs.SetInt("expirience.exp", expirience.exp);
        PlayerPrefs.SetInt("expirience.lvl", expirience.lvl);

        PlayerPrefs.SetInt("playerAttack.attackPoint", playerAttack.attackPoint);
        PlayerPrefs.SetFloat("healthPlayer.fullHealth", healthPlayer.fullHealth);

        PlayerPrefs.SetInt("enemyHealth.expAward", enemyHealth.expAward);
        PlayerPrefs.SetFloat("enemyHealth.fullHealth", enemyHealth.fullHealth);
        PlayerPrefs.Save();
    }


    // Update is called once per frame
    void Update()
    {
        moneyText.text = money.ToString();

        appleText.text = appleStorage.currentAmount.ToString() + "/" + appleStorage.roomCapacity.ToString();
        appleCurrentLevelText.text = appleStorage.roomLevel.ToString();
        appleCostText.text = (appleStorage.roomCapacity + 10).ToString();

        mushroomText.text = mushroomStorage.currentAmount.ToString() + "/" + mushroomStorage.roomCapacity.ToString();
        mushroomCurrentLevelText.text = mushroomStorage.roomLevel.ToString();
        mushroomCostText.text = (mushroomStorage.roomCapacity + 15).ToString();

        workersText.text = antMother.currentAmount.ToString() + "/" + antMother.roomCapacity.ToString();
        capacityOfWorkersText.text = antMother.roomLevel.ToString();
        capacityOfWorkersCostText.text = (antMother.roomLevel * 10).ToString();

        SeedText.text = seedStorage.currentAmount.ToString() + "/" + seedStorage.roomCapacity.ToString();
        seedCurrentLevelText.text = seedStorage.roomLevel.ToString();
        seedCostText.text = (seedStorage.roomCapacity + 12).ToString();


        currentSpeedAIText.text = antMother.speedAntAI.ToString();
        currentSpeedAICostText.text = (antMother.speedAntAI * 10).ToString();

        currentSpeedPlayerText.text = mover.speed.ToString();
        currentSpeedPlayerCostText.text = (mover.speed * 10).ToString();

        currentHealthPlayerText.text = healthPlayer.fullHealth.ToString();
        currentHealthlayerCostText.text = ((int)(healthPlayer.fullHealth * healthPlayer.fullHealth * (1 + Mathf.Log(healthPlayer.fullHealth, 5f)))).ToString(); //ecsperimenty eba
        //Debug.Log((int)(healthPlayer.fullHealth * healthPlayer.fullHealth * (1 + Mathf.Log(healthPlayer.fullHealth, 5f))));

        currentDamagePlayerText.text = playerAttack.attackPoint.ToString();
        currentDamagePlayerCostText.text = ((int)(playerAttack.attackPoint * playerAttack.attackPoint * (1 + Mathf.Log(playerAttack.attackPoint, 5f)))).ToString();

        if (processingRoom.roomLvl == 0)
        {
            processingCurrentLevelText.text = processingRoom.roomLvl.ToString();
            processingCostText.text = 12.ToString();
        }
        else
        {
            processingCurrentLevelText.text = "MAX";
            processingCostText.text = "MAX";
            var buttonProcessingUp = GameObject.FindGameObjectWithTag("ProcessingByeButton").GetComponent<Button>();
            if (buttonProcessingUp != null)
                buttonProcessingUp.interactable = false;
        }
    }

    public void UpgradeAppleLevel()
    {
        if ((money - (appleStorage.roomCapacity + 10)) >= 0)
        {
            money -= appleStorage.roomCapacity + 10;
            appleStorage.LevelUp();
            expirience.exp += appleStorage.roomLevel;
        }
    }

    public void UpgradeSeedLevel()
    {
        if ((money - (seedStorage.roomCapacity + 12)) >= 0)
        {
            money -= seedStorage.roomCapacity + 12;
            seedStorage.LevelUp();
            expirience.exp += seedStorage.roomLevel;
        }
    }

    public void UpgradeProcessingLevel()
    {
        if ((money - 12) >= 0 && processingRoom.roomLvl == 0)
        {
            money -= 12;
            processingRoom.roomLvl = 1;
            processingRoom.gameObject.SetActive(true);
            expirience.exp += processingRoom.roomLvl;
        }
    }

    public void UpgradeMushroomLevel()
    {
        if ((money - (mushroomStorage.roomCapacity + 15)) >= 0)
        {
            money -= mushroomStorage.roomCapacity + 15;
            mushroomStorage.LevelUp();
            expirience.exp += mushroomStorage.roomLevel;
        }
    }

    public void UpgradeCapacityOfWorkers()
    {
        if ((money - antMother.roomLevel * 10) >= 0)
        {
            money -= antMother.roomLevel * 10;
            antMother.roomCapacity++;
            antMother.roomLevel++;
            expirience.exp += antMother.roomLevel;
        }
    }

    public void UpgradeWorkersSpeed()
    {
        if ((money - antMother.speedAntAI * 10) >= 0)
        {
            money -= (int)antMother.speedAntAI * 10;
            antMother.speedAntAI += 0.5f;
            expirience.exp += 1;
        }
    }

    public void UpgradePlayerSpeed()
    {
        if ((money - mover.speed * 10) >= 0)
        {
            money -= (int)mover.speed * 10;
            mover.speed += 0.5f;
            expirience.exp += 1;
        }
    }

    public void UpgradePlayerHealth()
    {
        if ((money - healthPlayer.fullHealth + Mathf.Log(healthPlayer.fullHealth, 6f)) >= 0)
        {
            money -= (int)(healthPlayer.fullHealth + Mathf.Log(healthPlayer.fullHealth, 6f));
            healthPlayer.fullHealth += 1;
            expirience.exp += 1;
        }
    }

    public void UpgradePlayerDamage()
    {
        if (money - (int)(playerAttack.attackPoint * playerAttack.attackPoint * (1 + Mathf.Log(playerAttack.attackPoint, 5f))) >= 0)
        {
            money -= (int)(playerAttack.attackPoint * playerAttack.attackPoint * (1 + Mathf.Log(playerAttack.attackPoint, 5f)));
            playerAttack.attackPoint += 1;
            expirience.exp += 1;
        }
    }


}
