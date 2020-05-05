using UnityEngine;

public static class Keys
{
    public static bool AnyKey() {
        if (Input.GetKey(KeyCode.A) ^ Input.GetKey(KeyCode.S) ^ Input.GetKey(KeyCode.D) ^ Input.GetKey(KeyCode.W))
            return true;

        return false;
    }

    public static bool DontMove() {
        if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
            return true;

        return false;
    }


    public static bool DontMoveKeyWithMouse() {
        if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            return true;

        return false;
    }

}
