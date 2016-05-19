using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DataService  {

	private SQLiteConnection _connection;

	public DataService(string DatabaseName){

#if UNITY_EDITOR
            var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID 
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
            _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Final PATH: " + dbPath);     

	}

/*	
	public void LuoKanta() {
	_connection.DropTable<Putoava> ();
	_connection.CreateTable<Putoava> ();

	_connection.InsertAll (new[]{
		new Putoava{
			Id = 1,
			Tyyppi = 1,
			Nimi = "Muna",
			Vaikutus = 1,
			Kuva = "kuva"
		},
		new Putoava{
			Id = 2,
			Tyyppi = 1,
			Nimi = "Hiutale",
			Vaikutus = 2,
			Kuva = "kuva4"
		},
		new Putoava{
			Id = 3,
			Tyyppi = -1,
			Nimi = "Kapy",
			Vaikutus = -1,
			Kuva = "kuva2"
		},
		new Putoava{
			Id = 4,
			Tyyppi = 0,
			Nimi = "Sydan",
			Vaikutus = 0,
			Kuva = "kuva3"
		}
	});

	_connection.DropTable<Taso> ();
	_connection.CreateTable<Taso> ();

	_connection.InsertAll (new[]{
		new Taso{
			Id = 1,
			Hyvis = 1,
			Pahis = 2,
			Tahdet = 0,
			Lukittu = 1
		},
		new Taso{
			Id = 2,
			Hyvis = 2,
			Pahis = 2,
			Tahdet = 0,
			Lukittu = 1
		}
	});
	}

*/
	public Taso GetTaso(int nro) {
		return _connection.Table<Taso> ().Where (x => x.Id == nro).First();
	}


	public IEnumerable<Taso> GetTasot() {
		return _connection.Table<Taso> ();
	}


	public void addTotalStars(int stars) {
	Asetukset oldSettings = _connection.Table<Asetukset> ().First();

		oldSettings.Tahtisaldo += stars;
		// palauttaa muutettujen rivien lukumäärän
		_connection.Update(oldSettings);
	}


	public void updateLevelHighScore(int level, int stars) {
		//lisätään kerättyjen tähtien kokonaismäärään
		addTotalStars (stars);

		// päivitetään kenttäkohtainen highscore, jos on saatu enemmän tähtiä kuin ennen
		Taso levelToOpen = _connection.Table<Taso> ().Where (x => x.Id == level).First();
		// jos vanha high score on pienempi, päivitetään uusi
		if (levelToOpen.Tahdet < stars) {
			levelToOpen.Tahdet = stars;
			_connection.Update (levelToOpen);
		}
	}


	public int getTotalStars() {
		Asetukset a = _connection.Table<Asetukset> ().Where (x => x.Id == 1).First();
		return a.Tahtisaldo;		
	}


	public void unlockLevel(int level) {
	try {
		Taso levelToOpen = _connection.Table<Taso> ().Where (x => x.Id == level).First();
		
		levelToOpen.Lukittu = 0;
		_connection.Update (levelToOpen);
	}
	catch (InvalidOperationException ioe) {
			Debug.Log ("An InvalidOperationException happened when trying to unlock next level");
		}
	}

/*
	public IEnumerable<Person> GetPersons(){
		return _connection.Table<Person>();
	}

	public IEnumerable<Person> GetPersonsNamedRoberto(){
		return _connection.Table<Person>().Where(x => x.Name == "Roberto");
	}

	public Person GetJohnny(){
		return _connection.Table<Person>().Where(x => x.Name == "Johnny").FirstOrDefault();
	}

	public Person CreatePerson(){
		var p = new Person{
				Name = "Johnny",
				Surname = "Mnemonic",
				Age = 21
		};
		_connection.Insert (p);
		return p;
	}
*/
}
