using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Card", menuName ="Card")]
public class Card : ScriptableObject
{
    //описание значений
    //название карточки с настройками
    public new string name;

    //сила выстрела
    [Range(100,300)]
    public int shot_multiplyer;

    //скорость игрока
    public float player_speed;

    //скорость стрельбы
    public int shoot_speed;

}
