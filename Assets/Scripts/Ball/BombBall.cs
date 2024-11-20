using UnityEngine;

public class BombBall : Ball
{
    [Header("폭발 반경")]
    [SerializeField] private float explosionRadius = 5f;

    [Header("폭발력")]
    [SerializeField] private float explosionPower = 10f;

    protected override void Update()
    {
        base.Update();

        Bomb(transform.position);
    }

    void Bomb(Vector2 explosinoPosition)
    {
        if (!DetectSkill()) return;

        canSkill = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosinoPosition, explosionRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject == this.gameObject)
            {
                collider.isTrigger = true;
                continue;   //본인은 영향을 받지 않음
            }

            Rigidbody2D rigid = collider.GetComponent<Rigidbody2D>();

            if (rigid != null)
            {
                Vector2 direction = rigid.position - explosinoPosition; //폭발 중심에서의 방향

                float distance = direction.magnitude;           //폭발 중심에서의 벡터 스칼라
                float force = explosionPower / (distance + 1); //거리에 따른 폭발력 감소
                rigid.AddForce(direction.normalized * force, ForceMode2D.Impulse);

                //직접 충돌이 아닌 폭발로 인해 쓰러지는 핀 Fall Down 처리

                Pin pin = collider.GetComponent<Pin>();

                if (pin != null)
                {
                    pin.FallDown();
                }
            }
        }

        HideBall();
    }
}
