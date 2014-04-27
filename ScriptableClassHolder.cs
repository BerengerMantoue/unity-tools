/**************************************************************************************
 * Auteur : Bérenger Mantoue
 * Date : 09/03/2014
 * Version : 2.0
 * Descritption : Le but de ce package est de montrer comment sauvegarder / charger
 * des instances d'objets en utilisant la classe ScriptableObject.
 * ************************************************************************************/

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// The only purpose of that class is to save and load the list as an asset. This is the
/// object that will be saved.
/// </summary>
public class ScriptableClassHolder : ScriptableObject
{
	public List< ClassA > list;
    public ScriptableClassHolder(List< ClassA > L)
    {
        list = L;
    }
}

/// <summary>
/// First type of object contained by ClassHolder
/// </summary>
public class ClassA
{
    public ClassA() { }

    public virtual void Draw()
    {
        GUILayout.Label("AAAAAAAA");
    }
}

/// <summary>
/// Second type of object contained by ClassHolder, inheriting from the first
/// </summary>
public class ClassB : ClassA
{
    public ClassB() : base() { }

    public override void Draw()
    {
        GUILayout.Label("BBBBBBBB");
    }
}