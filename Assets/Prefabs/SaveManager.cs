using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class SaveManager : MonoBehaviour
{

    public static void SavePlayer(PlayerScript Player)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/JumpyJump.sav", FileMode.Create);

        PlayerData data = new PlayerData(Player);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static bool[] LoadPlayerBools()
    {
        if (File.Exists(Application.persistentDataPath + "/JumpyJump.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/JumpyJump.sav", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(stream);
            stream.Close();
            return data.BoolsToSave;

        }
        return null;
    }
    public static bool[] LoadPlayerTrails()
    {
        if (File.Exists(Application.persistentDataPath + "/JumpyJump.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/JumpyJump.sav", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(stream);
            stream.Close();
            return data.TrailsToSave;

        }
        return null;
    }
    public static bool[] LoadPlayerChars()
    {
        if (File.Exists(Application.persistentDataPath + "/JumpyJump.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/JumpyJump.sav", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(stream);
            stream.Close();
            return data.CharsToSave;

        }
        return null;
    }

    public static int[] LoadPlayerInts()
    {
        if (File.Exists(Application.persistentDataPath + "/JumpyJump.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/JumpyJump.sav", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(stream);
            stream.Close();
            return data.IntslsToSave;

        }
        return null;
    }

    public static float[] LoadPlayerFloats()
    {
        if (File.Exists(Application.persistentDataPath + "/JumpyJump.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/JumpyJump.sav", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(stream);
            stream.Close();
            return data.FloatslsToSave;

        }
        return null;
    }


    [Serializable]
    public class PlayerData
    {
        //public int HowManyStages = 60;

        public bool[] BoolsToSave;
        public bool[] TrailsToSave;
        public bool[] CharsToSave;
        public int[] IntslsToSave;
        public float[] FloatslsToSave;

        public PlayerData(PlayerScript Player)
        {
            FloatslsToSave = new float[50];
            IntslsToSave = new int[50];
            BoolsToSave = new bool[100]; // For Achivments, dont change length unless u add bools
            TrailsToSave = new bool[50];
            CharsToSave = new bool[100];

            IntslsToSave[0] = PlayerScript.BestScore;
            IntslsToSave[1] = PlayerScript.TotalTrails;
            IntslsToSave[2] = PlayerScript.TotalCharacters;
            IntslsToSave[3] = Player.ShieldsCount;
            IntslsToSave[4] = Player.WingsCount;
            IntslsToSave[5] = Player.TotalMoney;
            IntslsToSave[6] = Player.FireCount;
            IntslsToSave[7] = Player.GameManagerObject.CurrentChar;
            IntslsToSave[8] = Player.CurrentTrail;
            IntslsToSave[9] = Player.ShopAlert;
            IntslsToSave[10] = PlayerScript.ConfettiCounter;
            IntslsToSave[11] = Player.AlreadyRated;
            IntslsToSave[12] = Player.GameManager.ChooseChallenge;
            IntslsToSave[13] = PlayerScript.ChallengesComplete;
            IntslsToSave[14] = Player.FirstTimePlaying;
            IntslsToSave[15] = PlayerScript.BirdsCounter;


            FloatslsToSave[0] = AudioManager.CurrentVol;
            FloatslsToSave[1] = AudioManager.BackgroundVolume;
            FloatslsToSave[2] = AudioManager.EffectsVolume;

            for (int i = 0; i < PlayerScript.Rewarded.Length; i++)
            {
                BoolsToSave[i] = PlayerScript.Rewarded[i];

            }
            for (int i = 0; i < Player.TrailSaved.Length; i++)
            {
                TrailsToSave[i] = Player.TrailSaved[i];
            }

            for (int i = 0; i < Player.CharSaved.Length; i++)
            {
                CharsToSave[i] = Player.CharSaved[i];
            }


        }
    }
}

