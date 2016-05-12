using SQLite4Unity3d;

public class Putoava  {

	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }
	public int Tyyppi { get; set; }
	public string Nimi { get; set; }
	public int Vaikutus { get; set; }
	public string Kuva { get; set; }
	public string Prefab { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Putoava: Id={0}, Tyyppi={1}, Nimi={2}, Vaikutus={3}, Kuva={4}, Prefab={5}]", Id, Tyyppi, Nimi, Vaikutus, Kuva, Prefab);
	}
}
