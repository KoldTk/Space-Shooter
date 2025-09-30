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
public class StraightConfig
{
    public int bulletCount;
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
public enum PatternType
{
    Radial,
    Spiral,
    Wave,
    Straight,
}
public enum ShootType
{
    Continuous,
    Once,
}
