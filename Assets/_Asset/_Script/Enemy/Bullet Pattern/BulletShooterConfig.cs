using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooterConfig
{

}
[System.Serializable]
public class RadialPatternConfig
{
    public int bulletCount;
}
[System.Serializable]
public class StraightPatternConfig
{
    public int bulletCount;
    public float offset;
}
[System.Serializable]
public class SpiralPatternConfig
{
    public float spiralAngle;
    public float spiralSpeed;
    public bool clockwise;
}
[System.Serializable]
public class WavePatternConfig
{
    public int bulletCount;
    public float spreadAngle;
}
[System.Serializable]
public class RandomPatternConfig
{
    public float spreadAngle;
    public int bulletCount;
}
public enum PatternType
{
    Radial,
    Spiral,
    Wave,
    Straight,
    Random,
}
public enum ShootType
{
    Continuous,
    Once,
}
public enum BulletType
{
    Normal,
    Target,
    Delay,
    Wave,
}
