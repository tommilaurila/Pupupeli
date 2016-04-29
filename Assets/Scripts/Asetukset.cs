using SQLite4Unity3d;

public class Asetukset  {

	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }
	public int Tahtisaldo { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Asetukset: Id={0}, Tähtisaldo={1}", Id, Tahtisaldo);
	}
}
