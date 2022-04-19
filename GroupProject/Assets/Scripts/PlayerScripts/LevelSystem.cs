using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    private int level, experience, expToNextLevel;

    public LevelSystem()
    {
        level = 0;
        experience = 0;
        expToNextLevel = 100;
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        if (experience >= expToNextLevel) // Enough Exp
        {
            level++;
            experience -= expToNextLevel;
        }
    }

    public int GetLevel()
    {
        return level;
    }
}
