using SQLite4Unity3d;
using UnityEngine;
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


	public Taso GetTaso(int nro) {
		return _connection.Table<Taso> ().Where (x => x.Id == nro).First();
	}

	//TODO: tähtiä lisäytyy aivan liikaa!!
	public int addTotalStars(int stars) {
		Asetukset oldSettings = _connection.Table<Asetukset> ().First();

		oldSettings.Tahtisaldo += stars;
		// palauttaa muutettujen rivien lukumäärän
		return _connection.Update(oldSettings);
	}


	public int getTotalStars() {
		Asetukset a = _connection.Table<Asetukset> ().Where (x => x.Id == 1).First();
		Debug.Log ("Tähtisaldo on " + a.ToString());
		return a.Tahtisaldo;		
	}


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
}
