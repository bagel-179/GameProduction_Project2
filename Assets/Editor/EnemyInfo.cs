using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InvisEnemy))]
public class EnemyInfo : Editor
{
    private void OnSceneGUI()
    {
        InvisEnemy enemy = (InvisEnemy)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(enemy.transform.position, Vector3.up, Vector3.forward, 360, enemy.safezone);
    }
}
