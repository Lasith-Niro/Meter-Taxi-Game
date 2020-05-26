using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace GRIDCITY
{
	public class HostileCityManager : MonoBehaviour
	{

		#region Fields
		private static HostileCityManager _instance;
		public Mesh[] meshArray;
		public GameObject buildingPrefab;
		public BuildingProfile[] profileArray;
		public BuildingProfile wallProfile;
		public GameObject[] userinterfeses;

		[Header("Custom Settings")]
		public GameObject GrassPrefab;
		public GameObject RoadPrefab;

		static int x, y;

		[Header("Countdown")]
		public bool countdownDone = false;


		[Header("Vehicle")]
		public GameObject vehicle;

		private bool[,,] cityArray = new bool [20,20,20];   //increased array size to allow for larger city volume

		public static HostileCityManager Instance
		{
			get
			{
				return _instance;
			}
		}
		#endregion

		#region Properties	
		#endregion

		#region Methods
		#region Unity Methods

		// Use this for internal initialization
		void Awake () 
		{
			
			vehicle.SetActive(false);
			if (_instance == null)
			{
				_instance = this;
			}

			else
			{
				Destroy(gameObject);
				Debug.LogError("Multiple HostileCityManager instances in Scene. Destroying clone!");
			};
		}
		
		// Use this for external initialization
		void Start ()
		{
			// DEBUG
			
			int lvl = GameObject.Find("GameData").GetComponent<GameData>().GetMap();
			LevelDecrypter(lvl, out x, out y);

			FindObjectOfType<AudioManager>().Play("CarStart");

			// MAKE PARK IN RANDOM PLACE:
			int randomPlace = Random.Range(-5, 0);
			for (int i = randomPlace; i < 0; i += 1)
			{
				for (int j = randomPlace; j < 0; j += 1)
				{
					Instantiate(GrassPrefab, new Vector3(i, -0.09f, j), Quaternion.identity);
					SetSlot(i + 7 , 0, j +7, true);
				}
			}
			
			//CITY BUILDINGS:
			for (int i = x; i < y; i += 2)
			{ 
				for (int j = x; j < y; j += 2)
				{
					int random = Random.Range(0, profileArray.Length);
					Instantiate(buildingPrefab, new Vector3(i, 0.5f, j), Quaternion.identity).GetComponent<HostileTowerBlock>().SetProfile(profileArray[random]);
					SetSlot(i, 0, j, true);
				}
			}

			// ROAD :
			for (int i = x; i < y; i += 1)
			{
				for (int j = x; j < y; j += 1)
				{
					if (CheckSlot(i, 0, j) != true)
					{
						Debug.Log("place here");
						Instantiate(RoadPrefab, new Vector3(i, -0.09f, j), Quaternion.identity);
					}
					/*
					if (!CheckSlot(x + 2, 0, y + 2))
					{
					}
					*/
				}
			}
		}

		private void Update()
		{
			if (countdownDone == true)
			{
				vehicle.SetActive( true);
			}
		}
		#endregion

		public bool CheckSlot(int x, int y, int z)
		{
			if (x < 0 || x > 14 || y < 0 || y > 14 || z < 0 || z > 14) return true;
			else
			{
				return cityArray[x, y, z];
			}
		}

		public void SetSlot(int x, int y, int z, bool occupied)
		{
			if (!(x < 0 || x > 14 || y < 0 || y > 14 || z < 0 || z > 14))
			{
				cityArray[x, y, z] = occupied;
			}

		}

		private void LevelDecrypter(int map, out int x, out int y)
		{
			switch (map)
			{
				case 1:
					x = -5;
					y = 5;
					break;
				case 2:
					x = -6;
					y = 6;
					break;
				case 3:
					x = -7;
					y = 7;
					break;
				default:
					x = -5;
					y = 5;
					break;
			}
		}

		#endregion

	}
}
