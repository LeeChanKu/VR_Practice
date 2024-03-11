using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{

    public Transform m_FireTransform; // 총구의 위치

    public Text m_AmmoText; //탄환 갯수 UI텍스트

    public LineRenderer m_BulletLineRenderer; //총알 궤적

    public int m_MaxAmmo; // 최대 탄약 수

    public float m_Damage = 25;
    public float m_Delay = 0.2f; // 발사 속도
    public float m_Reload; // 재장전
    public float m_FireDistance = 100f;// 사정거리

    private enum State {Ready,Empty,Reloading }

    State m_CurrentState = State.Empty; //현재 총의 상태

    private float m_LastFireTime; // 마지막 격발 시점
    private int m_CurrentAmmo; //탐은 탄약 갯수
    // Start is called before the first frame update
    void Start()
    {
        m_CurrentState = State.Empty;//탄약이 없는상태로 시작 
        m_LastFireTime = 0; // 마지막 격발 초기화

        m_BulletLineRenderer.positionCount = 2; //궤적이 사용될 지점을 2개로
        m_BulletLineRenderer.enabled = false; //라인랜더러 끄는 함수

        UpdateUI(); //UI갱신
    }

    public void Fire() //발사 시도
    {
        // 총이 준비된 상태며 현재시간 >= 마지막 격발시점+연사간격
        if(m_CurrentState == State.Ready && Time.time >= m_LastFireTime + m_Delay)
        {
            m_LastFireTime = Time.time;   // 마지막 격발시점 갱신

            Shot();
            UpdateUI();
        }
    }

    private void Shot() // 발사 처리
    {
        RaycastHit hit; //충돌 정보 컨테이너

        // 총을 쏴서 총알이 맞은곳 : 총구 위치 + 총구위치의 앞쪽 방향 *사정거리
        Vector3 hitPosition = m_FireTransform.position + m_FireTransform.forward * m_FireDistance;

        //레이캐스트 (시작지점, 방향, 충돌 정보 컨테이너, 사정거리)
        if(Physics.Raycast(m_FireTransform.position, m_FireTransform.forward, out hit, m_FireDistance))
        {
            IDamageable target = hit.collider.GetComponent<IDamageable>();
            
            if(target != null)
            {
                target.OnDamage(m_Damage);
            }
            // 충돌 위치 저장
            hitPosition = hit.point;
        }

        //발사 이펙트
        StartCoroutine(ShotEffect(hitPosition));
        //탄환수 -1
        m_CurrentAmmo--;
        if (m_CurrentAmmo <= 0)
        {
            m_CurrentState = State.Empty;
        }

    }

    // 궤적표시
    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        //총알 궤적을 킴
        m_BulletLineRenderer.enabled = true;
        // 총구 위치
        m_BulletLineRenderer.SetPosition(0, m_FireTransform.position);
        // 피탄 위치
        m_BulletLineRenderer.SetPosition(1, hitPosition);

        yield return new WaitForSeconds(0.07f);

        m_BulletLineRenderer.enabled = false;
    }


    private void UpdateUI()
    {
        if(m_CurrentState == State.Empty)
        {
            m_AmmoText.text = "Empty";
        }
        else if(m_CurrentState == State.Reloading)
        {
            m_AmmoText.text = "RELOADING";
        }
        else
        {
            m_AmmoText.text = m_CurrentAmmo.ToString();
        }
    }
    
    
    //재장전
    public void Reload()
    {
        if(m_CurrentState != State.Reloading)
        {
            StartCoroutine(ReloadRoutin());
        }
    }

    private IEnumerator ReloadRoutin()
    {
        m_CurrentState = State.Reloading;

        UpdateUI();

        yield return new WaitForSeconds(m_Reload);
        m_CurrentAmmo = m_MaxAmmo;
        m_CurrentState = State.Ready;
        UpdateUI() ;
    }
}