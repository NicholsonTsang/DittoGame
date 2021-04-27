using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapball : Photon.MonoBehaviour
{

    public float MoveSpeed;

    public float DestroyTime;

    private void Awake()
    {
        StartCoroutine("DestroyByTime");
    }

    IEnumerator DestroyByTime()
    {
        yield return new WaitForSeconds(DestroyTime);
        this.GetComponent<PhotonView>().RPC("DestroyObject", PhotonTargets.AllBuffered);
    }


    [PunRPC]
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    public void Update()
    {
        transform.Translate(Vector2.right * MoveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!photonView.isMine)
            return;

        PhotonView target = collision.gameObject.GetComponent<PhotonView>();

        if (target != null && (!target.isMine || target.isSceneView))
        {
            this.GetComponent<PhotonView>().RPC("DestroyObject", PhotonTargets.AllBuffered);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
