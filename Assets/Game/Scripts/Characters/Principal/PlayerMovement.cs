using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singleton;

public class PlayerMovement : Singleton<PlayerMovement>
{
    [SerializeField] private Vector2 posInicial;
    [SerializeField] private float _moveSpeed = 5f;
    private Animator anim;

    private Camera MainCamera => Helpers.cam; 
    private Vector2 screenBounds;

    public FixedJoystick _joystick;
    private CanvasGroup cg_FixedJoyStick;
    [SerializeField] private Rigidbody2D _rigidbody;
    float _screenWidth, _screenHeight;
    private bool podeMover = false;

    private LevelController _levelController => LevelController.I;
    private BackgroundController _backgroundController => BackgroundController.I;
    private new void Awake()
    {
        transform.position = posInicial;
        anim = GetComponent<Animator>();
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        _screenWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; 
        _screenHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
        cg_FixedJoyStick = _joystick.GetComponent<CanvasGroup>();
    }

    private void OnValidate()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + _screenWidth, screenBounds.x - _screenWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + _screenHeight, screenBounds.y - _screenHeight);
        transform.position = viewPos;
    }

    private void Move()
    {
        if (podeMover)
        {
            _rigidbody.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, _joystick.Vertical * _moveSpeed, 0);
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
        }

    }

    public void AnimateBool(string nomeBool, bool state)
    {
        anim.SetBool(nomeBool, state);
    }

    public void AnimateTrigger(string nomeTrigger)
    {
        anim.SetTrigger(nomeTrigger);
    }

    public void SetAnimatorUnscaled(bool estado) 
    {
        anim.updateMode = (estado ? AnimatorUpdateMode.UnscaledTime : AnimatorUpdateMode.Normal);
    }
    public void PermitirMovimento(bool estado)
    {
        podeMover = estado;
        cg_FixedJoyStick.DOFade((estado ? 1 : 0), 0.4f);
    }

    public IEnumerator MoverParaX()
    {
        AnimateBool("Mover", true);
        yield return new WaitForSeconds(0.8f);
        transform.DOMoveX(-7.2f, 4f).SetEase(Ease.InSine);
        while (transform.position.x != -7.2f)
        {
            yield return null;
        }
        PermitirMovimento(true);
    }

    public IEnumerator MoverParaMeio()
    {
        PermitirMovimento(false);
        AnimateBool("Mover", true);
        transform.DOMove(new Vector2(-7.2f, 0), 3f).SetEase(Ease.InSine);
        while (transform.position.x != -7.2f)
        {
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        _backgroundController.MudarEstadoParallax(false);
    }


}
