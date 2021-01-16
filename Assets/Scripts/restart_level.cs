using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart_level : MonoBehaviour
{
    public void restart_game() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
