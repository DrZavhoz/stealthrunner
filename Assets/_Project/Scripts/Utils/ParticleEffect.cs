using System.Collections;
using FunnyBlox.Utils;
using UnityEngine;

namespace FunnyBlox
{
    public class ParticleEffect : MonoBehaviour
    {

        private Transform _transform;
        private ParticleSystem[] _particleSystem;
        private ParticleSystem.MainModule[] _particleSysteMain;
        private Material[] _particlesMaterial;

        private float _delayDespawn;
        private Transform _parent;

        private Quaternion _defaultRotation = Quaternion.identity;

        [SerializeField]
        private bool _autoDespawn = true;

        [SerializeField]
        private string _poolName;
        private Pool _particlesPool;

        private void Initialize()
        {
            if (_poolName != "")
                _particlesPool = PoolCollection.GetPool(_poolName);

            _transform = transform;
            _particleSystem = _transform.GetComponentsInChildren<ParticleSystem>();
            _particleSysteMain = new ParticleSystem.MainModule[_particleSystem.Length];
            _particlesMaterial = new Material[_particleSystem.Length];
            _delayDespawn = 0f;

            for (int i = 0; i < _particleSystem.Length; i++)
            {

                if (_delayDespawn < _particleSystem[i].main.startLifetime.constantMax + _particleSystem[i].main.duration)
                {
                    _delayDespawn = _particleSystem[i].main.startLifetime.constantMax + _particleSystem[i].main.duration;
                }

                _particleSysteMain[i] = _particleSystem[i].main;

                _particlesMaterial[i] = _particleSystem[i].GetComponent<Renderer>().material;
            }
        }

        public void Spawn(Vector3 position, Quaternion rotation)
        {
            if (_transform == null) Initialize();

            _transform.position = position;

            _transform.rotation = rotation;


            foreach (ParticleSystem ps in _particleSystem)
            {
                ps.Clear(true);
                ps.Play(true);
            }


            if (_autoDespawn)
            {
                if (!gameObject.activeSelf) return;
                StartCoroutine(DespawnCoroutine(_delayDespawn));
            }
        }

        public void Spawn(Vector3 position, Quaternion rotation, Color color)
        {
            if (_transform == null) Initialize();

            _transform.position = position;
            _transform.rotation = rotation;

            if (color != Color.clear)
            {
                for (int i = 0; i < _particleSysteMain.Length; i++)
                {
                    _particleSysteMain[i].startColor = color;
                }

                foreach (Material material in _particlesMaterial)
                {
                    material.color = color;
                }
            }
            foreach (ParticleSystem ps in _particleSystem)
            {
                ps.Clear(true);
                ps.Play(true);
            }

            if (_autoDespawn)
            {
                if (!gameObject.activeSelf) return;
                StartCoroutine(DespawnCoroutine(_delayDespawn));
            }
            else
            {
                if (!gameObject.activeSelf) return;
            }
        }

        public void Spawn(Vector3 position, Quaternion rotation, int type)
        {
            if (_transform == null) Initialize();

            _transform.position = position;
            _transform.rotation = rotation;

            for (int i = 0; i < _transform.childCount; i++)
            {
                if (i == type)
                    _transform.GetChild(i).gameObject.SetActive(true);
                else
                    _transform.GetChild(i).gameObject.SetActive(false);
            }

            foreach (ParticleSystem ps in _particleSystem)
            {
                ps.Clear(true);
                ps.Play(true);
            }


            if (_autoDespawn)
            {
                if (!gameObject.activeSelf) return;
                StartCoroutine(DespawnCoroutine(_delayDespawn));
            }
        }

        public void PlayEffect()
        {
            if (_transform == null) Initialize();

            foreach (ParticleSystem ps in _particleSystem)
            {
                ps.Clear(true);
                ps.Play(true);
            }

            if (_autoDespawn)
                StartCoroutine(DespawnCoroutine(_delayDespawn));//_particleSysteMain.startLifetime.constantMax
            else
            {
                if (!gameObject.activeSelf) return;
            }
        }

        private IEnumerator DespawnCoroutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            Despawn();
        }

        public void Despawn()
        {
            _particlesPool.FreeElement(_transform, true);
        }

        public void StopAndDespawn()
        {
            if (!gameObject.activeSelf) return;
            StartCoroutine(StopAndDespawnCoroutine());
        }

        private IEnumerator StopAndDespawnCoroutine()
        {
            _transform.parent = _parent;
            _transform.localScale = Vector3.one;
            foreach (ParticleSystem ps in _particleSystem)
            {
                ps.Stop(true);
            }
            yield return new WaitForSeconds(_delayDespawn);
            Despawn();
        }

        public void CleanAndDespawn()
        {
            foreach (ParticleSystem ps in _particleSystem)
            {
                ps.Stop(true);
                ps.Clear(true);
            }
            Despawn();
        }
    }
}
