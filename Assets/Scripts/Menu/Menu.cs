using UnityEngine;

public class Menu : MonoBehaviour
{
    private Animator _animator;
    private CanvasGroup _canvasgroup;

	

    public bool IsOpen
    {
        get { return _animator.GetBool("IsOpen"); }
        set { _animator.SetBool("IsOpen", value); }
    }

    public void Awake()
    {
		

	    _animator = GetComponent<Animator>();
        _canvasgroup = GetComponent<CanvasGroup>();

        var rect = GetComponent <RectTransform>();
        rect.offsetMax = rect.offsetMin = new Vector2(0, 0);
    }

    public void Update()
    {
	   

	    if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
	    {
//			AudioManager.PlaySound("FX/Interface/Bloop"); // probably not the place to play a sound effect!
		    _canvasgroup.blocksRaycasts = _canvasgroup.interactable = false;
	    }
	    else
		    _canvasgroup.blocksRaycasts = _canvasgroup.interactable = true;
    }
}