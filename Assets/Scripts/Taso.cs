using SQLite4Unity3d;

public class Taso  {

	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }
	public string Hyvis { get; set; }
	public string Pahis { get; set; }
	public int Tahdet { get; set; }
	public int Lukittu { get; set; }
	public int Hyvis_lkm { get; set; }
	public int Pahis_lkm { get; set; }
	public int Elama_lkm { get; set; }

	public override string ToString ()
	{
		return string.Format ("[Taso: Id={0}, Hyvis={1}, Pahis={2}, Tahdet={3}, Lukittu={4}, Hyvislkm={5}, Pahislkm={6}, Elamalkm={7}]", Id, Hyvis, Pahis, Tahdet, Lukittu, Hyvis_lkm, Pahis_lkm, Elama_lkm);
	}
}
