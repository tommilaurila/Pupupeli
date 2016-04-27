using SQLite4Unity3d;

public class Taso  {

	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }
	public int Hyvis { get; set; }
	public int Pahis { get; set; }
	public int Tahdet { get; set; }
	public int Lukittu { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Taso: Id={0}, Hyvis={1}, Pahis={2}, Tahdet={3}, Lukittu={4}]", Id, Hyvis, Pahis, Tahdet, Lukittu);
	}
}
