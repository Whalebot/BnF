using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class GameDataManager : MonoBehaviour
{
    public SaveData saveData1;
    public SaveData saveData2;
    public SaveData saveData3;

    public bool boss1Killed;
    public bool boss2Killed;
    public bool boss3Killed;

    public static bool sword1Unlocked;
    public static bool sword2Unlocked;
    public static bool sword3Unlocked;
    public static bool sword4Unlocked;
    public static bool sword5Unlocked;
    public static bool sword6Unlocked;
    public static bool sword7Unlocked;
    public static bool sword8Unlocked;
    public static bool sword9Unlocked;
    public static bool sword10Unlocked;
    public static bool sword11Unlocked;
    public static bool sword12Unlocked;

    public static int weaponSlot1 = 1;
    public static int weaponSlot2 = 2;
    public static int weaponSlot3 = 3;

    public static int killedEnemies = 0;

    // Use this for initialization
    void Start()
    {
        saveData1 = new SaveData();
        saveData2 = new SaveData();
        saveData3 = new SaveData();

        if (File.Exists(Application.persistentDataPath + "/saveData1.json") == true)
        {
            LoadSettings();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1")) BossKilled(1);
        if (Input.GetKeyDown("2")) BossKilled(2);
        if (Input.GetKeyDown("3")) BossKilled(3);

        if (Input.GetKeyDown("4")) print("sword1Unlocked");
    }

    public void BossKilled(int bossNumber)
    {
        if (bossNumber == 1)
        {
            boss1Killed = true;
            saveData1.beatBoss1 = boss1Killed;
        }
        if (bossNumber == 2)
        {
            boss2Killed = true;
            saveData1.beatBoss2 = boss2Killed;
        }

        if (bossNumber == 3)
        {
            boss1Killed = true;
            saveData1.beatBoss3 = boss3Killed;
        }

        SaveSettings();


    }

    public void SaveSettings()
    {
        saveData1.sword1 = sword1Unlocked;
        saveData1.sword2 = sword2Unlocked;
        saveData1.sword3 = sword3Unlocked;
        saveData1.sword4 = sword4Unlocked;
        saveData1.sword5 = sword5Unlocked;
        saveData1.sword6 = sword6Unlocked;
        saveData1.sword7 = sword7Unlocked;
        saveData1.sword8 = sword8Unlocked;
        saveData1.sword9 = sword9Unlocked;
        saveData1.sword10 = sword10Unlocked;
        saveData1.sword11 = sword11Unlocked;
        saveData1.sword12 = sword12Unlocked;

        string jsonData = JsonUtility.ToJson(saveData1, true);
        File.WriteAllText(Application.persistentDataPath + "/saveData1.json", jsonData);
    }

    public void LoadSettings()
    {
        saveData1 = JsonUtility.FromJson<SaveData>(File.ReadAllText(Application.persistentDataPath + "/saveData1.json"));
        boss1Killed = saveData1.beatBoss1;
        boss2Killed = saveData1.beatBoss2;
        boss3Killed = saveData1.beatBoss3;

        sword1Unlocked = saveData1.sword1;
        sword2Unlocked = saveData1.sword2;
        sword3Unlocked = saveData1.sword3;
        sword4Unlocked = saveData1.sword4;
        sword5Unlocked = saveData1.sword5;
        sword6Unlocked = saveData1.sword6;
        sword7Unlocked = saveData1.sword7;
        sword8Unlocked = saveData1.sword8;
        sword9Unlocked = saveData1.sword9;
        sword10Unlocked = saveData1.sword10;
        sword11Unlocked = saveData1.sword11;
        sword12Unlocked = saveData1.sword12;
    }
}
