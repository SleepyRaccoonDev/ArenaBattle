using System;

public interface IKillable
{
    event Action<Character> IsKilled;
}