using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Character : MonoBehaviour {
	
	private List<Person> people = new List<Person>();
	private string[] file;



	void Start ()
	{
		file = System.IO.File.ReadAllLines ("D:/Git-proj/-Name/-Name-objective/A&M_Game/Assets/Text/Skills.txt");
		GenerateNewCharacter ();

	}
	public void GenerateNewCharacter()
	{
		List<SkillStructure> skills = new List<SkillStructure> ();

		for (int i = 0; i < 7; i++) {
			skills.Add (new SkillStructure (file [i]));
		}
		people.Add(new Person(skills));

	}





}
