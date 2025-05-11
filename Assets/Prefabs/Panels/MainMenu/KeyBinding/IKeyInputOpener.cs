using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKeyInputOpener
{
    public void OpenKeyInputPanel();
    public void SetInputValue(KeyCode keyCode);
}
