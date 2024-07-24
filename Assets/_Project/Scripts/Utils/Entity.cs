using FunnyBlox.Utils;
using UnityEngine;

namespace FunnyBlox
{
    public abstract class Entity : MonoBehaviour
    {

        //public Settings settings;

        [HideInInspector]
        public Transform mTransform;

        public string poolName;

        [HideInInspector]
        public Pool pool;

        [SerializeField]
        private bool _useCollisions;

        [HideInInspector]
        public bool IsActive;

        [HideInInspector]
        public Vector3 InitializePosition;

        [HideInInspector]
        public Quaternion InitializeRotation;

        public virtual void Initialize()
        {
            pool = PoolCollection.GetPool(poolName);
            mTransform = transform;
            InitializePosition = mTransform.position;
            InitializeRotation = mTransform.rotation;
        }

        public virtual void Spawn()
        {
            if (!mTransform) Initialize();

            Spawn(InitializePosition);
        }

        public virtual void Spawn(Vector3 position)
        {
            if (!mTransform) Initialize();

            mTransform.position = position;
            mTransform.rotation = InitializeRotation;

            IsActive = true;
        }

        public virtual void Despawn()
        {
            if (!mTransform) Initialize();

            IsActive = false;

            if (pool != null)
                pool.FreeElement(mTransform, true);
        }

        #region COLLIDERS

        [HideInInspector]
        public Vector3 CollisionPoint;
        [HideInInspector]
        public Collision CollisionSender;

        private void OnCollisionEnter(Collision sender)
        {
            if (!_useCollisions) return;
            CollisionSender = sender;
            CollisionPoint = sender.GetContact(0).point;
            OnColliderDetection(sender.collider, false, true);
        }



        private void OnTriggerEnter(Collider sender)
        {
            if (!_useCollisions) return;
            OnColliderDetection(sender, true, true);
        }

        //private void OnTriggerStay(Collider sender)
        //{
        //    if(!_useCollisions) return;
        //    OnColliderDetection(sender, true, true);
        //}

        //private void OnTriggerExit(Collider sender)
        //{
        //    if(!_useCollisions) return;
        //    OnColliderDetection(sender, true, false);
        //}

        public abstract void OnColliderDetection(Collider sender, bool isTrigger, bool inSender);

        #endregion
    }

    
}



/* copy into children class
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Spawn()
        {
            base.Spawn();
        }

        public override void Spawn(Vector3 position)
        {
            base.Spawn(position);
        }

        public override void Despawn()
        {
            base.Despawn();
        }

        public override void OnColliderDetection(Collider sender, bool isTrigger, bool inSender)
        {

        }
 */