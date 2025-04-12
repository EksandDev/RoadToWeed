namespace _Project.Scripts.Fight
{
    public interface IDamageable
    {
        public float MaxHealth { get; }
        public float Health { get; }
        
        public void TakeDamage(float value);
    }
}