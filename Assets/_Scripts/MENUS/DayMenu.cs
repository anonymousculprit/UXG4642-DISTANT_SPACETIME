using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayMenu : MonoBehaviour
{
    public void JumpToDay(int dayNum) => ScheduleDayJump(dayNum);

    void ScheduleDayJump(int dayNum)
    {
        GameManager.instance.JumpToDay(dayNum);
        MenuInterfacer.instance.TurnMenuOff();
    }
}
