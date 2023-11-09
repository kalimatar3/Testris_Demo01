using UnityEngine;

public class ClassicGameMode : MyBehaviour, IGameMode
{
    public bool CanStart()
    {
        if(GameModeManager.Instance.GameModeController.GetCurrentGameMode().Equals(this)) {
            return true;
        }
        return false;
    }

    bool IGameMode.CanLose()
    {
       foreach (Transform element in BoardManager.Instance.Board.GetXZ_2D_Array(21)) {
        if(element != null && element.gameObject.activeInHierarchy) {
            return true;
        }
       }
       return false;
    }

    bool IGameMode.CanWin()
    {
        return false;
    }
}
