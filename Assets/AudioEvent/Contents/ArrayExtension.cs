using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayExtension {
    public static void Shuffle<T> (T[] array)
    {
		for (var i = array.Length - 1; i > 0; i--) {
			var r = Random.Range(0,i);
			var tmp = array[i];
			array[i] = array[r];
			array[r] = tmp;
		}
    }
}
