using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SphereCollider))]
[RequireComponent (typeof(Rigidbody))]
public class NewBehaviourScript : MonoBehaviour {
	public int bound=5;
	public float speed=6;
	public Vector3 jumpVelocity;

	private Transform _transform;
	private Rigidbody _rigidbody;
	private SphereCollider _sphereCollider;
	private ArrayList sprouts;
	private int currentPatch=-1; //-1 for none. keeps track of which dirt patch you're on
	private int[] growLevel; //this is rly messy lol
	private int[] maxGrowLevel;

	void Awake () {
		_transform = GetComponent<Transform> ();
		_rigidbody = GetComponent<Rigidbody> ();
		_sphereCollider = GetComponent<SphereCollider> ();
		sprouts = new ArrayList ();
		sprouts.Add(GameObject.Find("sprout1"));
		sprouts.Add(GameObject.Find("sprout2"));
		sprouts.Add(GameObject.Find("sprout3"));
		
		growLevel = new int[sprouts.Count];
		maxGrowLevel = new int[sprouts.Count];
		for (int i=0; i<growLevel.Length; i++) {
			maxGrowLevel[i]=i+1; //for a rly...straightforward sequence
			growLevel[i]=0;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.RightArrow)){
			transform.Translate (speed * Time.deltaTime,0,0);
		}else if (Input.GetKey(KeyCode.LeftArrow)){
			transform.Translate (-speed * Time.deltaTime,0,0);
		}else if (Input.GetKey(KeyCode.UpArrow)){
			transform.Translate (0,0,speed * Time.deltaTime);
		}else if (Input.GetKey(KeyCode.DownArrow)){
			transform.Translate (0,0,-speed * Time.deltaTime);
		}else if (_rigidbody.velocity.y==0 && Input.GetButtonDown ("Jump")){
			//only jump if you aren't already jumping
			//so if there's no current y velocity
			//this is rly hacky and doesn't really work but WHATEV!!!
			rigidbody.AddForce (jumpVelocity,ForceMode.VelocityChange);
		}else if (Input.GetButtonDown ("Fire1")){
			//if you're on the right patch and sprout hasn't finished growing,
			//scale the sprout up
			if (currentPatch!=-1 && growLevel[currentPatch]<maxGrowLevel[currentPatch]){
				GameObject currentSprout = (GameObject)sprouts[currentPatch];
				currentSprout.transform.localScale += new Vector3(0.5f,2f,0.5f);
				growLevel[currentPatch]++;
			}
		}

	}
	void OnTriggerEnter(Collider obj){
		if (obj.name == "dirtPatch1") {
			currentPatch = 0;
		} else if (obj.name == "dirtPatch2") {
			currentPatch = 1;
		}
	}
	void OnTriggerExit(Collider obj){
		currentPatch = -1;
	}

}
