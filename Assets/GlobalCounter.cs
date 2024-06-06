// File: Assets/Scripts/GlobalCounter.cs

using UnityEngine;

public class GlobalCounter : MonoBehaviour
{
    private static int counter = 1;

    // Function to get the current counter value and increment it
    public static int GetCounter()
    {
        int currentCounter = counter;
        counter++;
        return currentCounter;
    }
}
