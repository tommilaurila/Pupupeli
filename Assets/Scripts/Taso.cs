using SQLite4Unity3d;

public class Taso  {

	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }
	public string Hyvis { get; set; }		// hyviksen kuvatiedoston nimi
	public string Pahis { get; set; }		// pahiksen kuvatiedoston nimi
	public int Tahdet { get; set; }			// saatu tähtilkm pelatusta kentästä
	public int Lukittu { get; set; }		// kenttä lukittu 1=kyllä, 0=ei
	public int Hyvis_lkm { get; set; }		// hyvisten (kerättävien) lukumäärä
	public int Pahis_lkm { get; set; }		// pahisten (varottavien) lukumääärä
	public int Elama_lkm { get; set; }		// bonuselämien lukumäärä
	public int Taso_tyyppi { get; set; }	// tason tyyppi, oikean managerin valintaa varten

	// tasotyypit:
	// 0 = egg-level

	public override string ToString ()
	{
		return string.Format ("[Taso: Id={0}, Hyvis={1}, Pahis={2}, Tahdet={3}, Lukittu={4}, Hyvislkm={5}, Pahislkm={6}, Elamalkm={7}, TasoTyyppi={8}]", Id, Hyvis, Pahis, Tahdet, Lukittu, Hyvis_lkm, Pahis_lkm, Elama_lkm, Taso_tyyppi);
	}
}
