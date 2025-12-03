public interface IStatusEffectReceiver
{
    void ApplySlow(float slowPercent, float duration);
    void ApplyPoison(float dps, float duration);
}
