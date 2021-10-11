using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;
using DG.Tweening;
using CI.QuickSave;

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
        if (QuickSaveReader.Create("Bank") == null) return;
        var reader = QuickSaveReader.Create("Bank");
        money = reader.Read<int>("money");
        appleStorage.currentAmount = reader.Read<int>("appleStorage.currentAmount");
        seedStorage.currentAmount = reader.Read<int>("seedStorage.currentAmount");
        mushroomStorage.currentAmount = reader.Read<int>("mushroomStorage.currentAmount");
        processingRoom.roomLvl = reader.Read<int>("processingRoom.roomLvl");
        antMother.currentAmount = reader.Read<int>("antMother.currentAmount");

        antMother.roomCapacity = reader.Read<int>("antMother.roomCapacity");
        antMother.speedAntAI = reader.Read<int>("antMother.speedAntAI");
        antMother.roomLevel = reader.Read<int>("antMother.roomLevel");
        expirience.exp = reader.Read<int>("expirience.exp");
        expirience.lvl = reader.Read<int>("expirience.lvl");
        //healthPlayer.fullHealth = reader.Read<int>("healthPlayer.fullHealth");
        //playerAttack.attackPoint = reader.Read<int>("playerAttack.attackPoint");
        //enemyHealth.expAward = reader.Read<int>("enemyHealth.expAward");
        //enemyHealth.fullHealth = reader.Read<float>("enemyHealth.fullHealth");

    }


    private void OnApplicationPause(bool pause)
    {
        var writer = QuickSaveWriter.Create("Bank");
        writer.Write("money", money);

        writer.Write("appleStorage.currentAmount", appleStorage.currentAmount);
        writer.Write("appleStorage.roomLevel", appleStorage.roomLevel);
        writer.Write("appleStorage.roomCapacity", appleStorage.roomCapacity);

        writer.Write("seedStorage.currentAmount", seedStorage.currentAmount);
        writer.Write("seedStorage.roomLevel", seedStorage.roomLevel);
        writer.Write("seedStorage.roomCapacity", seedStorage.roomCapacity);

        writer.Write("mushroomStorage.currentAmount", mushroomStorage.currentAmount);
        writer.Write("mushroomStorage.roomLevel", mushroomStorage.roomLevel);
        writer.Write("mushroomStorage.roomCapacity", mushroomStorage.roomCapacity);

        writer.Write("processingRoom.roomLvl", processingRoom.roomLvl);

        writer.Write("antMother.currentAmount", antMother.currentAmount);
        writer.Write("antMother.roomCapacity", antMother.roomCapacity);
        writer.Write("antMother.speedAntAI", antMother.speedAntAI);
        writer.Write("antMother.roomLevel", antMother.roomLevel);

        writer.Write("expirience.exp", expirience.exp);
        writer.Write("expirience.lvl", expirience.lvl);

        writer.Write("playerAttack.attackPoint", playerAttack.attackPoint);
        writer.Write("expirience.lvl", healthPlayer.fullHealth);

        writer.Write("enemyHealth.expAward", enemyHealth.expAward);
        writer.Write("enemyHealth.fullHealth", enemyHealth.fullHealth);
        writer.Commit();
    }

    private void OnApplicationQuit()
    {
        var writer = QuickSaveWriter.Create("Bank");
        writer.Write("money", money);

        writer.Write("appleStorage.currentAmount", appleStorage.currentAmount);
        writer.Write("appleStorage.roomLevel", appleStorage.roomLevel);
        writer.Write("appleStorage.roomCapacity", appleStorage.roomCapacity);

        writer.Write("seedStorage.currentAmount", seedStorage.currentAmount);
        writer.Write("seedStorage.roomLevel", seedStorage.roomLevel);
        writer.Write("seedStorage.roomCapacity", seedStorage.roomCapacity);

        writer.Write("mushroomStorage.currentAmount", mushroomStorage.currentAmount);
        writer.Write("mushroomStorage.roomLevel", mushroomStorage.roomLevel);
        writer.Write("mushroomStorage.roomCapacity", mushroomStorage.roomCapacity);

        writer.Write("processingRoom.roomLvl", processingRoom.roomLvl);

        writer.Write("antMother.currentAmount", antMother.currentAmount);
        writer.Write("antMother.roomCapacity", antMother.roomCapacity);
        writer.Write("antMother.speedAntAI", antMother.speedAntAI);
        writer.Write("antMother.roomLevel", antMother.roomLevel);

        writer.Write("expirience.exp", expirience.exp);
        writer.Write("expirience.lvl", expirience.lvl);

        writer.Write("playerAttack.attackPoint", playerAttack.attackPoint);
        writer.Write("expirience.lvl", healthPlayer.fullHealth);
        writer.Commit();
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
        currentHealthlayerCostText.text = ((int)(healthPlayer.fullHealth * healthPlayer.fullHealth * (1 + Mathf.Log(healthPlayer.fullHealth, 6f)))).ToString(); //ecsperimenty eba

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
            if(buttonProcessingUp != null)
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
