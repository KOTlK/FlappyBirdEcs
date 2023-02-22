using Leopotam.EcsLite;

namespace Game.Runtime.Components.Physics
{
    public struct TriggerCollision
    {
        public int FirstEntity { get; set; }
        public int SecondEntity { get; set; }
        public bool ExecutedThisFrame { get; set; }

        public bool Has(int entity)
        {
            return FirstEntity == entity || SecondEntity == entity;
        }

        public bool IsCollisionBetween<TFirst, TSecond>(EcsPool<TFirst> firstPool, EcsPool<TSecond> secondPool)
        where TFirst : struct
        where TSecond : struct
        {
            return (firstPool.Has(FirstEntity) || firstPool.Has(SecondEntity)) &&
                   (secondPool.Has(FirstEntity) || secondPool.Has(SecondEntity));
        }
        
        public bool IsCollisionBetween<TFirst, TSecond>(EcsPool<TFirst> firstPool, EcsPool<TSecond> secondPool, out int first, out int second)
            where TFirst : struct
            where TSecond : struct
        {
            if (firstPool.Has(FirstEntity) && secondPool.Has(SecondEntity))
            {
                first = FirstEntity;
                second = SecondEntity;
                return true;
            }

            if (firstPool.Has(SecondEntity) && secondPool.Has(FirstEntity))
            {
                first = SecondEntity;
                second = FirstEntity;
                return true;
            }

            first = -1;
            second = -1;
            return false;
        }
    }
}