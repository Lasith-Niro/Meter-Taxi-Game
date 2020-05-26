using System.Collections;
using System.Collections.Generic;
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


		static int x, y;

		[Header("Countdown")]
		public bool countdownDone = false;


		[Header("Vehicle")]
		public GameObject vehicle;

		private bool[,,] cityArray = new bool [50,50,50];   //increased array size to allow for larger city volume

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
			int lvl = GameObject.Find("GameData").GetComponent<GameData>().GetMap();
			LevelDecrypter(lvl, out x, out y);
			FindObjectOfType<AudioManager>().Play("CarStart");
			/*
			userinterfeses = GameObject.FindGameObjectsWithTag("interfaces");
			foreach (GameObject userinterface in userinterfeses)
			{
				Debug.Log(userinterface.name);
				userinterface.SetActive(true);
			}
			*/

			//UPDATING PLANNING ARRAY TO ACCOUNT FOR MANUALLY PLACED|CITY GATE
			for (int ix = -1; ix < 2; ix++)
				{
					int iz = -7;
					for (int iy = 0; iy < 3; iy++)
					{
						SetSlot(ix + 7, iy, iz + 7, true);
					}
				}


			/*
			//BUILD CITY WALLS

			for (int i = -7; i < 8; i += 14)
			{
				for (int j = -7; j < 8; j += 1)
				{
					Instantiate(buildingPrefab, new Vector3(i, 0.05f, j), Quaternion.identity).GetComponent<HostileTowerBlock>().SetProfile(wallProfile);
				}
				for (int j = -6; j < 7; j += 1)
				{
					Instantiate(buildingPrefab, new Vector3(j, 0.05f, i), Quaternion.identity).GetComponent<HostileTowerBlock>().SetProfile(wallProfile);
				}
			}
			*/
			
				//CITY BUILDINGS:
				for (int i = x; i < y; i += 2)
				{
					for (int j = x; j < y; j += 2)
					{
						int random = Random.Range(0, profileArray.Length);
						Instantiate(buildingPrefab, new Vector3(i, 0.05f, j), Quaternion.identity).GetComponent<HostileTowerBlock>().SetProfile(profileArray[random]);
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
