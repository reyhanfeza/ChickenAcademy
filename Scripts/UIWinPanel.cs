using System.Collections;
using System.Collections.Generic;
using Base.Runtime.Manager;
using Base.Runtime.UI;
using UnityEngine;

public class UIWinPanel : BaseUIPanel
{
    public override void EnablePanel()
    {
        base.EnablePanel();
        TouchManager.Instance.ChangeJoystickState(false);
    }
}
