using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
// tietokanta
using Mono.Data.Sqlite; 
using System.Data; 
using System;


public class levelSelect : MonoBehaviour {

	public Text starsText;

	// Use this for initialization
	void Start () {
		readDB ();
		readStars ();
	}


	void readStars() {
		// luetaan jo kerätyt tähdet muistista
//		if (PlayerPrefs.HasKey ("collected_stars")) {
//			int collected_stars = PlayerPrefs.GetInt ("collected_stars");
//
//			starsText.text = collected_stars.ToString ();
//		} else
//			starsText.text = "0";
	}


	public void StartLevel(int level) {
		SceneManager.LoadScene (level);
	}


	void readDB() {
		string conn = "URI=file:" + Application.dataPath + "/Pupukanta.s3db"; //Path to database.
		Debug.Log("polku " + conn);

		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		if(dbconn == null) Debug.Log("yhteyttä ei ole");
		IDbCommand dbcmd = dbconn.CreateCommand();
		if (dbcmd == null)
			Debug.Log ("komentoa ei ole");
		string sqlQuery = "SELECT hyvis FROM Tasot";
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read())
		{
			int value = reader.GetInt32(0);

			Debug.Log( "value= "+value);
		}
		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;
	}

}
