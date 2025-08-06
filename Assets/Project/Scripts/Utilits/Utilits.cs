using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Utilits
{
    public static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }

    public static T GetEnum<T>(int num)
    {
        return (T)Enum.GetValues(typeof(T)).GetValue(num);
    }

    public static bool TryGetComponentInParent<TComponent>(Collider2D collider, out TComponent contract)
    {
        contract = collider.GetComponentInParent<TComponent>(true);
        return contract != null;
    }

    public static string StringToCount(Int64 count, string[] words)
    {
        if (words.Length == 0)
        {
            words = new string[] { "кредит", "кредита", "кредитов" };
        }

        Int64 n = Math.Abs(count) % 100;
        if ((n % 10 == 0) || (n % 10 >= 5 && n % 10 <= 9) || (n > 9 && n < 20)) return words[2];
        if (n % 10 == 1) return words[0];

        return words[1];
    }

    static public IEnumerator WaitForSeconds(float forSecond, UnityAction callback)
    {
        yield return new WaitForSeconds(forSecond);
        callback();
    }

    static public IEnumerator WaitForSecondsWithProgress(float forSecond, Action callback, Action<float> callbackTimer)
    {
        float elapsedTime = 0f;
        int updateRate = 4;
        float lastUpdateTime = 0;

        while (elapsedTime < forSecond)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / forSecond);

            if (Time.time - lastUpdateTime >= 1f / updateRate)
            {
                callbackTimer(progress);
                lastUpdateTime = Time.time;
            }

            yield return null;
        }

        callback();
    }
}
