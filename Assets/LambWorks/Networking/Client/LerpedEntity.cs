using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LambWorks.Networking.Client {

    [AddComponentMenu("LambWorks/Networking/Client/Interpolated Entity")]
    public class LerpedEntity : Entity {

        public float lerpSpeed = 5;
        private Vector3 dPos = Vector3.zero;
        private Quaternion dRot = Quaternion.identity;
        private Vector3 dSca = Vector3.zero;

        public override void UpdateEntity(Vector3 position, Quaternion rotation, Vector3 scale) {
            dPos = position;
            dRot = rotation;
            dSca = scale;
        }

        private void Update() {
            transform.position = Vector3.Lerp(transform.position, dPos, Time.deltaTime * lerpSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, dRot, Time.deltaTime * lerpSpeed);
            transform.localScale = Vector3.Lerp(transform.localScale, dSca, Time.deltaTime * lerpSpeed);
        }
    }

}